using Mindee.Exceptions;
using Mindee.Input;
using Mindee.Product.Invoice;
using Mindee.Product.InvoiceSplitter;
using Mindee.Product.Receipt;

namespace Mindee.IntegrationTests
{
    [Trait("Category", "Integration tests")]
    public class MindeeClientTest
    {
        [Fact]
        public async Task Parse_WithMultiplePages_MustSucceed()
        {
            var apiKey = Environment.GetEnvironmentVariable("Mindee__ApiKey");
            var mindeeClient = new MindeeClient(apiKey);
            var inputSource = new LocalInputSource("Resources/file_types/pdf/multipage_cut-2.pdf");
            var response = await mindeeClient.ParseAsync<InvoiceV4>(inputSource);
            Assert.NotNull(response);
            Assert.Equal("success", response.ApiRequest.Status);
            Assert.Equal(201, response.ApiRequest.StatusCode);
            Assert.Null(response.Document.Ocr);
            Assert.NotNull(response.Document.Inference);
            Assert.NotNull(response.Document.Inference.Prediction);
            Assert.Null(response.Document.Inference.Pages.First().Extras.Cropper);
            Assert.Equal(2, response.Document.Inference.Pages.Count);
        }

        [Fact]
        public async Task Parse_WithSinglePage_MustSucceed()
        {
            var apiKey = Environment.GetEnvironmentVariable("Mindee__ApiKey");
            var mindeeClient = new MindeeClient(apiKey);
            var inputSource = new LocalInputSource("Resources/file_types/receipt.jpg");
            var response = await mindeeClient.ParseAsync<ReceiptV5>(inputSource);
            Assert.NotNull(response);
            Assert.Equal("success", response.ApiRequest.Status);
            Assert.Equal(201, response.ApiRequest.StatusCode);
            Assert.Null(response.Document.Ocr);
            Assert.NotNull(response.Document.Inference);
            Assert.NotNull(response.Document.Inference.Prediction);
            Assert.Single(response.Document.Inference.Pages);
            Assert.Null(response.Document.Inference.Pages.First().Extras.Cropper);
        }

        [Fact]
        public async Task Parse_Cropper_MustSucceed()
        {
            var apiKey = Environment.GetEnvironmentVariable("Mindee__ApiKey");
            var mindeeClient = new MindeeClient(apiKey);
            var inputSource = new LocalInputSource("Resources/file_types/receipt.jpg");
            var predictOptions = new PredictOptions(cropper: true);
            var response = await mindeeClient.ParseAsync<ReceiptV5>(inputSource, predictOptions);
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
        public async Task Parse_AllWords_MustSucceed()
        {
            var apiKey = Environment.GetEnvironmentVariable("Mindee__ApiKey");
            var mindeeClient = new MindeeClient(apiKey);
            var inputSource = new LocalInputSource("Resources/file_types/receipt.jpg");
            var predictOptions = new PredictOptions(allWords: true);
            var response = await mindeeClient.ParseAsync<InvoiceV4>(inputSource, predictOptions);
            Assert.NotNull(response);
            Assert.Equal("success", response.ApiRequest.Status);
            Assert.Equal(201, response.ApiRequest.StatusCode);
            Assert.NotNull(response.Document.Ocr.ToString());
            Assert.Single(response.Document.Ocr.MvisionV1.Pages);
            Assert.NotEmpty(response.Document.Ocr.MvisionV1.Pages.First().AllWords);
            Assert.NotNull(response.Document.Inference);
            Assert.NotNull(response.Document.Inference.Prediction);
            Assert.Single(response.Document.Inference.Pages);
            Assert.Null(response.Document.Inference.Pages.First().Extras.Cropper);
        }

        [Fact]
        public async Task Parse_AllWords_And_Cropper_MustSucceed()
        {
            var apiKey = Environment.GetEnvironmentVariable("Mindee__ApiKey");
            var mindeeClient = new MindeeClient(apiKey);
            var inputSource = new LocalInputSource("Resources/file_types/receipt.jpg");
            var predictOptions = new PredictOptions(allWords: true, cropper: true);
            var response = await mindeeClient.ParseAsync<InvoiceV4>(
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
        public async Task Enqueue_AsyncOnly_Async_MustSucceed()
        {
            var apiKey = Environment.GetEnvironmentVariable("Mindee__ApiKey");
            var mindeeClient = new MindeeClient(apiKey);
            var inputSource = new LocalInputSource("Resources/products/invoice_splitter/default_sample.pdf");
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
        public async Task Enqueue_AsyncOnly_Sync_MustFail()
        {
            var apiKey = Environment.GetEnvironmentVariable("Mindee__ApiKey");
            var mindeeClient = new MindeeClient(apiKey);
            var inputSource = new LocalInputSource("Resources/products/invoice_splitter/default_sample.pdf");
            await Assert.ThrowsAsync<Mindee403Exception>(
                () => _ = mindeeClient.ParseAsync<InvoiceSplitterV1>(inputSource)
                );
        }
    }
}
