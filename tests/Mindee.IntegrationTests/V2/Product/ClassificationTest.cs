using Mindee.Input;
using Mindee.V2;
using Mindee.V2.Product.Classification;
using Mindee.V2.Product.Classification.Params;

namespace Mindee.IntegrationTests.V2.Product
{
    [Trait("Category", "V2")]
    [Trait("Category", "Integration")]
    public class ClassificationTest
    {
        private readonly string? _classificationModelId;
        private readonly Client _client;

        public ClassificationTest()
        {
            var apiKey = Environment.GetEnvironmentVariable("MindeeV2__ApiKey");
            _client = TestingUtilities.GetOrGenerateMindeeClientV2(apiKey);
            _classificationModelId = Environment.GetEnvironmentVariable("MindeeV2__Classification__Model__Id");
        }

        [Fact(Timeout = 180000)]
        public async Task Classification_DefaultSample_MustSucceed()
        {
            var inputSource = new LocalInputSource(
                Constants.V2ProductDir + "classification/default_invoice.jpg");
            var classificationParameters = new ClassificationParameters(_classificationModelId);


            var response = await _client.EnqueueAndGetResultAsync<ClassificationResponse>(
                inputSource, classificationParameters);

            Assert.NotNull(response);
            Assert.NotNull(response.Inference);

            var file = response.Inference.File;
            Assert.NotNull(file);
            Assert.Equal("default_invoice.jpg", file.Name);

            var result = response.Inference.Result;
            Assert.NotNull(result);

            var classifications = result.Classification;
            Assert.NotNull(classifications);
        }
    }
}
