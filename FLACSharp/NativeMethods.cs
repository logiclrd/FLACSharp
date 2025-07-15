using System;
using System.Runtime.InteropServices;
using System.Text;

namespace libFLAC;

public static class NativeMethods
{
	[DllImport("FLAC")]
	public static extern bool FLAC__format_sample_rate_is_valid(int sampleRate);
	[DllImport("FLAC")]
	public static extern bool FLAC__format_blocksize_is_subset(int blockSize, int sampleRate);
	[DllImport("FLAC")]
	public static extern bool FLAC__format_sample_rate_is_subset(int sampleRate);
	[DllImport("FLAC")]
	public static extern bool FLAC__format_vorbiscomment_entry_name_is_legal(string name);
	[DllImport("FLAC")]
	public static extern bool FLAC__format_vorbiscomment_entry_value_is_legal(byte[] value, int length);
	[DllImport("FLAC")]
	public static extern bool FLAC__format_vorbiscomment_entry_is_legal(byte[] entry, int length);
	[DllImport("FLAC")]
	public static extern bool FLAC__format_seektable_is_legal(ref StreamMetadata_SeekTable seekTable);
	[DllImport("FLAC")]
	public static extern int FLAC__format_seektable_sort(ref StreamMetadata_SeekTable seekTable);
	[DllImport("FLAC")]
	public static extern bool FLAC__format_cuesheet_is_legal(ref StreamMetadata_CueSheet cueSheet, bool checkCDDASubset, out string? violation);
	[DllImport("FLAC")]
	public static extern bool FLAC__format_picture_is_legal(ref StreamMetadata_Picture picture, out string? violation);

	[DllImport("FLAC")]
	public static extern IntPtr FLAC__stream_decoder_new();
	[DllImport("FLAC")]
	public static extern void FLAC__stream_decoder_delete(IntPtr decoder);
	[DllImport("FLAC")]
	public static extern bool FLAC__stream_decoder_set_ogg_serial_number(IntPtr decoder, int serialNumber);
	[DllImport("FLAC")]
	public static extern bool FLAC__stream_decoder_set_md5_checking(IntPtr decoder, bool value);
	[DllImport("FLAC")]
	public static extern bool FLAC__stream_decoder_set_metadata_respond(IntPtr decoder, MetadataType type);
	[DllImport("FLAC")]
	public static extern bool FLAC__stream_decoder_set_metadata_respond_application(IntPtr decoder, byte[] id);

	public static bool FLAC__stream_decoder_set_metadata_respond_application(IntPtr decoder, string id)
		=> FLAC__stream_decoder_set_metadata_respond_application(decoder, Encoding.ASCII.GetBytes(id.PadRight(4)));

	[DllImport("FLAC")]
	public static extern int FLAC__stream_decoder_set_metadata_respond_all(IntPtr decoder);
	[DllImport("FLAC")]
	public static extern bool FLAC__stream_decoder_set_metadata_ignore(IntPtr decoder, MetadataType type);
	[DllImport("FLAC")]
	public static extern bool FLAC__stream_decoder_set_metadata_ignore_application(IntPtr decoder, byte[] id);

	public static bool FLAC__stream_decoder_set_metadata_ignore_application(IntPtr decoder, string id)
		=> FLAC__stream_decoder_set_metadata_ignore_application(decoder, Encoding.ASCII.GetBytes(id.PadRight(4)));

