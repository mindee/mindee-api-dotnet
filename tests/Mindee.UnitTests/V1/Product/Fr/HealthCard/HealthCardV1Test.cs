using Mindee.Parsing.Common;
using Mindee.Product.Fr.HealthCard;

namespace Mindee.UnitTests.V1.Product.Fr.HealthCard
{
    [Trait("Category", "HealthCardV1")]
    public class HealthCardV1Test
    {
        [Fact]
        public async Task Predict_CheckEmpty()
        {
            var response = await GetPrediction("empty");
            var docPrediction = response.Document.Inference.Prediction;
            Assert.Empty(docPrediction.GivenNames);
            Assert.Null(docPrediction.Surname.Value);
            Assert.Null(docPrediction.SocialSecurity.Value);
            Assert.Null(docPrediction.IssuanceDate.Value);
        }

        [Fact]
        public async Task Predict_CheckSummary()
        {
            var response = await GetPrediction("complete");
            var expected = File.ReadAllText("Resources/v1/products/french_healthcard/response_v1/summary_full.rst");
            Assert.Equal(expected, response.Document.ToString());
        }

        private static async Task<PredictResponse<HealthCardV1>> GetPrediction(string name)
        {
            string fileName = $"Resources/v1/products/french_healthcard/response_v1/{name}.json";
            var mindeeAPi = UnitTestBase.GetMindeeApi(fileName);
            return await mindeeAPi.PredictPostAsync<HealthCardV1>(
                UnitTestBase.GetFakePredictParameter());
        }
    }
}
