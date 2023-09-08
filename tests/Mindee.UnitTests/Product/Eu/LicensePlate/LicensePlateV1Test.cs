using Mindee.Parsing.Common;
using Mindee.Product.Eu.LicensePlate;

namespace Mindee.UnitTests.Product.Eu.LicensePlate
{
    [Trait("Category", "LicensePlateV1")]
    public class LicensePlateV1Test
    {
        [Fact]
        public async Task Predict_CheckEmpty()
        {
            var response = await GetPrediction("empty");
            var docPrediction = response.Document.Inference.Prediction;
            Assert.Empty(docPrediction.LicensePlates);
        }

        [Fact]
        public async Task Predict_CheckSummary()
        {
            var response = await GetPrediction("complete");
            var expected = File.ReadAllText("Resources/products/license_plates/response_v1/summary_full.rst");
            Assert.Equal(expected, response.Document.ToString());
        }

        [Fact]
        public async Task Predict_CheckPage0()
        {
            var response = await GetPrediction("complete");
            var expected = File.ReadAllText("Resources/products/license_plates/response_v1/summary_page0.rst");
            Assert.Equal(expected, response.Document.Inference.Pages[0].ToString());
        }

        private static async Task<PredictResponse<LicensePlateV1>> GetPrediction(string name)
        {
            string fileName = $"Resources/products/license_plates/response_v1/{name}.json";
            var mindeeAPi = UnitTestBase.GetMindeeApi(fileName);
            return await mindeeAPi.PredictPostAsync<LicensePlateV1>(
                UnitTestBase.GetFakePredictParameter());
        }
    }
}
