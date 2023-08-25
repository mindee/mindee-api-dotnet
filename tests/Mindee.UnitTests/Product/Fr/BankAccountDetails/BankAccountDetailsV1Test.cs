using Mindee.Parsing.Common;
using Mindee.Product.Fr.BankAccountDetails;

namespace Mindee.UnitTests.Product.Fr.BankAccountDetails
{
    [Trait("Category", "BankAccountDetailsV1")]
    public class BankAccountDetailsV1Test
    {
        [Fact]
        public async Task Predict_CheckEmpty()
        {
            var response = await GetPrediction("empty");
            Assert.Null(response.Document.Inference.Prediction.Iban.Value);
            Assert.Null(response.Document.Inference.Prediction.AccountHolderName.Value);
            Assert.Null(response.Document.Inference.Prediction.Swift.Value);
        }

        [Fact]
        public async Task Predict_CheckSummary()
        {
            var response = await GetPrediction("complete");
            var expected = File.ReadAllText("Resources/products/bank_account_details/response_v1/summary_full.rst");
            Assert.Equal(expected, response.Document.ToString());
        }

        [Fact]
        public async Task Predict_CheckPage0()
        {
            var response = await GetPrediction("complete");
            var expected = File.ReadAllText("Resources/products/bank_account_details/response_v1/summary_page0.rst");
            Assert.Equal(expected, response.Document.Inference.Pages[0].ToString());
        }

        private static async Task<PredictResponse<BankAccountDetailsV1>> GetPrediction(string name)
        {
            string fileName = $"Resources/products/bank_account_details/response_v1/{name}.json";
            var mindeeAPi = UnitTestBase.GetMindeeApi(fileName);
            return await mindeeAPi.PredictPostAsync<BankAccountDetailsV1>(
                UnitTestBase.GetFakePredictParameter());
        }
    }
}
