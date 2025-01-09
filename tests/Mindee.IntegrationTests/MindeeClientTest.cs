using Mindee.Exceptions;
using Mindee.Http;
using Mindee.Input;
using Mindee.Product.Cropper;
using Mindee.Product.Generated;
using Mindee.Product.InternationalId;
using Mindee.Product.Invoice;
using Mindee.Product.InvoiceSplitter;
using Mindee.Product.Receipt;

namespace Mindee.IntegrationTests
{
    [Trait("Category", "Integration tests")]
    public class MindeeClientTest
    {
        private readonly MindeeClient _mindeeClient;

        public MindeeClientTest()
        {
            var apiKey = Environment.GetEnvironmentVariable("Mindee__ApiKey");
            _mindeeClient = TestingUtilities.GetOrGenerateMindeeClient(apiKey);
        }

        [Fact]
        public async Task Parse_File_Standard_MultiplePages_MustSucceed()
        {
            var inputSource = new LocalInputSource("Resources/file_types/pdf/multipage_cut-2.pdf");
            var response = await _mindeeClient.ParseAsync<InvoiceV4>(inputSource);
            Assert.NotNull(response);
            Assert.Equal("success", response.ApiRequest.Status);
            Assert.Equal(201, response.ApiRequest.StatusCode);
            Assert.Null(response.Document.Ocr);
            Assert.NotNull(response.Document.Inference);
            Assert.NotNull(response.Document.Inference.Prediction);
            Assert.Null(response.Document.Inference.Pages.First().Extras);
            Assert.Equal(2, response.Document.Inference.Pages.Count);
        }

        [Fact]
        public async Task Parse_File_Standard_SinglePage_MustSucceed()
        {
            var inputSource = new LocalInputSource("Resources/file_types/receipt.jpg");
            var response = await _mindeeClient.ParseAsync<ReceiptV5>(inputSource);
            Assert.NotNull(response);
            Assert.Equal("success", response.ApiRequest.Status);
            Assert.Equal(201, response.ApiRequest.StatusCode);
            Assert.Null(response.Document.Ocr);
            Assert.NotNull(response.Document.Inference);
            Assert.NotNull(response.Document.Inference.Prediction);
            Assert.Single(response.Document.Inference.Pages);
            Assert.Null(response.Document.Inference.Pages.First().Extras);
        }

        [Fact]
        public async Task Parse_Url_Standard_SinglePage_MustSucceed()
        {
            var inputSource =
                new UrlInputSource(
                    "https://raw.githubusercontent.com/mindee/client-lib-test-data/main/products/expense_receipts/default_sample.jpg");
            var response = await _mindeeClient.ParseAsync<ReceiptV5>(inputSource);
            Assert.NotNull(response);
            Assert.Equal("success", response.ApiRequest.Status);
            Assert.Equal(201, response.ApiRequest.StatusCode);
            Assert.Null(response.Document.Ocr);
            Assert.NotNull(response.Document.Inference);
            Assert.NotNull(response.Document.Inference.Prediction);
            Assert.Single(response.Document.Inference.Pages);
            Assert.Null(response.Document.Inference.Pages.First().Extras);
        }

        [Fact]
        public async Task Parse_Url_Standard_InvalidUrl_MustFail()
        {
            var inputSource = new UrlInputSource("https://bad-domain.test/invalid-file.ext");
            await Assert.ThrowsAsync<Mindee400Exception>(
                () => _mindeeClient.ParseAsync<ReceiptV5>(inputSource));
        }

        [Fact]
        public async Task Parse_File_Cropper_MustSucceed()
        {
            var inputSource = new LocalInputSource("Resources/file_types/receipt.jpg");
            var predictOptions = new PredictOptions(cropper: true);
            var response = await _mindeeClient.ParseAsync<ReceiptV5>(inputSource, predictOptions);
            Assert.NotNull(response);
            Assert.Equal("success", response.ApiRequest.Status);
            Assert.Equal(201, response.ApiRequest.StatusCode);
            Assert.Null(response.Document.Ocr);
            Assert.NotNull(response.Document.Inference);
            Assert.NotNull(response.Document.Inference.Prediction);
            Assert.Single(response.Document.Inference.Pages);
            Assert.NotNull(response.Document.Inference.Pages.First().Extras.Cropper);
            Assert.Single(response.Document.Inference.Pages.First().Extras.Cropper.Cropping);
        }

