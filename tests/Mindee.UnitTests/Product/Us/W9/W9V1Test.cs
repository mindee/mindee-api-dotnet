using Mindee.Parsing.Common;
using Mindee.Product.Us.W9;

namespace Mindee.UnitTests.Product.Us.W9
{
    [Trait("Category", "W9V1")]
    public class W9V1Test
    {
        [Fact]
        public async Task Predict_CheckEmpty()
        {
            var response = await GetPrediction("empty");
            var pagePrediction = response.Document.Inference.Pages.First().Prediction;
            Assert.Null(pagePrediction.Name.Value);
            Assert.Null(pagePrediction.Ssn.Value);
            Assert.Null(pagePrediction.Address.Value);
            Assert.Null(pagePrediction.CityStateZip.Value);
            Assert.Null(pagePrediction.BusinessName.Value);
            Assert.Null(pagePrediction.Ein.Value);
            Assert.Null(pagePrediction.TaxClassification.Value);
            Assert.Null(pagePrediction.TaxClassificationOtherDetails.Value);
            Assert.Null(pagePrediction.W9RevisionDate.Value);
            Assert.Null(pagePrediction.SignaturePosition.Polygon);
            Assert.Null(pagePrediction.SignaturePosition.BoundingBox);
            Assert.Null(pagePrediction.SignatureDatePosition.Polygon);
            Assert.Null(pagePrediction.SignatureDatePosition.BoundingBox);
            Assert.Null(pagePrediction.TaxClassificationLlc.Value);
        }

        [Fact]
        public async Task Predict_CheckSummary()
        {
            var response = await GetPrediction("complete");
            var expected = File.ReadAllText("Resources/products/us_w9/response_v1/summary_full.rst");
            Assert.Equal(expected, response.Document.ToString());
        }

        [Fact]
        public async Task Predict_CheckPage0()
        {
            var response = await GetPrediction("complete");
            var expected = File.ReadAllText("Resources/products/us_w9/response_v1/summary_page0.rst");
            Assert.Equal(expected, response.Document.Inference.Pages[0].ToString());
        }

        private static async Task<PredictResponse<W9V1>> GetPrediction(string name)
        {
            string fileName = $"Resources/products/us_w9/response_v1/{name}.json";
            var mindeeAPi = UnitTestBase.GetMindeeApi(fileName);
            return await mindeeAPi.PredictPostAsync<W9V1>(
                UnitTestBase.GetFakePredictParameter());
        }
    }
}
