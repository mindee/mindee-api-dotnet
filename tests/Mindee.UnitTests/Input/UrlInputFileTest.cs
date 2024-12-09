using System.Net;
using Mindee.Exceptions;
using Mindee.Input;
using Moq;
using RestSharp;

namespace Mindee.UnitTests.Input
{
    [Trait("Category", "URL loading")]
    public class UrlInputSourceTest
    {
        private readonly Mock<IRestClient> _mockRestClient = new();

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
        [Fact]
        public async Task AsLocalInputSource_SuccessfulDownload()
        {
            _mockRestClient
                .Setup(x => x.ExecuteAsync(It.IsAny<RestRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new RestResponse
                {
                    StatusCode = HttpStatusCode.OK,
                    RawBytes = [1, 2, 3, 4, 5],
                    IsSuccessStatusCode = true
                });

            var urlInputSource = new UrlInputSource("https://example.com/file.pdf");
            var result = await urlInputSource.AsLocalInputSource(restClient: _mockRestClient.Object);

            Assert.IsType<LocalInputSource>(result);
            Assert.Equal("file.pdf", result.Filename);
            Assert.Equal(5, result.FileBytes.Length);
        }

        [Fact]
        public async Task AsLocalInputSource_FailedDownload()
        {
            _mockRestClient
                .Setup(x => x.ExecuteAsync(It.IsAny<RestRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new RestResponse
                {
                    StatusCode = HttpStatusCode.NotFound,
                    ErrorMessage = "File not found",
                    IsSuccessStatusCode = false
                });

            var urlInputSource = new UrlInputSource("https://example.com/nonexistent.pdf");
            await Assert.ThrowsAsync<MindeeInputException>(
                () => urlInputSource.AsLocalInputSource(restClient: _mockRestClient.Object));
        }

        [Fact]
        public async Task AsLocalInputSource_WithCustomFilename()
        {
            _mockRestClient
                .Setup(x => x.ExecuteAsync(It.IsAny<RestRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new RestResponse
                {
                    StatusCode = HttpStatusCode.OK,
                    RawBytes = [1, 2, 3, 4, 5],
                    IsSuccessStatusCode = true
                });

            var urlInputSource = new UrlInputSource("https://example.com/file.pdf");
            var result = await urlInputSource.AsLocalInputSource("custom.pdf", restClient: _mockRestClient.Object);

            Assert.IsType<LocalInputSource>(result);
            Assert.Equal("custom.pdf", result.Filename);
        }

        [Fact]
        public async Task AsLocalInputSource_WithAuthentication()
        {
            _mockRestClient
                .Setup(x => x.ExecuteAsync(It.IsAny<RestRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new RestResponse
                {
                    StatusCode = HttpStatusCode.OK,
                    RawBytes = [1, 2, 3, 4, 5],
                    IsSuccessStatusCode = true
                });

            var urlInputSource = new UrlInputSource("https://example.com/file.pdf");
            var result = await urlInputSource.AsLocalInputSource(username: "user", password: "pass", restClient: _mockRestClient.Object);

            Assert.IsType<LocalInputSource>(result);
            Assert.Equal("file.pdf", result.Filename);
        }

        [Fact]
        public async Task AsLocalInputSource_InvalidFilename()
        {
            var urlInputSource = new UrlInputSource("https://example.com/file.pdf");
            await Assert.ThrowsAsync<MindeeInputException>(() => urlInputSource.AsLocalInputSource("invalid"));
        }
    }
}
