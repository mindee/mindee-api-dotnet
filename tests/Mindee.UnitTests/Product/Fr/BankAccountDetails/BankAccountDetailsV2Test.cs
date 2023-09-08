using Mindee.Parsing.Common;
using Mindee.Product.Fr.BankAccountDetails;

namespace Mindee.UnitTests.Product.Fr.BankAccountDetails
{
    [Trait("Category", "BankAccountDetailsV2")]
    public class BankAccountDetailsV2Test
    {
        [Fact]
        public async Task Predict_CheckEmpty()
        {
            var response = await GetPrediction("empty");
            var docPrediction = response.Document.Inference.Prediction;
            Assert.Null(docPrediction.AccountHoldersNames.Value);
            Assert.Null(docPrediction.Bban.BbanBankCode);
            Assert.Null(docPrediction.Bban.BbanBranchCode);
            Assert.Null(docPrediction.Bban.BbanKey);
            Assert.Null(docPrediction.Bban.BbanNumber);
            Assert.Null(docPrediction.Iban.Value);
            Assert.Null(docPrediction.SwiftCode.Value);
        }

        [Fact]
        public async Task Predict_CheckSummary()
        {
            var response = await GetPrediction("complete");
            var expected = File.ReadAllText("Resources/products/bank_account_details/response_v2/summary_full.rst");
            Assert.Equal(expected, response.Document.ToString());
        }

        [Fact]
        public async Task Predict_CheckPage0()
        {
            var response = await GetPrediction("complete");
            var expected = File.ReadAllText("Resources/products/bank_account_details/response_v2/summary_page0.rst");
            Assert.Equal(expected, response.Document.Inference.Pages[0].ToString());
        }

        private static async Task<PredictResponse<BankAccountDetailsV2>> GetPrediction(string name)
        {
            string fileName = $"Resources/products/bank_account_details/response_v2/{name}.json";
            var mindeeAPi = UnitTestBase.GetMindeeApi(fileName);
            return await mindeeAPi.PredictPostAsync<BankAccountDetailsV2>(
                UnitTestBase.GetFakePredictParameter());
        }
    }
}
