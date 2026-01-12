using Mindee.Parsing.Common;
using Mindee.Product.BarcodeReader;

namespace Mindee.UnitTests.V1.Product.BarcodeReader
{
    [Trait("Category", "BarcodeReaderV1")]
    public class BarcodeReaderV1Test
    {
        [Fact]
        public async Task Predict_CheckEmpty()
        {
            var response = await GetPrediction("empty");
            var docPrediction = response.Document.Inference.Prediction;
            Assert.Empty(docPrediction.Codes1D);
            Assert.Empty(docPrediction.Codes2D);
        }

        [Fact]
        public async Task Predict_CheckSummary()
        {
            var response = await GetPrediction("complete");
            var expected = File.ReadAllText(Constants.V1ProductDir + "barcode_reader/response_v1/summary_full.rst");
            Assert.Equal(expected, response.Document.ToString());
        }

        private static async Task<PredictResponse<BarcodeReaderV1>> GetPrediction(string name)
        {
            var fileName = Constants.V1RootDir + $"products/barcode_reader/response_v1/{name}.json";
            var mindeeAPi = UnitTestBase.GetMindeeApi(fileName);
            return await mindeeAPi.PredictPostAsync<BarcodeReaderV1>(
                UnitTestBase.GetFakePredictParameter());
        }
    }
}
