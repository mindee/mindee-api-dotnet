using Mindee.Parsing.Common;
using Mindee.Product.DeliveryNote;

namespace Mindee.UnitTests.V1.Product.DeliveryNote
{
    [Trait("Category", "DeliveryNoteV1")]
    public class DeliveryNoteV1Test
    {
        [Fact]
        public async Task Predict_CheckEmpty()
        {
            var response = await GetPrediction("empty");
            var docPrediction = response.Document.Inference.Prediction;
            Assert.Null(docPrediction.DeliveryDate.Value);
            Assert.Null(docPrediction.DeliveryNumber.Value);
            Assert.Null(docPrediction.SupplierName.Value);
            Assert.Null(docPrediction.SupplierAddress.Value);
            Assert.Null(docPrediction.CustomerName.Value);
            Assert.Null(docPrediction.CustomerAddress.Value);
            Assert.Null(docPrediction.TotalAmount.Value);
        }

        [Fact]
        public async Task Predict_CheckSummary()
        {
            var response = await GetPrediction("complete");
            var expected = File.ReadAllText("Resources/v1/products/delivery_notes/response_v1/summary_full.rst");
            Assert.Equal(expected, response.Document.ToString());
        }

        private static async Task<PredictResponse<DeliveryNoteV1>> GetPrediction(string name)
        {
            string fileName = $"Resources/v1/products/delivery_notes/response_v1/{name}.json";
            var mindeeAPi = UnitTestBase.GetMindeeApi(fileName);
            return await mindeeAPi.PredictPostAsync<DeliveryNoteV1>(
                UnitTestBase.GetFakePredictParameter());
        }
    }
}
