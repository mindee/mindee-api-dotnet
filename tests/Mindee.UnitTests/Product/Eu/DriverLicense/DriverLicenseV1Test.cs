using Mindee.Parsing.Common;
using Mindee.Product.Eu.DriverLicense;

namespace Mindee.UnitTests.Product.Eu.DriverLicense
{
    [Trait("Category", "DriverLicenseV1")]
    public class DriverLicenseV1Test
    {
        [Fact]
        public async Task Predict_CheckEmpty()
        {
            var response = await GetPrediction("empty");
            var docPrediction = response.Document.Inference.Prediction;
            Assert.Null(docPrediction.CountryCode.Value);
            Assert.Null(docPrediction.DocumentId.Value);
            Assert.Null(docPrediction.Category.Value);
            Assert.Null(docPrediction.LastName.Value);
            Assert.Null(docPrediction.FirstName.Value);
            Assert.Null(docPrediction.DateOfBirth.Value);
            Assert.Null(docPrediction.PlaceOfBirth.Value);
            Assert.Null(docPrediction.ExpiryDate.Value);
            Assert.Null(docPrediction.IssueDate.Value);
            Assert.Null(docPrediction.IssueAuthority.Value);
            Assert.Null(docPrediction.Mrz.Value);
            Assert.Null(docPrediction.Address.Value);
            var pagePrediction = response.Document.Inference.Pages.First().Prediction;
            Assert.Null(pagePrediction.Photo.Polygon);
            Assert.Null(pagePrediction.Photo.BoundingBox);
            Assert.Null(pagePrediction.Signature.Polygon);
            Assert.Null(pagePrediction.Signature.BoundingBox);
        }

        [Fact]
        public async Task Predict_CheckSummary()
        {
            var response = await GetPrediction("complete");
            var expected = File.ReadAllText("Resources/products/eu_driver_license/response_v1/summary_full.rst");
            Assert.Equal(expected, response.Document.ToString());
        }
        [Fact]
        public async Task Predict_CheckPage0()
        {
            var response = await GetPrediction("complete");
            var expected = File.ReadAllText("Resources/products/eu_driver_license/response_v1/summary_page0.rst");
            Assert.Equal(expected, response.Document.Inference.Pages[0].ToString());
        }

        private static async Task<PredictResponse<DriverLicenseV1>> GetPrediction(string name)
        {
            string fileName = $"Resources/products/eu_driver_license/response_v1/{name}.json";
            var mindeeAPi = UnitTestBase.GetMindeeApi(fileName);
            return await mindeeAPi.PredictPostAsync<DriverLicenseV1>(
                UnitTestBase.GetFakePredictParameter());
        }
    }
}
