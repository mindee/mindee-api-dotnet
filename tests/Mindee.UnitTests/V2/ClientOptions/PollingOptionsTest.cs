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
            Assert.Equal(3000, options.InitialDelayMilliSec);
            Assert.Equal(5000, options.IntervalMilliSec);
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
        }
    }
}
