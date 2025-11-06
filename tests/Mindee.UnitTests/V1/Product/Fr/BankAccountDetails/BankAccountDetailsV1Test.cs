using Mindee.Parsing.Common;
using Mindee.Product.Fr.BankAccountDetails;

namespace Mindee.UnitTests.V1.Product.Fr.BankAccountDetails
{
    [Trait("Category", "BankAccountDetailsV1")]
    public class BankAccountDetailsV1Test
    {
        [Fact]
        public async Task Predict_CheckEmpty()
        {
            var response = await GetPrediction("empty");
            var docPrediction = response.Document.Inference.Prediction;
            Assert.Null(docPrediction.Iban.Value);
            Assert.Null(docPrediction.AccountHolderName.Value);
            Assert.Null(docPrediction.Swift.Value);
        }

        [Fact]
        public async Task Predict_CheckSummary()
        {
            var response = await GetPrediction("complete");
            var expected = File.ReadAllText(Constants.V1ProductDir + "bank_account_details/response_v1/summary_full.rst");
            Assert.Equal(expected, response.Document.ToString());
        }

        private static async Task<PredictResponse<BankAccountDetailsV1>> GetPrediction(string name)
        {
            string fileName = Constants.V1RootDir + $"products/bank_account_details/response_v1/{name}.json";
            var mindeeAPi = UnitTestBase.GetMindeeApi(fileName);
            return await mindeeAPi.PredictPostAsync<BankAccountDetailsV1>(
                UnitTestBase.GetFakePredictParameter());
        }
    }
}
