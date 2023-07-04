using Mindee.Http;
using Mindee.Parsing.Common;
using Mindee.Pdf;
using Mindee.Product.Custom;
using Mindee.Product.Invoice;
using Moq;

namespace Mindee.UnitTests
{
    [Trait("Category", "Mindee client")]
    public class MindeeClientTest
    {
        [Fact]
        public async Task Execute_With_CustomDocument_WithFile()
        {
            var customEndpoint = new CustomEndpoint("", "");
            var predictable = new Mock<IPredictable>();
            predictable.Setup(x => x.PredictPostAsync<CustomV1>(
                    It.IsAny<CustomEndpoint>()
                    , It.IsAny<PredictParameter>()
                    ))
                .ReturnsAsync(new PredictResponse<CustomV1>());
            var mindeeClient = new MindeeClient(GetDefaultPdfOperation(), predictable.Object);

            mindeeClient.LoadDocument(new FileInfo("Resources/invoice/invoice.pdf"));

            var document = await mindeeClient.ParseAsync(customEndpoint);

            Assert.NotNull(document);
            predictable.Verify(p => p.PredictPostAsync<CustomV1>(
                It.IsAny<PredictParameter>())
                , Times.AtMostOnce()
                );
        }

        [Fact]
        public async Task Execute_With_OTSApi_WithFile()
        {
            var predictable = new Mock<IPredictable>();
            predictable.Setup(x => x.PredictPostAsync<InvoiceV4>(It.IsAny<PredictParameter>()))
                .ReturnsAsync(new PredictResponse<InvoiceV4>());
            var mindeeClient = new MindeeClient(GetDefaultPdfOperation(), predictable.Object);

            mindeeClient.LoadDocument(new FileInfo("Resources/invoice/invoice.pdf"));

            var document = await mindeeClient.ParseAsync<InvoiceV4>();

            Assert.NotNull(document);
            predictable.Verify(p => p.PredictPostAsync<InvoiceV4>(
                It.IsAny<PredictParameter>())
                , Times.AtMostOnce()
                );
        }

        [Fact]
        public async Task Execute_With_OTSApi_WithByteArray()
        {
            var predictable = new Mock<IPredictable>();
            predictable.Setup(x => x.PredictPostAsync<InvoiceV4>(
                    It.IsAny<PredictParameter>()
                    ))
                .ReturnsAsync(new PredictResponse<InvoiceV4>());
            var mindeeClient = new MindeeClient(GetDefaultPdfOperation(), predictable.Object);

            mindeeClient.LoadDocument(File.ReadAllBytes("Resources/invoice/invoice.pdf"), "myinvoice.pdf");

            var document = await mindeeClient.ParseAsync<InvoiceV4>();

            Assert.NotNull(document);
            predictable.Verify(p => p.PredictPostAsync<InvoiceV4>(It.IsAny<PredictParameter>()), Times.AtMostOnce());
        }

        [Fact]
        public async Task EnqueueParsing_With_OTSApi_WithFile()
        {
            var predictable = new Mock<IPredictable>();
            predictable.Setup(x => x.PredictAsyncPostAsync<InvoiceV4>(It.IsAny<PredictParameter>()))
                .ReturnsAsync(new AsyncPredictResponse<InvoiceV4>());
            var mindeeClient = new MindeeClient(GetDefaultPdfOperation(), predictable.Object);

            mindeeClient.LoadDocument(new FileInfo("Resources/invoice/invoice.pdf"));
            var response = await mindeeClient.EnqueueAsync<InvoiceV4>();

            Assert.NotNull(response);
            predictable.Verify(p => p.PredictAsyncPostAsync<InvoiceV4>(It.IsAny<PredictParameter>()), Times.AtMostOnce());
        }

        [Fact]
        public async Task GetEnqueuedParsing_With_OTSApi_WithFile_WithNoJobId_MustFail()
        {
            var predictable = new Mock<IPredictable>();
            predictable.Setup(x => x.DocumentQueueGetAsync<InvoiceV4>(It.IsAny<string>()))
                .ReturnsAsync(new AsyncPredictResponse<InvoiceV4>());
            var mindeeClient = new MindeeClient(GetDefaultPdfOperation(), predictable.Object);

            mindeeClient.LoadDocument(new FileInfo("Resources/invoice/invoice.pdf"));

            await Assert.ThrowsAsync<ArgumentNullException>(
                           () => _ = mindeeClient.ParseQueuedAsync<InvoiceV4>(""));
        }

        [Fact]
        public async Task GetEnqueuedParsing_With_OTSApi_WithFile_WithNoDocumentReadyYet()
        {
            var predictable = new Mock<IPredictable>();
            predictable.Setup(x => x.DocumentQueueGetAsync<InvoiceV4>(It.IsAny<string>()))
                .ReturnsAsync(new AsyncPredictResponse<InvoiceV4>());
            var mindeeClient = new MindeeClient(GetDefaultPdfOperation(), predictable.Object);

            mindeeClient.LoadDocument(new FileInfo("Resources/invoice/invoice.pdf"));

            var response = await mindeeClient.ParseQueuedAsync<InvoiceV4>("my-job-id");

            Assert.Null(response.Document);
            predictable.Verify(p => p.DocumentQueueGetAsync<InvoiceV4>(It.IsAny<string>()), Times.AtMostOnce());
        }

        [Fact]
        public async Task GetEnqueuedParsingWithJob_With_OTSApi_WithFile_WithNoDocumentReadyYet()
        {
            var predictable = new Mock<IPredictable>();
            predictable.Setup(x => x.DocumentQueueGetAsync<InvoiceV4>(It.IsAny<string>()))
                .ReturnsAsync(new AsyncPredictResponse<InvoiceV4>());
            var mindeeClient = new MindeeClient(GetDefaultPdfOperation(), predictable.Object);

            mindeeClient.LoadDocument(new FileInfo("Resources/invoice/invoice.pdf"));

            var response = await mindeeClient.ParseQueuedAsync<InvoiceV4>("my-job-id");

            Assert.NotNull(response);
            Assert.Null(response.Document);
            predictable.Verify(p => p.DocumentQueueGetAsync<InvoiceV4>(It.IsAny<string>()), Times.AtMostOnce());
        }

        private IPdfOperation GetDefaultPdfOperation() => Mock.Of<IPdfOperation>();
    }
}
