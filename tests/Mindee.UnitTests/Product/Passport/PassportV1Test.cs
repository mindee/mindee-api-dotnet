using Mindee.Parsing.Common;
using Mindee.Product.Passport;

namespace Mindee.UnitTests.Product.Passport
{
    [Trait("Category", "PassportV1")]
    public class PassportV1Test
    {
        [Fact]
        public async Task Predict_CheckEmpty()
        {
            var response = await GetPrediction("empty");
            Assert.Null(response.Document.Inference.Prediction.Country.Value);
            Assert.Null(response.Document.Inference.Prediction.IdNumber.Value);
            Assert.Empty(response.Document.Inference.Prediction.GivenNames);
            Assert.Null(response.Document.Inference.Prediction.Surname.Value);
            Assert.Null(response.Document.Inference.Prediction.BirthDate.Value);
            Assert.Null(response.Document.Inference.Prediction.BirthPlace.Value);
            Assert.Null(response.Document.Inference.Prediction.Gender.Value);
            Assert.Null(response.Document.Inference.Prediction.IssuanceDate.Value);
            Assert.Null(response.Document.Inference.Prediction.ExpiryDate.Value);
            Assert.Null(response.Document.Inference.Prediction.Mrz1.Value);
            Assert.Null(response.Document.Inference.Prediction.Mrz2.Value);
        }

        [Fact]
        public async Task Predict_CheckSummary()
        {
            var response = await GetPrediction("complete");
            var expected = File.ReadAllText("Resources/products/passport/response_v1/summary_full.rst");
            Assert.Equal(expected, response.Document.ToString());
        }

        [Fact]
        public async Task Predict_CheckPage0()
        {
            var response = await GetPrediction("complete");
            var expected = File.ReadAllText("Resources/products/passport/response_v1/summary_page0.rst");
            Assert.Equal(expected, response.Document.Inference.Pages[0].ToString());
        }

        private static async Task<PredictResponse<PassportV1>> GetPrediction(string name)
        {
            string fileName = $"Resources/products/passport/response_v1/{name}.json";
            var mindeeAPi = UnitTestBase.GetMindeeApi(fileName);
            return await mindeeAPi.PredictPostAsync<PassportV1>(
                UnitTestBase.GetFakePredictParameter());
        }
    }
}
