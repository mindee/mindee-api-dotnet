using Mindee.Http;
using Mindee.Input;
using Mindee.Parsing.V2;
using Moq;

namespace Mindee.UnitTests.V2
{
    [Trait("Category", "V2")]
    [Trait("Category", "Mindee client")]
    public class MindeeClientTest
    {
        private MindeeClientV2 MakeCustomMindeeClientV2(Mock<HttpApiV2> predictable)
        {
            predictable.Setup(
                x => x.ReqPostEnqueueInferenceAsync(It.IsAny<InferencePostParameters>())
                ).ReturnsAsync(new JobResponse());

            predictable.Setup(
                x => x.ReqGetInferenceAsync(It.IsAny<string>())
                ).ReturnsAsync(new InferenceResponse());

            predictable.Setup(
                x => x.ReqGetJobAsync(It.IsAny<string>())
            ).ReturnsAsync(new JobResponse());

            return new MindeeClientV2(httpApi: predictable.Object);
        }

        [Fact]
        public async Task Enqueue_Post_Async()
        {
            var predictable = new Mock<HttpApiV2>();
            var mindeeClient = MakeCustomMindeeClientV2(predictable);

            var inputSource = new LocalInputSource(
                new FileInfo(fileName: Constants.RootDir + "file_types/pdf/blank_1.pdf"));
            var inferenceParams = new InferenceParameters(
                modelId: "dummy-model-id",
                rawText: false,
                textContext: "Hello my name is mud.");
            var response = await mindeeClient.EnqueueInferenceAsync(
                inputSource, inferenceParams);

            Assert.NotNull(response);
            predictable.Verify(
                p => p.ReqPostEnqueueInferenceAsync(It.IsAny<InferencePostParameters>()),
                Times.AtMostOnce());
        }

        [Fact]
        public async Task Document_GetInference_Async()
        {
            var predictable = new Mock<HttpApiV2>();
            var mindeeClient = MakeCustomMindeeClientV2(predictable);
            var response = await mindeeClient.GetInferenceAsync("dummy-id");
            Assert.NotNull(response);

            predictable.Verify(
                p => p.ReqGetInferenceAsync(It.IsAny<string>()),
                Times.AtMostOnce());
        }
        // NOTE: The EnqueueAndGetInferenceAsync() method is covered in the integration tests.

        [Fact]
        public async Task Document_GetJob_Async()
        {
            var predictable = new Mock<HttpApiV2>();
            var mindeeClient = MakeCustomMindeeClientV2(predictable);
            var response = await mindeeClient.GetJobAsync("dummy-id");
            Assert.NotNull(response);

            predictable.Verify(
                p => p.ReqGetJobAsync(It.IsAny<string>()),
                Times.AtMostOnce());
        }

        [Fact]
        public void Inference_LoadsLocally()
        {
            var localResponse = new LocalResponse(
                new FileInfo(Constants.V2RootDir + "products/financial_document/complete.json"));
            var locallyLoadedResponse = localResponse.DeserializeResponse<InferenceResponse>();
            Assert.NotNull(locallyLoadedResponse);
            Assert.Equal("12345678-1234-1234-1234-123456789abc", locallyLoadedResponse.Inference.Model.Id);
            Assert.Equal("John Smith", locallyLoadedResponse.Inference.Result.Fields["supplier_name"].SimpleField.Value);
        }
    }

}
