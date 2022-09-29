using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Mindee.Infrastructure.Api;
using Mindee.Infrastructure.Prediction;
using Mindee.Prediction;
using RichardSzalay.MockHttp;

namespace Mindee.UnitTests.Prediction
{
    public class DocumentParserTest
    {
        [Fact]
        public async Task FromReceipt_WithReceiptData_MustSuccess()
        {
            IReceiptParsing receiptParsing = new ReceiptParsing(GetMindeeApi());
            var documentParser = new DocumentParser(null, receiptParsing, null);
            var prediction = await documentParser.FromReceipt(Stream.Null, "Bou");

            Assert.NotNull(prediction);
        }

        [Fact]
        public async Task FromReceipt_WithReceiptData_MustSuccessForCategory()
        {
            IReceiptParsing receiptParsing = new ReceiptParsing(GetMindeeApi());
            var documentParser = new DocumentParser(null, receiptParsing, null);
            var prediction = await documentParser.FromReceipt(Stream.Null, "Bou");

            Assert.Equal(0.99, prediction.Category.Confidence);
            Assert.Equal("transport", prediction.Category.Value);
        }

        [Fact]
        public async Task FromReceipt_WithReceiptData_MustSuccessForDate()
        {
            IReceiptParsing receiptParsing = new ReceiptParsing(GetMindeeApi());
            var documentParser = new DocumentParser(null, receiptParsing, null);
            var prediction = await documentParser.FromReceipt(Stream.Null, "Bou");

            Assert.Equal(0.99, prediction.Date.Confidence);
            Assert.Equal(0, prediction.Date.PageId);
            Assert.Equal("2017-04-12", prediction.Date.Value);
        }

        [Fact]
        public async Task FromReceipt_WithReceiptData_MustSuccessForTime()
        {
            IReceiptParsing receiptParsing = new ReceiptParsing(GetMindeeApi());
            var documentParser = new DocumentParser(null, receiptParsing, null);
            var prediction = await documentParser.FromReceipt(Stream.Null, "Bou");

            Assert.Equal(0.99, prediction.Time.Confidence);
            Assert.Equal(0, prediction.Time.PageId);
            Assert.Equal("07:21", prediction.Time.Value);
            Assert.Equal(new List<List<double>>()
            {
                new List<double>() { 0.1048, 0.5534 },
                new List<double>() { 0.8827, 0.8493 },
                new List<double>() { 0.8356, 0.8054 },
                new List<double>() { 0.1461, 0.8072 },
            }
            , prediction.Time.Polygon);
        }

        [Fact]
        public async Task FromReceipt_WithReceiptData_MustSuccessForLocale()
        {
            IReceiptParsing receiptParsing = new ReceiptParsing(GetMindeeApi());
            var documentParser = new DocumentParser(null, receiptParsing, null);
            var prediction = await documentParser.FromReceipt(Stream.Null, "Bou");

            Assert.Equal("fi", prediction.Locale.Language);
            Assert.Equal("FI", prediction.Locale.Country);
            Assert.Equal("EUR", prediction.Locale.Currency);
        }

        [Fact]
        public async Task FromReceipt_WithReceiptData_MustSuccessForTotalTaxesIncluded()
        {
            IReceiptParsing receiptParsing = new ReceiptParsing(GetMindeeApi());
            var documentParser = new DocumentParser(null, receiptParsing, null);
            var prediction = await documentParser.FromReceipt(Stream.Null, "Bou");

            Assert.Equal(473.88, prediction.TotalIncl.Value);
        }

        private static MindeeApi GetMindeeApi()
        {
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When("*")
                    .Respond("application/json", File.ReadAllText("receipt_response_full_v3.json"));

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
