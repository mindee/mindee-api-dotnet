using System.Diagnostics;
using Mindee.ClientOptions;

namespace Mindee.UnitTests.ClientOptions
{
    public class BaseClientTest
    {
        private class TestableBaseClient : BaseClient
        {
            public Task TestThrottlePollingAttemptAsync(
                int tryCounter,
                int maxRetries,
                BasePollingOptions pollingOptions,
                CancellationToken cancellationToken = default)
            {
                return ThrottlePollingAttemptAsync(tryCounter, maxRetries, pollingOptions, cancellationToken);
            }
        }

        private class TestPollingOptions : BasePollingOptions
        {
            public TestPollingOptions(int intervalMilliSec) : base(1.0, 1.0, 1)
            {
                IntervalMilliSec = intervalMilliSec;
            }
        }

        [Fact(DisplayName = "should delay for the specified polling interval when retries remain")]
        public async Task PollingAttempt_WhenTryCounterLessThanMaxRetries_DelaysForInterval()
        {
            var client = new TestableBaseClient();
            var options = new TestPollingOptions(intervalMilliSec: 100);
            var stopwatch = Stopwatch.StartNew();

            await client.TestThrottlePollingAttemptAsync(tryCounter: 1, maxRetries: 3, options);
            stopwatch.Stop();

            Assert.True(stopwatch.ElapsedMilliseconds >= 90);
        }

        [Fact(DisplayName = "should return immediately without delay when max retries are reached")]
        public async Task PollingAttempt_WhenTryCounterEqualsMaxRetries_DoesNotDelay()
        {
            var client = new TestableBaseClient();
            var options = new TestPollingOptions(intervalMilliSec: 1000);
            var stopwatch = Stopwatch.StartNew();

            await client.TestThrottlePollingAttemptAsync(tryCounter: 3, maxRetries: 3, options);
            stopwatch.Stop();

            Assert.True(stopwatch.ElapsedMilliseconds < 200);
        }

        [Fact(DisplayName = "should throw when the cancellation token is canceled")]
        public async Task PollingAttempt_WithCancelledToken_ThrowsException()
        {
            var client = new TestableBaseClient();
            var options = new TestPollingOptions(intervalMilliSec: 5000);
            using var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.Cancel();

            await Assert.ThrowsAnyAsync<OperationCanceledException>(() =>
                client.TestThrottlePollingAttemptAsync(
                    tryCounter: 0, maxRetries: 3, options, cancellationTokenSource.Token));
        }
    }
}
