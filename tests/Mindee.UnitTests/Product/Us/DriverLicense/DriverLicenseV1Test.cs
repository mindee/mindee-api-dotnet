using Mindee.Parsing.Common;
using Mindee.Product.Us.DriverLicense;

namespace Mindee.UnitTests.Product.Us.DriverLicense
{
    [Trait("Category", "DriverLicenseV1")]
    public class DriverLicenseV1Test
    {
        [Fact]
        public async Task Predict_CheckEmpty()
        {
            var response = await GetPrediction("empty");
            var docPrediction = response.Document.Inference.Prediction;
            Assert.Null(docPrediction.State.Value);
            Assert.Null(docPrediction.DriverLicenseId.Value);
            Assert.Null(docPrediction.ExpiryDate.Value);
            Assert.Null(docPrediction.IssuedDate.Value);
            Assert.Null(docPrediction.LastName.Value);
            Assert.Null(docPrediction.FirstName.Value);
            Assert.Null(docPrediction.Address.Value);
            Assert.Null(docPrediction.DateOfBirth.Value);
            Assert.Null(docPrediction.Restrictions.Value);
            Assert.Null(docPrediction.Endorsements.Value);
            Assert.Null(docPrediction.DlClass.Value);
            Assert.Null(docPrediction.Sex.Value);
            Assert.Null(docPrediction.Height.Value);
            Assert.Null(docPrediction.Weight.Value);
            Assert.Null(docPrediction.HairColor.Value);
            Assert.Null(docPrediction.EyeColor.Value);
            Assert.Null(docPrediction.DdNumber.Value);
            var pagePrediction = response.Document.Inference.Pages.First().Prediction;
            Assert.Empty(pagePrediction.Photo.Polygon);
            Assert.Empty(pagePrediction.Signature.Polygon);
        }

        [Fact]
        public async Task Predict_CheckSummary()
        {
            var response = await GetPrediction("complete");
            var expected = File.ReadAllText("Resources/products/us_driver_license/response_v1/summary_full.rst");
            Assert.Equal(expected, response.Document.ToString());
        }

        [Fact]
        public async Task Predict_CheckPage0()
        {
            var response = await GetPrediction("complete");
            var expected = File.ReadAllText("Resources/products/us_driver_license/response_v1/summary_page0.rst");
            Assert.Equal(expected, response.Document.Inference.Pages[0].ToString());
        }

        private static async Task<PredictResponse<DriverLicenseV1>> GetPrediction(string name)
        {
            string fileName = $"Resources/products/us_driver_license/response_v1/{name}.json";
            var mindeeAPi = UnitTestBase.GetMindeeApi(fileName);
            return await mindeeAPi.PredictPostAsync<DriverLicenseV1>(
                UnitTestBase.GetFakePredictParameter());
        }
    }
}
