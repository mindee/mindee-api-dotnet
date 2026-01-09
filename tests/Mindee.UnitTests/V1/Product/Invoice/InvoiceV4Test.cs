using Mindee.Parsing.Common;
using Mindee.Parsing.Standard;
using Mindee.Product.Invoice;

namespace Mindee.UnitTests.V1.Product.Invoice
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
            Assert.Null(docPrediction.PoNumber.Value);
            Assert.Empty(docPrediction.ReferenceNumbers);
            Assert.Null(docPrediction.Date.Value);
            Assert.Null(docPrediction.DueDate.Value);
            Assert.Null(docPrediction.PaymentDate.Value);
            Assert.Null(docPrediction.TotalNet.Value);
            Assert.Null(docPrediction.TotalAmount.Value);
            Assert.Null(docPrediction.TotalTax.Value);
            Assert.Empty(docPrediction.Taxes);
            Assert.Empty(docPrediction.SupplierPaymentDetails);
            Assert.Null(docPrediction.SupplierName.Value);
            Assert.Empty(docPrediction.SupplierCompanyRegistrations);
            Assert.Null(docPrediction.SupplierAddress.Value);
            Assert.Null(docPrediction.SupplierPhoneNumber.Value);
            Assert.Null(docPrediction.SupplierWebsite.Value);
            Assert.Null(docPrediction.SupplierEmail.Value);
            Assert.Null(docPrediction.CustomerName.Value);
            Assert.Empty(docPrediction.CustomerCompanyRegistrations);
            Assert.Null(docPrediction.CustomerAddress.Value);
            Assert.Null(docPrediction.CustomerId.Value);
            Assert.Null(docPrediction.ShippingAddress.Value);
            Assert.Null(docPrediction.BillingAddress.Value);
            Assert.IsType<ClassificationField>(docPrediction.DocumentType);
            Assert.IsType<ClassificationField>(docPrediction.DocumentTypeExtended);
            Assert.IsType<ClassificationField>(docPrediction.Subcategory);
            Assert.IsType<ClassificationField>(docPrediction.Category);
            Assert.Empty(docPrediction.LineItems);
        }

        [Fact]
        public async Task Predict_CheckSummary()
        {
            var response = await GetPrediction("complete");
            var expected = File.ReadAllText(Constants.V1ProductDir + "invoices/response_v4/summary_full.rst");
            Assert.Equal(expected, response.Document.ToString());
        }

        private static async Task<PredictResponse<InvoiceV4>> GetPrediction(string name)
        {
            var fileName = Constants.V1RootDir + $"products/invoices/response_v4/{name}.json";
            var mindeeAPi = UnitTestBase.GetMindeeApi(fileName);
            return await mindeeAPi.PredictPostAsync<InvoiceV4>(
                UnitTestBase.GetFakePredictParameter());
        }
    }
}
