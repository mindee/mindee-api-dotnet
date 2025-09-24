using Mindee.Exceptions;
using Mindee.Input;
using Mindee.Parsing.V2;
using Mindee.Parsing.V2.Field;

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

        private void AssertActiveOptions(
            InferenceActiveOptions activeOptions, bool rawText, bool polygon, bool confidence, bool rag)
        {
            Assert.NotNull(activeOptions);
            Assert.Equal(activeOptions.Rag, rag);
            Assert.Equal(activeOptions.Polygon, polygon);
            Assert.Equal(activeOptions.Confidence, confidence);
            Assert.Equal(activeOptions.RawText, rawText);
        }

        [Theory]
        [InlineData(false, false, false)]
        [InlineData(true, false, false)]
        [InlineData(false, true, false)]
        [InlineData(true, true, false)]
        [InlineData(false, false, true)]
        [InlineData(true, false, true)]
        [InlineData(false, true, true)]
        [InlineData(true, true, true)]
        public async Task Parse_File_Empty_MultiplePages_ParameterVariations_MustSucceed(
            bool rawText, bool polygon, bool confidence)
        {
            var inputSource = new LocalInputSource(
                "Resources/file_types/pdf/multipage_cut-2.pdf");
            var inferenceParams = new InferenceParameters(
                modelId: _findocModelId,
                rag: false,
                rawText: rawText,
                polygon: polygon,
                confidence: confidence);

            var response = await _mindeeClientV2.EnqueueAndGetInferenceAsync(
                inputSource, inferenceParams);
            Assert.NotNull(response);
            Assert.NotNull(response.Inference);

            Assert.NotNull(response.Inference.Model);
            Assert.Equal(_findocModelId, response.Inference.Model.Id);

            InferenceFile file = response.Inference.File;
            Assert.NotNull(file);
            Assert.Equal("multipage_cut-2.pdf", file.Name);
            Assert.Equal(2, file.PageCount);

            AssertActiveOptions(
                response.Inference.ActiveOptions, rawText, polygon, confidence, false);

            InferenceResult result = response.Inference.Result;
            Assert.NotNull(result);

            RawText resultRawText = result.RawText;
            if (rawText)
            {
                Assert.NotNull(resultRawText);
                Assert.Equal(2, resultRawText.Pages.Count);
            }
            else
                Assert.Null(resultRawText);

            InferenceFields fields = response.Inference.Result.Fields;
            Assert.NotNull(fields);
            Assert.NotNull(fields["supplier_name"]);
            SimpleField supplierName = fields["supplier_name"].SimpleField;
            Assert.Null(supplierName.Value);

            Assert.NotNull(fields["taxes"]);
            ListField taxes = fields["taxes"].ListField;
            Assert.Empty(taxes.ObjectItems);

            Assert.Empty(supplierName.Locations);
            if (confidence)
            {
                Assert.NotNull(supplierName.Confidence);
                Assert.NotNull(taxes.Confidence);
            }
            else
            {
                Assert.Null(supplierName.Confidence);
                Assert.Null(taxes.Confidence);
            }
        }

        [Fact]
        public async Task Parse_File_Filled_SinglePage_MustSucceed()
        {
            var inputSource = new LocalInputSource(
                "Resources/products/financial_document/default_sample.jpg");
            var inferenceParams = new InferenceParameters(
                modelId: _findocModelId);

            var response = await _mindeeClientV2.EnqueueAndGetInferenceAsync(
                inputSource, inferenceParams);
            Assert.NotNull(response);
            Assert.NotNull(response.Inference);

            Assert.NotNull(response.Inference.Model);
            Assert.Equal(_findocModelId, response.Inference.Model.Id);

            InferenceFile file = response.Inference.File;
            Assert.NotNull(file);
            Assert.Equal("default_sample.jpg", file.Name);
            Assert.Equal(1, file.PageCount);

            AssertActiveOptions(
                response.Inference.ActiveOptions, false, false, false, false);

            Assert.NotNull(response.Inference.Result);

            InferenceFields fields = response.Inference.Result.Fields;
            Assert.NotNull(fields);
            Assert.NotNull(fields["supplier_name"]);
            SimpleField supplierName = fields["supplier_name"].SimpleField;
            Assert.Equal("John Smith", supplierName.Value);
            Assert.Null(supplierName.Confidence);
            Assert.Empty(supplierName.Locations);
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
