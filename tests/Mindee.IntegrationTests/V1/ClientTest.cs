using Mindee.Exceptions;
using Mindee.Input;
using Mindee.V1;
using Mindee.V1.ClientOptions;
using Mindee.V1.Exceptions;
using Mindee.V1.Http;
using Mindee.V1.Product.Cropper;
using Mindee.V1.Product.Generated;
using Mindee.V1.Product.InternationalId;
using Mindee.V1.Product.Invoice;
using Mindee.V1.Product.InvoiceSplitter;
using Mindee.V1.Product.Receipt;

namespace Mindee.IntegrationTests.V1
{
    [Trait("Category", "V1")]
    [Trait("Category", "Integration")]
    public class ClientTest
    {
        private readonly Client _client;

        public ClientTest()
        {
            var apiKey = Environment.GetEnvironmentVariable("Mindee__ApiKey");
            _client = TestingUtilities.GetOrGenerateMindeeClient(apiKey);
        }

        [Fact(Timeout = 180000)]
        public async Task Parse_File_Standard_MultiplePages_MustSucceed()
        {
            var inputSource = new LocalInputSource(Constants.RootDir + "file_types/pdf/multipage_cut-2.pdf");
            var response = await _client.ParseAsync<InvoiceV4>(inputSource);
            Assert.NotNull(response);
            Assert.Equal("success", response.ApiRequest.Status);
            Assert.Equal(201, response.ApiRequest.StatusCode);
            Assert.Null(response.Document.Ocr);
            Assert.NotNull(response.Document.Inference);
            Assert.NotNull(response.Document.Inference.Prediction);
            Assert.Null(response.Document.Inference.Pages.First().Extras);
            Assert.Equal(2, response.Document.Inference.Pages.Count);
        }

        [Fact(Timeout = 180000)]
        public async Task Parse_File_Standard_SinglePage_MustSucceed()
        {
            var inputSource = new LocalInputSource(Constants.RootDir + "file_types/receipt.jpg");
            var response = await _client.ParseAsync<ReceiptV5>(inputSource);
            Assert.NotNull(response);
            Assert.Equal("success", response.ApiRequest.Status);
            Assert.Equal(201, response.ApiRequest.StatusCode);
            Assert.Null(response.Document.Ocr);
            Assert.NotNull(response.Document.Inference);
            Assert.NotNull(response.Document.Inference.Prediction);
            Assert.Single(response.Document.Inference.Pages);
            Assert.Null(response.Document.Inference.Pages.First().Extras);
        }

        [Fact(Timeout = 180000)]
        public async Task Parse_Url_Standard_SinglePage_MustSucceed()
        {
            var inputSource =
                new UrlInputSource(
                    "https://raw.githubusercontent.com/mindee/client-lib-test-data/main/v1/products/expense_receipts/default_sample.jpg");
            var response = await _client.ParseAsync<ReceiptV5>(inputSource);
            Assert.NotNull(response);
            Assert.Equal("success", response.ApiRequest.Status);
            Assert.Equal(201, response.ApiRequest.StatusCode);
            Assert.Null(response.Document.Ocr);
            Assert.NotNull(response.Document.Inference);
            Assert.NotNull(response.Document.Inference.Prediction);
            Assert.Single(response.Document.Inference.Pages);
            Assert.Null(response.Document.Inference.Pages.First().Extras);
        }

        [Fact(Timeout = 180000)]
        public async Task Parse_Url_Standard_InvalidUrl_MustFail()
        {
            var inputSource = new UrlInputSource("https://bad-domain.test/invalid-file.ext");
            await Assert.ThrowsAsync<MindeeHttpExceptionV1>(() => _client.ParseAsync<ReceiptV5>(inputSource));
        }

