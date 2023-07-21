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
            Assert.Empty(response.Document.Inference.Prediction.GivenNames);
            Assert.Null(response.Document.Inference.Prediction.Surname.Value);
            Assert.Null(response.Document.Inference.Prediction.SocialSecurity.Value);
            Assert.Null(response.Document.Inference.Prediction.IssuanceDate.Value);
        }

        [Fact]
        public async Task Predict_CheckSummary()
        {
            var response = await GetPrediction("complete");
            var expected = File.ReadAllText("Resources/fr/carte_vitale/response_v1/summary_full.rst");
            Assert.Equal(expected, response.Document.ToString());
        }

        [Fact]
        public async Task Predict_CheckPage0()
        {
            var response = await GetPrediction("complete");
            var expected = File.ReadAllText("Resources/fr/carte_vitale/response_v1/summary_page0.rst");
            Assert.Equal(expected, response.Document.Inference.Pages[0].ToString());
            Assert.Equal(0, response.Document.Inference.Pages[0].Orientation.Value);
        }

        private static async Task<PredictResponse<CarteVitaleV1>> GetPrediction(string name)
        {
            string fileName = $"Resources/fr/carte_vitale/response_v1/{name}.json";
            var mindeeAPi = UnitTestBase.GetMindeeApi(fileName);
            return await mindeeAPi.PredictPostAsync<CarteVitaleV1>(
                UnitTestBase.GetFakePredictParameter());
        }
    }
}
