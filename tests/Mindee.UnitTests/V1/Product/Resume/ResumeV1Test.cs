using Mindee.Parsing.Common;
using Mindee.Product.Resume;

namespace Mindee.UnitTests.V1.Product.Resume
{
    [Trait("Category", "ResumeV1")]
    public class ResumeV1Test
    {
        [Fact]
        public async Task Predict_CheckEmpty()
        {
            var response = await GetPrediction("empty");
            var docPrediction = response.Document.Inference.Prediction;
            Assert.Null(docPrediction.DocumentLanguage.Value);
            Assert.IsType<Mindee.Parsing.Standard.ClassificationField>(docPrediction.DocumentType);
            Assert.Empty(docPrediction.GivenNames);
            Assert.Empty(docPrediction.Surnames);
            Assert.Null(docPrediction.Nationality.Value);
            Assert.Null(docPrediction.EmailAddress.Value);
            Assert.Null(docPrediction.PhoneNumber.Value);
            Assert.Null(docPrediction.Address.Value);
            Assert.Empty(docPrediction.SocialNetworksUrls);
            Assert.Null(docPrediction.Profession.Value);
            Assert.Null(docPrediction.JobApplied.Value);
            Assert.Empty(docPrediction.Languages);
            Assert.Empty(docPrediction.HardSkills);
            Assert.Empty(docPrediction.SoftSkills);
            Assert.Empty(docPrediction.Education);
            Assert.Empty(docPrediction.ProfessionalExperiences);
            Assert.Empty(docPrediction.Certificates);
        }

        [Fact]
        public async Task Predict_CheckSummary()
        {
            var response = await GetPrediction("complete");
            var expected = File.ReadAllText("Resources/v1/products/resume/response_v1/summary_full.rst");
            Assert.Equal(expected, response.Document.ToString());
        }

        private static async Task<PredictResponse<ResumeV1>> GetPrediction(string name)
        {
            string fileName = $"Resources/v1/products/resume/response_v1/{name}.json";
            var mindeeAPi = UnitTestBase.GetMindeeApi(fileName);
            return await mindeeAPi.PredictPostAsync<ResumeV1>(
                UnitTestBase.GetFakePredictParameter());
        }
    }
}
