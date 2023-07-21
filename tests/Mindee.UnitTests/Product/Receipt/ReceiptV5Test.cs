using Mindee.Parsing.Common;
using Mindee.Product.Receipt;

namespace Mindee.UnitTests.Product.Receipt
{
    [Trait("Category", "ReceiptV5")]
    public class ReceiptV5Test
    {
        [Fact]
        public async Task Predict_CheckEmpty()
        {
            var response = await GetPrediction("empty");
            Assert.Null(response.Document.Inference.Prediction.Locale.Value);
            Assert.Null(response.Document.Inference.Prediction.Date.Value);
            Assert.Null(response.Document.Inference.Prediction.Time.Value);
            Assert.Null(response.Document.Inference.Prediction.TotalAmount.Value);
            Assert.Null(response.Document.Inference.Prediction.TotalNet.Value);
            Assert.Null(response.Document.Inference.Prediction.TotalTax.Value);
            Assert.Null(response.Document.Inference.Prediction.Tip.Value);
            Assert.Empty(response.Document.Inference.Prediction.Taxes);
            Assert.Null(response.Document.Inference.Prediction.SupplierName.Value);
            Assert.Empty(response.Document.Inference.Prediction.SupplierCompanyRegistrations);
            Assert.Null(response.Document.Inference.Prediction.SupplierAddress.Value);
            Assert.Null(response.Document.Inference.Prediction.SupplierPhoneNumber.Value);
            Assert.Empty(response.Document.Inference.Prediction.LineItems);
        }

        [Fact]
        public async Task Predict_CheckSummary()
        {
            var response = await GetPrediction("complete");
            var expected = File.ReadAllText("Resources/receipt/response_v5/summary_full.rst");
            Assert.Equal(expected, response.Document.ToString());
        }

        [Fact]
        public async Task Predict_CheckPage0()
        {
            var response = await GetPrediction("complete");
            var expected = File.ReadAllText("Resources/receipt/response_v5/summary_page0.rst");
            Assert.Equal(expected, response.Document.Inference.Pages[0].ToString());
            Assert.Equal(0, response.Document.Inference.Pages[0].Orientation.Value);
        }

        private static async Task<PredictResponse<ReceiptV5>> GetPrediction(string name)
        {
            string fileName = $"Resources/receipt/response_v5/{name}.json";
            var mindeeAPi = UnitTestBase.GetMindeeApi(fileName);
            return await mindeeAPi.PredictPostAsync<ReceiptV5>(
                UnitTestBase.GetFakePredictParameter());
        }
    }
}
