using Mindee.Exceptions;
using Mindee.V1.ClientOptions;

namespace Mindee.UnitTests.V1
{
    [Trait("Category", "V1")]
    [Trait("Category", "Mindee client options")]
    public class AsyncPollingOptionsTest
    {
        [Fact]
        public void ValidPollingOptions_MustInit()
        {
            var options = new AsyncPollingOptions(3.0, 5, 100);
            Assert.Equal(3.0, options.InitialDelaySec);
            Assert.Equal(5.0, options.IntervalSec);
            Assert.Equal(100, options.MaxRetries);
            Assert.Equal(1.0, options.BackoffFactor);
            Assert.Equal(5.0, options.MaxIntervalSec);
            Assert.Equal(3000, options.InitialDelayMilliSec);
            Assert.Equal(5000, options.IntervalMilliSec);
        }

        [Fact]
        public void PollingOptions_WithBackoff_MustIncreaseAndCapInterval()
        {
            var options = new AsyncPollingOptions(
                initialDelaySec: 2.0,
                intervalSec: 1.5,
                maxRetries: 80,
                backoffFactor: 2.0,
                maxIntervalSec: 5.0);

            Assert.Equal(1500, options.GetRetryDelayMilliSec(1));
            Assert.Equal(3000, options.GetRetryDelayMilliSec(2));
            Assert.Equal(5000, options.GetRetryDelayMilliSec(3));
            Assert.Equal(5000, options.GetRetryDelayMilliSec(10));
        }

        [Fact]
        public void InvalidPollingOptions_MustFail()
        {
            Assert.Throws<MindeeException>(() => _ = new AsyncPollingOptions(0.5)
            );
            Assert.Throws<MindeeException>(() => _ = new AsyncPollingOptions(intervalSec: 0.5)
            );
            Assert.Throws<MindeeException>(() => _ = new AsyncPollingOptions(maxRetries: 1)
            );
            Assert.Throws<MindeeException>(() => _ = new AsyncPollingOptions(backoffFactor: 0.9)
            );
            Assert.Throws<MindeeException>(() => _ = new AsyncPollingOptions(maxIntervalSec: 1.0)
            );
        }
    }
}
