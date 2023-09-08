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
            var docPrediction = response.Document.Inference.Prediction;
            Assert.Null(docPrediction.Locale.Value);
            Assert.NotNull(docPrediction.Category.Value);
            Assert.NotNull(docPrediction.Subcategory.Value);
            Assert.NotNull(docPrediction.DocumentType.Value);
            Assert.Null(docPrediction.Date.Value);
            Assert.Null(docPrediction.Time.Value);
            Assert.Null(docPrediction.TotalAmount.Value);
            Assert.Null(docPrediction.TotalNet.Value);
            Assert.Null(docPrediction.TotalTax.Value);
            Assert.Null(docPrediction.Tip.Value);
            Assert.Empty(docPrediction.Taxes);
            Assert.Null(docPrediction.SupplierName.Value);
            Assert.Empty(docPrediction.SupplierCompanyRegistrations);
            Assert.Null(docPrediction.SupplierAddress.Value);
            Assert.Null(docPrediction.SupplierPhoneNumber.Value);
            Assert.Empty(docPrediction.LineItems);
        }

        [Fact]
        public async Task Predict_CheckSummary()
        {
            var response = await GetPrediction("complete");
            var expected = File.ReadAllText("Resources/products/expense_receipts/response_v5/summary_full.rst");
            Assert.Equal(expected, response.Document.ToString());
        }

        [Fact]
        public async Task Predict_CheckPage0()
        {
            var response = await GetPrediction("complete");
            var expected = File.ReadAllText("Resources/products/expense_receipts/response_v5/summary_page0.rst");
            Assert.Equal(expected, response.Document.Inference.Pages[0].ToString());
        }

        private static async Task<PredictResponse<ReceiptV5>> GetPrediction(string name)
        {
            string fileName = $"Resources/products/expense_receipts/response_v5/{name}.json";
            var mindeeAPi = UnitTestBase.GetMindeeApi(fileName);
            return await mindeeAPi.PredictPostAsync<ReceiptV5>(
                UnitTestBase.GetFakePredictParameter());
        }
    }
}
