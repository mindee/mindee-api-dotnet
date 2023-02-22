using Mindee.Parsing.Common;
using Mindee.Parsing.Us.BankCheck;

namespace Mindee.UnitTests.Parsing.ShippingContainer
{
    [Trait("Category", "US Bank Check")]
    public class BankCheckV1Test
    {
        [Fact]
        public async Task Predict_CheckSummary()
        {
            var response = await GetPrediction();
            var expected = File.ReadAllText("Resources/us/bank_check/response_v1/summary_full.rst");
            Assert.Equal(
                expected,
                response.Document.ToString());
        }

        [Fact]
        public async Task Predict_CheckPage0()
        {
            var response = await GetPrediction();
            var expected = File.ReadAllText("Resources/us/bank_check/response_v1/summary_page0.rst");
            Assert.Equal(
                expected,
                response.Document.Inference.Pages.First().ToString());
        }

        private async Task<PredictResponse<BankCheckV1Inference>> GetPrediction()
        {
            const string fileName = "Resources/us/bank_check/response_v1/complete.json";
            var mindeeAPi = ParsingTestBase.GetMindeeApi(fileName);
            return await mindeeAPi.PredictPostAsync<BankCheckV1Inference>(ParsingTestBase.GetFakePredictParameter());
        }
    }
}
