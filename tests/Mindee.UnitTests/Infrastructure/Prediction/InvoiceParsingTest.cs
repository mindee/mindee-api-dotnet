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

            Assert.Equal(0.87, invoicePrediction.Customer.Confidence);
            Assert.Equal(0, invoicePrediction.Customer.PageId);
            Assert.Equal(new List<List<double>>()
            {
                new List<double>() { 0.072, 0.291 },
                new List<double>() { 0.164, 0.291 },
                new List<double>() { 0.164, 0.302 },
                new List<double>() { 0.072, 0.302 },
            }
            , invoicePrediction.Customer.Polygon);
            Assert.Equal("TEST BUSINESS", invoicePrediction.Customer.Value);
        }

        [Fact]
        public async Task Execute_WithInvoicePdf_MustSuccessForDate()
        {
            IInvoiceParsing invoiceParsing = new InvoiceParsing(GetMindeeApi());
            var invoicePrediction = await invoiceParsing.ExecuteAsync(Stream.Null, "Bou");

            Assert.Equal(0.99, invoicePrediction.Date.Confidence);
            Assert.Equal(0, invoicePrediction.Date.PageId);
            Assert.Equal("2016-01-25", invoicePrediction.Date.Value);
        }

        [Fact]
        public async Task Execute_WithInvoicePdf_MustSuccessForDocumentType()
        {
            IInvoiceParsing invoiceParsing = new InvoiceParsing(GetMindeeApi());
            var invoicePrediction = await invoiceParsing.ExecuteAsync(Stream.Null, "Bou");

            Assert.Equal("INVOICE", invoicePrediction.DocumentType.Value);
        }

        [Fact]
        public async Task Execute_WithInvoicePdf_MustSuccessForLocale()
        {
            IInvoiceParsing invoiceParsing = new InvoiceParsing(GetMindeeApi());
            var invoicePrediction = await invoiceParsing.ExecuteAsync(Stream.Null, "Bou");

            Assert.Equal("en", invoicePrediction.Locale.Language);
            Assert.Equal("USD", invoicePrediction.Locale.Currency);
        }

        [Fact]
        public async Task Execute_WithInvoicePdf_MustSuccessForTotalTaxesIncluded()
        {
            IInvoiceParsing invoiceParsing = new InvoiceParsing(GetMindeeApi());
            var invoicePrediction = await invoiceParsing.ExecuteAsync(Stream.Null, "Bou");

            Assert.Equal(93.5, invoicePrediction.TotalIncl.Value);
        }

        [Fact]
        public async Task Execute_WithInvoicePdf_MustSuccessForOrientation()
        {
            IInvoiceParsing invoiceParsing = new InvoiceParsing(GetMindeeApi());
            var invoicePrediction = await invoiceParsing.ExecuteAsync(Stream.Null, "Bou");

            Assert.Equal(90, invoicePrediction.Orientation.Degrees);
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
