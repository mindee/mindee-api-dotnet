using Mindee.Parsing;
using Mindee.Parsing.Us.BankCheck;

namespace Mindee.UnitTests.Parsing.ShippingContainer
{
    [Trait("Category", "US Bank Check")]
    public class BankCheckV1Test
    {
        [Fact]
        public async Task Predict_CheckSummary()
        {
            var mindeeAPi = GetMindeeApiForCarteVitale();
            var prediction = await mindeeAPi.PredictAsync<BankCheckV1Inference>(ParsingTestBase.GetFakePredictParameter());

            var expected = File.ReadAllText("Resources/us/bank_check/response_v1/summary_full.rst");

            Assert.Equal(
                expected,
                prediction.ToString());
        }

        private MindeeApi GetMindeeApiForCarteVitale(string fileName = "Resources/us/bank_check/response_v1/complete.json")
        {
            return ParsingTestBase.GetMindeeApi(fileName);
        }
    }
}
