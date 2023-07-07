using System.Net;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Mindee.Exceptions;
using Mindee.Http;
using Mindee.Product.Invoice;
using Mindee.Product.Receipt;
using Mindee.UnitTests.Parsing;
using RichardSzalay.MockHttp;

namespace Mindee.UnitTests.Http
{
    [Trait("Category", "Mindee Api")]
    public sealed class MindeeApiTest
    {
        private const string DateOutputFormat = "yyyy-MM-ddTHH:mm:ss.fffff";

        private MindeeApi InitMockServer(HttpStatusCode statusCode, string fileToLoad)
        {
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When("*")
                .Respond(
                    statusCode,
                    "application/json",
                    File.ReadAllText(fileToLoad)
                    );
            var mindeeApi = new MindeeApi(
                Options.Create(new MindeeSettings()
                {
                    ApiKey = "MyKey"
                }),
                new NullLogger<MindeeApi>(),
                mockHttp
                );
            return mindeeApi;
        }

        [Fact]
        public async Task Predict_WithWrongKey()
        {
            var mindeeApi = InitMockServer(HttpStatusCode.BadRequest, "Resources/errors/wrong_api_key.json");

            await Assert.ThrowsAsync<Mindee400Exception>(
               () => _ = mindeeApi.PredictPostAsync<ReceiptV4>(UnitTestBase.GetFakePredictParameter()));
        }

        [Fact]
        public async Task Predict_WithValidResponse()
        {
            var mindeeApi = InitMockServer(HttpStatusCode.OK, "Resources/invoice/response_v4/complete.json");

            var response = await mindeeApi.PredictPostAsync<InvoiceV4>(UnitTestBase.GetFakePredictParameter());
            var expected = File.ReadAllText("Resources/invoice/response_v4/summary_full.rst");

            Assert.NotNull(response);
            Assert.Equal(expected, response.Document.ToString());
        }

        [Fact]
        public async Task Predict_WithErrorResponse()
        {
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When("*")
                .Respond(
                    HttpStatusCode.BadRequest,
                    "application/json",
                    File.ReadAllText("Resources/errors/error_400_from_mindeeapi_with_object_response_in_detail.json")
                );

            var mindeeApi = new MindeeApi(
                Options.Create(new MindeeSettings() { ApiKey = "MyKey" }),
                new NullLogger<MindeeApi>(),
                mockHttp
                );

            await Assert.ThrowsAsync<Mindee400Exception>(
                           () => _ = mindeeApi.PredictPostAsync<ReceiptV4>(UnitTestBase.GetFakePredictParameter()));
        }

        [Fact]
        public async Task Predict_500Error()
        {
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When("*")
                .Respond(
                    HttpStatusCode.InternalServerError,
                    "application/json",
                    File.ReadAllText("Resources/errors/error_500_from_mindeeapi.json")
                );

            var mindeeApi = new MindeeApi(
                Options.Create(new MindeeSettings() { ApiKey = "MyKey" }),
                new NullLogger<MindeeApi>(),
                mockHttp
                );

            await Assert.ThrowsAsync<Mindee500Exception>(
                           () => _ = mindeeApi.PredictPostAsync<ReceiptV4>(UnitTestBase.GetFakePredictParameter()));
        }

        [Fact]
        public async Task Predict_429Error()
        {
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When("*")
                .Respond(
                    (HttpStatusCode)429,
                    "application/json",
                    File.ReadAllText("Resources/errors/error_429_from_mindeeapi.json")
                );

            var mindeeApi = new MindeeApi(
                Options.Create(new MindeeSettings() { ApiKey = "MyKey" }),
                new NullLogger<MindeeApi>(),
                mockHttp
                );

            await Assert.ThrowsAsync<Mindee429Exception>(
               () => _ = mindeeApi.PredictPostAsync<ReceiptV4>(
                   UnitTestBase.GetFakePredictParameter()
                   )
               );
        }

