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
        ///     Exponential backoff multiplier applied to polling interval after each attempt.
        ///     <c>1.0</c> keeps a fixed interval.
        /// </summary>
        public double BackoffFactor { get; set; }

        /// <summary>
        ///     Upper bound for backoff-adjusted interval.
        /// </summary>
        public double MaxIntervalSec { get; set; }

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
        /// Minimum supported backoff multiplier.
        /// </summary>
        protected readonly double MinBackoffFactor = 1.0;

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
            if (double.IsNaN(BackoffFactor) || double.IsInfinity(BackoffFactor))
            {
                throw new MindeeException("Polling backoff multiplier must be a finite number.");
            }
            if (BackoffFactor < MinBackoffFactor)
            {
                throw new MindeeException("Cannot set polling backoff multiplier below 1.0.");
            }
            if (double.IsNaN(MaxIntervalSec) || double.IsInfinity(MaxIntervalSec))
            {
                throw new MindeeException("Max polling interval must be a finite number.");
            }
            if (MaxIntervalSec < IntervalSec)
            {
                throw new MindeeException("Cannot set max polling interval below polling interval.");
            }
        }

        /// <summary>
        ///     Compute delay in milliseconds before the given polling attempt.
        /// </summary>
        /// <param name="attemptNumber">1-based polling attempt number.</param>
        /// <returns>Delay in milliseconds.</returns>
        public int GetRetryDelayMilliSec(int attemptNumber)
        {
            if (attemptNumber <= 1)
            {
                return IntervalMilliSec;
            }

            var exponentialInterval = IntervalSec * Math.Pow(BackoffFactor, attemptNumber - 1);
            var boundedInterval = Math.Min(exponentialInterval, MaxIntervalSec);
            return (int)Math.Floor(boundedInterval * 1000);
        }
    }
}
