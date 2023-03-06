using System.Net;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Mindee.Exceptions;
using Mindee.Parsing;
using Mindee.Parsing.Invoice;
using Mindee.Parsing.Receipt;
using RichardSzalay.MockHttp;

namespace Mindee.UnitTests.Parsing
{
    [Trait("Category", "Mindee Api")]
    public sealed class MindeeApiTest
    {
        [Fact]
        public async Task Predict_WithWrongKey()
        {
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When("*")
                    .Respond(HttpStatusCode.BadRequest, "application/json", File.ReadAllText("Resources/wrong_api_key.json"));

            var mindeeApi = new MindeeApi(
                Options.Create(new MindeeSettings() { ApiKey = "MyKey" }),
                new NullLogger<MindeeApi>(),
                mockHttp
                );

            await Assert.ThrowsAsync<MindeeException>(
               () => _ = mindeeApi.PredictAsync<ReceiptV4Inference>(ParsingTestBase.GetFakePredictParameter()));
        }

        [Fact]
        public async Task Predict_WithValidResponse()
        {
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When("*")
                    .Respond(HttpStatusCode.OK, "application/json", File.ReadAllText("Resources/invoice/response_v4/complete.json"));

            var mindeeApi = new MindeeApi(
                Options.Create(new MindeeSettings() { ApiKey = "MyKey" }),
                new NullLogger<MindeeApi>(),
                mockHttp
                );

            var document = await mindeeApi.PredictAsync<InvoiceV4Inference>(ParsingTestBase.GetFakePredictParameter());
            var expected = File.ReadAllText("Resources/invoice/response_v4/summary_full.rst");

            Assert.NotNull(document);
            Assert.Equal(
                expected,
                document.ToString());
        }

        [Fact]
        public async Task Predict_WithErrorResponse()
        {
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When("*")
                .Respond(
                    HttpStatusCode.BadRequest,
                    "application/json",
                    File.ReadAllText("Resources/errors/complete_with_object_response_in_detail.json")
                );

            var mindeeApi = new MindeeApi(
                Options.Create(new MindeeSettings() { ApiKey = "MyKey" }),
                new NullLogger<MindeeApi>(),
                mockHttp
                );

            await Assert.ThrowsAsync<MindeeException>(
                           () => _ = mindeeApi.PredictAsync<ReceiptV4Inference>(ParsingTestBase.GetFakePredictParameter()));
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
                           () => _ = mindeeApi.PredictAsync<ReceiptV4Inference>(ParsingTestBase.GetFakePredictParameter()));
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
                           () => _ = mindeeApi.PredictAsync<ReceiptV4Inference>(ParsingTestBase.GetFakePredictParameter()));
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
                           () => _ = mindeeApi.PredictAsync<ReceiptV4Inference>(ParsingTestBase.GetFakePredictParameter()));
        }

        [Fact]
        public async Task EnqueuePredict_Success()
        {
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When("*")
                .Respond(
                    HttpStatusCode.OK,
                    "application/json",
                    File.ReadAllText("Resources/async/enqueue_success_response.json")
                );
            var mindeeApi = new MindeeApi(
                Options.Create(new MindeeSettings() { ApiKey = "MyKey" }),
                new NullLogger<MindeeApi>(),
                mockHttp
                );

            var response = await mindeeApi.EnqueuePredictAsync<InvoiceV4Inference>(ParsingTestBase.GetFakePredictParameter());

            Assert.NotNull(response);
            Assert.Equal("success", response.ApiRequest.Status);
            Assert.Equal("processing", response.Job.Status);
            Assert.Equal("76c90710-3a1b-4b91-8a39-31a6543e347c", response.Job.Id);
        }

        [Fact]
        public async Task EnqueuePredict_WithProductWhichNotSupportsAsync_Fail()
        {
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When("*")
                .Respond(
                    HttpStatusCode.BadRequest,
                    "application/json",
                    File.ReadAllText("Resources/async/enqueue_fail_async_not_supported_response.json")
                );
            var mindeeApi = new MindeeApi(
                Options.Create(new MindeeSettings() { ApiKey = "MyKey" }),
                new NullLogger<MindeeApi>(),
                mockHttp
                );

            var response = await mindeeApi.EnqueuePredictAsync<InvoiceV4Inference>(ParsingTestBase.GetFakePredictParameter());

            Assert.NotNull(response);
            Assert.Equal("failure", response.ApiRequest.Status);
            Assert.Null(response.Job.Status);
            Assert.Null(response.Job.Id);
        }

        [Fact]
        public async Task GetJobAsync_WithJobInProgress()
        {
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When("*")
                .Respond(
                    HttpStatusCode.OK,
                    "application/json",
                    File.ReadAllText("Resources/async/get_job_in_progress.json")
                );
            var mindeeApi = new MindeeApi(
                Options.Create(new MindeeSettings() { ApiKey = "MyKey" }),
                new NullLogger<MindeeApi>(),
                mockHttp
                );

            var response = await mindeeApi.GetJobAsync<InvoiceV4Inference>("my-job-id");

            Assert.NotNull(response);
            Assert.Equal("job", response.ApiRequest.Resources.First());
            Assert.Equal("processing", response.Job.Status);
        }

        [Fact]
        public async Task GetJobAsync_WithSuccessfullJob()
        {
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When("*/documents/queue/*")
                .WithHeaders("Location", @"/products/Mindee/invoice_splitter_beta/v1/documents/async/e66cfef5-8a31-4278-8ced-004fd8a345b2")
                .Respond(HttpStatusCode.Redirect);

            mockHttp.When("*/documents/*")
                .Respond(
                    HttpStatusCode.OK,
                    "application/json",
                    File.ReadAllText("Resources/async/get_doc_parsed_successfuly.json")
                );

            var mindeeApi = new MindeeApi(
                Options.Create(new MindeeSettings() { ApiKey = "MyKey" }),
                new NullLogger<MindeeApi>(),
                mockHttp
                );

            var response = await mindeeApi.GetJobAsync<InvoiceV4Inference>("my-job-id");

            Assert.NotNull(response);
            Assert.NotNull(response.Document);
            Assert.Contains(response.ApiRequest.Resources, r => r.Equals("job"));
            Assert.Contains(response.ApiRequest.Resources, r => r.Equals("document"));
        }
    }
}