	[DllImport("FLAC")]
	public static extern int FLAC__stream_decoder_set_metadata_ignore_all(IntPtr decoder);
	[DllImport("FLAC")]
	public static extern StreamDecoderState FLAC__stream_decoder_get_state(IntPtr decoder);
	[DllImport("FLAC")]
	public static extern string FLAC__stream_decoder_get_resolved_state_string(IntPtr decoder);
	[DllImport("FLAC")]
	public static extern bool FLAC__stream_decoder_get_md5_checking(IntPtr decoder);
	[DllImport("FLAC")]
	public static extern long FLAC__stream_decoder_get_total_samples(IntPtr decoder);
	[DllImport("FLAC")]
	public static extern int FLAC__stream_decoder_get_channels(IntPtr decoder);
	[DllImport("FLAC")]
	public static extern ChannelAssignment FLAC__stream_decoder_get_channel_assignment(IntPtr decoder);
	[DllImport("FLAC")]
	public static extern int FLAC__stream_decoder_get_bits_per_sample(IntPtr decoder);
	[DllImport("FLAC")]
	public static extern int FLAC__stream_decoder_get_sample_rate(IntPtr decoder);
	[DllImport("FLAC")]
	public static extern int FLAC__stream_decoder_get_blocksize(IntPtr decoder);
	[DllImport("FLAC")]
	public static extern bool FLAC__stream_decoder_get_decode_position(IntPtr decoder, out long position);
	[DllImport("FLAC")]
	public static extern IntPtr FLAC__stream_decoder_get_client_data(IntPtr decoder);
	[DllImport("FLAC")]
	public static extern StreamDecoderInitStatus FLAC__stream_decoder_init_stream(IntPtr decoder, StreamDecoderReadCallback read_callback, StreamDecoderSeekCallback seek_callback, StreamDecoderTellCallback tell_callback, StreamDecoderLengthCallback length_callback, StreamDecoderEofCallback eof_callback, StreamDecoderWriteCallback write_callback, StreamDecoderMetadataCallback metadata_callback, StreamDecoderErrorCallback error_callback, IntPtr client_data);
	[DllImport("FLAC")]
	public static extern StreamDecoderInitStatus FLAC__stream_decoder_init_ogg_stream(IntPtr decoder, StreamDecoderReadCallback read_callback, StreamDecoderSeekCallback seek_callback, StreamDecoderTellCallback tell_callback, StreamDecoderLengthCallback length_callback, StreamDecoderEofCallback eof_callback, StreamDecoderWriteCallback write_callback, StreamDecoderMetadataCallback metadata_callback, StreamDecoderErrorCallback error_callback, IntPtr client_data);
	[DllImport("FLAC")]
	public static extern StreamDecoderInitStatus FLAC__stream_decoder_init_FILE(IntPtr decoder, /* FILE * */ IntPtr file, StreamDecoderWriteCallback writeCallback, StreamDecoderMetadataCallback metadataCallback, StreamDecoderErrorCallback errorCallback, IntPtr clientData);
	[DllImport("FLAC")]
	public static extern StreamDecoderInitStatus FLAC__stream_decoder_init_ogg_FILE(IntPtr decoder, /* FILE * */ IntPtr file, StreamDecoderWriteCallback writeCallback, StreamDecoderMetadataCallback metadataCallback, StreamDecoderErrorCallback errorCallback, IntPtr clientData);
	[DllImport("FLAC")]
	public static extern StreamDecoderInitStatus FLAC__stream_decoder_init_file(IntPtr decoder, string filename, StreamDecoderWriteCallback writeCallback, StreamDecoderMetadataCallback metadataCallback, StreamDecoderErrorCallback errorCallback, IntPtr clientData);
	[DllImport("FLAC")]
	public static extern StreamDecoderInitStatus FLAC__stream_decoder_init_ogg_file(IntPtr decoder, string filename, StreamDecoderWriteCallback writeCallback, StreamDecoderMetadataCallback metadataCallback, StreamDecoderErrorCallback errorCallback, IntPtr clientData);
	[DllImport("FLAC")]
	public static extern bool FLAC__stream_decoder_finish(IntPtr decoder);
	[DllImport("FLAC")]
	public static extern bool FLAC__stream_decoder_flush(IntPtr decoder);
	[DllImport("FLAC")]
	public static extern bool FLAC__stream_decoder_reset(IntPtr decoder);
	[DllImport("FLAC")]
	public static extern bool FLAC__stream_decoder_process_single(IntPtr decoder);
	[DllImport("FLAC")]
	public static extern bool FLAC__stream_decoder_process_until_end_of_metadata(IntPtr decoder);
	[DllImport("FLAC")]
	public static extern bool FLAC__stream_decoder_process_until_end_of_stream(IntPtr decoder);
	[DllImport("FLAC")]
	public static extern bool FLAC__stream_decoder_skip_single_frame(IntPtr decoder);
	[DllImport("FLAC")]
	public static extern bool FLAC__stream_decoder_seek_absolute(IntPtr decoder, long sample);

