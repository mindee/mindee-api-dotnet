using Mindee.Parsing;
using Mindee.Parsing.Fr.IdCard;

namespace Mindee.UnitTests.Parsing.Fr.IdCard
{
    [Trait("Category", "FR Id Card V1")]
    public class IdCardV1Test
    {
        [Fact]
        public async Task Predict_CheckSummary()
        {
            var mindeeAPi = GetMindeeApiForFrIdCard();
            var prediction = await mindeeAPi.PredictAsync<IdCardV1Inference>(ParsingTestBase.GetFakePredictParameter());

            var expected = File.ReadAllText("Resources/fr/id_card/response_v1/summary_full.rst");

            Assert.Equal(
                expected,
                prediction.ToString());
        }

        [Fact]
        public async Task Predict_CheckSummary_WithMultiplePages()
        {
            var mindeeAPi = GetMindeeApiForFrIdCard();
            var prediction = await mindeeAPi.PredictAsync<IdCardV1Inference>(ParsingTestBase.GetFakePredictParameter());

            var expected = File.ReadAllText("Resources/fr/id_card/response_v1/summary_page0.rst");

            Assert.Equal(
                expected,
                prediction.Inference.Pages.First().ToString());
        }

        private MindeeApi GetMindeeApiForFrIdCard(string fileName = "Resources/fr/id_card/response_v1/complete.json")
        {
            return ParsingTestBase.GetMindeeApi(fileName);
        }
    }
}
