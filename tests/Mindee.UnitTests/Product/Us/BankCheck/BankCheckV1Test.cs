using Mindee.Parsing.Common;
using Mindee.Product.Us.BankCheck;

namespace Mindee.UnitTests.Product.Us.BankCheck
{
    [Trait("Category", "BankCheckV1")]
    public class BankCheckV1Test
    {
        [Fact]
        public async Task Predict_CheckEmpty()
        {
            var response = await GetPrediction("empty");
            var docPrediction = response.Document.Inference.Prediction;
            Assert.Null(docPrediction.Date.Value);
            Assert.Null(docPrediction.Amount.Value);
            Assert.Empty(docPrediction.Payees);
            Assert.Null(docPrediction.RoutingNumber.Value);
            Assert.Null(docPrediction.AccountNumber.Value);
            Assert.Null(docPrediction.CheckNumber.Value);
            var pagePrediction = response.Document.Inference.Pages.First().Prediction;
            Assert.Empty(pagePrediction.CheckPosition.Polygon);
            Assert.Empty(pagePrediction.SignaturesPositions);
        }

        [Fact]
        public async Task Predict_CheckSummary()
        {
            var response = await GetPrediction("complete");
            var expected = File.ReadAllText("Resources/products/bank_check/response_v1/summary_full.rst");
            Assert.Equal(expected, response.Document.ToString());
        }

        [Fact]
        public async Task Predict_CheckPage0()
        {
            var response = await GetPrediction("complete");
            var expected = File.ReadAllText("Resources/products/bank_check/response_v1/summary_page0.rst");
            Assert.Equal(expected, response.Document.Inference.Pages[0].ToString());
        }

        private static async Task<PredictResponse<BankCheckV1>> GetPrediction(string name)
        {
            string fileName = $"Resources/products/bank_check/response_v1/{name}.json";
            var mindeeAPi = UnitTestBase.GetMindeeApi(fileName);
            return await mindeeAPi.PredictPostAsync<BankCheckV1>(
                UnitTestBase.GetFakePredictParameter());
        }
    }
}