        [Fact]
        public async Task Parse_File_Standard_AllWords_MustSucceed()
        {
            var inputSource = new LocalInputSource("Resources/file_types/receipt.jpg");
            var predictOptions = new PredictOptions(allWords: true);
            var response = await _mindeeClient.ParseAsync<InvoiceV4>(inputSource, predictOptions);
            Assert.NotNull(response);
            Assert.Equal("success", response.ApiRequest.Status);
            Assert.Equal(201, response.ApiRequest.StatusCode);
            Assert.NotNull(response.Document.Ocr.ToString());
            Assert.Single(response.Document.Ocr.MvisionV1.Pages);
            Assert.NotEmpty(response.Document.Ocr.MvisionV1.Pages.First().AllWords);
            Assert.NotNull(response.Document.Inference);
            Assert.NotNull(response.Document.Inference.Prediction);
            Assert.Single(response.Document.Inference.Pages);
            Assert.Null(response.Document.Inference.Pages.First().Extras);
        }

        [Fact]
        public async Task Parse_File_Standard_FullText_MustSucceed()
        {
            var inputSource = new LocalInputSource("Resources/products/international_id/default_sample.jpg");
            var predictOptions = new PredictOptions(fullText: true);
            var response = await _mindeeClient.EnqueueAndParseAsync<InternationalIdV2>(inputSource, predictOptions);
            Assert.NotNull(response);
            Assert.Equal("success", response.ApiRequest.Status);
            Assert.Equal(200, response.ApiRequest.StatusCode);
            Assert.NotNull(response.Document.Inference.Pages.First().Extras.FullTextOcr);
            Assert.NotNull(response.Document.Inference.Extras.FullTextOcr);
            Assert.Equal(response.Document.Inference.Pages.First().Extras.FullTextOcr.Content,
                response.Document.Inference.Extras.FullTextOcr);
            Assert.True(response.Document.Inference.Extras.FullTextOcr.Replace(" ", "").Length > 100);
        }

        [Fact]
        public async Task Parse_File_Standard_AllWords_And_Cropper_MustSucceed()
        {
            var inputSource = new LocalInputSource("Resources/file_types/receipt.jpg");
            var predictOptions = new PredictOptions(allWords: true, cropper: true);
            var response = await _mindeeClient.ParseAsync<InvoiceV4>(
                inputSource, predictOptions);
            Assert.NotNull(response);
            Assert.Equal("success", response.ApiRequest.Status);
            Assert.Equal(201, response.ApiRequest.StatusCode);
            Assert.NotNull(response.Document.Ocr);
            Assert.Single(response.Document.Ocr.MvisionV1.Pages);
            Assert.NotEmpty(response.Document.Ocr.MvisionV1.Pages.First().AllWords);
            Assert.NotNull(response.Document.Inference);
            Assert.NotNull(response.Document.Inference.Prediction);
            Assert.Single(response.Document.Inference.Pages);
            Assert.NotNull(response.Document.Inference.Pages.First().Extras.Cropper);
            Assert.Single(response.Document.Inference.Pages.First().Extras.Cropper.Cropping);
        }

        [Fact]
        public async Task Enqueue_File_Standard_SyncOnly_Async_MustFail()
        {
            var inputSource = new LocalInputSource("Resources/products/passport/default_sample.jpg");
            await Assert.ThrowsAsync<Mindee403Exception>(
                () => _mindeeClient.EnqueueAsync<CropperV1>(inputSource));
        }

        [Fact]
        public async Task Enqueue_File_Standard_AsyncOnly_Async_MustSucceed()
        {
            var inputSource = new LocalInputSource("Resources/products/invoice_splitter/default_sample.pdf");
            var response = await _mindeeClient.EnqueueAsync<InvoiceSplitterV1>(inputSource);

            Assert.NotNull(response);
            Assert.NotNull(response.ApiRequest);
            Assert.Equal("https://api.mindee.net/v1/products/mindee/invoice_splitter/v1/predict_async",
                response.ApiRequest.Url);
            Assert.Equal("success", response.ApiRequest.Status);
            Assert.Equal(202, response.ApiRequest.StatusCode);

            Assert.NotNull(response.Job);
            Assert.Equal("waiting", response.Job.Status);
            Assert.NotNull(response.Job.IssuedAt.ToString("yyyy"));
            Assert.Null(response.Job.AvailableAt);
        }

