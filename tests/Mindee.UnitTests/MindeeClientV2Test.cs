using Microsoft.Extensions.Logging.Abstractions;
using Mindee.Http;
using Mindee.Input;
using Mindee.Parsing.V2;
using Mindee.Pdf;
using Moq;

namespace Mindee.UnitTests
{
    [Trait("Category", "Mindee client")]
    [Trait("Category", "V2")]
    public class MindeeClientV2Test
    {
        private IPdfOperation GetDefaultPdfOperation() => new DocNetApi(new NullLogger<DocNetApi>());
        private MindeeClientV2 MakeCustomMindeeClientV2(Mock<HttpApiV2> predictable)
        {
            predictable.Setup(x => x.ReqPostEnqueueInferenceAsync(
                    It.IsAny<PredictParameterV2>()
                ))
                .ReturnsAsync(new JobResponse());
            predictable.Setup(x => x.ReqGetInference(
                It.IsAny<string>()
            )).ReturnsAsync(new InferenceResponse());
            return new MindeeClientV2(GetDefaultPdfOperation(), predictable.Object);
        }

        [Fact]
        public async Task Enqueue_Post_Async()
        {
            var predictable = new Mock<HttpApiV2>();
            var mindeeClient = MakeCustomMindeeClientV2(predictable);

            var inputSource = new LocalInputSource(new FileInfo("Resources/file_types/pdf/blank_1.pdf"));
            var response = await mindeeClient.EnqueueInferenceAsync(
                inputSource, new InferenceParameters("dummy-model-id"));

            Assert.NotNull(response);
            predictable.Verify(p => p.ReqPostEnqueueInferenceAsync(
                    It.IsAny<PredictParameterV2>())
                , Times.AtMostOnce());
        }

        [Fact]
        public async Task Document_GetQueued_Async()
        {
            var predictable = new Mock<HttpApiV2>();
            var mindeeClient = MakeCustomMindeeClientV2(predictable);
            var response = await mindeeClient.GetJobAsync(
                "dummy-id");

            Assert.NotNull(response);
            predictable.Verify(p => p.ReqGetInference(
                    It.IsAny<string>())
                , Times.AtMostOnce());
        }
        // NOTE: The EnqueueAndGetInferenceAsync() method is covered in the integration tests.

        [Fact]
        public void Inference_LoadsLocally()
        {
            var mindeeClient = new MindeeClientV2("dummy");
            var localResponse = new LocalResponse(new FileInfo("Resources/v2/products/financial_document/complete.json"));
            var locallyLoadedResponse = mindeeClient.LoadInference(localResponse);
            Assert.NotNull(locallyLoadedResponse);
            Assert.Equal("12345678-1234-1234-1234-123456789abc", locallyLoadedResponse.Inference.ResultModel.Id);
            Assert.Equal("John Smith", locallyLoadedResponse.Inference.Result.Fields["supplier_name"].SimpleField.Value);
        }
    }

}
