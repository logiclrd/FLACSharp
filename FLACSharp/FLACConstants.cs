namespace libFLAC;

public class FLACConstants
{
	/** The minimum block size, in samples, permitted by the format. */
	public const int MinBlockSize = 16;

	/** The maximum block size, in samples, permitted by the format. */
	public const int MaxBlockSize = 65535;

	/** The maximum block size, in samples, permitted by the FLAC subset for
	 *  sample rates up to 48kHz. */
	public const int SubsetMaxBlockSize48000Hz = 4608;

	/** The maximum number of channels permitted by the format. */
	public const int MaxChannels = 8;

	/** The minimum sample resolution permitted by the format. */
	public const int MinBitsPerSample = 4;

	/** The maximum sample resolution permitted by the format. */
	public const int MaxBitsPerSampel = 32;

	/** The maximum sample resolution permitted by libFLAC.
	 *
	 * FLAC__MAX_BITS_PER_SAMPLE is the limit of the FLAC format.  However,
	 * the reference encoder/decoder used to be limited to 24 bits. This
	 * value was used to signal that limit.
	 */
	public const int ReferenceCodecMaxBitsPerSample = 32;

	/** The maximum sample rate permitted by the format.  The value is
	 *  ((2 ^ 20) - 1)
	 */
	public const int MaxSampleRate = 1048575;

	/** The maximum LPC order permitted by the format. */
	public const int MaxLPCOrder = 32;

	/** The maximum LPC order permitted by the FLAC subset for sample rates
	 *  up to 48kHz. */
	public const int SubsetMaxLPCOrder48000Hz = 12;

	/** The minimum quantized linear predictor coefficient precision
	 *  permitted by the format.
	 */
	public const int MinQLPCoefficientPrecision = 5;

	/** The maximum quantized linear predictor coefficient precision
	 *  permitted by the format.
	 */
	public const int MaxQLPCoefficientPrecision = 15;

	/** The maximum order of the fixed predictors permitted by the format. */
	public const int MaxFixedOrder = 4;

	/** The maximum Rice partition order permitted by the format. */
	public const int MaxRicePartitionOrder = 15;

	/** The maximum Rice partition order permitted by the FLAC Subset. */
	public const int SubsetMaxRicePartitionOrder = 8;

	/** The length of the FLAC signature in bytes. */
	public const int StreamSyncLength = 4;

}
