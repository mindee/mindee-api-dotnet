using System;
using System.Threading;

namespace Mindee.Http
{
    internal static class HttpTimeouts
    {
        private const string HardTimeoutEnvVar = "MINDEE_TEST_HARD_TIMEOUT_SECONDS";

        internal static CancellationTokenSource CreateHardTimeoutCts()
        {
            var hardTimeout = ReadHardTimeout();
            return hardTimeout.HasValue
                ? new CancellationTokenSource(hardTimeout.Value)
                : new CancellationTokenSource();
        }

        private static TimeSpan? ReadHardTimeout()
        {
            var raw = Environment.GetEnvironmentVariable(HardTimeoutEnvVar);
            if (string.IsNullOrWhiteSpace(raw))
            {
                return null;
            }

            if (!int.TryParse(raw, out var seconds) || seconds <= 0)
            {
                return null;
            }

            return TimeSpan.FromSeconds(seconds);
        }
    }
}
