using Mindee.Parsing.Common;
using Mindee.Parsing.Invoice;

namespace Mindee.UnitTests.Parsing.Invoice
{
    [Trait("Category", "Invoice V4")]
    public class InvoiceV4Test
    {
        [Fact]
        public async Task Predict_CheckSummary()
        {
            var response = await GetPrediction();
            var expected = File.ReadAllText("Resources/invoice/response_v4/summary_full.rst");
            Assert.Equal(
                expected,
                response.Document.ToString());
        }

        [Fact]
        public async Task Predict_CheckSummary_WithMultiplePages()
        {
            var response = await GetPrediction();
            var expected = File.ReadAllText("Resources/invoice/response_v4/summary_page0.rst");
            Assert.Equal(
                expected,
                response.Document.Inference.Pages.First().ToString());
        }

        [Fact]
        public async Task Predict_MustSuccessForOrientation()
        {
            var response = await GetPrediction();
            Assert.Equal(0, response.Document.Inference.Pages.First().Orientation.Value);
        }

        private async Task<PredictResponse<InvoiceV4Inference>> GetPrediction()
        {
            const string fileName = "Resources/invoice/response_v4/complete.json";
            var mindeeAPi = ParsingTestBase.GetMindeeApi(fileName);
            return await mindeeAPi.PredictPostAsync<InvoiceV4Inference>(ParsingTestBase.GetFakePredictParameter());
        }
    }
}
