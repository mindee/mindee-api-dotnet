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

            await Assert.ThrowsAsync<MindeeException>(
                () => _ = mindeeApi.PredictAsync<ReceiptV4Inference>(ParsingTestBase.GetFakePredictParameter())
                );
        }

        [Fact]
        public async Task Predict_WithValidResponse()
        {
            var mindeeApi = InitMockServer(HttpStatusCode.OK, "Resources/invoice/response_v4/complete.json");

            var document = await mindeeApi.PredictAsync<InvoiceV4Inference>(ParsingTestBase.GetFakePredictParameter());
            var expected = File.ReadAllText("Resources/invoice/response_v4/summary_full.rst");

            Assert.NotNull(document);
            Assert.Equal(expected, document.ToString());
        }

        [Fact]
        public async Task Predict_WithErrorResponse()
        {
            var mindeeApi = InitMockServer(HttpStatusCode.BadRequest, "Resources/errors/complete_with_object_response_in_detail.json");

            await Assert.ThrowsAsync<MindeeException>(
                () => _ = mindeeApi.PredictAsync<ReceiptV4Inference>(ParsingTestBase.GetFakePredictParameter())
                );
        }

        [Fact]
        public async Task EnqueuePredict_Post_Fail_Forbidden()
        {
            var mindeeApi = InitMockServer(HttpStatusCode.BadRequest, "Resources/async/post_fail_forbidden.json");

            var response = await mindeeApi.EnqueuePredictAsync<InvoiceV4Inference>(ParsingTestBase.GetFakePredictParameter());

            Assert.NotNull(response);
            Assert.Equal("failure", response.ApiRequest.Status);
            Assert.Null(response.Job.Status);
            Assert.Null(response.Job.Id);
            Assert.Equal("2023-01-01T01:00:00.00000", response.Job.IssuedAt.ToString(DateOutputFormat));
            Assert.Null(response.Job.AvailableAt);
        }

        [Fact]
        public async Task EnqueuePredict_Post_Success()
        {
            var mindeeApi = InitMockServer(HttpStatusCode.OK, "Resources/async/post_success.json");

            var response = await mindeeApi.EnqueuePredictAsync<InvoiceV4Inference>(ParsingTestBase.GetFakePredictParameter());

            Assert.NotNull(response);
            Assert.Equal("success", response.ApiRequest.Status);
            Assert.Equal("waiting", response.Job.Status);
            Assert.Equal("76c90710-3a1b-4b91-8a39-31a6543e347c", response.Job.Id);
            Assert.Equal("2023-02-16T13:33:49.60294", response.Job.IssuedAt.ToString(DateOutputFormat));
            Assert.Null(response.Job.AvailableAt?.ToString(DateOutputFormat));
        }

        [Fact]
        public async Task EnqueuePredict_Get_Processing()
        {
            var mindeeApi = InitMockServer(HttpStatusCode.OK, "Resources/async/get_processing.json");

            var response = await mindeeApi.EnqueuePredictAsync<InvoiceV4Inference>(ParsingTestBase.GetFakePredictParameter());

            Assert.NotNull(response);
            Assert.Equal("success", response.ApiRequest.Status);
            Assert.Equal("processing", response.Job.Status);
            Assert.Equal("76c90710-3a1b-4b91-8a39-31a6543e347c", response.Job.Id);
            Assert.Equal("2023-03-16T12:33:49.60294", response.Job.IssuedAt.ToString(DateOutputFormat));
            Assert.Null(response.Job.AvailableAt);
        }
    }
}
