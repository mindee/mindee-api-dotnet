using Mindee.Exceptions;
using Mindee.Parsing.Invoice;
using Mindee.Parsing.InvoiceSplitter;
using Mindee.Parsing.Receipt;

namespace Mindee.IntegrationTests
{
    [Trait("Category", "Non regression tests")]
    public class MindeeClientTest
    {
        [Fact]
        public async Task Parse_Invoice_V4_WithMultiplePages_MustSuccess()
        {
            var apiKey = Environment.GetEnvironmentVariable("Mindee__ApiKey");

            var mindeeClient = MindeeClientInit.Create(apiKey);
            mindeeClient.LoadDocument(new FileInfo("Resources/invoice/invoice.pdf"));

            var parsedDocument = await mindeeClient.ParseAsync<InvoiceV4Inference>();

            Assert.NotNull(parsedDocument);
            Assert.NotNull(parsedDocument.Inference);
            Assert.NotNull(parsedDocument.Inference.DocumentPrediction);
            Assert.Equal(2, parsedDocument.Inference.Pages.Count);
        }

        [Fact]
        public async Task Parse_Receipt_V4_MustSuccess()
        {
            var apiKey = Environment.GetEnvironmentVariable("Mindee__ApiKey");

            var mindeeClient = MindeeClientInit.Create(apiKey);
            mindeeClient.LoadDocument(new FileInfo("Resources/receipt/sample.jpg"));

            var parsedDocument = await mindeeClient.ParseAsync<ReceiptV4Inference>();

            Assert.NotNull(parsedDocument);
            Assert.NotNull(parsedDocument.Inference);
            Assert.NotNull(parsedDocument.Inference.DocumentPrediction);
            Assert.Single(parsedDocument.Inference.Pages);

            var expected = File.ReadAllText("Resources/receipt/response_v4/sample_summary.rst");

            Assert.Equal(
                expected,
                parsedDocument.Inference.ToString());
        }

        [Fact]
        public async Task Parse_Receipt_V4_WithTip_MustSuccess()
        {
            var apiKey = Environment.GetEnvironmentVariable("Mindee__ApiKey");

            var mindeeClient = MindeeClientInit.Create(apiKey);
            mindeeClient.LoadDocument(new FileInfo("Resources/receipt/sample-with-tip.jpg"));

            var parsedDocument = await mindeeClient.ParseAsync<ReceiptV4Inference>();

            Assert.NotNull(parsedDocument);
            Assert.NotNull(parsedDocument.Inference);
            Assert.NotNull(parsedDocument.Inference.DocumentPrediction);
            Assert.Single(parsedDocument.Inference.Pages);

            var expected = File.ReadAllText("Resources/receipt/response_v4/sample_with_tip_summary.rst");

            Assert.Equal(
                expected,
                parsedDocument.Inference.ToString());
        }

        [Fact]
        public async Task Parse_InvoiceSplitter_V1_2Invoices_MustSuccess()
        {
            var apiKey = Environment.GetEnvironmentVariable("Mindee__ApiKey");

            var mindeeClient = MindeeClientInit.Create(apiKey);
            mindeeClient.LoadDocument(new FileInfo("Resources/invoice_splitter/2_invoices.pdf"));

            var parsedDocument = await mindeeClient.ParseAsync<InvoiceSplitterV1Inference>();

            Assert.NotNull(parsedDocument);
            Assert.NotNull(parsedDocument.Inference);
            Assert.NotNull(parsedDocument.Inference.DocumentPrediction);

            var expected = File.ReadAllText("Resources/invoice_splitter/response_v1/2_invoices_inference_summary.rst");

            Assert.Equal(
                expected,
                parsedDocument.Inference.ToString());
        }

        [Fact]
        public async Task EnqueueParsing_InvoiceSplitter_V1_2Invoices_MustFail()
        {
            var apiKey = Environment.GetEnvironmentVariable("Mindee__ApiKey");

            var mindeeClient = MindeeClientInit.Create(apiKey);
            mindeeClient.LoadDocument(new FileInfo("Resources/invoice_splitter/2_invoices.pdf"));

            var response = await mindeeClient.EnqueueParsingAsync<InvoiceSplitterV1Inference>();
            Assert.Equal(403, response.ApiRequest.StatusCode);
        }
    }
}
