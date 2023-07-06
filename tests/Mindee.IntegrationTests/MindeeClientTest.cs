using Mindee.Exceptions;
using Mindee.Input;
using Mindee.Product.Invoice;
using Mindee.Product.InvoiceSplitter;
using Mindee.Product.Receipt;

namespace Mindee.IntegrationTests
{
    [Trait("Category", "Non regression tests")]
    public class MindeeClientTest
    {
        [Fact]
        public async Task Parse_Invoice_V4_WithMultiplePages_MustSucceed()
        {
            var apiKey = Environment.GetEnvironmentVariable("Mindee__ApiKey");
            var mindeeClient = new MindeeClient(apiKey);
            var inputSource = new LocalInputSource("Resources/invoice/invoice.pdf");
            var response = await mindeeClient.ParseAsync<InvoiceV4>(inputSource);
            Assert.NotNull(response);
            Assert.NotNull(response.Document.Inference);
            Assert.NotNull(response.Document.Inference.Prediction);
            Assert.Equal(2, response.Document.Inference.Pages.Count);
        }

        [Fact]
        public async Task Parse_Receipt_V4_MustSucceed()
        {
            var apiKey = Environment.GetEnvironmentVariable("Mindee__ApiKey");
            var mindeeClient = new MindeeClient(apiKey);
            var inputSource = new LocalInputSource("Resources/receipt/sample.jpg");
            var response = await mindeeClient.ParseAsync<ReceiptV4>(inputSource);
            Assert.NotNull(response);
            Assert.NotNull(response.Document.Inference);
            Assert.NotNull(response.Document.Inference.Prediction);
            Assert.Single(response.Document.Inference.Pages);

            var expected = File.ReadAllText("Resources/receipt/response_v4/sample_summary.rst");
            Assert.Equal(
                expected,
                response.Document.Inference.ToString());
        }

        [Fact]
        public async Task Parse_Receipt_V4_WithTip_MustSucceed()
        {
            var apiKey = Environment.GetEnvironmentVariable("Mindee__ApiKey");
            var mindeeClient = new MindeeClient(apiKey);
            var inputSource = new LocalInputSource("Resources/receipt/sample-with-tip.jpg");
            var response = await mindeeClient.ParseAsync<ReceiptV4>(inputSource);
            Assert.NotNull(response);
            Assert.NotNull(response.Document.Inference);
            Assert.NotNull(response.Document.Inference.Prediction);
            Assert.Single(response.Document.Inference.Pages);

            var expected = File.ReadAllText("Resources/receipt/response_v4/sample_with_tip_summary.rst");
            Assert.Equal(
                expected,
                response.Document.Inference.ToString());
        }

        [Fact]
        public async Task Parse_InvoiceSplitter_V1_2Invoices_MustSucceed()
        {
            var apiKey = Environment.GetEnvironmentVariable("Mindee__ApiKey");
            var mindeeClient = new MindeeClient(apiKey);
            var inputSource = new LocalInputSource("Resources/invoice_splitter/2_invoices.pdf");
            var response = await mindeeClient.EnqueueAsync<InvoiceSplitterV1>(inputSource);

            Assert.NotNull(response);

            Assert.NotNull(response.ApiRequest);
            Assert.Equal("https://api.mindee.net/v1/products/mindee/invoice_splitter/v1/predict_async", response.ApiRequest.Url);
            Assert.Equal("success", response.ApiRequest.Status);
            Assert.Equal(202, response.ApiRequest.StatusCode);

            Assert.NotNull(response.Job);
            Assert.Equal("waiting", response.Job.Status);
            Assert.NotNull(response.Job.IssuedAt.ToString("yyyy"));
            Assert.Null(response.Job.AvailableAt);
        }

        [Fact]
        public async Task EnqueueParsing_InvoiceSplitter_V1_2Invoices_MustFail()
        {
            var apiKey = Environment.GetEnvironmentVariable("Mindee__ApiKey");
            var mindeeClient = new MindeeClient(apiKey);
            var inputSource = new LocalInputSource("Resources/invoice_splitter/2_invoices.pdf");
            await Assert.ThrowsAsync<Mindee403Exception>(
                () => _ = mindeeClient.ParseAsync<InvoiceSplitterV1>(inputSource)
                );
        }
    }
}
