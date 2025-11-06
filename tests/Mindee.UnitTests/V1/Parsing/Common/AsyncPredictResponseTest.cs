using System.Text.Json;
using Mindee.Parsing.Common;
using Mindee.Product.InvoiceSplitter;

namespace Mindee.UnitTests.V1.Parsing.Common
{
    [Trait("Category", "AsyncPredictResponse")]
    public class AsyncPredictResponseTest
    {
        private const string DateTimeFormat = "yyyy-MM-ddTHH:mm:ss";

        [Fact]
        public async Task WhenAsyncPost_ReturnsErrorForbidden_MustBeDeserialized()
        {
            var response = await JsonSerializer.DeserializeAsync<AsyncPredictResponse<InvoiceSplitterV1>>(
                new FileInfo("Resources/v1/async/post_fail_forbidden.json").OpenRead());

            Assert.NotNull(response);
            Assert.Equal("failure", response.ApiRequest.Status);
            Assert.Equal(403, response.ApiRequest.StatusCode);
            Assert.Null(response.Job);
        }

        [Fact]
        public async Task WhenAsyncPost_ReturnsSuccess_MustBeDeserialized()
        {
            var response = await JsonSerializer.DeserializeAsync<AsyncPredictResponse<InvoiceSplitterV1>>(
                new FileInfo("Resources/v1/async/post_success.json").OpenRead());

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
                new FileInfo("Resources/v1/async/get_completed.json").OpenRead());

            Assert.NotNull(response);
            Assert.Equal("success", response.ApiRequest.Status);
            Assert.Equal(200, response.ApiRequest.StatusCode);
            Assert.Equal("completed", response.Job.Status);
            Assert.Equal("2023-03-21T13:52:56", response.Job.IssuedAt.ToString(DateTimeFormat));
            Assert.Equal("2023-03-21T13:53:00", response.Job.AvailableAt?.ToString(DateTimeFormat));
            Assert.NotNull(response.Document);
            Assert.Equal(2, response.Document.NPages);
        }

        [Fact]
        public async Task WhenAsyncGet_ReturnsFailedJob_MustBeDeserialized()
        {
            var response = await JsonSerializer.DeserializeAsync<AsyncPredictResponse<InvoiceSplitterV1>>(
                new FileInfo("Resources/v1/async/get_failed_job_error.json").OpenRead());

            Assert.NotNull(response);
            Assert.Equal("success", response.ApiRequest.Status);
            Assert.Equal(200, response.ApiRequest.StatusCode);
            Assert.Equal("failed", response.Job.Status);
            Assert.Equal("2024-02-20T10:31:06", response.Job.IssuedAt.ToString(DateTimeFormat));
            Assert.Equal("2024-02-20T10:31:06", response.Job.AvailableAt?.ToString(DateTimeFormat));
            Assert.NotNull(response.Job.Error);
            Assert.Equal("ServerError", response.Job.Error.Code);
            Assert.Null(response.Document);
        }
    }
}
