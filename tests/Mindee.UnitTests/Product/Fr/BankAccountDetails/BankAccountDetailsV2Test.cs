using Mindee.Parsing.Common;
using Mindee.Product.Fr.BankAccountDetails;

namespace Mindee.UnitTests.Product.Fr.BankAccountDetails
{
    [Trait("Category", "BankAccountDetailsV2")]
    public class BankAccountDetailsV2Test
    {
        [Fact]
        public async Task Predict_CheckSummary()
        {
            var response = await GetPrediction();
            var expected = File.ReadAllText("Resources/fr/bank_account_details/response_v2/summary_full.rst");
            Assert.Equal(expected, response.Document.ToString());
        }

        [Fact]
        public async Task Predict_CheckPage0()
        {
            var response = await GetPrediction();
            var expected = File.ReadAllText("Resources/fr/bank_account_details/response_v2/summary_page0.rst");
            Assert.Equal(expected, response.Document.Inference.Pages[0].ToString());
            Assert.Equal(0, response.Document.Inference.Pages[0].Orientation.Value);
        }

        private static async Task<PredictResponse<BankAccountDetailsV2>> GetPrediction()
        {
            const string fileName = "Resources/fr/bank_account_details/response_v2/complete.json";
            var mindeeAPi = UnitTestBase.GetMindeeApi(fileName);
            return await mindeeAPi.PredictPostAsync<BankAccountDetailsV2>(
                UnitTestBase.GetFakePredictParameter());
        }
    }
}
