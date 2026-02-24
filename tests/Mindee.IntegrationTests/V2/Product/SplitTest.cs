using Mindee.Input;
using Mindee.V2;
using Mindee.V2.Product.Split;
using Mindee.V2.Product.Split.Params;

namespace Mindee.IntegrationTests.V2.Product
{
    [Trait("Category", "V2")]
    [Trait("Category", "Integration")]
    public class SplitTest
    {
        private readonly string? _splitModelId;
        private readonly Client _client;

        public SplitTest()
        {
            var apiKey = Environment.GetEnvironmentVariable("MindeeV2__ApiKey");
            _client = TestingUtilities.GetOrGenerateMindeeClientV2(apiKey);
            _splitModelId = Environment.GetEnvironmentVariable("MindeeV2__Split__Model__Id");
        }

        [Fact(Timeout = 180000)]
        public async Task Split_DefaultSample_MustSucceed()
        {
            var inputSource = new LocalInputSource(
                Constants.V2RootDir + "products/split/default_sample.pdf");
            var splitParameters = new SplitParameters(_splitModelId);


            var response = await _client.EnqueueAndGetResultAsync<SplitResponse>(
                inputSource, splitParameters);

            Assert.NotNull(response);
            Assert.NotNull(response.Inference);

            var file = response.Inference.File;
            Assert.NotNull(file);
            Assert.Equal("default_sample.pdf", file.Name);

            var result = response.Inference.Result;
            Assert.NotNull(result);

            var splits = result.Splits;
            Assert.NotNull(splits);
            Assert.Equal(2, splits.Count);
        }
    }
}
