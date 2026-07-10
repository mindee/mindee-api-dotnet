using Mindee.Exceptions;

namespace Mindee.UnitTests.V2.ClientOptions
{
    [Trait("Category", "PollingOptions")]
    [Trait("Category", "V2")]
    public class PollingOptionsTest
    {
        [Fact]
        public void PollingOptions_ValidOptions_MustInit()
        {
            var options = new Mindee.V2.ClientOptions.PollingOptions(3.0, 5, 100);
            Assert.Equal(3.0, options.InitialDelaySec);
            Assert.Equal(5.0, options.IntervalSec);
            Assert.Equal(100, options.MaxRetries);
            Assert.Equal(1.0, options.BackoffFactor);
            Assert.Equal(5.0, options.MaxIntervalSec);
            Assert.Equal(3000, options.InitialDelayMilliSec);
            Assert.Equal(5000, options.IntervalMilliSec);
            Assert.Equal(5000, options.GetRetryDelayMilliSec(1));
        }

        [Fact]
        public void PollingOptions_WithBackoff_MustIncreaseAndCapInterval()
        {
            var options = new Mindee.V2.ClientOptions.PollingOptions(
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
        public void PollingOptions_InvalidOptions_MustThrow()
        {
            Assert.Throws<MindeeException>(
                () => new Mindee.V2.ClientOptions.PollingOptions(-1.0, 1.5, 80));
            Assert.Throws<MindeeException>(
                () => new Mindee.V2.ClientOptions.PollingOptions(2.0, -1.5, 80));
            Assert.Throws<MindeeException>(
                () => new Mindee.V2.ClientOptions.PollingOptions(2.0, 1.5, -1));
            Assert.Throws<MindeeException>(
                () => new Mindee.V2.ClientOptions.PollingOptions(2.0, 1.5, 80, backoffFactor: 0.9));
            Assert.Throws<MindeeException>(
                () => new Mindee.V2.ClientOptions.PollingOptions(2.0, 1.5, 80, maxIntervalSec: 1.0));
        }
    }
}
