using Mindee.Parsing.Common;
using Mindee.Product.FinancialDocument;

namespace Mindee.UnitTests.V1.Product.FinancialDocument
{
    [Trait("Category", "Financial V1")]
    public class FinancialDocumentV1Test
    {
        [Fact]
        public async Task Predict_CheckEmpty()
        {
            var response = await GetPrediction("empty");
            Assert.Null(response.Document.Inference.Prediction.Locale.Value);
            Assert.Null(response.Document.Inference.Prediction.InvoiceNumber.Value);
            Assert.Empty(response.Document.Inference.Prediction.ReferenceNumbers);
            Assert.Equal(0.51, response.Document.Inference.Prediction.Category.Confidence);
            Assert.Null(response.Document.Inference.Prediction.Date.Value);
            Assert.Null(response.Document.Inference.Prediction.DueDate.Value);
            Assert.Null(response.Document.Inference.Prediction.BillingAddress.Value);
            Assert.Equal("EXPENSE RECEIPT", response.Document.Inference.Prediction.DocumentType.Value);
            Assert.Equal("EXPENSE RECEIPT", response.Document.Inference.Prediction.DocumentTypeExtended.Value);
            Assert.Null(response.Document.Inference.Prediction.DocumentNumber.Value);
            Assert.Null(response.Document.Inference.Prediction.TotalNet.Value);
            Assert.Null(response.Document.Inference.Prediction.TotalAmount.Value);
            Assert.Empty(response.Document.Inference.Prediction.Taxes);
            Assert.Empty(response.Document.Inference.Prediction.SupplierPaymentDetails);
            Assert.Null(response.Document.Inference.Prediction.SupplierName.Value);
            Assert.Empty(response.Document.Inference.Prediction.SupplierCompanyRegistrations);
            Assert.Null(response.Document.Inference.Prediction.SupplierAddress.Value);
            Assert.Null(response.Document.Inference.Prediction.SupplierPhoneNumber.Value);
            Assert.Null(response.Document.Inference.Prediction.CustomerName.Value);
            Assert.Null(response.Document.Inference.Prediction.CustomerId.Value);
            Assert.Empty(response.Document.Inference.Prediction.CustomerCompanyRegistrations);
            Assert.Null(response.Document.Inference.Prediction.CustomerAddress.Value);
            Assert.Null(response.Document.Inference.Prediction.TotalTax.Value);
            Assert.Null(response.Document.Inference.Prediction.Tip.Value);
            Assert.Null(response.Document.Inference.Prediction.Time.Value);
            Assert.Empty(response.Document.Inference.Prediction.LineItems);
        }

        [Fact]
        public async Task Predict_Invoice_CheckSummary()
        {
            var response = await GetPrediction("complete_invoice");
            var expected =
                File.ReadAllText(Constants.V1ProductDir + "financial_document/response_v1/summary_full_invoice.rst");
            Assert.Equal(
                expected,
                response.Document.ToString());
        }

        [Fact]
        public async Task Predict_Invoice_FirstPage_CheckSummary()
        {
            var response = await GetPrediction("complete_invoice");
            var expected =
                File.ReadAllText(Constants.V1ProductDir + "financial_document/response_v1/summary_page0_invoice.rst");
            Assert.Equal(
                expected,
                response.Document.Inference.Pages.First().ToString());
        }

        [Fact]
        public async Task Predict_Receipt_CheckSummary()
        {
            var response = await GetPrediction("complete_receipt");
            var expected =
                File.ReadAllText(Constants.V1ProductDir + "financial_document/response_v1/summary_full_receipt.rst");
            Assert.Equal(
                expected,
                response.Document.ToString());
        }

        [Fact]
        public async Task Predict_Receipt_FirstPage_CheckSummary()
        {
            var response = await GetPrediction("complete_receipt");
            var expected =
                File.ReadAllText(Constants.V1ProductDir + "financial_document/response_v1/summary_page0_receipt.rst");
            Assert.Equal(
                expected,
                response.Document.Inference.Pages.First().ToString());
        }

        private static async Task<PredictResponse<FinancialDocumentV1>> GetPrediction(string name)
        {
            var fileName = Constants.V1RootDir + $"products/financial_document/response_v1/{name}.json";
            var mindeeAPi = UnitTestBase.GetMindeeApi(fileName);
            return await mindeeAPi.PredictPostAsync<FinancialDocumentV1>(
                UnitTestBase.GetFakePredictParameter());
        }
    }
}
