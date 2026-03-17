using System;

namespace Mindee.V2.ClientOptions
{
    /// <summary>
    /// Polling options for V2 parsing.
    /// </summary>
    public class PollingOptions : Mindee.ClientOptions.BasePollingOptions
    {
        /// <inheritdoc />
        public PollingOptions(double initialDelaySec = 2.0, double intervalSec = 1.5, int maxRetries = 80)
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
