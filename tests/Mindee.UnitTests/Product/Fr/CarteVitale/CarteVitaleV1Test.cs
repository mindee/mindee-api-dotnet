using Mindee.Parsing.Common;
using Mindee.Product.Fr.CarteVitale;

namespace Mindee.UnitTests.Product.Fr.CarteVitale
{
    [Trait("Category", "CarteVitaleV1")]
    public class CarteVitaleV1Test
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
            var expected = File.ReadAllText("Resources/products/carte_vitale/response_v1/summary_full.rst");
            Assert.Equal(expected, response.Document.ToString());
        }

        private static async Task<PredictResponse<CarteVitaleV1>> GetPrediction(string name)
        {
            string fileName = $"Resources/products/carte_vitale/response_v1/{name}.json";
            var mindeeAPi = UnitTestBase.GetMindeeApi(fileName);
            return await mindeeAPi.PredictPostAsync<CarteVitaleV1>(
                UnitTestBase.GetFakePredictParameter());
        }
    }
}
