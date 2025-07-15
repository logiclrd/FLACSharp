using System;
using System.Collections;
using System.Runtime.InteropServices;

public static class FLACLibrary
{
	static IntPtr s_library = NativeLibrary.Load("FLAC");

	public static bool IsLoaded => (s_library != IntPtr.Zero);

	static string? GetExportedString(string symbolName, int length = -1)
	{
		if (s_library == IntPtr.Zero)
			return null;

		var ptr = NativeLibrary.GetExport(s_library, symbolName);

		if (ptr == IntPtr.Zero)
			return null;
		else if (length >= 0)
			return Marshal.PtrToStringAnsi(ptr, length);
		else
			return Marshal.PtrToStringAnsi(ptr);
	}

	static int GetExportedInt32(string symbolName)
	{
		if (s_library == IntPtr.Zero)
			return 0;

		var ptr = NativeLibrary.GetExport(s_library, symbolName);

		if (ptr == IntPtr.Zero)
			return 0;
		else
			return Marshal.ReadInt32(ptr);
	}

	static long GetExportedInt64(string symbolName)
	{
		if (s_library == IntPtr.Zero)
			return 0;

		var ptr = NativeLibrary.GetExport(s_library, symbolName);

		if (ptr == IntPtr.Zero)
			return 0;
		else
			return Marshal.ReadInt64(ptr);
	}

	internal static string? GetExportedTableString(string symbolName, int tableIndex)
	{
		if (s_library == IntPtr.Zero)
			return null;

		var ptr = NativeLibrary.GetExport(s_library, symbolName);

		if (ptr == IntPtr.Zero)
			return null;
		else
		{
			ptr = Marshal.ReadIntPtr(ptr, tableIndex * IntPtr.Size);

			return Marshal.PtrToStringAnsi(ptr);
		}
	}

	public static string? GetVersionString() => GetExportedString("FLAC__VERSION_STRING");
	public static string? GetVendorString() => GetExportedString("FLAC__VENDOR_STRING");

	public static string? GetStreamSyncString() => GetExportedString("FLAC__STREAM_SYNC_STRING", 4);

	public static int GetStreamSync() => GetExportedInt32("FLAC__STREAM_SYNC");

	public static int GetPartitionedRiceEscapeParameter() => GetExportedInt32("FLAC__ENTROPY_CODING_METHOD_PARTITIONED_RICE_ESCAPE_PARAMETER");
	public static int GetPartitionedRice2EscapeParameter() => GetExportedInt32("FLAC__ENTROPY_CODING_METHOD_PARTITIONED_RICE2_ESCAPE_PARAMETER");

	public static class SubframeTypeByteAlignmentMasks
	{
		public static int Constant => GetExportedInt32("FLAC__SUBFRAME_TYPE_CONSTANT_BYTE_ALIGNED_MASK");
		public static int Verbatim => GetExportedInt32("FLAC__SUBFRAME_TYPE_VERBATIM_BYTE_ALIGNED_MASK");
		public static int Fixed => GetExportedInt32("FLAC__SUBFRAME_TYPE_FIXED_BYTE_ALIGNED_MASK");
		public static int LPC => GetExportedInt32("FLAC__SUBFRAME_TYPE_LPC_BYTE_ALIGNED_MASK");
	}

	public static int FrameHeaderSync => GetExportedInt32("FLAC__FRAME_HEADER_SYNC");

	public static long SeekPointPlaceHolderValue => GetExportedInt64("FLAC__STREAM_METADATA_SEEKPOINT_PLACEHOLDER");

	public static class BitLengths
	{
		public static int StreamSync => GetExportedInt32("FLAC__STREAM_SYNC_LEN");

		public static int PartitionedRiceOrder => GetExportedInt32("FLAC__ENTROPY_CODING_METHOD_PARTITIONED_RICE_ORDER_LEN");
		public static int PartitionedRiceParameter => GetExportedInt32("FLAC__ENTROPY_CODING_METHOD_PARTITIONED_RICE_PARAMETER_LEN");
		public static int PartitionedRice2Parameter => GetExportedInt32("FLAC__ENTROPY_CODING_METHOD_PARTITIONED_RICE2_PARAMETER_LEN");
		public static int PartitionedRiceRaw => GetExportedInt32("FLAC__ENTROPY_CODING_METHOD_PARTITIONED_RICE_RAW_LEN");

