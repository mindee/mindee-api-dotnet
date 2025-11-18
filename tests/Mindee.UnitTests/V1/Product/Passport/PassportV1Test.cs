using Mindee.Parsing.Common;
using Mindee.Product.Passport;

namespace Mindee.UnitTests.V1.Product.Passport
{
    [Trait("Category", "PassportV1")]
    public class PassportV1Test
    {
        [Fact]
        public async Task Predict_CheckEmpty()
        {
            var response = await GetPrediction("empty");
            var docPrediction = response.Document.Inference.Prediction;
            Assert.Null(docPrediction.Country.Value);
            Assert.Null(docPrediction.IdNumber.Value);
            Assert.Empty(docPrediction.GivenNames);
            Assert.Null(docPrediction.Surname.Value);
            Assert.Null(docPrediction.BirthDate.Value);
            Assert.Null(docPrediction.BirthPlace.Value);
            Assert.Null(docPrediction.Gender.Value);
            Assert.Null(docPrediction.IssuanceDate.Value);
            Assert.Null(docPrediction.ExpiryDate.Value);
            Assert.Null(docPrediction.Mrz1.Value);
            Assert.Null(docPrediction.Mrz2.Value);
        }

        [Fact]
        public async Task Predict_CheckSummary()
        {
            var response = await GetPrediction("complete");
            var expected = File.ReadAllText(Constants.V1ProductDir + "passport/response_v1/summary_full.rst");
            Assert.Equal(expected, response.Document.ToString());
        }

        private static async Task<PredictResponse<PassportV1>> GetPrediction(string name)
        {
            string fileName = Constants.V1RootDir + $"products/passport/response_v1/{name}.json";
            var mindeeAPi = UnitTestBase.GetMindeeApi(fileName);
            return await mindeeAPi.PredictPostAsync<PassportV1>(
                UnitTestBase.GetFakePredictParameter());
        }
    }
}
