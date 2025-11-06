using Mindee.Parsing.Common;
using Mindee.Product.Receipt;

namespace Mindee.UnitTests.V1.Product.Receipt
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
            Assert.IsType<Mindee.Parsing.Standard.ClassificationField>(docPrediction.Category);
            Assert.IsType<Mindee.Parsing.Standard.ClassificationField>(docPrediction.Subcategory);
            Assert.IsType<Mindee.Parsing.Standard.ClassificationField>(docPrediction.DocumentType);
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
            Assert.Null(docPrediction.ReceiptNumber.Value);
            Assert.Empty(docPrediction.LineItems);
        }

        [Fact]
        public async Task Predict_CheckSummary()
        {
            var response = await GetPrediction("complete");
            var expected = File.ReadAllText("Resources/v1/products/expense_receipts/response_v5/summary_full.rst");
            Assert.Equal(expected, response.Document.ToString());
        }

        private static async Task<PredictResponse<ReceiptV5>> GetPrediction(string name)
        {
            string fileName = $"Resources/v1/products/expense_receipts/response_v5/{name}.json";
            var mindeeAPi = UnitTestBase.GetMindeeApi(fileName);
            return await mindeeAPi.PredictPostAsync<ReceiptV5>(
                UnitTestBase.GetFakePredictParameter());
        }
    }
}