	[DllImport("FLAC")]
	public static extern IntPtr FLAC__stream_encoder_new();
	[DllImport("FLAC")]
	public static extern void FLAC__stream_encoder_delete(IntPtr encoder);
	[DllImport("FLAC")]
	public static extern bool FLAC__stream_encoder_set_ogg_serial_number(IntPtr encoder, int serialNumber);
	[DllImport("FLAC")]
	public static extern bool FLAC__stream_encoder_set_verify(IntPtr encoder, bool value);
	[DllImport("FLAC")]
	public static extern bool FLAC__stream_encoder_set_streamable_subset(IntPtr encoder, bool value);
	[DllImport("FLAC")]
	public static extern bool FLAC__stream_encoder_set_channels(IntPtr encoder, int value);
	[DllImport("FLAC")]
	public static extern bool FLAC__stream_encoder_set_bits_per_sample(IntPtr encoder, int value);
	[DllImport("FLAC")]
	public static extern bool FLAC__stream_encoder_set_sample_rate(IntPtr encoder, int value);
	[DllImport("FLAC")]
	public static extern bool FLAC__stream_encoder_set_compression_level(IntPtr encoder, int value);
	[DllImport("FLAC")]
	public static extern bool FLAC__stream_encoder_set_blocksize(IntPtr encoder, int value);
	[DllImport("FLAC")]
	public static extern bool FLAC__stream_encoder_set_do_mid_side_stereo(IntPtr encoder, bool value);
	[DllImport("FLAC")]
	public static extern bool FLAC__stream_encoder_set_loose_mid_side_stereo(IntPtr encoder, bool value);
	[DllImport("FLAC")]
	public static extern bool FLAC__stream_encoder_set_apodization(IntPtr encoder, string specification);
	[DllImport("FLAC")]
	public static extern bool FLAC__stream_encoder_set_max_lpc_order(IntPtr encoder, int value);
	[DllImport("FLAC")]
	public static extern bool FLAC__stream_encoder_set_qlp_coeff_precision(IntPtr encoder, int value);
	[DllImport("FLAC")]
	public static extern bool FLAC__stream_encoder_set_do_qlp_coeff_prec_search(IntPtr encoder, bool value);
	[DllImport("FLAC")]
	public static extern bool FLAC__stream_encoder_set_do_escape_coding(IntPtr encoder, bool value);
	[DllImport("FLAC")]
	public static extern bool FLAC__stream_encoder_set_do_exhaustive_model_search(IntPtr encoder, bool value);
	[DllImport("FLAC")]
	public static extern bool FLAC__stream_encoder_set_min_residual_partition_order(IntPtr encoder, int value);
	[DllImport("FLAC")]
	public static extern bool FLAC__stream_encoder_set_max_residual_partition_order(IntPtr encoder, int value);
	[DllImport("FLAC")]
	public static extern bool FLAC__stream_encoder_set_rice_parameter_search_dist(IntPtr encoder, int value);
	[DllImport("FLAC")]
	public static extern bool FLAC__stream_encoder_set_total_samples_estimate(IntPtr encoder, long value);
	[DllImport("FLAC")]
	public static extern bool FLAC__stream_encoder_set_metadata(IntPtr encoder, IntPtr[] metadata, int num_blocks);
	[DllImport("FLAC")]
	public static extern bool FLAC__stream_encoder_set_metadata(IntPtr encoder, byte[][] metadata, int num_blocks);

