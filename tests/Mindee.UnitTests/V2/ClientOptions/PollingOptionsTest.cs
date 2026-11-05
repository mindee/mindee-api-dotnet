using Mindee.Exceptions;

namespace Mindee.UnitTests.V2.ClientOptions
{
    [Trait("Category", "PollingOptions")]
    [Trait("Category", "V2")]
    public class PollingOptionsTest
    {
        [Fact(DisplayName = "provides sensible defaults")]
        public void PollingOptions_DefaultOptions_MustInit()
        {
            var options = new Mindee.V2.ClientOptions.PollingOptions();
            Assert.Equal(2.0, options.InitialDelaySec);
            Assert.Equal(1.5, options.IntervalSec);
            Assert.Equal(80, options.MaxRetries);
            Assert.Equal(2000, options.InitialDelayMilliSec);
            Assert.Equal(1500, options.IntervalMilliSec);
        }

        [Fact(DisplayName = "allows custom values")]
        public void PollingOptions_ValidOptions_MustInit()
        {
            var options = new Mindee.V2.ClientOptions.PollingOptions(3.0, 5, 100);
            Assert.Equal(3.0, options.InitialDelaySec);
            Assert.Equal(5.0, options.IntervalSec);
            Assert.Equal(100, options.MaxRetries);
            Assert.Equal(3000, options.InitialDelayMilliSec);
            Assert.Equal(5000, options.IntervalMilliSec);
        }

        [Fact(DisplayName = "raises an error with invalid options")]
        public void PollingOptions_InvalidOptions_MustThrow()
        {
            Assert.Throws<MindeeException>(
                () => new Mindee.V2.ClientOptions.PollingOptions(-1.0, 1.5, 80));
            Assert.Throws<MindeeException>(
                () => new Mindee.V2.ClientOptions.PollingOptions(2.0, -1.5, 80));
            Assert.Throws<MindeeException>(
                () => new Mindee.V2.ClientOptions.PollingOptions(2.0, 1.5, -1));
        }
    }
}
