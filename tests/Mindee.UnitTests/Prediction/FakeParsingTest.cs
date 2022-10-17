
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
using Mindee.Parsing;
using Mindee.Parsing.Common;
using RichardSzalay.MockHttp;

namespace Mindee.UnitTests.Prediction
{
    public class FakeParsingTest
    {
        [Fact]
        public async Task Execute_WithAnyKindOfData_MustFail()
        {
            var mindeeAPi = GetMindeeApi();

            await Assert.ThrowsAsync<NotSupportedException>(
               () => _ = mindeeAPi.PredictAsync<FakePrediction>(GetFakePredictParameter()));
        }

        private PredictParameter GetFakePredictParameter()
        {
            return
                new PredictParameter(
                    new byte[] { byte.MinValue },
                        "Bou");
        }

        private static MindeeApi GetMindeeApi(string fileName = "Resources/receipt_response_full_v3.json")
        {
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When("*")
                    .Respond("application/json", File.ReadAllText(fileName));

            var config = new ConfigurationBuilder()
                            .AddInMemoryCollection(new Dictionary<string, string>() {
                                { "MindeeApiSettings:ApiKey", "blou" }
                            })
                        .Build();

            return new MindeeApi(
                new NullLogger<MindeeApi>(),
                config,
                mockHttp
                );
        }
    }

    internal sealed class FakePrediction : PredictionBase
    {

    }
}
