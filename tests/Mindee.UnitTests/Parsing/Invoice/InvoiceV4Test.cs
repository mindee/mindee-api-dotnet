using Mindee.Parsing;
using Mindee.Parsing.Invoice;

namespace Mindee.UnitTests.Parsing.Invoice
{
    [Trait("Category", "Invoice V4")]
    public class InvoiceV4Test
    {
        [Fact(Skip = "Waiting for the summary format update.")]
        public async Task Predict_MustSuccess()
        {
            var mindeeAPi = GetMindeeApiForInvoice();
            var invoicePrediction = await mindeeAPi.PredictAsync<InvoiceV4Prediction>(ParsingTestBase.GetFakePredictParameter());

            var expected = File.ReadAllText("Resources/invoice/response_v4/doc_to_string.txt");

            Assert.Equal(expected, invoicePrediction.ToString());
        }

        [Fact]
        public async Task Predict_MustSuccessForInvoiceNumber()
        {
            var mindeeAPi = GetMindeeApiForInvoice();
            var invoicePrediction = await mindeeAPi.PredictAsync<InvoiceV4Prediction>(ParsingTestBase.GetFakePredictParameter());

            Assert.Equal("0042004801351",
                invoicePrediction.Inference.Pages.First().Prediction.InvoiceNumber.Value);
        }

        [Fact]
        public async Task Predict_MustSuccessForCustomerName()
        {
            var mindeeAPi = GetMindeeApiForInvoice();
            var invoicePrediction = await mindeeAPi.PredictAsync<InvoiceV4Prediction>(ParsingTestBase.GetFakePredictParameter());

            Assert.Equal(0, invoicePrediction.Inference.Pages.First().Prediction.CustomerName.Confidence);
            Assert.Equal(0, invoicePrediction.Inference.Pages.First().Id);
            Assert.Equal(new List<List<double>>()
            , invoicePrediction.Inference.Pages.First().Prediction.CustomerName.Polygon);
            Assert.Null(invoicePrediction.Inference.Pages.First().Prediction.CustomerName.Value);
        }

        [Fact]
        public async Task Predict_MustSuccessForCustomerCompanyRegistrations()
        {
            var mindeeAPi = GetMindeeApiForInvoice();
            var invoicePrediction = await mindeeAPi.PredictAsync<InvoiceV4Prediction>(ParsingTestBase.GetFakePredictParameter());

            Assert.Equal("FR00000000000",
                invoicePrediction.Inference.Prediction.CustomerCompanyRegistrations.First().Value);
            Assert.Equal("111222333",
                invoicePrediction.Inference.Prediction.CustomerCompanyRegistrations.Last().Value);
        }

        [Fact]
        public async Task Predict_MustSuccessForCustomerAddress()
        {
            var mindeeAPi = GetMindeeApiForInvoice();
            var invoicePrediction = await mindeeAPi.PredictAsync<InvoiceV4Prediction>(ParsingTestBase.GetFakePredictParameter());

            Assert.Equal("1954 Bloon Street West Toronto, ON, M6P 3K9 Canada",
                invoicePrediction.Inference.Pages.Last().Prediction.CustomerAddress.Value);
        }

        [Fact]
        public async Task Predict_MustSuccessForDate()
        {
            var mindeeAPi = GetMindeeApiForInvoice();
            var invoicePrediction = await mindeeAPi.PredictAsync<InvoiceV4Prediction>(ParsingTestBase.GetFakePredictParameter());

            Assert.Equal(0.99, invoicePrediction.Inference.Pages.First().Prediction.Date.Confidence);
            Assert.Equal(0, invoicePrediction.Inference.Pages.First().Id);
            Assert.Equal("2020-02-17", invoicePrediction.Inference.Pages.First().Prediction.Date.Value);
            Assert.Equal(new List<List<double>>()
            {
                new List<double>() { 0.382, 0.306 },
                new List<double>() { 0.44, 0.306 },
                new List<double>() { 0.44, 0.318 },
                new List<double>() { 0.382, 0.318 },
            }
            , invoicePrediction.Inference.Pages.First().Prediction.Date.Polygon);
        }

        [Fact]
        public async Task Predict_MustSuccessForDueDate()
        {
            var mindeeAPi = GetMindeeApiForInvoice();
            var invoicePrediction = await mindeeAPi.PredictAsync<InvoiceV4Prediction>(ParsingTestBase.GetFakePredictParameter());

            Assert.Equal("2020-02-17", invoicePrediction.Inference.Pages.First().Prediction.DueDate.Value);
        }

        [Fact]
        public async Task Predict_MustSuccessForDocumentType()
        {
            var mindeeAPi = GetMindeeApiForInvoice();
            var invoicePrediction = await mindeeAPi.PredictAsync<InvoiceV4Prediction>(ParsingTestBase.GetFakePredictParameter());

            Assert.Equal("INVOICE", invoicePrediction.Inference.Pages.First().Prediction.DocumentType.Value);
        }

