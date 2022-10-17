using Mindee.Parsing;
using Mindee.Parsing.Invoice;

namespace Mindee.UnitTests.Parsing.Prediction
{
    public class InvoiceParsingTest : ParsingTestBase
    {
        [Fact]
        public async Task Execute_WithInvoicePdf_MustSuccess()
        {
            var mindeeAPi = GetMindeeApiForInvoice();
            var invoicePrediction = await mindeeAPi.PredictAsync<InvoicePrediction>(GetFakePredictParameter());

            Assert.NotNull(invoicePrediction);
        }

        [Fact]
        public async Task Execute_WithInvoicePdf_MustSuccessForCustomer()
        {
            var mindeeAPi = GetMindeeApiForInvoice();
            var invoicePrediction = await mindeeAPi.PredictAsync<InvoicePrediction>(GetFakePredictParameter());

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
            var mindeeAPi = GetMindeeApiForInvoice();
            var invoicePrediction = await mindeeAPi.PredictAsync<InvoicePrediction>(GetFakePredictParameter());

            Assert.Equal(0.99, invoicePrediction.Inference.Pages.First().Prediction.Date.Confidence);
            Assert.Equal(0, invoicePrediction.Inference.Pages.First().Id);
            Assert.Equal("2016-01-25", invoicePrediction.Inference.Pages.First().Prediction.Date.Value);
        }

        [Fact]
        public async Task Execute_WithInvoicePdf_MustSuccessForDocumentType()
        {
            var mindeeAPi = GetMindeeApiForInvoice();
            var invoicePrediction = await mindeeAPi.PredictAsync<InvoicePrediction>(GetFakePredictParameter());

            Assert.Equal("INVOICE", invoicePrediction.Inference.Pages.First().Prediction.DocumentType.Value);
        }

        [Fact]
        public async Task Execute_WithInvoicePdf_MustSuccessForLocale()
        {
            var mindeeAPi = GetMindeeApiForInvoice();
            var invoicePrediction = await mindeeAPi.PredictAsync<InvoicePrediction>(GetFakePredictParameter());

            Assert.Equal("en", invoicePrediction.Inference.Pages.First().Prediction.Locale.Language);
            Assert.Equal("USD", invoicePrediction.Inference.Pages.First().Prediction.Locale.Currency);
        }

        [Fact]
        public async Task Execute_WithInvoicePdf_MustSuccessForTotalTaxesIncluded()
        {
            var mindeeAPi = GetMindeeApiForInvoice();
            var invoicePrediction = await mindeeAPi.PredictAsync<InvoicePrediction>(GetFakePredictParameter());

            Assert.Equal(93.5, invoicePrediction.Inference.Pages.First().Prediction.TotalIncl.Value);
        }

        [Fact]
        public async Task Execute_WithInvoicePdf_MustSuccessForOrientation()
        {
            var mindeeAPi = GetMindeeApiForInvoice();
            var invoicePrediction = await mindeeAPi.PredictAsync<InvoicePrediction>(GetFakePredictParameter());

            Assert.Equal(90, invoicePrediction.Inference.Pages.First().Orientation.Value);
        }

        [Fact]
        public async Task Execute_WithInvoiceDataWithOcrAsked_MustSuccessForOcr()
        {
            var mindeeAPi = GetMindeeApiForInvoice("Resources/inv2 - withFullText.json");
            var parseParameter = new PredictParameter(
                        new byte[] { byte.MinValue },
                        "Bou",
                        true);
            var invoicePrediction = await mindeeAPi.PredictAsync<InvoicePrediction>(parseParameter);

            Assert.Equal(0.92, invoicePrediction.Ocr.MvisionV1.Pages.First().AllWords.First().Confidence);
            Assert.Equal(new List<List<double>>()
            {
                new List<double>() { 0.635, 0.924 },
                new List<double>() { 0.705, 0.924 },
                new List<double>() { 0.705, 0.936 },
                new List<double>() { 0.635, 0.936 },
            }
            , invoicePrediction.Ocr.MvisionV1.Pages.First().AllWords.First().Polygon);
            Assert.Equal("Payment", invoicePrediction.Ocr.MvisionV1.Pages.First().AllWords.First().Text);
        }

        private MindeeApi GetMindeeApiForInvoice(string fileName = "Resources/inv2.json")
        {
            return GetMindeeApi(fileName);

        }
    }
}