	public static bool FLAC__stream_encoder_set_metadata(IntPtr encoder, StreamMetadata[] metadata, int num_blocks)
	{
		byte[][] serializedMetadata = new byte[metadata.Length][];

		for (int i = 0; i < metadata.Length; i++)
		{
			switch (metadata[i].Type)
			{
				case MetadataType.STREAMINFO:
				{
					var with = new StreamMetadata_with_StreamInfo() { StreamInfo = metadata[i].Data.StreamInfo };
					serializedMetadata[i] = StructureSerializer.MarshalToBytes(with);
					break;
				}
				case MetadataType.PADDING:
				{
					var with = new StreamMetadata_with_Padding() { Padding = metadata[i].Data.Padding };
					serializedMetadata[i] = StructureSerializer.MarshalToBytes(with);
					break;
				}
				case MetadataType.APPLICATION:
				{
					var with = new StreamMetadata_with_Application() { Application = metadata[i].Data.Application };
					serializedMetadata[i] = StructureSerializer.MarshalToBytes(with);
					break;
				}
				case MetadataType.SEEKTABLE:
				{
					var with = new StreamMetadata_with_SeekTable() { SeekTable = metadata[i].Data.SeekTable };
					serializedMetadata[i] = StructureSerializer.MarshalToBytes(with);
					break;
				}
				case MetadataType.VORBISCOMMENT:
				{
					var with = new StreamMetadata_with_VorbisComment() { VorbisComment = metadata[i].Data.VorbisComment };
					serializedMetadata[i] = StructureSerializer.MarshalToBytes(with);
					break;
				}
				case MetadataType.CUESHEET:
				{
					var with = new StreamMetadata_with_CueSheet() { CueSheet = metadata[i].Data.CueSheet };
					serializedMetadata[i] = StructureSerializer.MarshalToBytes(with);
					break;
				}
				case MetadataType.PICTURE:
				{
					var with = new StreamMetadata_with_Picture() { Picture = metadata[i].Data.Picture };
					serializedMetadata[i] = StructureSerializer.MarshalToBytes(with);
					break;
				}
				default: throw new NotSupportedException();
			}
		}

		return FLAC__stream_encoder_set_metadata(encoder, serializedMetadata, num_blocks);
	}