		public static int EntropyCodingMethodType => GetExportedInt32("FLAC__ENTROPY_CODING_METHOD_TYPE_LEN");

		public static int LPCQLPCoefficientPrecision => GetExportedInt32("FLAC__SUBFRAME_LPC_QLP_COEFF_PRECISION_LEN");
		public static int LPCQLPShift => GetExportedInt32("FLAC__SUBFRAME_LPC_QLP_SHIFT_LEN");

		public static int SubframeZeroPad => GetExportedInt32("FLAC__SUBFRAME_ZERO_PAD_LEN");
		public static int SubframeType => GetExportedInt32("FLAC__SUBFRAME_TYPE_LEN");
		public static int SubframeWastedBitsFlag => GetExportedInt32("FLAC__SUBFRAME_WASTED_BITS_FLAG_LEN");

		public static int FrameHeaderSync => GetExportedInt32("FLAC__FRAME_HEADER_SYNC_LEN");
		public static int FrameHeaderReserved => GetExportedInt32("FLAC__FRAME_HEADER_RESERVED_LEN");
		public static int FrameHeaderBlockingStrategy => GetExportedInt32("FLAC__FRAME_HEADER_BLOCKING_STRATEGY_LEN");
		public static int FrameHeaderBlockSize => GetExportedInt32("FLAC__FRAME_HEADER_BLOCK_SIZE_LEN");
		public static int FrameHeaderSampleRate => GetExportedInt32("FLAC__FRAME_HEADER_SAMPLE_RATE_LEN");
		public static int FrameHeaderChannelAssignment => GetExportedInt32("FLAC__FRAME_HEADER_CHANNEL_ASSIGNMENT_LEN");
		public static int FrameHeaderBitsPerSample => GetExportedInt32("FLAC__FRAME_HEADER_BITS_PER_SAMPLE_LEN");
		public static int FrameHeaderZeroPad => GetExportedInt32("FLAC__FRAME_HEADER_ZERO_PAD_LEN");
		public static int FrameHeaderCRC => GetExportedInt32("FLAC__FRAME_HEADER_CRC_LEN");

		public static int FrameFooterCRC => GetExportedInt32("FLAC__FRAME_FOOTER_CRC_LEN");

		public static int StreamInfoMinBlockSize => GetExportedInt32("FLAC__STREAM_METADATA_STREAMINFO_MIN_BLOCK_SIZE_LEN");
		public static int StreamInfoMaxBlockSize => GetExportedInt32("FLAC__STREAM_METADATA_STREAMINFO_MAX_BLOCK_SIZE_LEN");
		public static int StreamInfoMinFrameSize => GetExportedInt32("FLAC__STREAM_METADATA_STREAMINFO_MIN_FRAME_SIZE_LEN");
		public static int StreamInfoMaxFrameSize => GetExportedInt32("FLAC__STREAM_METADATA_STREAMINFO_MAX_FRAME_SIZE_LEN");
		public static int StreamInfoSampleRate => GetExportedInt32("FLAC__STREAM_METADATA_STREAMINFO_SAMPLE_RATE_LEN");
		public static int StreamInfoChannels => GetExportedInt32("FLAC__STREAM_METADATA_STREAMINFO_CHANNELS_LEN");
		public static int StreamInfoBitsPerSample => GetExportedInt32("FLAC__STREAM_METADATA_STREAMINFO_BITS_PER_SAMPLE_LEN");
		public static int StreamInfoTotalSamples => GetExportedInt32("FLAC__STREAM_METADATA_STREAMINFO_TOTAL_SAMPLES_LEN");
		public static int StreamInfoMD5Sum => GetExportedInt32("FLAC__STREAM_METADATA_STREAMINFO_MD5SUM_LEN");

		public static int ApplicationID => GetExportedInt32("FLAC__STREAM_METADATA_APPLICATION_ID_LEN");

		public static int SeekPointSampleNumber => GetExportedInt32("FLAC__STREAM_METADATA_SEEKPOINT_SAMPLE_NUMBER_LEN");
		public static int SeekPointStreamOffset => GetExportedInt32("FLAC__STREAM_METADATA_SEEKPOINT_STREAM_OFFSET_LEN");
		public static int SeekPointFrameSamples => GetExportedInt32("FLAC__STREAM_METADATA_SEEKPOINT_FRAME_SAMPLES_LEN");

