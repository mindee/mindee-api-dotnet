﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Mindee.Infrastructure.Api;
using Mindee.Infrastructure.Prediction;
using Mindee.Domain.Parsing;
using RichardSzalay.MockHttp;
using Mindee.Domain;

namespace Mindee.UnitTests.Parsing
{
    public class DocumentParserTest
    {
        [Fact]
        public async Task FromReceipt_WithReceiptData_MustSuccess()
        {
            IReceiptParsing receiptParsing = new ReceiptParsing(GetMindeeApi());
            var documentParser = new DocumentParser(null, receiptParsing, null);
            var prediction = await documentParser.WithReceiptType(GetFakeParseParameter());

            Assert.NotNull(prediction);
        }

        [Fact]
        public async Task FromReceipt_WithReceiptData_MustSuccessForCategory()
        {
            IReceiptParsing receiptParsing = new ReceiptParsing(GetMindeeApi());
            var documentParser = new DocumentParser(null, receiptParsing, null);
            var prediction = await documentParser.WithReceiptType(GetFakeParseParameter());

            Assert.Equal(0.99, prediction.Inference.Pages.First().Prediction.Category.Confidence);
            Assert.Equal("transport", prediction.Inference.Pages.First().Prediction.Category.Value);
        }

        [Fact]
        public async Task FromReceipt_WithReceiptData_MustSuccessForDate()
        {
            IReceiptParsing receiptParsing = new ReceiptParsing(GetMindeeApi());
            var documentParser = new DocumentParser(null, receiptParsing, null);
            var prediction = await documentParser.WithReceiptType(GetFakeParseParameter());

            Assert.Equal(0.99, prediction.Inference.Pages.First().Prediction.Date.Confidence);
            Assert.Equal(0, prediction.Inference.Pages.First().Id);
            Assert.Equal("2017-04-12", prediction.Inference.Pages.First().Prediction.Date.Value);
        }

        [Fact]
        public async Task FromReceipt_WithReceiptData_MustSuccessForTime()
        {
            IReceiptParsing receiptParsing = new ReceiptParsing(GetMindeeApi());
            var documentParser = new DocumentParser(null, receiptParsing, null);
            var prediction = await documentParser.WithReceiptType(GetFakeParseParameter());

            Assert.Equal(0.99, prediction.Inference.Pages.First().Prediction.Time.Confidence);
            Assert.Equal(0, prediction.Inference.Pages.First().Id);
            Assert.Equal("07:21", prediction.Inference.Pages.First().Prediction.Time.Value);
            Assert.Equal(new List<List<double>>()
            {
                new List<double>() { 0.1048, 0.5534 },
                new List<double>() { 0.8827, 0.8493 },
                new List<double>() { 0.8356, 0.8054 },
                new List<double>() { 0.1461, 0.8072 },
            }
            , prediction.Inference.Pages.First().Prediction.Time.Polygon);
        }

        [Fact]
        public async Task FromReceipt_WithReceiptData_MustSuccessForLocale()
        {
            IReceiptParsing receiptParsing = new ReceiptParsing(GetMindeeApi());
            var documentParser = new DocumentParser(null, receiptParsing, null);
            var prediction = await documentParser.WithReceiptType(GetFakeParseParameter());

            Assert.Equal("fi", prediction.Inference.Pages.First().Prediction.Locale.Language);
            Assert.Equal("FI", prediction.Inference.Pages.First().Prediction.Locale.Country);
            Assert.Equal("EUR", prediction.Inference.Pages.First().Prediction.Locale.Currency);
        }

        [Fact]
        public async Task FromReceipt_WithReceiptData_MustSuccessForTotalTaxesIncluded()
        {
            IReceiptParsing receiptParsing = new ReceiptParsing(GetMindeeApi());
            var documentParser = new DocumentParser(null, receiptParsing, null);
            var prediction = await documentParser.WithReceiptType(GetFakeParseParameter());

            Assert.Equal(473.88, prediction.Inference.Pages.First().Prediction.TotalIncl.Value);
        }

        private ParseParameter GetFakeParseParameter()
        {
            return
                new ParseParameter(
                    new DocumentClient(
                        Stream.Null,
                        "Bou"));
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
