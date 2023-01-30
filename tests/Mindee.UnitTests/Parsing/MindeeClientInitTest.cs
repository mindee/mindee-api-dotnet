namespace Mindee.UnitTests.Parsing
{
    public class MindeeClientInitTest
    {
        [Fact]
        [Trait("Category", "MindeeClient init")]
        public async Task Create_WithApiKey()
        {
            Assert.NotNull(MindeeClientInit.Create("MyApiKey"));
        }

        [Fact]
        [Trait("Category", "MindeeClient init")]
        public async Task Create_WithoutApiKey()
        {
            Assert.NotNull(MindeeClientInit.Create(new MindeeSettings() { ApiKey = "MyApiKey" }));
        }
    }
}
