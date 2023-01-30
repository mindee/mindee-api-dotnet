using Mindee.Parsing.Financial;

namespace Mindee.UnitTests.Parsing.Financial
{
    [Trait("Category", "Financial V1")]
    public class FinancialV1Test
    {
        [Fact]
        public async Task Predict_Invoice_CheckSummary()
        {
            var mindeeAPi = ParsingTestBase.GetMindeeApi("Resources/financial_document/response_v1/complete_invoice.json");
            var prediction = await mindeeAPi.PredictAsync<FinancialV1Inference>(ParsingTestBase.GetFakePredictParameter());

            var expected = File.ReadAllText("Resources/financial_document/response_v1/summary_full_invoice.rst");

            Assert.Equal(
                expected,
                prediction.ToString());
        }

        [Fact]
        public async Task Predict_Invoice_FirstPage_CheckSummary()
        {
            var mindeeAPi = ParsingTestBase.GetMindeeApi("Resources/financial_document/response_v1/complete_invoice.json");
            var prediction = await mindeeAPi.PredictAsync<FinancialV1Inference>(ParsingTestBase.GetFakePredictParameter());

            var expected = File.ReadAllText("Resources/financial_document/response_v1/summary_page0_invoice.rst");

            Assert.Equal(
                expected,
                prediction.Inference.Pages.First().ToString());
        }

        [Fact]
        public async Task Predict_Receipt_CheckSummary()
        {
            var mindeeAPi = ParsingTestBase.GetMindeeApi("Resources/financial_document/response_v1/complete_receipt.json");
            var prediction = await mindeeAPi.PredictAsync<FinancialV1Inference>(ParsingTestBase.GetFakePredictParameter());

            var expected = File.ReadAllText("Resources/financial_document/response_v1/summary_full_receipt.rst");

            Assert.Equal(
                expected,
                prediction.ToString());
        }

        [Fact]
        public async Task Predict_Receipt_FirstPage_CheckSummary()
        {
            var mindeeAPi = ParsingTestBase.GetMindeeApi("Resources/financial_document/response_v1/complete_receipt.json");
            var prediction = await mindeeAPi.PredictAsync<FinancialV1Inference>(ParsingTestBase.GetFakePredictParameter());

            var expected = File.ReadAllText("Resources/financial_document/response_v1/summary_page0_receipt.rst");

            Assert.Equal(
                expected,
                prediction.Inference.Pages.First().ToString());
        }
    }
}
