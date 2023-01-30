namespace Mindee.UnitTests.Parsing
{
    [Trait("Category", "MindeeClient init")]
    public class MindeeClientInitTest
    {
        [Fact]
        public async Task Create_WithApiKey()
        {
            Assert.NotNull(MindeeClientInit.Create("MyApiKey"));
        }

        [Fact]
        public async Task Create_WithoutApiKey()
        {
            Assert.NotNull(MindeeClientInit.Create(new MindeeSettings() { ApiKey = "MyApiKey" }));
        }
    }
}
