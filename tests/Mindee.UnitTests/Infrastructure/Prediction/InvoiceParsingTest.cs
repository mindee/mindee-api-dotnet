using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Mindee.Infrastructure.Api;
using Mindee.Infrastructure.Prediction;
using Mindee.Prediction;
using RichardSzalay.MockHttp;

namespace Mindee.UnitTests.Infrastructure.Prediction
{
    public class InvoiceParsingTest
    {
        [Fact]
        public async Task Execute_WithInvoicePdf_MustSuccess()
        {
            IInvoiceParsing invoiceParsing = new InvoiceParsing(GetMindeeApi());
            var invoicePrediction = await invoiceParsing.ExecuteAsync(Stream.Null, "Bou");

            Assert.NotNull(invoicePrediction);
        }

        [Fact]
        public async Task Execute_WithInvoicePdf_MustSuccessForCustomer()
        {
            IInvoiceParsing invoiceParsing = new InvoiceParsing(GetMindeeApi());
            var invoicePrediction = await invoiceParsing.ExecuteAsync(Stream.Null, "Bou");

            Assert.Equal(0.87, invoicePrediction.Inference.Pages.First().Prediction.Customer.Confidence);
            Assert.Equal(0, invoicePrediction.Inference.Pages.First().Id);
            Assert.Equal(new List<List<double>>()
            {
                new List<double>() { 0.072, 0.291 },
                new List<double>() { 0.164, 0.291 },
                new List<double>() { 0.164, 0.302 },
                new List<double>() { 0.072, 0.302 },
            }
            , invoicePrediction.Inference.Pages.First().Prediction.Customer.Polygon);
            Assert.Equal("TEST BUSINESS", invoicePrediction.Inference.Pages.First().Prediction.Customer.Value);
        }

        [Fact]
        public async Task Execute_WithInvoicePdf_MustSuccessForDate()
        {
            IInvoiceParsing invoiceParsing = new InvoiceParsing(GetMindeeApi());
            var invoicePrediction = await invoiceParsing.ExecuteAsync(Stream.Null, "Bou");

            Assert.Equal(0.99, invoicePrediction.Inference.Pages.First().Prediction.Date.Confidence);
            Assert.Equal(0, invoicePrediction.Inference.Pages.First().Id);
            Assert.Equal("2016-01-25", invoicePrediction.Inference.Pages.First().Prediction.Date.Value);
        }

        [Fact]
        public async Task Execute_WithInvoicePdf_MustSuccessForDocumentType()
        {
            IInvoiceParsing invoiceParsing = new InvoiceParsing(GetMindeeApi());
            var invoicePrediction = await invoiceParsing.ExecuteAsync(Stream.Null, "Bou");

            Assert.Equal("INVOICE", invoicePrediction.Inference.Pages.First().Prediction.DocumentType.Value);
        }

        [Fact]
        public async Task Execute_WithInvoicePdf_MustSuccessForLocale()
        {
            IInvoiceParsing invoiceParsing = new InvoiceParsing(GetMindeeApi());
            var invoicePrediction = await invoiceParsing.ExecuteAsync(Stream.Null, "Bou");

            Assert.Equal("en", invoicePrediction.Inference.Pages.First().Prediction.Locale.Language);
            Assert.Equal("USD", invoicePrediction.Inference.Pages.First().Prediction.Locale.Currency);
        }

        [Fact]
        public async Task Execute_WithInvoicePdf_MustSuccessForTotalTaxesIncluded()
        {
            IInvoiceParsing invoiceParsing = new InvoiceParsing(GetMindeeApi());
            var invoicePrediction = await invoiceParsing.ExecuteAsync(Stream.Null, "Bou");

            Assert.Equal(93.5, invoicePrediction.Inference.Pages.First().Prediction.TotalIncl.Value);
        }

        [Fact(Skip = "Orientation can not be get for now.")]
        public async Task Execute_WithInvoicePdf_MustSuccessForOrientation()
        {
            IInvoiceParsing invoiceParsing = new InvoiceParsing(GetMindeeApi());
            var invoicePrediction = await invoiceParsing.ExecuteAsync(Stream.Null, "Bou");

            Assert.Equal(90, invoicePrediction.Inference.Pages.First().Prediction.Orientation.Degrees);
        }

        private static MindeeApi GetMindeeApi()
        {
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When("*")
                    .Respond("application/json", File.ReadAllText("inv2.json"));

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
