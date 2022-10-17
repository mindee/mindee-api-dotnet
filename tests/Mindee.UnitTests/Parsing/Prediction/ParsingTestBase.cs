
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
using Mindee.Parsing;
using RichardSzalay.MockHttp;

namespace Mindee.UnitTests.Parsing.Prediction
{
    public abstract class ParsingTestBase
    {
        protected PredictParameter GetFakePredictParameter()
        {
            return
                new PredictParameter(
                    new byte[] { byte.MinValue },
                        "Bou");
        }

        protected MindeeApi GetMindeeApi(string fileName = "Resources/receipt_response_full_v3.json")
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
}
