using Mindee.Exceptions;
using Mindee.Input;
using Mindee.Parsing;
using Mindee.Parsing.Common;
using Mindee.Parsing.CustomBuilder;
using Mindee.Parsing.Invoice;
using Mindee.Pdf;
using Moq;

namespace Mindee.UnitTests.Parsing
{
    [Trait("Category", "Mindee client")]
    public class MindeeClientTest
    {
        [Fact]
        public async Task Execute_With_CustomDocument_WithFile()
        {
            var customEndpoint = new CustomEndpoint("", "");
            var predictable = new Mock<IPredictable>();
            predictable.Setup(x => x.PredictAsync<CustomV1Inference>(It.IsAny<CustomEndpoint>(), It.IsAny<PredictParameter>()))
                .ReturnsAsync(new Document<CustomV1Inference>());
            var mindeeClient = new MindeeClient(GetDefaultPdfOperation(), predictable.Object);

            mindeeClient.LoadDocument(new FileInfo("Resources/invoice/invoice.pdf"));

            var document = await mindeeClient.ParseAsync(customEndpoint);

            Assert.NotNull(document);
            predictable.Verify(p => p.PredictAsync<CustomV1Inference>(It.IsAny<PredictParameter>()), Times.AtMostOnce());
        }

        [Fact]
        public async Task Execute_With_OTSApi_WithFile()
        {
            var predictable = new Mock<IPredictable>();
            predictable.Setup(x => x.PredictAsync<InvoiceV4Inference>(It.IsAny<PredictParameter>()))
                .ReturnsAsync(new Document<InvoiceV4Inference>());
            var mindeeClient = new MindeeClient(GetDefaultPdfOperation(), predictable.Object);

            mindeeClient.LoadDocument(new FileInfo("Resources/invoice/invoice.pdf"));

            var document = await mindeeClient.ParseAsync<InvoiceV4Inference>();

            Assert.NotNull(document);
            predictable.Verify(p => p.PredictAsync<InvoiceV4Inference>(It.IsAny<PredictParameter>()), Times.AtMostOnce());
        }

        [Fact]
        public async Task Execute_With_OTSApi_WithByteArray()
        {
            var predictable = new Mock<IPredictable>();
            predictable.Setup(x => x.PredictAsync<InvoiceV4Inference>(It.IsAny<PredictParameter>()))
                .ReturnsAsync(new Document<InvoiceV4Inference>());
            var mindeeClient = new MindeeClient(GetDefaultPdfOperation(), predictable.Object);

            mindeeClient.LoadDocument(File.ReadAllBytes("Resources/invoice/invoice.pdf"), "myinvoice.pdf");

            var document = await mindeeClient.ParseAsync<InvoiceV4Inference>();

            Assert.NotNull(document);
            predictable.Verify(p => p.PredictAsync<InvoiceV4Inference>(It.IsAny<PredictParameter>()), Times.AtMostOnce());
        }

        [Fact]
        public async Task EnqueueParsing_With_OTSApi_WithFile()
        {
            var predictable = new Mock<IPredictable>();
            predictable.Setup(x => x.EnqueuePredictAsync<InvoiceV4Inference>(It.IsAny<PredictParameter>()))
                .ReturnsAsync(new PredictEnqueuedResponse());
            var mindeeClient = new MindeeClient(GetDefaultPdfOperation(), predictable.Object);

            mindeeClient.LoadDocument(new FileInfo("Resources/invoice/invoice.pdf"));

            var response = await mindeeClient.EnqueueParsingAsync<InvoiceV4Inference>();

            Assert.NotNull(response);
            predictable.Verify(p => p.EnqueuePredictAsync<InvoiceV4Inference>(It.IsAny<PredictParameter>()), Times.AtMostOnce());
        }

        [Fact]
        public async Task GetEnqueuedParsing_With_OTSApi_WithFile_WithNoJobId_MustFail()
        {
            var predictable = new Mock<IPredictable>();
            predictable.Setup(x => x.GetJobAsync<InvoiceV4Inference>(It.IsAny<string>()))
                .ReturnsAsync(new GetJobResponse<InvoiceV4Inference>());
            var mindeeClient = new MindeeClient(GetDefaultPdfOperation(), predictable.Object);

            mindeeClient.LoadDocument(new FileInfo("Resources/invoice/invoice.pdf"));

            await Assert.ThrowsAsync<ArgumentNullException>(
                           () => _ = mindeeClient.GetEnqueuedParsingAsync<InvoiceV4Inference>(""));
        }

        [Fact]
        public async Task GetEnqueuedParsing_With_OTSApi_WithFile_WithNoDocumentReadyYet()
        {
            var predictable = new Mock<IPredictable>();
            predictable.Setup(x => x.GetJobAsync<InvoiceV4Inference>(It.IsAny<string>()))
                .ReturnsAsync(new GetJobResponse<InvoiceV4Inference>());
            var mindeeClient = new MindeeClient(GetDefaultPdfOperation(), predictable.Object);

            mindeeClient.LoadDocument(new FileInfo("Resources/invoice/invoice.pdf"));

            var response = await mindeeClient.GetEnqueuedParsingAsync<InvoiceV4Inference>("my-job-id");

            Assert.Null(response);
            predictable.Verify(p => p.GetJobAsync<InvoiceV4Inference>(It.IsAny<string>()), Times.AtMostOnce());
        }

        [Fact]
        public async Task GetEnqueuedParsingWithJob_With_OTSApi_WithFile_WithNoDocumentReadyYet()
        {
            var predictable = new Mock<IPredictable>();
            predictable.Setup(x => x.GetJobAsync<InvoiceV4Inference>(It.IsAny<string>()))
                .ReturnsAsync(new GetJobResponse<InvoiceV4Inference>());
            var mindeeClient = new MindeeClient(GetDefaultPdfOperation(), predictable.Object);

            mindeeClient.LoadDocument(new FileInfo("Resources/invoice/invoice.pdf"));

            var response = await mindeeClient.GetEnqueuedParsingWithJobAsync<InvoiceV4Inference>("my-job-id");

            Assert.NotNull(response);
            Assert.Null(response.Document);
            predictable.Verify(p => p.GetJobAsync<InvoiceV4Inference>(It.IsAny<string>()), Times.AtMostOnce());
        }

        private IPdfOperation GetDefaultPdfOperation() => Mock.Of<IPdfOperation>();
    }
}
