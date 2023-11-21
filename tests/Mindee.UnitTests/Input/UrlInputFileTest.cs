using Mindee.Exceptions;
using Mindee.Input;

namespace Mindee.UnitTests.Input
{
    [Trait("Category", "URL loading")]
    public class UrlInputSourceTest
    {
        [Fact]
        public void Can_Load_Type_String()
        {
            Assert.IsType<UrlInputSource>(
                new UrlInputSource("https://www.example.com/some/file.ext"));
            Assert.IsType<UrlInputSource>(
                new UrlInputSource("https://www.example.com/some/file"));
        }

        [Fact]
        public void Can_Load_Type_Uri()
        {
            Assert.IsType<UrlInputSource>(
                new UrlInputSource(new Uri("https://www.example.com/some/file.ext")));
            Assert.IsType<UrlInputSource>(
                new UrlInputSource(new Uri("https://www.example.com/some/file")));
        }

        [Fact]
        public void DoesNot_Load_InvalidUrl()
        {
            Assert.Throws<MindeeInputException>(
                () => new UrlInputSource("http://www.example.com/some/file.ext"));
            Assert.Throws<MindeeInputException>(
                () => new UrlInputSource("file://users/home/some/file.ext"));
            Assert.Throws<UriFormatException>(
                () => new UrlInputSource("invalid-url"));
        }
    }
}
