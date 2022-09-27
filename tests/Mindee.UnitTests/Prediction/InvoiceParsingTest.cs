using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Mindee.Infrastructure.Api;
using Mindee.Infrastructure.Prediction;
using Mindee.Prediction;
using RichardSzalay.MockHttp;

namespace Mindee.UnitTests.Prediction
{
    public class InvoiceParsingTest
    {
        [Fact]
        public async Task Execute_WithInvoicePdf_MustSuccess()
        {
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When("*")
                    .Respond("application/json", File.ReadAllText("inv2.json"));
            var mindeeApi = new MindeeApi(
                new NullLogger<MindeeApi>(), 
                Options.Create(new MindeeApiSettings()
                {
                    ApiKey = "Expelliarmus"
                }),
                mockHttp
                );

            IInvoiceParsing invoiceParsing = new InvoiceParsing(mindeeApi);
            var invoicePrediction = await invoiceParsing.ExecuteAsync(Stream.Null, "Bou");

            Assert.NotNull(invoicePrediction);
        }
    }
}
