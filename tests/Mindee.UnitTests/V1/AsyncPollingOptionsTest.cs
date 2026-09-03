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
            Assert.Equal(3000, options.InitialDelayMilliSec);
            Assert.Equal(5000, options.IntervalMilliSec);
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
        }
    }
}
