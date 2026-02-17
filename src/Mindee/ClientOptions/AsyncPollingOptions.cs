using System;
using Mindee.Exceptions;

namespace Mindee.ClientOptions
{
    /// <summary>
    ///     ResultOptions to pass for asynchronous parsing with polling.
    /// </summary>
    public sealed class AsyncPollingOptions
    {
        /// <summary>
        ///     ResultOptions to pass for asynchronous parsing with polling.
        /// </summary>
        /// <param name="initialDelaySec">
        ///     <see cref="InitialDelaySec" />
        /// </param>
        /// <param name="intervalSec">
        ///     <see cref="IntervalSec" />
        /// </param>
        /// <param name="maxRetries">
        ///     <see cref="MaxRetries" />
        /// </param>
        public AsyncPollingOptions(double initialDelaySec = 2.0, double intervalSec = 1.5, int maxRetries = 80)
        {
            var minInitialDelaySec = 1.0;
            var minIntervalSec = 1.0;
            var minRetries = 2;
            if (initialDelaySec < minInitialDelaySec)
            {
                throw new MindeeException(
                    $"Cannot set initial polling delay to less than {Math.Floor(minInitialDelaySec)} second(s).");
            }

            if (intervalSec < minIntervalSec)
            {
                throw new MindeeException(
                    $"Cannot set polling interval to less than {Math.Floor(minIntervalSec)} second(s).");
            }

            if (maxRetries < minRetries)
            {
                throw new MindeeException($"Cannot set async retry to less than {minRetries} attempts.");
            }

            InitialDelaySec = initialDelaySec;
            IntervalSec = intervalSec;
            MaxRetries = maxRetries;
            InitialDelayMilliSec = (int)Math.Floor(InitialDelaySec * 1000);
            IntervalMilliSec = (int)Math.Floor(IntervalSec * 1000);
        }

        /// <summary>
        ///     Initial delay before the first polling attempt.
        /// </summary>
        public double InitialDelaySec { get; }

        /// <summary>
        ///     Delay between each polling attempt.
        /// </summary>
        public double IntervalSec { get; }

        /// <summary>
        ///     Total number of polling attempts.
        /// </summary>
        public int MaxRetries { get; }

        /// <summary>
        ///     Wait this many milliseconds between each polling attempt.
        /// </summary>
        public int InitialDelayMilliSec { get; }

        /// <summary>
        ///     Wait this many milliseconds before the first polling attempt.
        /// </summary>
        public int IntervalMilliSec { get; }
    }
}
