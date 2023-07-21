using Mindee.Parsing.Common;
using Mindee.Product.Fr.IdCard;

namespace Mindee.UnitTests.Product.Fr.IdCard
{
    [Trait("Category", "IdCardV1")]
    public class IdCardV1Test
    {
        [Fact]
        public async Task Predict_CheckEmpty()
        {
            var response = await GetPrediction("empty");
            Assert.Null(response.Document.Inference.Prediction.IdNumber.Value);
            Assert.Empty(response.Document.Inference.Prediction.GivenNames);
            Assert.Null(response.Document.Inference.Prediction.Surname.Value);
            Assert.Null(response.Document.Inference.Prediction.BirthDate.Value);
            Assert.Null(response.Document.Inference.Prediction.BirthPlace.Value);
            Assert.Null(response.Document.Inference.Prediction.ExpiryDate.Value);
            Assert.Null(response.Document.Inference.Prediction.Authority.Value);
            Assert.Null(response.Document.Inference.Prediction.Gender.Value);
            Assert.Null(response.Document.Inference.Prediction.Mrz1.Value);
            Assert.Null(response.Document.Inference.Prediction.Mrz2.Value);
        }

        [Fact]
        public async Task Predict_CheckSummary()
        {
            var response = await GetPrediction("complete");
            var expected = File.ReadAllText("Resources/fr/id_card/response_v1/summary_full.rst");
            Assert.Equal(expected, response.Document.ToString());
        }

        [Fact]
        public async Task Predict_CheckPage0()
        {
            var response = await GetPrediction("complete");
            var expected = File.ReadAllText("Resources/fr/id_card/response_v1/summary_page0.rst");
            Assert.Equal(expected, response.Document.Inference.Pages[0].ToString());
            Assert.Equal(0, response.Document.Inference.Pages[0].Orientation.Value);
        }

        private static async Task<PredictResponse<IdCardV1>> GetPrediction(string name)
        {
            string fileName = $"Resources/fr/id_card/response_v1/{name}.json";
            var mindeeAPi = UnitTestBase.GetMindeeApi(fileName);
            return await mindeeAPi.PredictPostAsync<IdCardV1>(
                UnitTestBase.GetFakePredictParameter());
        }
    }
}
