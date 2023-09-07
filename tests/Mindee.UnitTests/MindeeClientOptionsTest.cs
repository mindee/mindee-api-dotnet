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
                () => _ = new AsyncPollingOptions(intervalSec: 1)
                );
        }
    }
}
