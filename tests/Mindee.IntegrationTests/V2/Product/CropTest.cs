using Mindee.Input;
using Mindee.V2;
using Mindee.V2.Product.Crop;
using Mindee.V2.Product.Crop.Params;

namespace Mindee.IntegrationTests.V2.Product
{
    [Trait("Category", "V2")]
    [Trait("Category", "Integration")]
    public class CropTest
    {
        private readonly string? _cropModelId;
        private readonly Client _client;

        public CropTest()
        {
            var apiKey = Environment.GetEnvironmentVariable("MindeeV2__ApiKey");
            _client = TestingUtilities.GetOrGenerateMindeeClientV2(apiKey);
            _cropModelId = Environment.GetEnvironmentVariable("MindeeV2__Crop__Model__Id");
        }

        [Fact(Timeout = 180000)]
        public async Task Crop_DefaultSample_MustSucceed()
        {
            var inputSource = new LocalInputSource(
                Constants.V2ProductDir + "crop/default_sample.jpg");
            var productParams = new CropParameters(_cropModelId);

            var response = await _client.EnqueueAndGetResultAsync<CropResponse>(
                inputSource, productParams);

            Assert.NotNull(response);
            Assert.NotNull(response.Inference);

            var file = response.Inference.File;
            Assert.NotNull(file);
            Assert.Equal("default_sample.jpg", file.Name);

            var result = response.Inference.Result;
            Assert.NotNull(result);

            var crops = result.Crops;
            Assert.Equal(2, crops.Count);
            foreach (var crop in crops)
            {
                Assert.NotNull(crop.ObjectType);
                Assert.NotNull(crop.Location);
            }
        }
    }
}
