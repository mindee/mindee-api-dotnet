using Mindee.Parsing.Common;
using Mindee.Product.Receipt;

namespace Mindee.UnitTests.Parsing.Receipt
{
    [Trait("Category", "Receipt V4")]
    public class ReceiptV4Test
    {
        [Fact]
        public async Task Predict_CheckSummary()
        {
            var response = await GetPrediction();
            var expected = File.ReadAllText("Resources/receipt/response_v4/summary_full.rst");
            Assert.Equal(
                expected,
                response.Document.ToString());
        }

        [Fact]
        public async Task Predict_CheckSummary_WithMultiplePages()
        {
            var response = await GetPrediction();
            var expected = File.ReadAllText("Resources/receipt/response_v4/summary_page0.rst");
            Assert.Equal(
                expected,
                response.Document.Inference.Pages.First().ToString());
        }

        [Fact]
        public async Task Predict_MustSuccessForCategory()
        {
            var response = await GetPrediction();

            Assert.Equal(0.94, response.Document.Inference.Pages.First().Prediction.Category.Confidence);
            Assert.Equal("food", response.Document.Inference.Pages.First().Prediction.Category.Value);
        }

        [Fact]
        public async Task Predict_MustSuccessForDate()
        {
            var response = await GetPrediction();

            Assert.Equal(0.99, response.Document.Inference.Pages.First().Prediction.Date.Confidence);
            Assert.Equal(0, response.Document.Inference.Pages.First().Id);
            Assert.Equal("2014-07-07", response.Document.Inference.Pages.First().Prediction.Date.Value);
        }

        [Fact]
        public async Task Predict_MustSuccessForTime()
        {
            var response = await GetPrediction();

            Assert.Equal(0.99, response.Document.Inference.Pages.First().Prediction.Time.Confidence);
            Assert.Equal(0, response.Document.Inference.Pages.First().Id);
            Assert.Equal("20:20", response.Document.Inference.Pages.First().Prediction.Time.Value);
            Assert.Equal(new List<List<double>>()
            {
                new List<double>() { 0.635, 0.142 },
                new List<double>() { 0.778, 0.142 },
                new List<double>() { 0.778, 0.168 },
                new List<double>() { 0.635, 0.168 },
            }
            , response.Document.Inference.Pages.First().Prediction.Time.Polygon);
        }

        [Fact]
        public async Task Predict_WithReceiptData_MustSuccessForOrientation()
        {
            var response = await GetPrediction();
            Assert.Equal(0, response.Document.Inference.Pages.First().Orientation.Value);
        }

        [Fact]
        public async Task Predict_WithCropping_MustSuccess()
        {
            const string fileName = "Resources/receipt/response_v4/complete_with_cropper.json";
            var mindeeAPi = ParsingTestBase.GetMindeeApi(fileName);
            var response = await mindeeAPi.PredictPostAsync<ReceiptV4>(ParsingTestBase.GetFakePredictParameter());

            Assert.NotNull(response.Document.Inference.Pages.First().Extras.Cropper);
            Assert.Single(response.Document.Inference.Pages.First().Extras.Cropper.Cropping);
            Assert.Equal(new List<List<double>>()
            {
                new List<double>() { 0.057, 0.008 },
                new List<double>() { 0.846, 0.008 },
                new List<double>() { 0.846, 1.0 },
                new List<double>() { 0.057, 1.0 },
            }
            , response.Document.Inference.Pages.First().Extras.Cropper.Cropping.First().BoundingBox);

            Assert.Equal(new List<List<double>>()
            {
                new List<double>() { 0.161, 0.016 },
                new List<double>() { 0.744, 0.009 },
                new List<double>() { 0.845, 0.996 },
                new List<double>() { 0.057, 0.998 },
            }
            , response.Document.Inference.Pages.First().Extras.Cropper.Cropping.First().Quadrangle);

            Assert.Equal(new List<List<double>>()
            {
                new List<double>() { 0.052, 0.011 },
                new List<double>() { 0.839, 0.007 },
                new List<double>() { 0.844, 0.994 },
                new List<double>() { 0.057, 0.998 },
            }
            , response.Document.Inference.Pages.First().Extras.Cropper.Cropping.First().Rectangle);

            Assert.Equal(new List<List<double>>()
            {
                new List<double>() { 0.127, 0.344 },
                new List<double>() { 0.152, 0.16 },
                new List<double>() { 0.156, 0.053 },
                new List<double>() { 0.18, 0.004 },
                new List<double>() { 0.191, 0.004 },
                new List<double>() { 0.215, 0.016 },
                new List<double>() { 0.426, 0.006 },
                new List<double>() { 0.484, 0.018 },
                new List<double>() { 0.686, 0.01 },
                new List<double>() { 0.725, 0.016 },
                new List<double>() { 0.744, 0.045 },
                new List<double>() { 0.773, 0.242 },
                new List<double>() { 0.775, 0.318 },
                new List<double>() { 0.789, 0.436 },
                new List<double>() { 0.801, 0.473 },
                new List<double>() { 0.807, 0.662 },
                new List<double>() { 0.822, 0.719 },
                new List<double>() { 0.842, 0.936 },
                new List<double>() { 0.836, 0.996 },
                new List<double>() { 0.061, 0.996 },
                new List<double>() { 0.055, 0.975 },
                new List<double>() { 0.07, 0.828 },
                new List<double>() { 0.086, 0.732 },
                new List<double>() { 0.113, 0.514 },
            }
            , response.Document.Inference.Pages.First().Extras.Cropper.Cropping.First().Polygon);
        }

        private static async Task<PredictResponse<ReceiptV4>> GetPrediction()
        {
            const string fileName = "Resources/receipt/response_v4/complete.json";
            var mindeeAPi = ParsingTestBase.GetMindeeApi(fileName);
            return await mindeeAPi.PredictPostAsync<ReceiptV4>(ParsingTestBase.GetFakePredictParameter());
        }
    }
}
