using Mindee.Parsing;
using Mindee.Parsing.Common;
using Mindee.Parsing.ShippingContainer;

namespace Mindee.UnitTests.Parsing.ShippingContainer
{
    [Trait("Category", "ShippingContainerV1")]
    public class ShippingContainerV1Test
    {
        [Fact]
        public async Task Predict_CheckSummary()
        {
            var prediction = await GetPrediction();
            var expected = File.ReadAllText("Resources/shipping_container/response_v1/summary_full.rst");
            Assert.Equal(expected, prediction.ToString());
        }

        [Fact]
        public async Task Predict_CheckPage0()
        {
            var prediction = await GetPrediction();
            var expected = File.ReadAllText("Resources/shipping_container/response_v1/summary_page0.rst");
            Assert.Equal(expected, prediction.Inference.Pages[0].ToString());
        }

        private async Task<Document<ShippingContainerV1Inference>> GetPrediction()
        {
            string fileName = "Resources/shipping_container/response_v1/complete.json";
            var mindeeAPi = ParsingTestBase.GetMindeeApi(fileName);
            return await mindeeAPi.PredictAsync<ShippingContainerV1Inference>(ParsingTestBase.GetFakePredictParameter());
        }
    }
}