        [Fact(Timeout = 180000)]
        public async Task Parse_File_Cropper_MustSucceed()
        {
            var inputSource = new LocalInputSource(Constants.RootDir + "file_types/receipt.jpg");
            var predictOptions = new PredictOptions(cropper: true);
            var response = await _client.ParseAsync<ReceiptV5>(inputSource, predictOptions);
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

        [Fact(Timeout = 180000)]
        public async Task Parse_File_Standard_AllWords_MustSucceed()
        {
            var inputSource = new LocalInputSource(Constants.RootDir + "file_types/receipt.jpg");
            var predictOptions = new PredictOptions(true);
            var response = await _client.ParseAsync<InvoiceV4>(inputSource, predictOptions);
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

        [Fact(Timeout = 180000)]
        public async Task Parse_File_Standard_FullText_MustSucceed()
        {
            var inputSource = new LocalInputSource(Constants.V1ProductDir + "international_id/default_sample.jpg");
            var predictOptions = new PredictOptions(fullText: true);
            var response = await _client.EnqueueAndParseAsync<InternationalIdV2>(inputSource, predictOptions);
            Assert.NotNull(response);
            Assert.Equal("success", response.ApiRequest.Status);
            Assert.Equal(200, response.ApiRequest.StatusCode);
            Assert.NotNull(response.Document.Inference.Pages.First().Extras.FullTextOcr);
            Assert.NotNull(response.Document.Inference.Extras.FullTextOcr);
            Assert.Equal(response.Document.Inference.Pages.First().Extras.FullTextOcr.Content,
                response.Document.Inference.Extras.FullTextOcr);
            Assert.True(response.Document.Inference.Extras.FullTextOcr.Replace(" ", "").Length > 100);
        }

        [Fact(Timeout = 180000)]
        public async Task Parse_File_Standard_AllWords_And_Cropper_MustSucceed()
        {
            var inputSource = new LocalInputSource(Constants.RootDir + "file_types/receipt.jpg");
            var predictOptions = new PredictOptions(true, cropper: true);
            var response = await _client.ParseAsync<InvoiceV4>(
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

        [Fact(Timeout = 180000)]
        public async Task Enqueue_File_Standard_SyncOnly_Async_MustFail()
        {
            var inputSource = new LocalInputSource(Constants.V1ProductDir + "passport/default_sample.jpg");
            await Assert.ThrowsAsync<MindeeHttpExceptionV1>(() => _client.EnqueueAsync<CropperV1>(inputSource));
        }

        [Fact(Timeout = 180000)]
        public async Task Enqueue_File_Standard_AsyncOnly_Async_MustSucceed()
        {
            var inputSource = new LocalInputSource(Constants.V1ProductDir + "invoice_splitter/default_sample.pdf");
            var response = await _client.EnqueueAsync<InvoiceSplitterV1>(inputSource);

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

        [Fact(Timeout = 180000)]
        public async Task Enqueue_File_Standard_AsyncOnly_Sync_MustFail()
        {
            var inputSource = new LocalInputSource(Constants.V1ProductDir + "invoice_splitter/default_sample.pdf");
            await Assert.ThrowsAsync<MindeeHttpExceptionV1>(() =>
                _client.ParseAsync<InvoiceSplitterV1>(inputSource));
        }

        [Fact(Timeout = 180000)]
        public async Task EnqueueAndParse_File_Standard_AsyncOnly_Async_MustSucceed()
        {
            var inputSource = new LocalInputSource(Constants.V1ProductDir + "invoice_splitter/default_sample.pdf");
            var pollingOptions = new AsyncPollingOptions();
            var response = await _client.EnqueueAndParseAsync<InvoiceSplitterV1>(
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
            Assert.NotNull(response.Document.Inference.Prediction.InvoicePageGroups);
        }


        [Fact(Timeout = 180000)]
        public async Task EnqueueAndParse_File_Standard_AsyncOnly_Async_UrlSource_MustSucceed()
        {
            var inputSource =
                new UrlInputSource(
                    "https://raw.githubusercontent.com/mindee/client-lib-test-data/main/v1/products/invoice_splitter/default_sample.pdf");
            var pollingOptions = new AsyncPollingOptions();
            var response = await _client.EnqueueAndParseAsync<InvoiceSplitterV1>(
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
            Assert.NotNull(response.Document.Inference.Prediction.InvoicePageGroups);
        }

        [Fact(Timeout = 180000)]
        public async Task EnqueueAndParse_File_Standard_AsyncOnly_Async_UrlSource_CustomEndpoint_MustSucceed()
        {
            var inputSource =
                new UrlInputSource(
                    "https://raw.githubusercontent.com/mindee/client-lib-test-data/main/v1/products/international_id/default_sample.jpg");
            var pollingOptions = new AsyncPollingOptions();
            var endpoint = new CustomEndpoint("international_id", "mindee", "2");
            var response = await _client.EnqueueAndParseAsync<GeneratedV1>(
                inputSource, endpoint, pollingOptions: pollingOptions);

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
            Assert.NotNull(response.Document.Inference.Prediction);
        }


        [Fact(Timeout = 180000)]
        public async Task ParseQueued_Standard_InvalidJob_MustFail()
        {
            var jobId = RandomString(15);
            await Assert.ThrowsAsync<MindeeHttpExceptionV1>(() =>
                _client.ParseQueuedAsync<InvoiceSplitterV1>(jobId));
        }

        [Fact(Timeout = 180000)]
        public async Task Enqueue_File_Generated_AsyncOnly_Sync_MustFail()
        {
            var endpoint = new CustomEndpoint("international_id", "mindee", "2");
            var inputSource = new LocalInputSource(Constants.V1ProductDir + "international_id/default_sample.jpg");
            await Assert.ThrowsAsync<MindeeHttpExceptionV1>(() =>
                _client.ParseAsync<GeneratedV1>(inputSource, endpoint));
        }

        [Fact(Timeout = 180000)]
        public async Task EnqueueAndParse_File_Generated_AsyncOnly_Async_MustSucceed()
        {
            var endpoint = new CustomEndpoint("international_id", "mindee", "2");
            var inputSource = new LocalInputSource(Constants.V1ProductDir + "international_id/default_sample.jpg");
            var predictOptions = new PredictOptions(fullText: true);
            var response = await _client.EnqueueAndParseAsync<GeneratedV1>(
                inputSource, endpoint, predictOptions);

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

            Assert.NotNull(response.Document.Inference.Extras.FullTextOcr);
        }

        [Fact(Timeout = 180000)]
        public async Task ParseQueued_Generated_InvalidJob_MustFail()
        {
            var jobId = RandomString(15);
            var endpoint = new CustomEndpoint("international_id", "mindee", "2");
            await Assert.ThrowsAsync<MindeeHttpExceptionV1>(() =>
                _client.ParseQueuedAsync<GeneratedV1>(endpoint, jobId));
        }

        private static string RandomString(int length)
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
