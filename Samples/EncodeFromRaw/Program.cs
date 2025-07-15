using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

using libFLAC;

class Program
{
	static void Main(string[] args)
	{
		if (args.Length == 0)
		{
			Console.WriteLine("usage: EncodeFromRaw <filename.flac> [<sample rate> [<channels> [<bits per sample>]]]");
			return;
		}

		string rawFileName = args[0];
		int sampleRate = (args.Length >= 2) ? int.Parse(args[1]) : 48000;
		int channels = (args.Length >= 3) ? int.Parse(args[2]) : 2;
		int bitsPerSample = (args.Length >= 4) ? int.Parse(args[3]) : 16;

		string flacFileName = Path.ChangeExtension(rawFileName, "flac");

		try
		{
			using (var inputStream = File.OpenRead(rawFileName))
			using (var outputStream = File.OpenWrite(flacFileName))
			{
				EncodeFLAC(inputStream, sampleRate, channels, bitsPerSample, Path.GetFileNameWithoutExtension(rawFileName), outputStream);
			}
		}
		catch (Exception e)
		{
			Console.WriteLine("ERROR: {0}", e);
		}
	}

	const int SampleBufferLength = 65536;

	static void EncodeFLAC(Stream inputStream, int sampleRate, int numChannels, int bitsPerSample, string title, Stream outputStream)
	{
		int bytesPerSample = bitsPerSample / 8;

		int sampleCount = (int)inputStream.Length / bytesPerSample;

		/* metadata structures & the amount */
		IntPtr[] metadataPtrs = new IntPtr[3];

		int numMetadata = 0;

		IntPtr encoder;

		encoder = NativeMethods.FLAC__stream_encoder_new();

		if (encoder == IntPtr.Zero)
			throw new Exception("failed to instantiate encoder");

		if (!NativeMethods.FLAC__stream_encoder_set_channels(encoder, numChannels))
			throw new Exception("failed to set number of channels");

		if (!NativeMethods.FLAC__stream_encoder_set_bits_per_sample(encoder, 16))
			throw new Exception("failed to set bits per sample");

		if (sampleRate > FLACConstants.MaxSampleRate)
			sampleRate = FLACConstants.MaxSampleRate;

		// FLAC only supports 10 Hz granularity for frequencies above 65535 Hz if the streamable subset is chosen, and only a maximum frequency of 655350 Hz.
		if (!NativeMethods.FLAC__format_sample_rate_is_subset(sampleRate))
			NativeMethods.FLAC__stream_encoder_set_streamable_subset(encoder, false);

		if (!NativeMethods.FLAC__stream_encoder_set_sample_rate(encoder, sampleRate))
			throw new Exception("failed to set sample rate");

		if (!NativeMethods.FLAC__stream_encoder_set_compression_level(encoder, 5))
			throw new Exception("failed to set compression level");

		if (!NativeMethods.FLAC__stream_encoder_set_total_samples_estimate(encoder, sampleCount))
			throw new Exception("failed to set toal samples estimate");

		if (!NativeMethods.FLAC__stream_encoder_set_verify(encoder, false))
			throw new Exception("failed to set verify flag");

		/* okay, now we have to hijack the writedata, so we can set metadata */

		/* this shouldn't be needed for export */
		metadataPtrs[numMetadata] = NativeMethods.FLAC__metadata_object_new(MetadataType.VORBISCOMMENT);
		if (metadataPtrs[numMetadata] != IntPtr.Zero)
		{
			if (AppendVorbisComment(metadataPtrs[numMetadata], "SAMPLERATE=" + sampleRate)
				&& AppendVorbisComment(metadataPtrs[numMetadata], "TITLE=" + title))
				numMetadata++;

			bool AppendVorbisComment(IntPtr metadataPtr, string comment)
			{
				byte[] bytes = Encoding.UTF8.GetBytes(comment);

				if (bytes.Length > 0)
				{
					IntPtr bytesMemory = IntPtr.Zero;
					try
					{
						bytesMemory = Marshal.AllocHGlobal(bytes.Length);

						Marshal.Copy(bytes, 0, bytesMemory, bytes.Length);

						VorbisCommentEntry e;

						e.Length = bytes.Length;
						e.Entry = bytesMemory;

						NativeMethods.FLAC__metadata_object_vorbiscomment_append_comment(metadataPtr, e, true);

						return true;
					}
					finally
					{
						if (bytesMemory != IntPtr.Zero)
							Marshal.FreeHGlobal(bytesMemory);
					}
				}

				return false;
			}
		}

		if (!NativeMethods.FLAC__stream_encoder_set_metadata(encoder, metadataPtrs, numMetadata))
			throw new Exception("failed to set metadata");

		var status = NativeMethods.FLAC__stream_encoder_init_stream(
			encoder,
			OnWrite,
			OnSeek,
			OnTell,
			null, /* metadata callback */
			IntPtr.Zero);

		if (status != StreamEncoderInitStatus.OK)
		{
			string cause = status.ToString();

			if (status == StreamEncoderInitStatus.EncoderError)
				cause = NativeMethods.FLAC__stream_encoder_get_state(encoder).ToString();

			throw new Exception("failed to initialize FLAC encoder: " + cause);
		}

		/* buffer this */
		long offset;
		long totalBytes = sampleCount * bytesPerSample;
		byte[] sourceSamples = new byte[SampleBufferLength];
		int[] convertedSamples = new int[SampleBufferLength / bytesPerSample];
		for (offset = 0; offset < totalBytes; offset += SampleBufferLength)
		{
			int needed = (int)Math.Min(totalBytes - offset, SampleBufferLength);

			if (sourceSamples.Length > needed)
				sourceSamples = new byte[needed];

			inputStream.ReadExactly(sourceSamples);

			switch (bitsPerSample)
			{
				case 8:
					// Assume unsigned signed bytes.
					for (int i = 0, l = sourceSamples.Length; i < l; i++)
						convertedSamples[i] = unchecked((sbyte)(sourceSamples[i] ^ 128));
					break;
				case 16:
					for (int i = 0, l = sourceSamples.Length / 2; i < l; i++)
						convertedSamples[i] = BitConverter.ToInt16(sourceSamples, i * 2);
					break;
				case 32:
					for (int i = 0, l = sourceSamples.Length / 4; i < l; i++)
						convertedSamples[i] = BitConverter.ToInt32(sourceSamples, i * 4);
					break;
			}

			if (!NativeMethods.FLAC__stream_encoder_process_interleaved(encoder, convertedSamples, convertedSamples.Length / numChannels))
						throw new Exception("process sample chunk failed: " + NativeMethods.FLAC__stream_encoder_get_state(encoder));
		}

		if (!NativeMethods.FLAC__stream_encoder_finish(encoder))
			throw new Exception("failed to finish encoding");

		NativeMethods.FLAC__stream_encoder_delete(encoder);

		/* cleanup the metadata crap */
		for (int i = 0; i < numMetadata; i++)
			NativeMethods.FLAC__metadata_object_delete(metadataPtrs[i]);

		StreamEncoderWriteStatus OnWrite(IntPtr encoder, IntPtr bufferPtr, UIntPtr bytes, uint samples, uint currentFrame, IntPtr clientData)
		{
			try
			{
				unsafe
				{
					outputStream.Write(new Span<byte>((void*)bufferPtr, (int)bytes));
					return StreamEncoderWriteStatus.OK;
				}
			}
			catch
			{
				return StreamEncoderWriteStatus.FatalError;
			}
		}

		StreamEncoderSeekStatus OnSeek(IntPtr encoder, long absoluteByteOffset, IntPtr clientData)
		{
			if (!outputStream.CanSeek)
				return StreamEncoderSeekStatus.Unsupported;

			try
			{
				outputStream.Position = absoluteByteOffset;
				return StreamEncoderSeekStatus.OK;
			}
			catch
			{
				return StreamEncoderSeekStatus.Error;
			}
		}

		StreamEncoderTellStatus OnTell(IntPtr encoder, out long absoluteByteOffset, IntPtr clientData)
		{
			try
			{
				absoluteByteOffset = outputStream.Position;
				return StreamEncoderTellStatus.OK;
			}
			catch
			{
				absoluteByteOffset = -1;
				return StreamEncoderTellStatus.Error;
			}
		}
	}
}
