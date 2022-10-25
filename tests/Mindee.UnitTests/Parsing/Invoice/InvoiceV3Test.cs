using Mindee.Parsing;
using Mindee.Parsing.Invoice;

namespace Mindee.UnitTests.Parsing.Invoice
{
    public class InvoiceV3Test : ParsingTestBase
    {
        [Fact]
        [Trait("Category", "Invoice V3")]
        public async Task Predict_MustSuccess()
        {
            var mindeeAPi = GetMindeeApiForInvoice();
            var invoicePrediction = await mindeeAPi.PredictAsync<InvoiceV3Prediction>(GetFakePredictParameter());

            Assert.NotNull(invoicePrediction);
        }

        [Fact]
        [Trait("Category", "Invoice V3")]
        public async Task Predict_MustSuccessForCustomer()
        {
            var mindeeAPi = GetMindeeApiForInvoice();
            var invoicePrediction = await mindeeAPi.PredictAsync<InvoiceV3Prediction>(GetFakePredictParameter());

            Assert.Equal(0, invoicePrediction.Inference.Pages.First().Prediction.Customer.Confidence);
            Assert.Equal(0, invoicePrediction.Inference.Pages.First().Id);
            Assert.Equal(new List<List<double>>()
            , invoicePrediction.Inference.Pages.First().Prediction.Customer.Polygon);
            Assert.Null(invoicePrediction.Inference.Pages.First().Prediction.Customer.Value);
        }

        [Fact]
        [Trait("Category", "Invoice V3")]
        public async Task Predict_MustSuccessForDate()
        {
            var mindeeAPi = GetMindeeApiForInvoice();
            var invoicePrediction = await mindeeAPi.PredictAsync<InvoiceV3Prediction>(GetFakePredictParameter());

            Assert.Equal(0.99, invoicePrediction.Inference.Pages.First().Prediction.Date.Confidence);
            Assert.Equal(0, invoicePrediction.Inference.Pages.First().Id);
            Assert.Equal("2020-02-17", invoicePrediction.Inference.Pages.First().Prediction.Date.Value);
            Assert.Equal(new List<List<double>>()
            {
                new List<double>() { 0.382, 0.306 },
                new List<double>() { 0.44, 0.306 },
                new List<double>() { 0.44, 0.318 },
                new List<double>() { 0.382, 0.318 },
            }
            , invoicePrediction.Inference.Pages.First().Prediction.Date.Polygon);
        }

        [Fact]
        [Trait("Category", "Invoice V3")]
        public async Task Predict_MustSuccessForDocumentType()
        {
            var mindeeAPi = GetMindeeApiForInvoice();
            var invoicePrediction = await mindeeAPi.PredictAsync<InvoiceV3Prediction>(GetFakePredictParameter());

            Assert.Equal("INVOICE", invoicePrediction.Inference.Pages.First().Prediction.DocumentType.Value);
        }

        [Fact]
        [Trait("Category", "Invoice V3")]
        public async Task Predict_MustSuccessForLocale()
        {
            var mindeeAPi = GetMindeeApiForInvoice();
            var invoicePrediction = await mindeeAPi.PredictAsync<InvoiceV3Prediction>(GetFakePredictParameter());

            Assert.Equal("fr", invoicePrediction.Inference.Pages.First().Prediction.Locale.Language);
            Assert.Equal("EUR", invoicePrediction.Inference.Pages.First().Prediction.Locale.Currency);
        }

        [Fact]
        [Trait("Category", "Invoice V3")]
        public async Task Predict_MustSuccessForTotalTaxesIncluded()
        {
            var mindeeAPi = GetMindeeApiForInvoice();
            var invoicePrediction = await mindeeAPi.PredictAsync<InvoiceV3Prediction>(GetFakePredictParameter());

            Assert.Equal(587.95, invoicePrediction.Inference.Pages.First().Prediction.TotalIncl.Value);
        }

        [Fact]
        [Trait("Category", "Invoice V3")]
        public async Task Predict_MustSuccessForOrientation()
        {
            var mindeeAPi = GetMindeeApiForInvoice();
            var invoicePrediction = await mindeeAPi.PredictAsync<InvoiceV3Prediction>(GetFakePredictParameter());

            Assert.Equal(0, invoicePrediction.Inference.Pages.First().Orientation.Value);
        }

        [Fact]
        [Trait("Category", "Invoice V3")]
        public async Task Predict_MustSuccessForOcr()
        {
            var mindeeAPi = GetMindeeApiForInvoice("Resources/invoice/response_v3/complete_with_ocr.json");
            var parseParameter = new PredictParameter(
                        new byte[] { byte.MinValue },
                        "Bou",
                        true);
            var invoicePrediction = await mindeeAPi.PredictAsync<InvoiceV3Prediction>(parseParameter);

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

        private MindeeApi GetMindeeApiForInvoice(string fileName = "Resources/invoice/response_v3/complete.json")
        {
            return GetMindeeApi(fileName);

        }
    }
}
