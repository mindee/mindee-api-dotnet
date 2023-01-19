using Mindee.Parsing;
using Mindee.Parsing.ShippingContainer;

namespace Mindee.UnitTests.Parsing.ShippingContainer
{
    [Trait("Category", "Shipping Container")]
    public class ShippingContainerV1Test
    {
        [Fact]
        public async Task Predict_CheckSummary()
        {
            var mindeeAPi = GetMindeeApiForCarteVitale();
            var prediction = await mindeeAPi.PredictAsync<ShippingContainerV1Inference>(ParsingTestBase.GetFakePredictParameter());

            var expected = File.ReadAllText("Resources/shipping_container/response_v1/summary_full.rst");

            Assert.Equal(
                expected,
                prediction.ToString());
        }

        private MindeeApi GetMindeeApiForCarteVitale(string fileName = "Resources/shipping_container/response_v1/complete.json")
        {
            return ParsingTestBase.GetMindeeApi(fileName);
        }
    }
}
