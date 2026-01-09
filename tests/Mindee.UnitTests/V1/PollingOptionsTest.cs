using Mindee.Exceptions;

namespace Mindee.UnitTests.V1
{
    [Trait("Category", "V1")]
    [Trait("Category", "Mindee client options")]
    public class PollingOptionsTest
    {
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
