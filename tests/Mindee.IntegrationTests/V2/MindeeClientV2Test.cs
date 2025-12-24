using Mindee.Exceptions;
using Mindee.Input;
using Mindee.Parsing.V2;
using Mindee.Parsing.V2.Field;

namespace Mindee.IntegrationTests.V2
{
    [Trait("Category", "V2")]
    [Trait("Category", "Integration")]
    public class MindeeClientV2Test
    {
        private readonly MindeeClientV2 _mindeeClientV2;
        private readonly string? _findocModelId;

        public MindeeClientV2Test()
        {
            string? apiKey = Environment.GetEnvironmentVariable("MindeeV2__ApiKey");
            _mindeeClientV2 = TestingUtilities.GetOrGenerateMindeeClientV2(apiKey);
            _findocModelId = Environment.GetEnvironmentVariable("MindeeV2__Findoc__Model__Id");
        }

        private static void AssertActiveOptions(
            InferenceActiveOptions activeOptions, bool rawText, bool polygon, bool confidence, bool rag,
            bool textContext, bool dataSchemaReplace)
        {
            Assert.NotNull(activeOptions);
            Assert.Equal(rag, activeOptions.Rag);
            Assert.Equal(polygon, activeOptions.Polygon);
            Assert.Equal(confidence, activeOptions.Confidence);
            Assert.Equal(rawText, activeOptions.RawText);
            Assert.Equal(textContext, activeOptions.TextContext);
            Assert.Equal(dataSchemaReplace, activeOptions.DataSchema.Replace);
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
                Constants.RootDir + "file_types/pdf/multipage_cut-2.pdf");
            var inferenceParams = new InferenceParameters(
                modelId: _findocModelId,
                rag: false,
                rawText: rawText,
                polygon: polygon,
                confidence: confidence);

            InferenceResponse response = await _mindeeClientV2.EnqueueAndGetInferenceAsync(
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
                response.Inference.ActiveOptions, rawText, polygon, confidence, false, false, false);

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
            // the server sometimes returns "null"
            // Assert.Null(supplierName.Value);