	[DllImport("FLAC")]
	public static extern bool FLAC__stream_encoder_set_limit_min_bitrate(IntPtr encoder, bool value);
	[DllImport("FLAC")]
	public static extern StreamEncoderState FLAC__stream_encoder_get_state(IntPtr encoder);
	[DllImport("FLAC")]
	public static extern StreamDecoderState FLAC__stream_encoder_get_verify_decoder_state(IntPtr encoder);
	[DllImport("FLAC")]
	public static extern string FLAC__stream_encoder_get_resolved_state_string(IntPtr encoder);
	[DllImport("FLAC")]
	public static extern void FLAC__stream_encoder_get_verify_decoder_error_stats(IntPtr encoder, out long absoluteSample, out int frameNumber, out int channel, out int sample, out int expected, out int got);
	[DllImport("FLAC")]
	public static extern bool FLAC__stream_encoder_get_verify(IntPtr encoder);
	[DllImport("FLAC")]
	public static extern bool FLAC__stream_encoder_get_streamable_subset(IntPtr encoder);
	[DllImport("FLAC")]
	public static extern int FLAC__stream_encoder_get_channels(IntPtr encoder);
	[DllImport("FLAC")]
	public static extern int FLAC__stream_encoder_get_bits_per_sample(IntPtr encoder);
	[DllImport("FLAC")]
	public static extern int FLAC__stream_encoder_get_sample_rate(IntPtr encoder);
	[DllImport("FLAC")]
	public static extern int FLAC__stream_encoder_get_compression_level(IntPtr encoder);
	[DllImport("FLAC")]
	public static extern int FLAC__stream_encoder_get_blocksize(IntPtr encoder);
	[DllImport("FLAC")]
	public static extern bool FLAC__stream_encoder_get_do_mid_side_stereo(IntPtr encoder);
	[DllImport("FLAC")]
	public static extern bool FLAC__stream_encoder_get_loose_mid_side_stereo(IntPtr encoder);
	[DllImport("FLAC")]
	public static extern int FLAC__stream_encoder_get_max_lpc_order(IntPtr encoder);
	[DllImport("FLAC")]
	public static extern int FLAC__stream_encoder_get_qlp_coeff_precision(IntPtr encoder);
	[DllImport("FLAC")]
	public static extern bool FLAC__stream_encoder_get_do_qlp_coeff_prec_search(IntPtr encoder);
	[DllImport("FLAC")]
	public static extern bool FLAC__stream_encoder_get_do_escape_coding(IntPtr encoder);
	[DllImport("FLAC")]
	public static extern bool FLAC__stream_encoder_get_do_exhaustive_model_search(IntPtr encoder);
	[DllImport("FLAC")]
	public static extern int FLAC__stream_encoder_get_min_residual_partition_order(IntPtr encoder);
	[DllImport("FLAC")]
	public static extern int FLAC__stream_encoder_get_max_residual_partition_order(IntPtr encoder);
	[DllImport("FLAC")]
	public static extern int FLAC__stream_encoder_get_rice_parameter_search_dist(IntPtr encoder);
	[DllImport("FLAC")]
	public static extern long FLAC__stream_encoder_get_total_samples_estimate(IntPtr encoder);
	[DllImport("FLAC")]
	public static extern bool FLAC__stream_encoder_get_limit_min_bitrate(IntPtr encoder);
	[DllImport("FLAC")]
	public static extern StreamEncoderInitStatus FLAC__stream_encoder_init_stream(IntPtr encoder, StreamEncoderWriteCallback writeCallback, StreamEncoderSeekCallback seekCallback, StreamEncoderTellCallback tellCallback, StreamEncoderMetadataCallback? metadataCallback, IntPtr clientData);
	[DllImport("FLAC")]
	public static extern StreamEncoderInitStatus FLAC__stream_encoder_init_ogg_stream(IntPtr encoder, StreamEncoderReadCallback readCallback, StreamEncoderWriteCallback writeCallback, StreamEncoderSeekCallback seekCallback, StreamEncoderTellCallback tellCallback, StreamEncoderMetadataCallback? metadataCallback, IntPtr clientData);
	[DllImport("FLAC")]
	public static extern StreamEncoderInitStatus FLAC__stream_encoder_init_FILE(IntPtr encoder, /* FILE * */ IntPtr file, StreamEncoderProgressCallback progressCallback, IntPtr clientData);
	[DllImport("FLAC")]
	public static extern StreamEncoderInitStatus FLAC__stream_encoder_init_ogg_FILE(IntPtr encoder, /* FILE * */ IntPtr file, StreamEncoderProgressCallback progressCallback, IntPtr clientData);
	[DllImport("FLAC")]
	public static extern StreamEncoderInitStatus FLAC__stream_encoder_init_file(IntPtr encoder, string filename, StreamEncoderProgressCallback progressCallback, IntPtr clientData);
	[DllImport("FLAC")]
	public static extern StreamEncoderInitStatus FLAC__stream_encoder_init_ogg_file(IntPtr encoder, string filename, StreamEncoderProgressCallback progressCallback, IntPtr clientData);
	[DllImport("FLAC")]
	public static extern bool FLAC__stream_encoder_finish(IntPtr encoder);
	[DllImport("FLAC")]
	public static extern bool FLAC__stream_encoder_process(IntPtr encoder, int[] buffer, int samples);
	[DllImport("FLAC")]
	public static extern bool FLAC__stream_encoder_process_interleaved(IntPtr encoder, int[] buffer, int samples);

	[DllImport("FLAC")]
	public static extern IntPtr FLAC__metadata_object_new(MetadataType type);
	[DllImport("FLAC")]
	public static extern void FLAC__metadata_object_delete(IntPtr @object);
	[DllImport("FLAC")]
	public static extern bool FLAC__metadata_object_application_set_data(IntPtr @object, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] data, int length, bool copy);
	[DllImport("FLAC")]
	public static extern bool FLAC__metadata_object_vorbiscomment_append_comment(IntPtr @object, VorbisCommentEntry entry, bool copy);
}
