using Mindee.Input;

namespace Mindee.IntegrationTests
{
    [Trait("Category", "Integration tests")]
    public class MindeeClientV2Test
    {
        private readonly MindeeClientV2 _mindeeClientV2;

        public MindeeClientV2Test()
        {
            var apiKey = Environment.GetEnvironmentVariable("Mindee__ApiKey");
            _mindeeClientV2 = TestingUtilities.GetOrGenerateMindeeClientV2(apiKey);
        }

        [Fact(Skip = "V2 is not live yet.")]
        public async Task Parse_File_Standard_MultiplePages_MustSucceed()
        {
            var inputSource = new LocalInputSource("Resources/file_types/pdf/multipage_cut-2.pdf");
            var response = await _mindeeClientV2.EnqueueAndParseAsync(inputSource, "VALID-MODEL-UUID");
            Assert.NotNull(response);
            Assert.Equal("success", response.ApiRequest.Status);
            Assert.Equal(201, response.ApiRequest.StatusCode);
            Assert.NotNull(response.Document.Inference);
            Assert.NotNull(response.Document.Inference.Prediction);
            Assert.Null(response.Document.Inference.Pages.First().Extras);
            Assert.Equal(2, response.Document.Inference.Pages.Count);
        }

        // TODO: fix enqueue and parse for url input source
        // [Fact(Skip = "V2 is not live yet.")]
        // public async Task Parse_Url_Standard_SinglePage_MustSucceed()
        // {
        //     var inputSource =
        //         new UrlInputSource(
        //             "https://raw.githubusercontent.com/mindee/client-lib-test-data/main/products/expense_receipts/default_sample.jpg");
        //     var response = await _mindeeClientV2.EnqueueAndParseAsync(inputSource, "VALID-MODEL-UUID");
        //     Assert.NotNull(response);
        //     Assert.Equal("success", response.ApiRequest.Status);
        //     Assert.Equal(201, response.ApiRequest.StatusCode);
        //     Assert.NotNull(response.Document.Inference);
        //     Assert.NotNull(response.Document.Inference.Prediction);
        //     Assert.Single(response.Document.Inference.Pages);
        //     Assert.Null(response.Document.Inference.Pages.First().Extras);
        // }
    }
}
