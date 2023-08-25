using Mindee.Parsing.Common;
using Mindee.Product.Invoice;

namespace Mindee.UnitTests.Product.Invoice
{
    [Trait("Category", "InvoiceV4")]
    public class InvoiceV4Test
    {
        [Fact]
        public async Task Predict_CheckSummary()
        {
            var response = await GetPrediction();
            var expected = File.ReadAllText("Resources/products/invoices/response_v4/summary_full.rst");
            Assert.Equal(expected, response.Document.ToString());
        }

        [Fact]
        public async Task Predict_CheckPage0()
        {
            var response = await GetPrediction();
            var expected = File.ReadAllText("Resources/products/invoices/response_v4/summary_page0.rst");
            Assert.Equal(expected, response.Document.Inference.Pages[0].ToString());
            Assert.Equal(0, response.Document.Inference.Pages[0].Orientation.Value);
        }

        private static async Task<PredictResponse<InvoiceV4>> GetPrediction()
        {
            const string fileName = "Resources/products/invoices/response_v4/complete.json";
            var mindeeAPi = UnitTestBase.GetMindeeApi(fileName);
            return await mindeeAPi.PredictPostAsync<InvoiceV4>(
                UnitTestBase.GetFakePredictParameter());
        }
    }
}
