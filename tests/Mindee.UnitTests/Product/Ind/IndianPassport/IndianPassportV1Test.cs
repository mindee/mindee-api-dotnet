using Mindee.Parsing.Common;
using Mindee.Product.Ind.IndianPassport;

namespace Mindee.UnitTests.Product.Ind.IndianPassport
{
    [Trait("Category", "IndianPassportV1")]
    public class IndianPassportV1Test
    {
        [Fact]
        public async Task Predict_CheckEmpty()
        {
            var response = await GetPrediction("empty");
            var docPrediction = response.Document.Inference.Prediction;
            Assert.IsType<Mindee.Parsing.Standard.ClassificationField>(docPrediction.PageNumber);
            Assert.Null(docPrediction.Country.Value);
            Assert.Null(docPrediction.IdNumber.Value);
            Assert.Null(docPrediction.GivenNames.Value);
            Assert.Null(docPrediction.Surname.Value);
            Assert.Null(docPrediction.BirthDate.Value);
            Assert.Null(docPrediction.BirthPlace.Value);
            Assert.Null(docPrediction.IssuancePlace.Value);
            Assert.IsType<Mindee.Parsing.Standard.ClassificationField>(docPrediction.Gender);
            Assert.Null(docPrediction.IssuanceDate.Value);
            Assert.Null(docPrediction.ExpiryDate.Value);
            Assert.Null(docPrediction.Mrz1.Value);
            Assert.Null(docPrediction.Mrz2.Value);
            Assert.Null(docPrediction.LegalGuardian.Value);
            Assert.Null(docPrediction.NameOfSpouse.Value);
            Assert.Null(docPrediction.NameOfMother.Value);
            Assert.Null(docPrediction.OldPassportDateOfIssue.Value);
            Assert.Null(docPrediction.OldPassportNumber.Value);
            Assert.Null(docPrediction.OldPassportPlaceOfIssue.Value);
            Assert.Null(docPrediction.Address1.Value);
            Assert.Null(docPrediction.Address2.Value);
            Assert.Null(docPrediction.Address3.Value);
            Assert.Null(docPrediction.FileNumber.Value);
        }

        [Fact]
        public async Task Predict_CheckSummary()
        {
            var response = await GetPrediction("complete");
            var expected = File.ReadAllText("Resources/products/ind_passport/response_v1/summary_full.rst");
            Assert.Equal(expected, response.Document.ToString());
        }

        private static async Task<PredictResponse<IndianPassportV1>> GetPrediction(string name)
        {
            string fileName = $"Resources/products/ind_passport/response_v1/{name}.json";
            var mindeeAPi = UnitTestBase.GetMindeeApi(fileName);
            return await mindeeAPi.PredictPostAsync<IndianPassportV1>(
                UnitTestBase.GetFakePredictParameter());
        }
    }
}
