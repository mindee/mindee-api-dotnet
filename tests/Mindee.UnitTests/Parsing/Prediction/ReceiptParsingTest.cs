using Mindee.Parsing;
using Mindee.Parsing.Receipt;

namespace Mindee.UnitTests.Parsing.Prediction
{
    public class ReceiptParsingTest : ParsingTestBase
    {
        [Fact]
        public async Task Predict_MustSuccess()
        {
            var mindeeAPi = GetMindeeApiForReceipt();
            var prediction = await mindeeAPi.PredictAsync<ReceiptPrediction>(GetFakePredictParameter());

            Assert.NotNull(prediction);
        }

        [Fact]
        public async Task Predict_MustSuccessForCategory()
        {
            var mindeeAPi = GetMindeeApiForReceipt();
            var prediction = await mindeeAPi.PredictAsync<ReceiptPrediction>(GetFakePredictParameter());

            Assert.Equal(0.99, prediction.Inference.Pages.First().Prediction.Category.Confidence);
            Assert.Equal("food", prediction.Inference.Pages.First().Prediction.Category.Value);
        }

        [Fact]
        public async Task Predict_MustSuccessForDate()
        {
            var mindeeAPi = GetMindeeApiForReceipt();
            var prediction = await mindeeAPi.PredictAsync<ReceiptPrediction>(GetFakePredictParameter());

            Assert.Equal(0.99, prediction.Inference.Pages.First().Prediction.Date.Confidence);
            Assert.Equal(0, prediction.Inference.Pages.First().Id);
            Assert.Equal("2016-02-26", prediction.Inference.Pages.First().Prediction.Date.Value);
        }

        [Fact]
        public async Task Predict_MustSuccessForTime()
        {
            var mindeeAPi = GetMindeeApiForReceipt();
            var prediction = await mindeeAPi.PredictAsync<ReceiptPrediction>(GetFakePredictParameter());

            Assert.Equal(0.99, prediction.Inference.Pages.First().Prediction.Time.Confidence);
            Assert.Equal(0, prediction.Inference.Pages.First().Id);
            Assert.Equal("15:20", prediction.Inference.Pages.First().Prediction.Time.Value);
            Assert.Equal(new List<List<double>>()
            {
                new List<double>() { 0.62, 0.173 },
                new List<double>() { 0.681, 0.173 },
                new List<double>() { 0.681, 0.191 },
                new List<double>() { 0.62, 0.191 },
            }
            , prediction.Inference.Pages.First().Prediction.Time.Polygon);
        }

        [Fact]
        public async Task Predict_WithReceiptData_MustSuccessForLocale()
        {
            var mindeeAPi = GetMindeeApiForReceipt();
            var prediction = await mindeeAPi.PredictAsync<ReceiptPrediction>(GetFakePredictParameter());

            Assert.Equal("en", prediction.Inference.Pages.First().Prediction.Locale.Language);
            Assert.Equal("GB", prediction.Inference.Pages.First().Prediction.Locale.Country);
            Assert.Equal("GBP", prediction.Inference.Pages.First().Prediction.Locale.Currency);
        }

        [Fact]
        public async Task Predict_WithReceiptData_MustSuccessForTotalTaxesIncluded()
        {
            var mindeeAPi = GetMindeeApiForReceipt();
            var prediction = await mindeeAPi.PredictAsync<ReceiptPrediction>(GetFakePredictParameter());

            Assert.Equal(10.2, prediction.Inference.Pages.First().Prediction.TotalIncl.Value);
        }

        [Fact]
        public async Task Predict_WithReceiptData_MustSuccessForOrientation()
        {
            var mindeeAPi = GetMindeeApiForReceipt();
            var prediction = await mindeeAPi.PredictAsync<ReceiptPrediction>(GetFakePredictParameter());

            Assert.Equal(0, prediction.Inference.Pages.First().Orientation.Value);
        }

        private MindeeApi GetMindeeApiForReceipt(string fileName = "Resources/receipt/response/complete.json")
        {
            return GetMindeeApi(fileName);
        }
    }
}
