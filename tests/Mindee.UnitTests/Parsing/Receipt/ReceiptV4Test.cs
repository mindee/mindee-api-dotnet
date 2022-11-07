using Mindee.Parsing;
using Mindee.Parsing.Receipt;

namespace Mindee.UnitTests.Parsing.Receipt
{
    public class ReceiptV4Test
    {
        [Fact]
        [Trait("Category", "Receipt V4")]
        public async Task Predict_MustSuccess()
        {
            var mindeeAPi = GetMindeeApiForReceipt();
            var prediction = await mindeeAPi.PredictAsync<ReceiptV4Prediction>(ParsingTestBase.GetFakePredictParameter());

            Assert.NotNull(prediction);
        }

        [Fact]
        [Trait("Category", "Receipt V4")]
        public async Task Predict_MustSuccessForCategory()
        {
            var mindeeAPi = GetMindeeApiForReceipt();
            var prediction = await mindeeAPi.PredictAsync<ReceiptV4Prediction>(ParsingTestBase.GetFakePredictParameter());

            Assert.Equal(0.94, prediction.Inference.Pages.First().Prediction.Category.Confidence);
            Assert.Equal("food", prediction.Inference.Pages.First().Prediction.Category.Value);
        }

        [Fact]
        [Trait("Category", "Receipt V4")]
        public async Task Predict_MustSuccessForDate()
        {
            var mindeeAPi = GetMindeeApiForReceipt();
            var prediction = await mindeeAPi.PredictAsync<ReceiptV4Prediction>(ParsingTestBase.GetFakePredictParameter());

            Assert.Equal(0.99, prediction.Inference.Pages.First().Prediction.Date.Confidence);
            Assert.Equal(0, prediction.Inference.Pages.First().Id);
            Assert.Equal("2014-07-07", prediction.Inference.Pages.First().Prediction.Date.Value);
        }

        [Fact]
        [Trait("Category", "Receipt V4")]
        public async Task Predict_MustSuccessForTime()
        {
            var mindeeAPi = GetMindeeApiForReceipt();
            var prediction = await mindeeAPi.PredictAsync<ReceiptV4Prediction>(ParsingTestBase.GetFakePredictParameter());

            Assert.Equal(0.99, prediction.Inference.Pages.First().Prediction.Time.Confidence);
            Assert.Equal(0, prediction.Inference.Pages.First().Id);
            Assert.Equal("20:20", prediction.Inference.Pages.First().Prediction.Time.Value);
            Assert.Equal(new List<List<double>>()
            {
                new List<double>() { 0.635, 0.142 },
                new List<double>() { 0.778, 0.142 },
                new List<double>() { 0.778, 0.168 },
                new List<double>() { 0.635, 0.168 },
            }
            , prediction.Inference.Pages.First().Prediction.Time.Polygon);
        }

        [Fact]
        [Trait("Category", "Receipt V4")]
        public async Task Predict_WithReceiptData_MustSuccessForLocale()
        {
            var mindeeAPi = GetMindeeApiForReceipt();
            var prediction = await mindeeAPi.PredictAsync<ReceiptV4Prediction>(ParsingTestBase.GetFakePredictParameter());

            Assert.Equal("en", prediction.Inference.Pages.First().Prediction.Locale.Language);
            Assert.Equal("US", prediction.Inference.Pages.First().Prediction.Locale.Country);
            Assert.Equal("USD", prediction.Inference.Pages.First().Prediction.Locale.Currency);
        }

        [Fact]
        [Trait("Category", "Receipt V4")]
        public async Task Predict_WithReceiptData_MustSuccessForTotalTaxes()
        {
            var mindeeAPi = GetMindeeApiForReceipt();
            var prediction = await mindeeAPi.PredictAsync<ReceiptV4Prediction>(ParsingTestBase.GetFakePredictParameter());

            Assert.Equal(3.34, prediction.Inference.Pages.First().Prediction.TotalTax.Value);
        }

        [Fact]
        [Trait("Category", "Receipt V4")]
        public async Task Predict_WithReceiptData_MustSuccessForTip()
        {
            var mindeeAPi = GetMindeeApiForReceipt();
            var prediction = await mindeeAPi.PredictAsync<ReceiptV4Prediction>(ParsingTestBase.GetFakePredictParameter());

            Assert.Equal(10.0, prediction.Inference.Pages.First().Prediction.Tip.Value);
        }

        [Fact]
        [Trait("Category", "Receipt V4")]
        public async Task Predict_WithReceiptData_MustSuccessForNetTotal()
        {
            var mindeeAPi = GetMindeeApiForReceipt();
            var prediction = await mindeeAPi.PredictAsync<ReceiptV4Prediction>(ParsingTestBase.GetFakePredictParameter());

            Assert.Equal(40.48, prediction.Inference.Pages.First().Prediction.TotalNet.Value);
        }

        [Fact]
        [Trait("Category", "Receipt V4")]
        public async Task Predict_WithReceiptData_MustSuccessForTotal()
        {
            var mindeeAPi = GetMindeeApiForReceipt();
            var prediction = await mindeeAPi.PredictAsync<ReceiptV4Prediction>(ParsingTestBase.GetFakePredictParameter());

            Assert.Equal(53.82, prediction.Inference.Pages.First().Prediction.TotalAmount.Value);
        }

        [Fact]
        [Trait("Category", "Receipt V4")]
        public async Task Predict_WithReceiptData_MustSuccessForOrientation()
        {
            var mindeeAPi = GetMindeeApiForReceipt();
            var prediction = await mindeeAPi.PredictAsync<ReceiptV4Prediction>(ParsingTestBase.GetFakePredictParameter());

            Assert.Equal(0, prediction.Inference.Pages.First().Orientation.Value);
        }

        public async Task Predict_WithReceiptData_MustSuccessForTaxes()
        {
            var mindeeAPi = GetMindeeApiForReceipt();
            var prediction = await mindeeAPi.PredictAsync<ReceiptV4Prediction>(ParsingTestBase.GetFakePredictParameter());

            Assert.Null(prediction.Inference.Pages.First().Prediction.Taxes.First().Base);
            Assert.Null(prediction.Inference.Pages.First().Prediction.Taxes.First().Rate);
            Assert.Equal(3.34, prediction.Inference.Pages.First().Prediction.Taxes.First().Value);
        }

        private MindeeApi GetMindeeApiForReceipt(string fileName = "Resources/receipt/response_v4/complete-with-tip.json")
        {
            return ParsingTestBase.GetMindeeApi(fileName);
        }
    }
}
