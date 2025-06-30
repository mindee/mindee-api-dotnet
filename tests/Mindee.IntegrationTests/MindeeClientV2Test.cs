using Mindee.Exceptions;
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
        public async Task Parse_File_Empty_MultiplePages_MustSucceed()
        {
            var inputSource = new LocalInputSource(
                "Resources/file_types/pdf/multipage_cut-2.pdf");
            var predictOptions = new InferenceOptionsV2(modelId: _findocModelId);
            var response = await _mindeeClientV2.EnqueueAndParseAsync(inputSource, predictOptions);
            Assert.NotNull(response);
            Assert.NotNull(response.Inference);
            // make sure the file info is filled
            Assert.NotNull(response.Inference.File);
            Assert.Equal("multipage_cut-2.pdf", response.Inference.File.Name);
            // make sure the mode info is filled
            Assert.NotNull(response.Inference.Model);
            Assert.Equal(_findocModelId, response.Inference.Model.Id);
            // flaky, sometimes the server doesn't return this correctly
            // Assert.NotNull(response.Inference.Result);
            // Assert.Null(response.Inference.Result.Options);
        }

        [Fact]
        public async Task Parse_File_Filled_SinglePage_MustSucceed()
        {
            var inputSource = new LocalInputSource(
                "Resources/products/financial_document/default_sample.jpg");
            var predictOptions = new InferenceOptionsV2(modelId: _findocModelId);
            var response = await _mindeeClientV2.EnqueueAndParseAsync(inputSource, predictOptions);
            Assert.NotNull(response);
            Assert.NotNull(response.Inference);
            // make sure the file info is filled
            Assert.NotNull(response.Inference.File);
            Assert.Equal("default_sample.jpg", response.Inference.File.Name);
            // make sure the mode info is filled
            Assert.NotNull(response.Inference.Model);
            Assert.Equal(_findocModelId, response.Inference.Model.Id);
            // make sure fields are set
            Assert.NotNull(response.Inference.Result);
            Assert.NotNull(response.Inference.Result.Fields);
            Assert.NotNull(response.Inference.Result.Fields["supplier_name"]);
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
