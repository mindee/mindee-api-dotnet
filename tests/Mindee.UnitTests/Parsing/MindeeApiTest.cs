using System.Net;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Mindee.Exceptions;
using Mindee.Parsing;
using Mindee.Parsing.Receipt;
using RichardSzalay.MockHttp;

namespace Mindee.UnitTests.Parsing
{
    public sealed class MindeeApiTest
    {
        [Fact]
        [Trait("Category", "Mindee Api")]
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
               () => _ = mindeeApi.PredictAsync<ReceiptV4Prediction>(ParsingTestBase.GetFakePredictParameter()));
        }
    }
}
