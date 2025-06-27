using Microsoft.Extensions.DependencyInjection;
using Mindee.Exceptions;
using Mindee.Extensions.DependencyInjection;
using Mindee.Input;

namespace Mindee.IntegrationTests
{
    [Trait("Category", "Integration")]
    [Trait("Category", "V2")]
    public class MindeeClientV2Test
    {
        private readonly MindeeClientV2 _mindeeClientV2;
        private readonly string? _findocModelId;

        public MindeeClientV2Test()
        {
            var apiKey = Environment.GetEnvironmentVariable("MindeeV2__ApiKey");
            _mindeeClientV2 = TestingUtilities.GetOrGenerateMindeeClientV2(apiKey);
            _findocModelId = Environment.GetEnvironmentVariable("MindeeV2__Findoc__Model__Id");
        }

        [Fact]
        public async Task Parse_File_Standard_MultiplePages_MustSucceed()
        {
            var inputSource = new LocalInputSource("Resources/file_types/pdf/multipage_cut-2.pdf");
            var predictOptions = new InferenceOptionsV2(modelId: _findocModelId);
            var response = await _mindeeClientV2.EnqueueAndParseAsync(inputSource, predictOptions);
            Assert.NotNull(response);
            Assert.NotNull(response.Inference);
            Assert.NotNull(response.Inference.ToString());
            // flaky, sometimes the server doesn't return an options key
            // Assert.Null(response.Inference.Result.Options);
        }

        [Fact(Skip = "URL sources aren't supported yet.")]
        public async Task Parse_Url_Standard_SinglePage_MustSucceed()
        {
            var inputSource =
                new UrlInputSource(
                    "https://raw.githubusercontent.com/mindee/client-lib-test-data/main/products/expense_receipts/default_sample.jpg");
            var predictOptions = new InferenceOptionsV2(modelId: _findocModelId);
            var response = await _mindeeClientV2.EnqueueAndParseAsync(inputSource, predictOptions);
            Assert.NotNull(response);
            Assert.NotNull(response.Inference);
            Assert.Null(response.Inference.Result.Options);
        }

        [Fact]
        public async Task Invalid_UUID_MustThrowError422()
        {
            var inputSource = new LocalInputSource("Resources/file_types/pdf/multipage_cut-2.pdf");
            var predictOptions = new InferenceOptionsV2("INVALID MODEL ID");
            var ex = await Assert.ThrowsAsync<MindeeHttpExceptionV2>(
                () => _mindeeClientV2.EnqueueAsync(inputSource, predictOptions));
            Assert.Equal(422, ex.Status);
        }
    }
}
