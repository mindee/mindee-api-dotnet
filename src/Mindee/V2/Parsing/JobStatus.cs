namespace Mindee.V2.Parsing
{
    /// <summary>
    ///     Typed job status for V2 polling responses.
    /// </summary>
    public enum JobStatus
    {
        /// <summary>
        ///     Unknown or unsupported status value.
        /// </summary>
        Unknown = 0,

        /// <summary>
        ///     Job is still processing.
        /// </summary>
        Processing,

        /// <summary>
        ///     Job finished successfully and results can be retrieved.
        /// </summary>
        Processed,

        /// <summary>
        ///     Job failed.
        /// </summary>
        Failed
    }
}
