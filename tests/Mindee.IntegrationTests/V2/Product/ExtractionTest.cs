using Mindee.Input;
using Mindee.V2;
using Mindee.V2.Product.Extraction;
using Mindee.V2.Product.Extraction.Params;

namespace Mindee.IntegrationTests.V2.Product
{
    [Trait("Category", "V2")]
    [Trait("Category", "Integration")]
    public class ExtractionTest
    {
        private readonly string? _extractionModelId;
        private readonly Client _client;

        public ExtractionTest()
        {
            var apiKey = Environment.GetEnvironmentVariable("MindeeV2__ApiKey");
            _client = TestingUtilities.GetOrGenerateMindeeClientV2(apiKey);
            _extractionModelId = Environment.GetEnvironmentVariable("MindeeV2__Findoc__Model__Id");
        }

        [Fact(Timeout = 180000)]
        public async Task Extraction_DefaultSample_MustSucceed()
        {
            var inputSource = new LocalInputSource(
                Constants.V2ProductDir + "extraction/financial_document/default_sample.jpg");
            var extractionParameters = new ExtractionParameters(_extractionModelId);


            var response = await _client.EnqueueAndGetResultAsync<ExtractionResponse>(
                inputSource, extractionParameters);

            Assert.NotNull(response);
            Assert.NotNull(response.Inference);

            var file = response.Inference.File;
            Assert.NotNull(file);
            Assert.Equal("default_sample.jpg", file.Name);

            var result = response.Inference.Result;
            Assert.NotNull(result);

            var fields = result.Fields;
            Assert.NotNull(fields);
        }
    }
}
