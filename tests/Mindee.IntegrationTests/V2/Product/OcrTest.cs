using Mindee.Input;
using Mindee.V2;
using Mindee.V2.Product.Ocr;
using Mindee.V2.Product.Ocr.Params;

namespace Mindee.IntegrationTests.V2.Product
{
    [Trait("Category", "V2")]
    [Trait("Category", "Integration")]
    public class OcrTest
    {
        private readonly string? _ocrModelId;
        private readonly Client _client;

        public OcrTest()
        {
            var apiKey = Environment.GetEnvironmentVariable("MindeeV2__ApiKey");
            _client = TestingUtilities.GetOrGenerateMindeeClientV2(apiKey);
            _ocrModelId = Environment.GetEnvironmentVariable("MindeeV2__Ocr__Model__Id");
        }

        [Fact(Timeout = 180000)]
        public async Task Ocr_DefaultSample_MustSucceed()
        {
            var inputSource = new LocalInputSource(
                Constants.V2ProductDir + "ocr/default_sample.jpg");
            var ocrParameters = new OcrParameters(_ocrModelId);

            var response = await _client.EnqueueAndGetResultAsync<OcrResponse>(
                inputSource, ocrParameters);

            Assert.NotNull(response);
            Assert.NotNull(response.Inference);

            var file = response.Inference.File;
            Assert.NotNull(file);
            Assert.Equal("default_sample.jpg", file.Name);

            var result = response.Inference.Result;
            Assert.NotNull(result);

            var pages = result.Pages;
            Assert.Single(pages);
        }
    }
}
