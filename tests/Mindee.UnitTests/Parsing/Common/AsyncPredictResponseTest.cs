using System.Text.Json;
using Mindee.Parsing.Common;
using Mindee.Product.InvoiceSplitter;

namespace Mindee.UnitTests.Parsing.Common
{
    [Trait("Category", "AsyncPredictResponse")]
    public class AsyncPredictResponseTest
    {
        private const string DateTimeFormat = "yyyy-MM-ddTHH:mm:ss";

        [Fact]
        public async Task WhenAsyncPost_ReturnsErrorForbidden_MustBeDeserialized()
        {
            var response = await JsonSerializer.DeserializeAsync<AsyncPredictResponse<InvoiceSplitterV1>>(
                new FileInfo("Resources/async/post_fail_forbidden.json").OpenRead());

            Assert.NotNull(response);
            Assert.Equal("failure", response.ApiRequest.Status);
            Assert.Equal(403, response.ApiRequest.StatusCode);
            Assert.Null(response.Job.Status);
            Assert.Equal("2023-01-01T00:00:00", response.Job.IssuedAt.ToString(DateTimeFormat));
            Assert.Null(response.Job.AvailableAt);
            Assert.Null(response.Document);
        }

        [Fact]
        public async Task WhenAsyncPost_ReturnsSuccess_MustBeDeserialized()
        {
            var response = await JsonSerializer.DeserializeAsync<AsyncPredictResponse<InvoiceSplitterV1>>(
                new FileInfo("Resources/async/post_success.json").OpenRead());

            Assert.NotNull(response);
            Assert.Equal("success", response.ApiRequest.Status);
            Assert.Equal(200, response.ApiRequest.StatusCode);
            Assert.Equal("waiting", response.Job.Status);
            Assert.Equal("2023-02-16T12:33:49", response.Job.IssuedAt.ToString(DateTimeFormat));
            Assert.Null(response.Job.AvailableAt);
            Assert.Null(response.Document);
        }

        [Fact]
        public async Task WhenAsyncGet_ReturnsCompleted_MustBeDeserialized()
        {
            var response = await JsonSerializer.DeserializeAsync<AsyncPredictResponse<InvoiceSplitterV1>>(
                new FileInfo("Resources/async/get_completed.json").OpenRead());

            Assert.NotNull(response);
            Assert.Equal("success", response.ApiRequest.Status);
            Assert.Equal(200, response.ApiRequest.StatusCode);
            Assert.Equal("completed", response.Job.Status);
            Assert.Equal("2023-03-21T13:52:56", response.Job.IssuedAt.ToString(DateTimeFormat));
            Assert.Equal("2023-03-21T13:53:00", response.Job.AvailableAt?.ToString(DateTimeFormat));
            Assert.NotNull(response.Document);
            Assert.Equal(2, response.Document.NPages);
        }
    }
}
