using System;

namespace Mindee.V1.ClientOptions
{
    /// <summary>
    /// Polling options for asynchronous parsing.
    /// </summary>
    public class AsyncPollingOptions : Mindee.ClientOptions.BasePollingOptions
    {
        /// <inheritdoc />
        /// <param name="initialDelaySec">Initial delay before the first polling attempt.</param>
        /// <param name="intervalSec">Delay between each polling attempt.</param>
        /// <param name="maxRetries">Total number of polling attempts.</param>
        public AsyncPollingOptions(
            double initialDelaySec = 2.0,
            double intervalSec = 1.5,
            int maxRetries = 80)
        : base(1.0, 1.0, 2)
        {
            InitialDelaySec = initialDelaySec;
            IntervalSec = intervalSec;
            MaxRetries = maxRetries;
            ValidateSettings();

            InitialDelayMilliSec = (int)Math.Floor(InitialDelaySec * 1000);
            IntervalMilliSec = (int)Math.Floor(IntervalSec * 1000);
        }
    }
}
