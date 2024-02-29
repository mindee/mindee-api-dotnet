using Mindee.Parsing.Common;
using Mindee.Product.Invoice;

namespace Mindee.UnitTests.Product.Invoice
{
    [Trait("Category", "InvoiceV4")]
    public class InvoiceV4Test
    {
        [Fact]
        public async Task Predict_CheckEmpty()
        {
            var response = await GetPrediction("empty");
            var docPrediction = response.Document.Inference.Prediction;
            Assert.Null(docPrediction.Locale.Value);
            Assert.Null(docPrediction.InvoiceNumber.Value);
            Assert.Empty(docPrediction.ReferenceNumbers);
            Assert.Null(docPrediction.Date.Value);
            Assert.Null(docPrediction.DueDate.Value);
            Assert.Null(docPrediction.TotalNet.Value);
            Assert.Null(docPrediction.TotalAmount.Value);
            Assert.Null(docPrediction.TotalTax.Value);
            Assert.Empty(docPrediction.Taxes);
            Assert.Empty(docPrediction.SupplierPaymentDetails);
            Assert.Null(docPrediction.SupplierName.Value);
            Assert.Empty(docPrediction.SupplierCompanyRegistrations);
            Assert.Null(docPrediction.SupplierAddress.Value);
            Assert.Null(docPrediction.CustomerName.Value);
            Assert.Empty(docPrediction.CustomerCompanyRegistrations);
            Assert.Null(docPrediction.CustomerAddress.Value);
            Assert.IsType<Mindee.Parsing.Standard.ClassificationField>(docPrediction.DocumentType);
            Assert.Empty(docPrediction.LineItems);
        }

        [Fact]
        public async Task Predict_CheckSummary()
        {
            var response = await GetPrediction("complete");
            var expected = File.ReadAllText("Resources/products/invoices/response_v4/summary_full.rst");
            Assert.Equal(expected, response.Document.ToString());
        }

        private static async Task<PredictResponse<InvoiceV4>> GetPrediction(string name)
        {
            string fileName = $"Resources/products/invoices/response_v4/{name}.json";
            var mindeeAPi = UnitTestBase.GetMindeeApi(fileName);
            return await mindeeAPi.PredictPostAsync<InvoiceV4>(
                UnitTestBase.GetFakePredictParameter());
        }
    }
}
