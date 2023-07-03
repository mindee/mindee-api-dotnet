using Mindee.Parsing.Common;
using Mindee.Parsing.Fr.BankAccountDetails;

namespace Mindee.UnitTests.Parsing.Fr.BankAccountDetails
{
    [Trait("Category", "BankAccountDetailsV1")]
    public class BankAccountDetailsV1Test
    {
        [Fact]
        public async Task Predict_CheckSummary()
        {
            var response = await GetPrediction();
            var expected = File.ReadAllText("Resources/fr/bank_account_details/response_v1/summary_full.rst");
            Assert.Equal(expected, response.Document.ToString());
        }

        [Fact]
        public async Task Predict_CheckPage0()
        {
            var response = await GetPrediction();
            var expected = File.ReadAllText("Resources/fr/bank_account_details/response_v1/summary_page0.rst");
            Assert.Equal(expected, response.Document.Inference.Pages[0].ToString());
        }

        private async Task<PredictResponse<BankAccountDetailsV1Inference>> GetPrediction()
        {
            const string fileName = "Resources/fr/bank_account_details/response_v1/complete.json";
            var mindeeAPi = ParsingTestBase.GetMindeeApi(fileName);
            return await mindeeAPi.PredictAsync<BankAccountDetailsV1Inference>(ParsingTestBase.GetFakePredictParameter());
        }
    }
}
