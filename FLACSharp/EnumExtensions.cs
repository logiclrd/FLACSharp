using System.Threading.Channels;

namespace libFLAC;

public static class EnumExtensions
{
	/** Maps a FLAC__EntropyCodingMethodType to a C string.
	 *
	 *  Using a FLAC__EntropyCodingMethodType as the parameter to this extension method
	 *  give the string equivalent.
	 */
	public static string? GetDescription(this EntropyCodingMethodType value)
		=> FLACLibrary.GetExportedTableString("FLAC__EntropyCodingMethodTypeString", (int)value);
	public static string? GetDescription(this SubframeType value)
		=> FLACLibrary.GetExportedTableString("FLAC__SubframeTypeString", (int)value);
	public static string? GetDescription(this ChannelAssignment value)
		=> FLACLibrary.GetExportedTableString("FLAC__ChannelAssignmentString", (int)value);
	public static string? GetDescription(this FrameNumberType value)
		=> FLACLibrary.GetExportedTableString("FLAC__FrameNumberTypeString", (int)value);
	public static string? GetDescription(this MetadataType value)
		=> FLACLibrary.GetExportedTableString("FLAC__MetadataTypeString", (int)value);
	public static string? GetDescription(this StreamMetadata_Picture_Type value)
		=> FLACLibrary.GetExportedTableString("FLAC__StreamMetadata_Picture_TypeString", (int)value);
	public static string? GetDescription(this StreamEncoderState value)
		=> FLACLibrary.GetExportedTableString("FLAC__StreamEncoderStateString", (int)value);
	public static string? GetDescription(this StreamEncoderInitStatus value)
		=> FLACLibrary.GetExportedTableString("FLAC__StreamEncoderInitStatusString", (int)value);
	public static string? GetDescription(this StreamEncoderReadStatus value)
		=> FLACLibrary.GetExportedTableString("FLAC__StreamEncoderReadStatusString", (int)value);
	public static string? GetDescription(this StreamEncoderWriteStatus value)
		=> FLACLibrary.GetExportedTableString("FLAC__StreamEncoderWriteStatusString", (int)value);
	public static string? GetDescription(this StreamEncoderSeekStatus value)
		=> FLACLibrary.GetExportedTableString("FLAC__StreamEncoderSeekStatusString", (int)value);
	public static string? GetDescription(this StreamEncoderTellStatus value)
		=> FLACLibrary.GetExportedTableString("FLAC__StreamEncoderTellStatusString", (int)value);
	public static string? GetDescription(this StreamDecoderState value)
		=> FLACLibrary.GetExportedTableString("FLAC__StreamDecoderStateString", (int)value);
	public static string? GetDescription(this StreamDecoderInitStatus value)
		=> FLACLibrary.GetExportedTableString("FLAC__StreamDecoderInitStatusString", (int)value);
	public static string? GetDescription(this StreamDecoderReadStatus value)
		=> FLACLibrary.GetExportedTableString("FLAC__StreamDecoderReadStatusString", (int)value);
	public static string? GetDescription(this StreamDecoderSeekStatus value)
		=> FLACLibrary.GetExportedTableString("FLAC__StreamDecoderSeekStatusString", (int)value);
	public static string? GetDescription(this StreamDecoderTellStatus value)
		=> FLACLibrary.GetExportedTableString("FLAC__StreamDecoderTellStatusString", (int)value);
	public static string? GetDescription(this StreamDecoderLengthStatus value)
		=> FLACLibrary.GetExportedTableString("FLAC__StreamDecoderLengthStatusString", (int)value);
	public static string? GetDescription(this StreamDecoderWriteStatus value)
		=> FLACLibrary.GetExportedTableString("FLAC__StreamDecoderWriteStatusString", (int)value);
	public static string? GetDescription(this StreamDecoderErrorStatus value)
		=> FLACLibrary.GetExportedTableString("FLAC__StreamDecoderErrorStatusString", (int)value);
}
