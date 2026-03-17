using System;
using Mindee.Exceptions;

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

        /// <summary>
        /// Minimum initial delay.
        /// </summary>
        protected readonly double MinInitialDelaySec;

        /// <summary>
        /// Minimum interval between polling attempts.
        /// </summary>
        protected readonly double MinIntervalSec;

        /// <summary>
        /// Minimum number of retries.
        /// </summary>
        protected readonly int MinRetries;

        /// <summary>
        /// Default constructor.
        /// </summary>
        protected BasePollingOptions(double minInitialDelaySec, double minIntervalSec, int minRetries)
        {
            this.MinInitialDelaySec = minInitialDelaySec;
            this.MinIntervalSec = minIntervalSec;
            this.MinRetries = minRetries;
        }

        /// <summary>
        /// Validate the polling settings.
        /// </summary>
        /// <exception cref="MindeeException"></exception>
        protected void ValidateSettings()
        {
            if (InitialDelaySec < MinInitialDelaySec)
            {
                throw new MindeeException(
                    $"Cannot set initial polling delay to less than {Math.Floor(MinInitialDelaySec)} second(s).");
            }
            if (IntervalSec < MinIntervalSec)
            {
                throw new MindeeException(
                    $"Cannot set polling interval to less than {Math.Floor(MinIntervalSec)} second(s).");
            }
            if (MaxRetries < MinRetries)
            {
                throw new MindeeException($"Cannot set async retry to less than {MinRetries} attempts.");
            }
        }
    }
}
