using Mindee.Parsing.Common;
using Mindee.Product.Fr.IdCard;

namespace Mindee.UnitTests.Product.Fr.IdCard
{
    [Trait("Category", "IdCardV2")]
    public class IdCardV2Test
    {
        [Fact]
        public async Task Predict_CheckEmpty()
        {
            var response = await GetPrediction("empty");
            Assert.Null(response.Document.Inference.Prediction.Nationality.Value);
            Assert.Null(response.Document.Inference.Prediction.CardAccessNumber.Value);
            Assert.Null(response.Document.Inference.Prediction.DocumentNumber.Value);
            Assert.Empty(response.Document.Inference.Prediction.GivenNames);
            Assert.Null(response.Document.Inference.Prediction.Surname.Value);
            Assert.Null(response.Document.Inference.Prediction.AlternateName.Value);
            Assert.Null(response.Document.Inference.Prediction.BirthDate.Value);
            Assert.Null(response.Document.Inference.Prediction.BirthPlace.Value);
            Assert.Null(response.Document.Inference.Prediction.Gender.Value);
            Assert.Null(response.Document.Inference.Prediction.ExpiryDate.Value);
            Assert.Null(response.Document.Inference.Prediction.Mrz1.Value);
            Assert.Null(response.Document.Inference.Prediction.Mrz2.Value);
            Assert.Null(response.Document.Inference.Prediction.Mrz3.Value);
            Assert.Null(response.Document.Inference.Prediction.IssueDate.Value);
            Assert.Null(response.Document.Inference.Prediction.Authority.Value);
        }

        [Fact]
        public async Task Predict_CheckSummary()
        {
            var response = await GetPrediction("complete");
            var expected = File.ReadAllText("Resources/products/idcard_fr/response_v2/summary_full.rst");
            Assert.Equal(expected, response.Document.ToString());
        }

        [Fact]
        public async Task Predict_CheckPage0()
        {
            var response = await GetPrediction("complete");
            var expected = File.ReadAllText("Resources/products/idcard_fr/response_v2/summary_page0.rst");
            Assert.Equal(expected, response.Document.Inference.Pages[0].ToString());
        }

        private static async Task<PredictResponse<IdCardV2>> GetPrediction(string name)
        {
            string fileName = $"Resources/products/idcard_fr/response_v2/{name}.json";
            var mindeeAPi = UnitTestBase.GetMindeeApi(fileName);
            return await mindeeAPi.PredictPostAsync<IdCardV2>(
                UnitTestBase.GetFakePredictParameter());
        }
    }
}
