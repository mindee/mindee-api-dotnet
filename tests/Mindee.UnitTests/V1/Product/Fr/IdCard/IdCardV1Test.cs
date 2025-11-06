using Mindee.Parsing.Common;
using Mindee.Product.Fr.IdCard;

namespace Mindee.UnitTests.V1.Product.Fr.IdCard
{
    [Trait("Category", "IdCardV1")]
    public class IdCardV1Test
    {
        [Fact]
        public async Task Predict_CheckEmpty()
        {
            var response = await GetPrediction("empty");
            var docPrediction = response.Document.Inference.Prediction;
            Assert.Null(docPrediction.IdNumber.Value);
            Assert.Empty(docPrediction.GivenNames);
            Assert.Null(docPrediction.Surname.Value);
            Assert.Null(docPrediction.BirthDate.Value);
            Assert.Null(docPrediction.BirthPlace.Value);
            Assert.Null(docPrediction.ExpiryDate.Value);
            Assert.Null(docPrediction.Authority.Value);
            Assert.Null(docPrediction.Gender.Value);
            Assert.Null(docPrediction.Mrz1.Value);
            Assert.Null(docPrediction.Mrz2.Value);
            var pagePrediction = response.Document.Inference.Pages.First().Prediction;
            Assert.IsType<Mindee.Parsing.Standard.ClassificationField>(pagePrediction.DocumentSide);
        }

        [Fact]
        public async Task Predict_CheckSummary()
        {
            var response = await GetPrediction("complete");
            var expected = File.ReadAllText(Constants.V1ProductDir + "idcard_fr/response_v1/summary_full.rst");
            Assert.Equal(expected, response.Document.ToString());
        }
        [Fact]
        public async Task Predict_CheckPage0()
        {
            var response = await GetPrediction("complete");
            var expected = File.ReadAllText(Constants.V1ProductDir + "idcard_fr/response_v1/summary_page0.rst");
            Assert.Equal(expected, response.Document.Inference.Pages[0].ToString());
        }

        private static async Task<PredictResponse<IdCardV1>> GetPrediction(string name)
        {
            string fileName = Constants.V1RootDir + $"products/idcard_fr/response_v1/{name}.json";
            var mindeeAPi = UnitTestBase.GetMindeeApi(fileName);
            return await mindeeAPi.PredictPostAsync<IdCardV1>(
                UnitTestBase.GetFakePredictParameter());
        }
    }
}