            Assert.NotNull(fields["taxes"]);
            ListField taxes = fields["taxes"].ListField;
            // the server sometimes returns a list of empty objects
            // Assert.Empty(taxes.ObjectItems);

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
                Constants.V1ProductDir + "financial_document/default_sample.jpg");
            var inferenceParams = new InferenceParameters(
                modelId: _findocModelId,
                textContext: "this is an invoice.");

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
                response.Inference.ActiveOptions, false, false, false, false, true, false);

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
        public async Task FailedWebhook_Retrieve_Job_MustSucceed()
        {
            string? webhookId = Environment.GetEnvironmentVariable("MindeeV2__Failure__Webhook__Id");

            var inputSource = new LocalInputSource(Constants.RootDir + "file_types/pdf/multipage_cut-1.pdf");
            var inferenceParams = new InferenceParameters(
                modelId: _findocModelId, webhookIds: new List<string?> { webhookId });

            JobResponse enqueueResponse = await _mindeeClientV2.EnqueueInferenceAsync(inputSource, inferenceParams);
            Assert.NotNull(enqueueResponse);
            Assert.NotNull(enqueueResponse.Job);
            Assert.NotNull(enqueueResponse.Job.Webhooks);

            string jobId = enqueueResponse.Job.Id;
            Assert.NotNull(jobId);

            Thread.Sleep(200);

            JobResponse jobResponse = await _mindeeClientV2.GetJobAsync(jobId);
            Assert.NotNull(jobResponse);

            Job job = jobResponse.Job;
            Assert.Equal(_findocModelId, job.ModelId);
            Assert.NotNull(job.Webhooks);

            JobWebhook webhook = job.Webhooks.First();
            Assert.NotNull(webhook);
            Assert.Equal(webhookId, webhook.Id);
            Assert.Equal("Processing", webhook.Status);

            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(1000);

                JobResponse loopJobResponse = await _mindeeClientV2.GetJobAsync(jobId);
                JobWebhook loopWebhook = loopJobResponse.Job.Webhooks.First();
                Assert.NotNull(loopWebhook);
                Assert.Equal(webhookId, loopWebhook.Id);

                if (loopWebhook.Status != "Failed")
                    continue;

                ErrorResponse error = loopWebhook.Error;
                Assert.NotNull(error);
                Assert.True(error.Status >= 400);

                return;
            }

            throw new Exception("Did not receive a failed webhook.");
        }

        [Fact]
        public async Task Invalid_Model_MustThrowError()
        {
            var inputSource = new LocalInputSource(Constants.RootDir + "file_types/pdf/multipage_cut-2.pdf");
            var inferenceParams = new InferenceParameters(
                modelId: "INVALID MODEL ID",
                textContext: "hello my name is mud");
            var ex = await Assert.ThrowsAsync<MindeeHttpExceptionV2>(() =>
                _mindeeClientV2.EnqueueInferenceAsync(inputSource, inferenceParams));
            Assert.Equal(422, ex.Status);
            Assert.StartsWith("422-", ex.Code);
        }

        [Fact(Skip = "Temporarily disabled due to server-side errors")]
        public async Task NotFound_Job_MustThrowError()
        {
            var ex = await Assert.ThrowsAsync<MindeeHttpExceptionV2>(() =>
                _mindeeClientV2.GetJobAsync("fc405e37-4ba4-4d03-aeba-533a8d1f0f21"));
            Assert.Equal(404, ex.Status);
            Assert.StartsWith("404-", ex.Code);
        }

        [Fact]
        public async Task NotFound_Inference_MustThrowError()
        {
            var ex = await Assert.ThrowsAsync<MindeeHttpExceptionV2>(() =>
                _mindeeClientV2.GetInferenceAsync("fc405e37-4ba4-4d03-aeba-533a8d1f0f21"));
            Assert.Equal(404, ex.Status);
            Assert.StartsWith("404-", ex.Code);
        }

        [Fact]
        public async Task Url_InputSource_MustNotRaiseErrors()
        {
            var url = Environment.GetEnvironmentVariable("MindeeV2__Blank__Pdf__Url");
            Assert.False(string.IsNullOrWhiteSpace(url),
                "Environment variable MindeeV2__Blank__Pdf__Url must be set and contain a valid URL.");

            var inputSource = new UrlInputSource(new Uri(url!));
            var inferenceParams = new InferenceParameters(modelId: _findocModelId);
            var response = await _mindeeClientV2.EnqueueAndGetInferenceAsync(inputSource, inferenceParams);

            Assert.NotNull(response);
            Assert.NotNull(response.Inference);
        }

        [Fact]
        public async Task DataSchemaOverride_OverridesTheDataSchemaSuccessfully()
        {
            var inputSource = new LocalInputSource(
                Constants.RootDir + "file_types/pdf/blank_1.pdf");
            string dataSchemaContents =
                File.ReadAllText(Constants.V2RootDir + "inference/data_schema_replace_param.json");
            var inferenceParams = new InferenceParameters(
                modelId: _findocModelId,
                dataSchema: dataSchemaContents);

            var response = await _mindeeClientV2.EnqueueAndGetInferenceAsync(
                inputSource, inferenceParams);
            Assert.NotNull(response);
            Assert.NotNull(response.Inference);

            Assert.NotNull(response.Inference.Model);
            Assert.Equal(_findocModelId, response.Inference.Model.Id);

            InferenceFile file = response.Inference.File;
            Assert.NotNull(file);
            Assert.Equal("blank_1.pdf", file.Name);
            Assert.Equal(1, file.PageCount);

            AssertActiveOptions(
                response.Inference.ActiveOptions,
                false,
                false,
                false,
                false,
                false,
                true
            );

            Assert.NotNull(response.Inference.Result);

            InferenceFields fields = response.Inference.Result.Fields;
            Assert.NotNull(fields);
            Assert.Equal("a test value", fields["test_replace"].ToString());
        }
    }
}
