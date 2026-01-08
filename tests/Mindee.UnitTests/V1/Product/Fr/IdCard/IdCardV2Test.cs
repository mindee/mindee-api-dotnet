using Mindee.Parsing.Common;
using Mindee.Parsing.Standard;
using Mindee.Product.Fr.IdCard;

namespace Mindee.UnitTests.V1.Product.Fr.IdCard
{
    [Trait("Category", "IdCardV2")]
    public class IdCardV2Test
    {
        [Fact]
        public async Task Predict_CheckEmpty()
        {
            var response = await GetPrediction("empty");
            var docPrediction = response.Document.Inference.Prediction;
            Assert.Null(docPrediction.Nationality.Value);
            Assert.Null(docPrediction.CardAccessNumber.Value);
            Assert.Null(docPrediction.DocumentNumber.Value);
            Assert.Empty(docPrediction.GivenNames);
            Assert.Null(docPrediction.Surname.Value);
            Assert.Null(docPrediction.AlternateName.Value);
            Assert.Null(docPrediction.BirthDate.Value);
            Assert.Null(docPrediction.BirthPlace.Value);
            Assert.Null(docPrediction.Gender.Value);
            Assert.Null(docPrediction.ExpiryDate.Value);
            Assert.Null(docPrediction.Mrz1.Value);
            Assert.Null(docPrediction.Mrz2.Value);
            Assert.Null(docPrediction.Mrz3.Value);
            Assert.Null(docPrediction.IssueDate.Value);
            Assert.Null(docPrediction.Authority.Value);
            var pagePrediction = response.Document.Inference.Pages.First().Prediction;
            Assert.IsType<ClassificationField>(pagePrediction.DocumentType);
            Assert.IsType<ClassificationField>(pagePrediction.DocumentSide);
        }

        [Fact]
        public async Task Predict_CheckSummary()
        {
            var response = await GetPrediction("complete");
            var expected = File.ReadAllText(Constants.V1ProductDir + "idcard_fr/response_v2/summary_full.rst");
            Assert.Equal(expected, response.Document.ToString());
        }

        [Fact]
        public async Task Predict_CheckPage0()
        {
            var response = await GetPrediction("complete");
            var expected = File.ReadAllText(Constants.V1ProductDir + "idcard_fr/response_v2/summary_page0.rst");
            Assert.Equal(expected, response.Document.Inference.Pages[0].ToString());
        }

        private static async Task<PredictResponse<IdCardV2>> GetPrediction(string name)
        {
            var fileName = Constants.V1RootDir + $"products/idcard_fr/response_v2/{name}.json";
            var mindeeAPi = UnitTestBase.GetMindeeApi(fileName);
            return await mindeeAPi.PredictPostAsync<IdCardV2>(
                UnitTestBase.GetFakePredictParameter());
        }
    }
}
