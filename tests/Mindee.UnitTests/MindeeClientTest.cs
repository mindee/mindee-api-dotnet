using Mindee.Http;
using Mindee.Input;
using Mindee.Parsing.Common;
using Mindee.Pdf;
using Mindee.Product.Custom;
using Mindee.Product.Generated;
using Mindee.Product.InternationalId;
using Mindee.Product.Invoice;
using Moq;

namespace Mindee.UnitTests
{
    [Trait("Category", "Mindee client")]
    public class MindeeClientTest
    {
        [Fact]
        public async Task Parse_CustomProduct_WithFile_NoOptions()
        {
            var predictable = new Mock<IHttpApi>();
            var mindeeClient = MakeCustomMindeeClient(predictable);

            var endpoint = new CustomEndpoint("", "");
            var inputSource = new LocalInputSource(new FileInfo("Resources/file_types/pdf/blank_1.pdf"));
            var response = await mindeeClient.ParseAsync(
                inputSource, endpoint);

            Assert.NotNull(response);
            predictable.Verify(p => p.PredictPostAsync<CustomV1>(
                    It.IsAny<PredictParameter>(), endpoint)
                , Times.AtMostOnce());
        }

        [Fact]
        public async Task Parse_CustomProduct_WithUrl_NoOptions()
        {
            var predictable = new Mock<IHttpApi>();
            var mindeeClient = MakeCustomMindeeClient(predictable);

            var endpoint = new CustomEndpoint("", "");
            var inputSource = new UrlInputSource("https://example.com/blank_1.pdf");
            var response = await mindeeClient.ParseAsync(
                inputSource, endpoint);

            Assert.NotNull(response);
            predictable.Verify(p => p.PredictPostAsync<CustomV1>(
                    It.IsAny<PredictParameter>(), endpoint)
                , Times.AtMostOnce());
        }

        [Fact]
        public async Task Parse_CustomProduct_WithFile_PredictOptions()
        {
            var predictable = new Mock<IHttpApi>();
            var mindeeClient = MakeCustomMindeeClient(predictable);

            var endpoint = new CustomEndpoint("", "");
            var inputSource = new LocalInputSource(new FileInfo("Resources/file_types/pdf/blank_1.pdf"));
            var predictOptions = new PredictOptions(allWords: true, cropper: true);
            var response = await mindeeClient.ParseAsync(
                inputSource,
                endpoint,
                predictOptions: predictOptions);

            Assert.NotNull(response);
            predictable.Verify(p => p.PredictPostAsync<CustomV1>(
                    It.IsAny<PredictParameter>(), endpoint)
                , Times.AtMostOnce());
        }

        [Fact]
        public async Task Parse_GeneratedProduct_WithFile_NoOptions()
        {
            var predictable = new Mock<IHttpApi>();
            var mindeeClient = MakeGeneratedMindeeClient(predictable);

            var endpoint = new CustomEndpoint("", "");
            var inputSource = new LocalInputSource(new FileInfo("Resources/file_types/pdf/blank_1.pdf"));
            var response = await mindeeClient.ParseAsync<GeneratedV1>(
                inputSource, endpoint);

            Assert.NotNull(response);
            predictable.Verify(p => p.PredictPostAsync<GeneratedV1>(
                    It.IsAny<PredictParameter>(), endpoint),
                Times.AtMostOnce());
        }

        [Fact]
        public async Task Parse_GeneratedProduct_WithFile_PredictOptions()
        {
            var predictable = new Mock<IHttpApi>();
            var mindeeClient = MakeGeneratedMindeeClient(predictable);

            var endpoint = new CustomEndpoint("", "");
            var inputSource = new LocalInputSource(new FileInfo("Resources/file_types/pdf/blank_1.pdf"));
            var predictOptions = new PredictOptions(allWords: true, cropper: true);
            var response = await mindeeClient.ParseAsync<GeneratedV1>(
                inputSource, endpoint, predictOptions);

            Assert.NotNull(response);
            predictable.Verify(p => p.PredictPostAsync<GeneratedV1>(
                    It.IsAny<PredictParameter>(), endpoint),
                Times.AtMostOnce());
        }

        [Fact]
        public async Task Parse_GeneratedProduct_WithUrl_NoOptions()
        {
            var predictable = new Mock<IHttpApi>();
            var mindeeClient = MakeGeneratedMindeeClient(predictable);

            var endpoint = new CustomEndpoint("", "");
            var inputSource = new UrlInputSource("https://example.com/blank_1.pdf");
            var response = await mindeeClient.ParseAsync<GeneratedV1>(
                inputSource, endpoint);

            Assert.NotNull(response);
            predictable.Verify(p => p.PredictPostAsync<GeneratedV1>(
                    It.IsAny<PredictParameter>(), endpoint),
                Times.AtMostOnce());
        }