        [Fact]
        public async Task Enqueue_File_Standard_AsyncOnly_Sync_MustFail()
        {
            var inputSource = new LocalInputSource("Resources/products/invoice_splitter/default_sample.pdf");
            await Assert.ThrowsAsync<Mindee403Exception>(
                () => _mindeeClient.ParseAsync<InvoiceSplitterV1>(inputSource));
        }

        [Fact]
        public async Task EnqueueAndParse_File_Standard_AsyncOnly_Async_MustSucceed()
        {
            var inputSource = new LocalInputSource("Resources/products/invoice_splitter/default_sample.pdf");
            var pollingOptions = new AsyncPollingOptions(maxRetries: 80);
            var response = await _mindeeClient.EnqueueAndParseAsync<InvoiceSplitterV1>(
                inputSource, pollingOptions: pollingOptions);

            Assert.NotNull(response);
            Assert.NotNull(response.ApiRequest);
            Assert.Contains("/v1/products/mindee/invoice_splitter/v1/documents/", response.ApiRequest.Url);
            Assert.Equal("success", response.ApiRequest.Status);
            Assert.Equal(200, response.ApiRequest.StatusCode);

            Assert.NotNull(response.Job);
            Assert.Equal("completed", response.Job.Status);
            Assert.NotNull(response.Job.IssuedAt.ToString("yyyy"));
            Assert.NotNull(response.Job.AvailableAt?.ToString("yyyy"));

            Assert.NotNull(response.Document);
            Assert.NotNull(response.Document.Inference.Prediction.PageGroups);
        }

        [Fact]
        public async Task ParseQueued_Standard_InvalidJob_MustFail()
        {
            var jobId = RandomString(15);
            await Assert.ThrowsAsync<Mindee404Exception>(
                () => _mindeeClient.ParseQueuedAsync<InvoiceSplitterV1>(jobId));
        }

        [Fact]
        public async Task Enqueue_File_Generated_AsyncOnly_Sync_MustFail()
        {
            var endpoint = new CustomEndpoint("international_id", "mindee", "2");
            var inputSource = new LocalInputSource("Resources/products/international_id/default_sample.jpg");
            await Assert.ThrowsAsync<Mindee403Exception>(
                () => _mindeeClient.ParseAsync<GeneratedV1>(inputSource, endpoint));
        }

        [Fact]
        public async Task EnqueueAndParse_File_Generated_AsyncOnly_Async_MustSucceed()
        {
            var endpoint = new CustomEndpoint("international_id", "mindee", "2");
            var inputSource = new LocalInputSource("Resources/products/international_id/default_sample.jpg");
            var response = await _mindeeClient.EnqueueAndParseAsync<GeneratedV1>(inputSource, endpoint);

            Assert.NotNull(response);
            Assert.NotNull(response.ApiRequest);
            Assert.Contains("/v1/products/mindee/international_id/v2/documents/", response.ApiRequest.Url);
            Assert.Equal("success", response.ApiRequest.Status);
            Assert.Equal(200, response.ApiRequest.StatusCode);

            Assert.NotNull(response.Job);
            Assert.Equal("completed", response.Job.Status);
            Assert.NotNull(response.Job.IssuedAt.ToString("yyyy"));
            Assert.NotNull(response.Job.AvailableAt?.ToString("yyyy"));

            Assert.NotNull(response.Document);
            Assert.NotNull(response.Document.Inference.Prediction.Fields);
        }

        [Fact]
        public async Task ParseQueued_Generated_InvalidJob_MustFail()
        {
            var jobId = RandomString(15);
            var endpoint = new CustomEndpoint("international_id", "mindee", "2");
            await Assert.ThrowsAsync<Mindee404Exception>(
                () => _mindeeClient.ParseQueuedAsync<GeneratedV1>(endpoint, jobId));
        }

        private static string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
