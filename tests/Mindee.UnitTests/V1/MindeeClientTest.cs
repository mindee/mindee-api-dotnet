using Microsoft.Extensions.Logging.Abstractions;
using Mindee.Http;
using Mindee.Input;
using Mindee.Parsing.Common;
using Mindee.Pdf;
using Mindee.Product.Generated;
using Mindee.Product.InternationalId;
using Mindee.Product.Invoice;
using Moq;

namespace Mindee.UnitTests.V1
{
    [Trait("Category", "V1")]
    [Trait("Category", "Mindee client")]
    public class MindeeV1ClientTest
    {

        [Fact]
        public async Task ParseQueued_GeneratedProduct_WithJob_NoDocumentInResponse()
        {
            var predictable = new Mock<IHttpApi>();
            var mindeeClient = MindeeV1ClientTest.MakeGeneratedMindeeClient(predictable);

            var endpoint = new CustomEndpoint("", "");
            var response = await mindeeClient.ParseQueuedAsync<GeneratedV1>(endpoint, "my-job-id");

            Assert.NotNull(response);
            Assert.Null(response.Document);
            predictable.Verify(p => p.DocumentQueueGetAsync<GeneratedV1>(
                    It.IsAny<string>(), endpoint)
                , Times.AtMostOnce());
        }

        [Fact]
        public async Task Parse_StandardProduct_WithFile_NoOptions()
        {
            var predictable = new Mock<IHttpApi>();
            var mindeeClient = MakeStandardMindeeClient(predictable);

            var inputSource = new LocalInputSource(new FileInfo(Constants.RootDir + "file_types/pdf/blank_1.pdf"));
            Assert.Equal(1, inputSource.GetPageCount());
            var document = await mindeeClient.ParseAsync<InvoiceV4>(
                inputSource);

            Assert.NotNull(document);
            predictable.Verify(p => p.PredictPostAsync<InvoiceV4>(
                    It.IsAny<PredictParameter>(), null)
                , Times.AtMostOnce());
        }

        [Fact]
        public async Task Parse_StandardProduct_WithUrl_NoOptions()
        {
            var predictable = new Mock<IHttpApi>();
            var mindeeClient = MindeeV1ClientTest.MakeStandardMindeeClient(predictable);

            var inputSource = new UrlInputSource("https://example.com/blank_1.pdf");
            var response = await mindeeClient.ParseAsync<InvoiceV4>(
                inputSource);

            Assert.NotNull(response);
            predictable.Verify(p => p.PredictPostAsync<InvoiceV4>(
                    It.IsAny<PredictParameter>(), null)
                , Times.AtMostOnce());
        }

        [Fact]
        public async Task Parse_StandardProduct_WithFile_PredictOptions()
        {
            var predictable = new Mock<IHttpApi>();
            var mindeeClient = MakeStandardMindeeClient(predictable);

            var inputSource = new LocalInputSource(
                new FileInfo(Constants.RootDir + "file_types/pdf/blank_1.pdf"));
            var predictOptions = new PredictOptions(true, cropper: true);
            var response = await mindeeClient.ParseAsync<InvoiceV4>(
                inputSource, predictOptions);

            Assert.NotNull(response);
            predictable.Verify(p => p.PredictPostAsync<InvoiceV4>(
                    It.IsAny<PredictParameter>(), null)
                , Times.AtMostOnce());
        }

        [Fact]
        public async Task Parse_StandardProduct_WithFile_PageOptions()
        {
            var predictable = new Mock<IHttpApi>();
            var mindeeClient = MakeStandardMindeeClient(predictable);

            var inputSource = new LocalInputSource(
                new FileInfo(Constants.RootDir + "file_types/pdf/multipage.pdf"));
            Assert.Equal(12, inputSource.GetPageCount());
            var pageOptions = new PageOptions([1, 2, 3]);
            var response = await mindeeClient.ParseAsync<InvoiceV4>(
                inputSource, pageOptions: pageOptions);

            Assert.NotNull(response);
            predictable.Verify(p => p.PredictPostAsync<InvoiceV4>(
                    It.IsAny<PredictParameter>(), null)
                , Times.AtMostOnce());
        }

        [Fact]
        public async Task Parse_StandardProduct_WithByteArray_NoOptions()
        {
            var predictable = new Mock<IHttpApi>();
            var mindeeClient = MakeStandardMindeeClient(predictable);

            var inputSource = new LocalInputSource(
                File.ReadAllBytes(Constants.RootDir + "file_types/pdf/blank_1.pdf"),
                "blank_1.pdf"
            );
            var response = await mindeeClient.ParseAsync<InvoiceV4>(
                inputSource);

            Assert.NotNull(response);
            predictable.Verify(p => p.PredictPostAsync<InvoiceV4>(
                    It.IsAny<PredictParameter>(), null)
                , Times.AtMostOnce());
        }

        [Fact]
        public async Task Enqueue_StandardProduct_WithFile_NoOptions()
        {
            var predictable = new Mock<IHttpApi>();
            var mindeeClient = MakeStandardMindeeClient(predictable);

            var inputSource = new LocalInputSource(Constants.RootDir + "file_types/pdf/blank_1.pdf");
            var response = await mindeeClient.EnqueueAsync<InvoiceV4>(inputSource);

            Assert.NotNull(response);
            predictable.Verify(p => p.PredictAsyncPostAsync<InvoiceV4>(
                    It.IsAny<PredictParameter>(), null)
                , Times.AtMostOnce());
        }