        [Fact]
        public async Task Predict_MustSuccessForLocale()
        {
            var mindeeAPi = GetMindeeApiForInvoice();
            var invoicePrediction = await mindeeAPi.PredictAsync<InvoiceV4Prediction>(ParsingTestBase.GetFakePredictParameter());

            Assert.Equal("fr", invoicePrediction.Inference.Pages.First().Prediction.Locale.Language);
            Assert.Equal("EUR", invoicePrediction.Inference.Pages.First().Prediction.Locale.Currency);
        }

        [Fact]
        public async Task Predict_MustSuccessForTotalTaxesIncluded()
        {
            var mindeeAPi = GetMindeeApiForInvoice();
            var invoicePrediction = await mindeeAPi.PredictAsync<InvoiceV4Prediction>(ParsingTestBase.GetFakePredictParameter());

            Assert.Equal(587.95, invoicePrediction.Inference.Pages.First().Prediction.TotalAmount.Value);
        }

        [Fact]
        public async Task Predict_MustSuccessForTotalTaxesExcluded()
        {
            var mindeeAPi = GetMindeeApiForInvoice();
            var invoicePrediction = await mindeeAPi.PredictAsync<InvoiceV4Prediction>(ParsingTestBase.GetFakePredictParameter());

            Assert.Equal(489.97, invoicePrediction.Inference.Pages.First().Prediction.TotalNet.Value);
        }

        [Fact]
        public async Task Predict_MustSuccessForSupplierName()
        {
            var mindeeAPi = GetMindeeApiForInvoice();
            var invoicePrediction = await mindeeAPi.PredictAsync<InvoiceV4Prediction>(ParsingTestBase.GetFakePredictParameter());

            Assert.Equal("TURNPIKE DESIGNS CO.", invoicePrediction.Inference.Pages.Last().Prediction.SupplierName.Value);
        }

        [Fact]
        public async Task Predict_MustSuccessForSupplierAddress()
        {
            var mindeeAPi = GetMindeeApiForInvoice();
            var invoicePrediction = await mindeeAPi.PredictAsync<InvoiceV4Prediction>(ParsingTestBase.GetFakePredictParameter());

            Assert.Equal("156 University Ave, Toronto ON, Canada M5H 2H7",
                invoicePrediction.Inference.Pages.Last().Prediction.SupplierAddress.Value);
        }

        [Fact]
        public async Task Predict_MustSuccessForSupplierCompanyRegistrations()
        {
            var mindeeAPi = GetMindeeApiForInvoice();
            var invoicePrediction = await mindeeAPi.PredictAsync<InvoiceV4Prediction>(ParsingTestBase.GetFakePredictParameter());

            Assert.Equal("501124705", invoicePrediction.Inference.Pages.First().Prediction.SupplierCompanyRegistrations.First().Value);
            Assert.Equal("FR33501124705", invoicePrediction.Inference.Pages.First().Prediction.SupplierCompanyRegistrations.Last().Value);
        }

        [Fact]
        public async Task Predict_MustSuccessForSupplierPaymentDetails()
        {
            var mindeeAPi = GetMindeeApiForInvoice();
            var invoicePrediction = await mindeeAPi.PredictAsync<InvoiceV4Prediction>(ParsingTestBase.GetFakePredictParameter());

            Assert.Equal("FR7640254025476501124705368", invoicePrediction.Inference.Pages.First().Prediction.SupplierPaymentDetails.First().Iban);
        }

        [Fact]
        public async Task Predict_MustSuccessForLineItems()
        {
            var mindeeAPi = GetMindeeApiForInvoice();
            var invoicePrediction = await mindeeAPi.PredictAsync<InvoiceV4Prediction>(ParsingTestBase.GetFakePredictParameter());

            Assert.NotEmpty(invoicePrediction.Inference.Prediction.LineItems);

            Assert.Equal("XXX81125600010", invoicePrediction.Inference.Prediction.LineItems.Skip(2).First().ProductCode);
            Assert.Equal(1.0, invoicePrediction.Inference.Prediction.LineItems.Skip(2).First().Quantity);
            Assert.Equal("a long string describing the item",
                invoicePrediction.Inference.Prediction.LineItems.Skip(2).First().Description);
            Assert.Equal(4.31, invoicePrediction.Inference.Prediction.LineItems.First().TotalAmount);
            Assert.Equal(2.1, invoicePrediction.Inference.Prediction.LineItems.First().TaxRate);
        }

        [Fact]
        public async Task Predict_MustSuccessForOrientation()
        {
            var mindeeAPi = GetMindeeApiForInvoice();
            var invoicePrediction = await mindeeAPi.PredictAsync<InvoiceV4Prediction>(ParsingTestBase.GetFakePredictParameter());

            Assert.Equal(0, invoicePrediction.Inference.Pages.First().Orientation.Value);
        }

        private MindeeApi GetMindeeApiForInvoice(string fileName = "Resources/invoice/response_v4/complete.json")
        {
            return ParsingTestBase.GetMindeeApi(fileName);
        }
    }
}
