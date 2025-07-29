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
            var predictOptions = new InferenceParameters(modelId: _findocModelId);
            var response = await _mindeeClientV2.EnqueueAndGetInferenceAsync(inputSource, predictOptions);
            Assert.NotNull(response);
            Assert.NotNull(response.Inference);
            Assert.NotNull(response.Inference.File);
            Assert.Equal("multipage_cut-2.pdf", response.Inference.File.Name);
            Assert.NotNull(response.Inference.Model);
            Assert.Equal(_findocModelId, response.Inference.Model.Id);
            Assert.NotNull(response.Inference.Result);
            Assert.Null(response.Inference.Result.Options);
        }

        [Fact]
        public async Task Parse_File_Filled_SinglePage_MustSucceed()
        {
            var inputSource = new LocalInputSource(
                "Resources/products/financial_document/default_sample.jpg");
            var inferenceParams = new InferenceParameters(modelId: _findocModelId);
            var response = await _mindeeClientV2.EnqueueAndGetInferenceAsync(inputSource, inferenceParams);
            Assert.NotNull(response);
            Assert.NotNull(response.Inference);
            Assert.NotNull(response.Inference.File);
            Assert.Equal("default_sample.jpg", response.Inference.File.Name);
            Assert.NotNull(response.Inference.Model);
            Assert.Equal(_findocModelId, response.Inference.Model.Id);
            Assert.NotNull(response.Inference.Result);
            Assert.NotNull(response.Inference.Result.Fields);
            Assert.NotNull(response.Inference.Result.Fields["supplier_name"]);
            Assert.Equal("John Smith", response.Inference.Result.Fields["supplier_name"].SimpleField.Value);
        }

        [Fact]
        public async Task Invalid_Model_MustThrowError()
        {
            var inputSource = new LocalInputSource("Resources/file_types/pdf/multipage_cut-2.pdf");
            var predictOptions = new InferenceParameters("INVALID MODEL ID");
            var ex = await Assert.ThrowsAsync<MindeeHttpExceptionV2>(
                () => _mindeeClientV2.EnqueueInferenceAsync(inputSource, predictOptions));
            Assert.Equal(422, ex.Status);
        }

        [Fact]
        public async Task NotFound_Job_MustThrowError()
        {
            var ex = await Assert.ThrowsAsync<MindeeHttpExceptionV2>(
                () => _mindeeClientV2.GetJobAsync("fc405e37-4ba4-4d03-aeba-533a8d1f0f21"));
            Assert.Equal(404, ex.Status);
        }

        [Fact]
        public async Task NotFound_Inference_MustThrowError()
        {
            var ex = await Assert.ThrowsAsync<MindeeHttpExceptionV2>(
                () => _mindeeClientV2.GetInferenceAsync("fc405e37-4ba4-4d03-aeba-533a8d1f0f21"));
            Assert.Equal(404, ex.Status);
        }

        [Fact]
        public async Task Url_InputSource_MustNotRaiseErrors()
        {
            var url = Environment.GetEnvironmentVariable("Mindee__V2__Se__Tests__Blank__Pdf__Url");
            Assert.False(string.IsNullOrWhiteSpace(url),
                "Environment variable Mindee__V2__Se__Tests__Blank__Pdf__Url must be set and contain a valid URL.");

            var inputSource = new UrlInputSource(new Uri(url!));
            var inferenceParams = new InferenceParameters(modelId: _findocModelId);
            var response = await _mindeeClientV2.EnqueueAndGetInferenceAsync(inputSource, inferenceParams);

            Assert.NotNull(response);
            Assert.NotNull(response.Inference);
        }
    }
}
