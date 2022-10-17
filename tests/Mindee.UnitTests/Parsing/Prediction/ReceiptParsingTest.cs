using Mindee.Parsing;
using Mindee.Parsing.Receipt;

namespace Mindee.UnitTests.Parsing.Prediction
{
    public class ReceiptParsingTest : ParsingTestBase
    {
        [Fact]
        public async Task Execute_WithReceiptData_MustSuccess()
        {
            var mindeeAPi = GetMindeeApiForReceipt();
            var prediction = await mindeeAPi.PredictAsync<ReceiptPrediction>(GetFakePredictParameter());

            Assert.NotNull(prediction);
        }

        [Fact]
        public async Task Execute_WithReceiptData_MustSuccessForCategory()
        {
            var mindeeAPi = GetMindeeApiForReceipt();
            var prediction = await mindeeAPi.PredictAsync<ReceiptPrediction>(GetFakePredictParameter());

            Assert.Equal(0.99, prediction.Inference.Pages.First().Prediction.Category.Confidence);
            Assert.Equal("transport", prediction.Inference.Pages.First().Prediction.Category.Value);
        }

        [Fact]
        public async Task Execute_WithReceiptData_MustSuccessForDate()
        {
            var mindeeAPi = GetMindeeApiForReceipt();
            var prediction = await mindeeAPi.PredictAsync<ReceiptPrediction>(GetFakePredictParameter());

            Assert.Equal(0.99, prediction.Inference.Pages.First().Prediction.Date.Confidence);
            Assert.Equal(0, prediction.Inference.Pages.First().Id);
            Assert.Equal("2017-04-12", prediction.Inference.Pages.First().Prediction.Date.Value);
        }

        [Fact]
        public async Task Execute_WithReceiptData_MustSuccessForTime()
        {
            var mindeeAPi = GetMindeeApiForReceipt();
            var prediction = await mindeeAPi.PredictAsync<ReceiptPrediction>(GetFakePredictParameter());

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
        public async Task Execute_WithReceiptData_MustSuccessForLocale()
        {
            var mindeeAPi = GetMindeeApiForReceipt();
            var prediction = await mindeeAPi.PredictAsync<ReceiptPrediction>(GetFakePredictParameter());

            Assert.Equal("fi", prediction.Inference.Pages.First().Prediction.Locale.Language);
            Assert.Equal("FI", prediction.Inference.Pages.First().Prediction.Locale.Country);
            Assert.Equal("EUR", prediction.Inference.Pages.First().Prediction.Locale.Currency);
        }

        [Fact]
        public async Task Execute_WithReceiptData_MustSuccessForTotalTaxesIncluded()
        {
            var mindeeAPi = GetMindeeApiForReceipt();
            var prediction = await mindeeAPi.PredictAsync<ReceiptPrediction>(GetFakePredictParameter());

            Assert.Equal(473.88, prediction.Inference.Pages.First().Prediction.TotalIncl.Value);
        }

        [Fact]
        public async Task Execute_WithReceiptData_MustSuccessForOrientation()
        {
            var mindeeAPi = GetMindeeApiForReceipt();
            var prediction = await mindeeAPi.PredictAsync<ReceiptPrediction>(GetFakePredictParameter());

            Assert.Equal(0, prediction.Inference.Pages.First().Orientation.Value);
        }

        private MindeeApi GetMindeeApiForReceipt(string fileName = "Resources/receipt_response_full_v3.json")
        {
            return GetMindeeApi(fileName);
        }
    }
}