        [Fact]
        public async Task Enqueue_StandardProduct_WithUrl_NoOptions()
        {
            var predictable = new Mock<IHttpApi>();
            var mindeeClient = MakeStandardMindeeClient(predictable);

            var inputSource = new UrlInputSource("https://example.com/blank_1.pdf");
            var response = await mindeeClient.EnqueueAsync<InvoiceV4>(inputSource);

            Assert.NotNull(response);
            predictable.Verify(p => p.PredictAsyncPostAsync<InvoiceV4>(
                    It.IsAny<PredictParameter>(), null)
                , Times.AtMostOnce());
        }

        [Fact]
        public async Task Enqueue_StandardProduct_WithFile_PredictOptions()
        {
            var predictable = new Mock<IHttpApi>();
            var mindeeClient = MakeStandardMindeeClient(predictable);

            var inputSource = new LocalInputSource(
                Constants.RootDir + "file_types/pdf/blank_1.pdf");
            var predictOptions = new PredictOptions(true, cropper: true);
            var response = await mindeeClient.EnqueueAsync<InvoiceV4>(inputSource, predictOptions);

            Assert.NotNull(response);
            predictable.Verify(p => p.PredictAsyncPostAsync<InvoiceV4>(
                    It.IsAny<PredictParameter>(), null)
                , Times.AtMostOnce());
        }

        [Fact]
        public async Task ParseQueued_StandardProduct_WithNoJob()
        {
            var predictable = new Mock<IHttpApi>();
            var mindeeClient = MakeStandardMindeeClient(predictable);

            await Assert.ThrowsAsync<ArgumentNullException>(() => _ = mindeeClient.ParseQueuedAsync<InvoiceV4>(""));
        }

        [Fact]
        public async Task ParseQueued_StandardProduct_WithJob_NoDocumentInResponse()
        {
            var predictable = new Mock<IHttpApi>();
            var mindeeClient = MakeStandardMindeeClient(predictable);

            var response = await mindeeClient.ParseQueuedAsync<InvoiceV4>("my-job-id");

            Assert.NotNull(response);
            Assert.Null(response.Document);
            predictable.Verify(p => p.DocumentQueueGetAsync<InvoiceV4>(
                    It.IsAny<string>(), null)
                , Times.AtMostOnce());
        }

        [Fact]
        public void GivenJsonInput_WhenSync_ShouldDeserializeCorrectly()
        {
            var predictable = new Mock<IHttpApi>();
            var mindeeClient = MakeStandardMindeeClient(predictable);

            var localresponse = new LocalResponse(
                new FileInfo(Constants.V1ProductDir + "invoices/response_v4/complete.json"));
            var response = mindeeClient.LoadPrediction<InvoiceV4>(localresponse);

            Assert.NotNull(response);
            Assert.Equal(
                response.Document.ToString(),
                File.ReadAllText(Constants.V1ProductDir + "invoices/response_v4/summary_full.rst"));
        }

        [Fact]
        public void GivenJsonInput_WhenAsync_ShouldDeserializeCorrectly()
        {
            var predictable = new Mock<IHttpApi>();
            var mindeeClient = MakeStandardMindeeClient(predictable);

            var localresponse = new LocalResponse(
                new FileInfo(Constants.V1ProductDir + "international_id/response_v2/complete.json"));
            var response = mindeeClient.LoadPrediction<InternationalIdV2>(localresponse);

            Assert.NotNull(response);
            Assert.Equal(
                response.Document.ToString(),
                File.ReadAllText(Constants.V1ProductDir + "international_id/response_v2/summary_full.rst"));
        }

        private static DocNetApi GetDefaultPdfOperation()
        {
            return new DocNetApi(new NullLogger<DocNetApi>());
        }

        private static MindeeClient MakeGeneratedMindeeClient(Mock<IHttpApi> predictable)
        {
            predictable.Setup(x => x.PredictPostAsync<GeneratedV1>(
                    It.IsAny<PredictParameter>()
                    , It.IsAny<CustomEndpoint>()
                ))
                .ReturnsAsync(new PredictResponse<GeneratedV1>());
            predictable.Setup(x => x.PredictAsyncPostAsync<GeneratedV1>(
                    It.IsAny<PredictParameter>()
                    , It.IsAny<CustomEndpoint>()))
                .ReturnsAsync(new AsyncPredictResponse<GeneratedV1>());
            predictable.Setup(x => x.DocumentQueueGetAsync<GeneratedV1>(
                    It.IsAny<string>()
                    , It.IsAny<CustomEndpoint>()))
                .ReturnsAsync(new AsyncPredictResponse<GeneratedV1>());
            return new MindeeClient(GetDefaultPdfOperation(), predictable.Object);
        }

        private static MindeeClient MakeStandardMindeeClient(Mock<IHttpApi> predictable)
        {
            predictable.Setup(x => x.PredictPostAsync<InvoiceV4>(
                    It.IsAny<PredictParameter>(), null))
                .ReturnsAsync(new PredictResponse<InvoiceV4>());
            predictable.Setup(x => x.PredictAsyncPostAsync<InvoiceV4>(
                    It.IsAny<PredictParameter>(), null))
                .ReturnsAsync(new AsyncPredictResponse<InvoiceV4>());
            predictable.Setup(x => x.DocumentQueueGetAsync<InvoiceV4>(
                    It.IsAny<string>(), null))
                .ReturnsAsync(new AsyncPredictResponse<InvoiceV4>());
            return new MindeeClient(GetDefaultPdfOperation(), predictable.Object);
        }
    }
}
