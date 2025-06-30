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
        private MindeeClientV2 MakeCustomMindeeClientV2(Mock<IHttpApiV2> predictable)
        {
            predictable.Setup(x => x.EnqueuePostAsync(
                    It.IsAny<PredictParameterV2>()
                ))
                .ReturnsAsync(new AsyncJobResponse());
            predictable.Setup(x => x.DocumentQueueGetAsync(
                It.IsAny<string>()
            )).ReturnsAsync(new AsyncInferenceResponse());
            return new MindeeClientV2(GetDefaultPdfOperation(), predictable.Object);
        }

        [Fact]
        public async Task Enqueue_Post_Async()
        {
            var predictable = new Mock<IHttpApiV2>();
            var mindeeClient = MakeCustomMindeeClientV2(predictable);

            var inputSource = new LocalInputSource(new FileInfo("Resources/file_types/pdf/blank_1.pdf"));
            var response = await mindeeClient.EnqueueAsync(
                inputSource, new InferenceOptionsV2("dummy-model-id"));

            Assert.NotNull(response);
            predictable.Verify(p => p.EnqueuePostAsync(
                    It.IsAny<PredictParameterV2>())
                , Times.AtMostOnce());
        }

        [Fact]
        public async Task Document_GetQueued_Async()
        {
            var predictable = new Mock<IHttpApiV2>();
            var mindeeClient = MakeCustomMindeeClientV2(predictable);
            var response = await mindeeClient.ParseQueuedAsync(
                "dummy-id");

            Assert.NotNull(response);
            predictable.Verify(p => p.DocumentQueueGetAsync(
                    It.IsAny<string>())
                , Times.AtMostOnce());
        }
        // NOTE: The EnqueueAndParseAsync() method is covered in the integration tests.
    }

}