		public static int VorbisCommentEntryLength => GetExportedInt32("FLAC__STREAM_METADATA_VORBIS_COMMENT_ENTRY_LENGTH_LEN");

		public static int VorbisCommentNumComments => GetExportedInt32("FLAC__STREAM_METADATA_VORBIS_COMMENT_NUM_COMMENTS_LEN");

		public static int CueSheetIndexOffset => GetExportedInt32("FLAC__STREAM_METADATA_CUESHEET_INDEX_OFFSET_LEN");
		public static int CueSheetIndexNumber => GetExportedInt32("FLAC__STREAM_METADATA_CUESHEET_INDEX_NUMBER_LEN");
		public static int CueSheetIndexReserved => GetExportedInt32("FLAC__STREAM_METADATA_CUESHEET_INDEX_RESERVED_LEN");

		public static int CueSheetTrackOffset => GetExportedInt32("FLAC__STREAM_METADATA_CUESHEET_TRACK_OFFSET_LEN");
		public static int CueSheetTrackNumber => GetExportedInt32("FLAC__STREAM_METADATA_CUESHEET_TRACK_NUMBER_LEN");
		public static int CueSheetTrackISRC => GetExportedInt32("FLAC__STREAM_METADATA_CUESHEET_TRACK_ISRC_LEN");
		public static int CueSheetTrackType => GetExportedInt32("FLAC__STREAM_METADATA_CUESHEET_TRACK_TYPE_LEN");
		public static int CueSheetTrackPreEmphasis => GetExportedInt32("FLAC__STREAM_METADATA_CUESHEET_TRACK_PRE_EMPHASIS_LEN");
		public static int CueSheetTrackReserved => GetExportedInt32("FLAC__STREAM_METADATA_CUESHEET_TRACK_RESERVED_LEN");
		public static int CueSheetTrackNumIndices => GetExportedInt32("FLAC__STREAM_METADATA_CUESHEET_TRACK_NUM_INDICES_LEN");

		public static int CueSheetMediaCatalogNumber => GetExportedInt32("FLAC__STREAM_METADATA_CUESHEET_MEDIA_CATALOG_NUMBER_LEN");
		public static int CueSheetLeadIn => GetExportedInt32("FLAC__STREAM_METADATA_CUESHEET_LEAD_IN_LEN");
		public static int CueSheetIsCD => GetExportedInt32("FLAC__STREAM_METADATA_CUESHEET_IS_CD_LEN");
		public static int CueSheetReserved => GetExportedInt32("FLAC__STREAM_METADATA_CUESHEET_RESERVED_LEN");
		public static int CueSheetNumTracks => GetExportedInt32("FLAC__STREAM_METADATA_CUESHEET_NUM_TRACKS_LEN");

		public static int PictureType => GetExportedInt32("FLAC__STREAM_METADATA_PICTURE_TYPE_LEN");
		public static int PictureMIMETypeLength => GetExportedInt32("FLAC__STREAM_METADATA_PICTURE_MIME_TYPE_LENGTH_LEN");
		public static int PictureDescriptionLength => GetExportedInt32("FLAC__STREAM_METADATA_PICTURE_DESCRIPTION_LENGTH_LEN");
		public static int PictureWidth => GetExportedInt32("FLAC__STREAM_METADATA_PICTURE_WIDTH_LEN");
		public static int PictureHeight => GetExportedInt32("FLAC__STREAM_METADATA_PICTURE_HEIGHT_LEN");
		public static int PictureDepth => GetExportedInt32("FLAC__STREAM_METADATA_PICTURE_DEPTH_LEN");
		public static int PictureColours => GetExportedInt32("FLAC__STREAM_METADATA_PICTURE_COLORS_LEN");
		public static int PictureDataLength => GetExportedInt32("FLAC__STREAM_METADATA_PICTURE_DATA_LENGTH_LEN");

		public static int StreamMetadataIsLast => GetExportedInt32("FLAC__STREAM_METADATA_IS_LAST_LEN");
		public static int StreamMetadataType => GetExportedInt32("FLAC__STREAM_METADATA_TYPE_LEN");
		public static int StreamMetadataLength => GetExportedInt32("FLAC__STREAM_METADATA_LENGTH_LEN");
	}
}
