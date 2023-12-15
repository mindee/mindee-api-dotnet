using Mindee.Exceptions;

namespace Mindee.UnitTests
{
    [Trait("Category", "Mindee client options")]
    public class MindeeClientOptionsTest
    {
        [Fact]
        public void InvalidPollingOptions_MustFail()
        {
            Assert.Throws<MindeeException>(
                () => _ = new AsyncPollingOptions(initialDelaySec: 1)
                );
            Assert.Throws<MindeeException>(
                () => _ = new AsyncPollingOptions(intervalSec: 0.5)
                );
            Assert.Throws<MindeeException>(
                () => _ = new AsyncPollingOptions(maxRetries: 1)
                );
        }
    }
}
