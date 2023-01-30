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
    }
}
