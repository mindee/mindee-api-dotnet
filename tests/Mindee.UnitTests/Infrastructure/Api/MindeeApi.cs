using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Mindee.Infrastructure.Api;
using RichardSzalay.MockHttp;

namespace Mindee.UnitTests.Infrastructure.Api
{
    public class MindeeApiTest
    {
        [Fact]
        public async Task Execute_WithInvoiceData_WithOcrAsked_MustGetOcrData()
        {
            var mindeeApi = GetMindeeApi("resources/inv2 - withFullText.json");
            var predictParameter = new PredictParameter(
                        Stream.Null,
                        "Bou",
                        true);
            var predictResponse = await mindeeApi.PredictInvoiceAsync(predictParameter);

            Assert.Equal(0.92, predictResponse.Document.Ocr.MvisionV1.Pages.First().AllWords.First().Confidence);
            Assert.Equal(new List<List<double>>()
            {
                new List<double>() { 0.635, 0.924 },
                new List<double>() { 0.705, 0.924 },
                new List<double>() { 0.705, 0.936 },
                new List<double>() { 0.635, 0.936 },
            }
            , predictResponse.Document.Ocr.MvisionV1.Pages.First().AllWords.First().Polygon);
            Assert.Equal("Payment", predictResponse.Document.Ocr.MvisionV1.Pages.First().AllWords.First().Text);
        }

        private static MindeeApi GetMindeeApi(string fileNmae)
        {
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When("*")
                    .Respond("application/json", File.ReadAllText(fileNmae));

            var config = new ConfigurationBuilder()
                            .AddInMemoryCollection(new Dictionary<string, string>() {
                                { "MindeeApiSettings:ApiKey", "blou" }
                            })
                        .Build();

            return new MindeeApi(
                new NullLogger<MindeeApi>(),
                Options.Create(new MindeeApiSettings()
                {
                    ApiKey = "Expelliarmus"
                }),
                config,
                mockHttp
                );
        }
    }
}