        [Fact]
        public async Task Enqueue_GeneratedProduct_WithFile_NoOptions()
        {
            var predictable = new Mock<IHttpApi>();
            var mindeeClient = MakeGeneratedMindeeClient(predictable);

            var endpoint = new CustomEndpoint("", "");
            var inputSource = new LocalInputSource("Resources/file_types/pdf/blank_1.pdf");
            var response = await mindeeClient.EnqueueAsync<GeneratedV1>(
                inputSource, endpoint);

            Assert.NotNull(response);
            predictable.Verify(p => p.PredictAsyncPostAsync<GeneratedV1>(
                    It.IsAny<PredictParameter>(), endpoint),
                Times.AtMostOnce());
        }

        [Fact]
        public async Task Enqueue_GeneratedProduct_WithFile_PredictOptions()
        {
            var predictable = new Mock<IHttpApi>();
            var mindeeClient = MakeGeneratedMindeeClient(predictable);

            var endpoint = new CustomEndpoint("", "");
            var inputSource = new LocalInputSource("Resources/file_types/pdf/blank_1.pdf");
            var predictOptions = new PredictOptions(allWords: true, cropper: true);
            var response = await mindeeClient.EnqueueAsync<GeneratedV1>(
                inputSource, endpoint, predictOptions);

            Assert.NotNull(response);
            predictable.Verify(p => p.PredictAsyncPostAsync<GeneratedV1>(
                    It.IsAny<PredictParameter>(), endpoint)
                , Times.AtMostOnce());
        }

        [Fact]
        public async Task Enqueue_GeneratedProduct_WithUrl_NoOptions()
        {
            var predictable = new Mock<IHttpApi>();
            var mindeeClient = MakeGeneratedMindeeClient(predictable);

            var endpoint = new CustomEndpoint("", "");
            var inputSource = new UrlInputSource("https://example.com/blank_1.pdf");
            var response = await mindeeClient.EnqueueAsync<GeneratedV1>(inputSource, endpoint);

            Assert.NotNull(response);
            predictable.Verify(p => p.PredictAsyncPostAsync<InvoiceV4>(
                    It.IsAny<PredictParameter>(), endpoint)
                , Times.AtMostOnce());
        }

        [Fact]
        public async Task ParseQueued_GeneratedProduct_WithJob_NoDocumentInResponse()
        {
            var predictable = new Mock<IHttpApi>();
            var mindeeClient = MakeGeneratedMindeeClient(predictable);

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

            var inputSource = new LocalInputSource(new FileInfo("Resources/file_types/pdf/blank_1.pdf"));
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
            var mindeeClient = MakeStandardMindeeClient(predictable);

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

            var inputSource = new LocalInputSource(new FileInfo("Resources/file_types/pdf/blank_1.pdf"));
            var predictOptions = new PredictOptions(allWords: true, cropper: true);
            var response = await mindeeClient.ParseAsync<InvoiceV4>(
                inputSource, predictOptions);

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
                File.ReadAllBytes("Resources/file_types/pdf/blank_1.pdf"),
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

            var inputSource = new LocalInputSource("Resources/file_types/pdf/blank_1.pdf");
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

            var inputSource = new LocalInputSource("Resources/file_types/pdf/blank_1.pdf");
            var predictOptions = new PredictOptions(allWords: true, cropper: true);
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

            await Assert.ThrowsAsync<ArgumentNullException>(
                () => _ = mindeeClient.ParseQueuedAsync<InvoiceV4>(""));
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
                new FileInfo("Resources/products/invoices/response_v4/complete.json"));
            var response = mindeeClient.LoadPrediction<InvoiceV4>(localresponse);

            Assert.NotNull(response);
            Assert.Equal(
                response.Document.ToString(),
                File.ReadAllText("Resources/products/invoices/response_v4/summary_full.rst"));
        }

        [Fact]
        public void GivenJsonInput_WhenAsync_ShouldDeserializeCorrectly()
        {
            var predictable = new Mock<IHttpApi>();
            var mindeeClient = MakeStandardMindeeClient(predictable);

            var localresponse = new LocalResponse(
                new FileInfo("Resources/products/international_id/response_v2/complete.json"));
            var response = mindeeClient.LoadPrediction<InternationalIdV2>(localresponse);

            Assert.NotNull(response);
            Assert.Equal(
                response.Document.ToString(),
                File.ReadAllText("Resources/products/international_id/response_v2/summary_full.rst"));
        }

        private IPdfOperation GetDefaultPdfOperation() => Mock.Of<IPdfOperation>();

        private MindeeClient MakeCustomMindeeClient(Mock<IHttpApi> predictable)
        {
            predictable.Setup(x => x.PredictPostAsync<CustomV1>(
                    It.IsAny<PredictParameter>()
                    , It.IsAny<CustomEndpoint>()
                ))
                .ReturnsAsync(new PredictResponse<CustomV1>());
            return new MindeeClient(GetDefaultPdfOperation(), predictable.Object);
        }

        private MindeeClient MakeGeneratedMindeeClient(Mock<IHttpApi> predictable)
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

        private MindeeClient MakeStandardMindeeClient(Mock<IHttpApi> predictable)
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
