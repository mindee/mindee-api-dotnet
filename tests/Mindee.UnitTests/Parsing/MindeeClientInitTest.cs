namespace Mindee.UnitTests.Parsing
{
    [Trait("Category", "Mindee client init")]
    public class MindeeClientInitTest
    {
        [Fact]
        public void Create_WithApiKey()
        {
            Assert.NotNull(MindeeClientInit.Create("MyApiKey"));
        }

        [Fact]
        public void Create_WithoutApiKey()
        {
            Assert.NotNull(MindeeClientInit.Create(new MindeeSettings() { ApiKey = "MyApiKey" }));
        }
    }
}
