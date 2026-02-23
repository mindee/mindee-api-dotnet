using System;
using Mindee.ClientOptions;
using Mindee.Exceptions;

namespace Mindee.V2.ClientOptions
{
    /// <summary>
    /// Polling options for V2 parsing.
    /// </summary>
    public class PollingOptions : Mindee.ClientOptions.BasePollingOptions
    {
        /// <inheritdoc />
        public PollingOptions(double initialDelaySec = 2.0, double intervalSec = 1.5, int maxRetries = 80)
        {
            const double minInitialDelaySec = 1.0;
            const double minIntervalSec = 1.0;
            const int minRetries = 2;
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
    }
}
