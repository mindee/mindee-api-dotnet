namespace Mindee.ClientOptions
{
    /// <summary>
    ///     ResultOptions to pass for asynchronous parsing with polling.
    /// </summary>
    public abstract class BasePollingOptions
    {
        /// <summary>
        ///     Initial delay before the first polling attempt.
        /// </summary>
        public double InitialDelaySec { get; set; }

        /// <summary>
        ///     Delay between each polling attempt.
        /// </summary>
        public double IntervalSec { get; set; }

        /// <summary>
        ///     Total number of polling attempts.
        /// </summary>
        public int MaxRetries { get; set; }

        /// <summary>
        ///     Wait this many milliseconds between each polling attempt.
        /// </summary>
        public int InitialDelayMilliSec { get; set; }

        /// <summary>
        ///     Wait this many milliseconds before the first polling attempt.
        /// </summary>
        public int IntervalMilliSec { get; set; }
    }
}
