using System.Runtime.InteropServices;
using System.Text;

using libFLAC;

class Program
{
	static void Main(string[] args)
	{
		if (args.Length == 0)
		{
			Console.WriteLine("usage: DecodeToRaw <filename.flac>");
			Console.WriteLine("  outputs filename.raw");
			return;
		}

		string flacFileName = args[0];
		string rawFileName = Path.ChangeExtension(flacFileName, "raw");

		try
		{
			using (var inputStream = File.OpenRead(flacFileName))
			using (var outputStream = File.OpenWrite(rawFileName))
			{
				DecodeFLAC(inputStream, outputStream);
			}
		}
		catch (Exception e)
		{
			Console.WriteLine("ERROR: {0}", e);
		}
	}

	static void DecodeFLAC(Stream inputStream, Stream outputStream)
	{
		bool isEOF = inputStream.CanSeek && (inputStream.Position >= inputStream.Length);

		byte[] buffer = new byte[4];

		inputStream.ReadExactly(buffer);

		if (Encoding.ASCII.GetString(buffer) != "fLaC")
			throw new Exception("Magic does not match");

		var decoder = NativeMethods.FLAC__stream_decoder_new();

		if (decoder == IntPtr.Zero)
			throw new Exception("Failed to obtain decoder");

		NativeMethods.FLAC__stream_decoder_set_metadata_respond_all(decoder);

		byte[] readBuffer = new byte[8192];

		StreamDecoderInitStatus xx =
			NativeMethods.FLAC__stream_decoder_init_stream(
				decoder,
				OnRead, OnSeek,
				OnTell, OnLength,
				OnEOF, OnWrite,
				OnMeta, OnError,
				IntPtr.Zero
			);

		if (xx != StreamDecoderInitStatus.OK)
			throw new Exception("init failed");

		NativeMethods.FLAC__stream_decoder_set_metadata_respond_all(decoder);

		// Format fields are filled only once actual decoding begins. So, we read to the end of the
		// metadata, and then we process one single block more so we hit some audio data.
		if (!NativeMethods.FLAC__stream_decoder_process_until_end_of_metadata(decoder)
		 || !NativeMethods.FLAC__stream_decoder_process_single(decoder))
			throw new Exception("Failed to read FLAC file: " + NativeMethods.FLAC__stream_decoder_get_state(decoder));

		Console.WriteLine("Reading FLAC data:");
		Console.WriteLine("  Sample rate:     {0}", NativeMethods.FLAC__stream_decoder_get_sample_rate(decoder));
		Console.WriteLine("  Channels:        {0}", NativeMethods.FLAC__stream_decoder_get_channels(decoder));
		Console.WriteLine("  Bits per sample: {0}", NativeMethods.FLAC__stream_decoder_get_bits_per_sample(decoder));

		bool result = NativeMethods.FLAC__stream_decoder_process_until_end_of_stream(decoder);

		if (!result)
			throw new Exception("Failed to read FLAC file: " + NativeMethods.FLAC__stream_decoder_get_state(decoder));

		NativeMethods.FLAC__stream_decoder_finish(decoder);
		NativeMethods.FLAC__stream_decoder_delete(decoder);

		StreamDecoderReadStatus OnRead(IntPtr decoder, IntPtr bufferPtr, ref UIntPtr bytes, IntPtr clientData)
		{
			if (bytes <= 0)
				return StreamDecoderReadStatus.Abort;

			try
			{
				if (readBuffer.Length < (int)bytes)
					readBuffer = new byte[bytes * 2];

				int numRead = inputStream.Read(readBuffer, 0, (int)bytes);

				Marshal.Copy(readBuffer, 0, bufferPtr, numRead);

				bytes = (UIntPtr)numRead;

				if (numRead == 0)
				{
					isEOF = true;
					return StreamDecoderReadStatus.EndOfStream;
				}
				else
					return StreamDecoderReadStatus.Continue;
			}
			catch
			{
				return StreamDecoderReadStatus.Abort;
			}
		}

		StreamDecoderSeekStatus OnSeek(IntPtr decoder, long absoluteByteOffset, IntPtr clientData)
		{
			if (!inputStream.CanSeek)
				return StreamDecoderSeekStatus.Unsupported;

			try
			{
				inputStream.Position = absoluteByteOffset;
				return StreamDecoderSeekStatus.OK;
			}
			catch
			{
				return StreamDecoderSeekStatus.Error;
			}
		}

		StreamDecoderTellStatus OnTell(IntPtr decoder, out long absoluteByteOffset, IntPtr clientData)
		{
			try
			{
				absoluteByteOffset = inputStream.Position;
				return StreamDecoderTellStatus.OK;
			}
			catch
			{
				absoluteByteOffset = -1;
				return StreamDecoderTellStatus.Error;
			}
		}

		StreamDecoderLengthStatus OnLength(IntPtr decoder, out long streamLength, IntPtr clientData)
		{
			try
			{
				streamLength = inputStream.Length;
				return StreamDecoderLengthStatus.OK;
			}
			catch
			{
				streamLength = -1;
				return StreamDecoderLengthStatus.Error;
			}
		}

		bool OnEOF(IntPtr decoder, IntPtr clientData)
		{
			return isEOF;
		}

		StreamDecoderWriteStatus OnWrite(IntPtr decoder, Frame frame, [MarshalAs(UnmanagedType.LPArray, SizeConst = FLACConstants.MaxChannels)] IntPtr[] buffer, IntPtr clientData)
		{
			byte[] sampleBuffer = new byte[2];

			for (int i = 0; i < frame.Header.BlockSize; i++)
				for (int c = 0; c < frame.Header.Channels; c++)
				{
					int sample = Marshal.ReadInt32(buffer[c], i * 4);

					BitConverter.TryWriteBytes(sampleBuffer, unchecked((short)sample));

					outputStream.Write(sampleBuffer);
				}

			return StreamDecoderWriteStatus.Continue;
		}

		void OnMeta(IntPtr decoder, /* const FLAC__StreamMetadata * */ IntPtr metadata, IntPtr clientData)
		{
			// TODO
			throw new NotImplementedException();
		}

		void OnError(IntPtr decoder, StreamDecoderErrorStatus status, IntPtr clientData)
		{
			Console.WriteLine("FLAC ERROR: {0}", status);
		}
	}

}
