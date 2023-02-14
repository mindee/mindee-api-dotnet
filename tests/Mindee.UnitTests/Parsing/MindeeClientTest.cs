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

        private IPdfOperation GetDefaultPdfOperation() => Mock.Of<IPdfOperation>();
    }
}