        [Fact]
        public async Task Predict_401Error()
        {
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When("*")
                .Respond(
                    HttpStatusCode.Unauthorized,
                    "application/json",
                    File.ReadAllText("Resources/errors/error_401_from_mindeeapi.json")
                    );

            var mindeeApi = new MindeeApi(
                Options.Create(new MindeeSettings() { ApiKey = "MyKey" }),
                new NullLogger<MindeeApi>(),
                mockHttp
                );

            await Assert.ThrowsAsync<Mindee401Exception>(
                () => _ = mindeeApi.PredictPostAsync<ReceiptV4>(
                    UnitTestBase.GetFakePredictParameter()
                    )
                );
        }

        [Fact]
        public async Task PredictAsyncPostAsync_WithFailForbidden()
        {
            var mindeeApi = InitMockServer(HttpStatusCode.BadRequest, "Resources/async/post_fail_forbidden.json");

            var response = await mindeeApi.PredictAsyncPostAsync<InvoiceV4>(UnitTestBase.GetFakePredictParameter());

            Assert.NotNull(response);
            Assert.Equal("failure", response.ApiRequest.Status);
            Assert.Null(response.Job.Status);
            Assert.Null(response.Job.Id);
            Assert.Equal("2023-01-01T00:00:00.00000", response.Job.IssuedAt.ToString(DateOutputFormat));
            Assert.Null(response.Job.AvailableAt);
        }

        [Fact]
        public async Task PredictAsyncPostAsync_WithSuccess()
        {
            var mindeeApi = InitMockServer(HttpStatusCode.OK, "Resources/async/post_success.json");

            var response = await mindeeApi.PredictAsyncPostAsync<InvoiceV4>(UnitTestBase.GetFakePredictParameter());

            Assert.NotNull(response);
            Assert.Equal("success", response.ApiRequest.Status);
            Assert.Equal("waiting", response.Job.Status);
            Assert.Equal("76c90710-3a1b-4b91-8a39-31a6543e347c", response.Job.Id);
            Assert.Equal("2023-02-16T12:33:49.60294", response.Job.IssuedAt.ToString(DateOutputFormat));
            Assert.Null(response.Job.AvailableAt?.ToString(DateOutputFormat));
        }

        [Fact]
        public async Task DocumentQueueGetAsync_WithJobProcessing()
        {
            var mindeeApi = InitMockServer(HttpStatusCode.OK, "Resources/async/get_processing.json");

            var response = await mindeeApi.DocumentQueueGetAsync<InvoiceV4>("76c90710-3a1b-4b91-8a39-31a6543e347c");

            Assert.NotNull(response);
            Assert.Equal("success", response.ApiRequest.Status);
            Assert.Equal("processing", response.Job.Status);
            Assert.Equal("76c90710-3a1b-4b91-8a39-31a6543e347c", response.Job.Id);
            Assert.Equal("2023-03-16T12:33:49.60294", response.Job.IssuedAt.ToString(DateOutputFormat));
            Assert.Null(response.Job.AvailableAt);
        }

        [Fact]
        public async Task DocumentQueueGetAsync_WithSuccess()
        {
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When("*/documents/queue/*")
                .WithHeaders(
                    "Location",
                    @"/products/Mindee/invoice_splitter_beta/v1/documents/async/e66cfef5-8a31-4278-8ced-004fd8a345b2"
                    )
                .Respond(HttpStatusCode.Redirect);

            mockHttp.When("*/documents/*")
                .Respond(
                    HttpStatusCode.OK,
                    "application/json",
                    File.ReadAllText("Resources/async/get_completed.json")
                );

            var mindeeApi = new MindeeApi(
                Options.Create(new MindeeSettings() { ApiKey = "MyKey" }),
                new NullLogger<MindeeApi>(),
                mockHttp
                );

            var response = await mindeeApi.DocumentQueueGetAsync<InvoiceV4>("my-job-id");

            Assert.NotNull(response);
            Assert.NotNull(response.Document);
            Assert.Contains(response.ApiRequest.Resources, r => r.Equals("job"));
            Assert.Contains(response.ApiRequest.Resources, r => r.Equals("document"));
        }
    }
}
