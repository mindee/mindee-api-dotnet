using Mindee.Parsing;
using Mindee.Parsing.InvoiceSplitter;

namespace Mindee.UnitTests.Parsing.InvoiceSplitter
{
    [Trait("Category", "Invoice Splitter V1")]
    public class InvoiceSplitterV1Test
    {
        [Fact]
        public async Task Predict_CheckSummary()
        {
            var mindeeAPi = GetMindeeApiForInvoice();
            var prediction = await mindeeAPi.PredictAsync<InvoiceSplitterV1Inference>(ParsingTestBase.GetFakePredictParameter());

            var expected = File.ReadAllText("Resources/invoice_splitter/response_v1/2_invoices_summary.rst");

            Assert.Equal(
                expected,
                prediction.ToString());
        }

        private MindeeApi GetMindeeApiForInvoice(string fileName = "Resources/invoice_splitter/response_v1/2_invoices_response.json")
        {
            return ParsingTestBase.GetMindeeApi(fileName);
        }
    }
}
