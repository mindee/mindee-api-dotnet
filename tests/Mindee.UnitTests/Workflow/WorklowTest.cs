using System.Text.Json;
using Mindee.Http;
using Mindee.Input;
using Mindee.Parsing.Common;
using Mindee.Pdf;
using Mindee.Product.Generated;
using Moq;

namespace Mindee.UnitTests.Workflow
{
    [Trait("Category", "Workflow")]
    public abstract class WorklowTest
    {
        private readonly MindeeClient client;
        private readonly Mock<MindeeClient> mockedClient;
        private readonly Mock<IHttpApi> mindeeApi;

        protected WorklowTest()
        {
            mindeeApi = new Mock<IHttpApi>();
            Mock<IPdfOperation> pdfOperation = new Mock<IPdfOperation>();
            client = new MindeeClient(pdfOperation.Object, mindeeApi.Object);
            mockedClient = new Mock<MindeeClient>();
        }

        [Fact]
        public async Task GivenAWorkflowMockFileShouldReturnAValidWorkflowObject()
        {
            // Arrange
            var file = new FileInfo("src/test/resources/file_types/pdf/blank_1.pdf");
            var workflowResponse = new WorkflowResponse<GeneratedV1> { Execution = new Execution<GeneratedV1>(), ApiRequest = null };

            mindeeApi.Setup(api => api.ExecutionQueuePost<GeneratedV1>(
                    It.IsAny<string>(),
                    It.IsAny<PredictParameter>()))
                .ReturnsAsync(workflowResponse);

            // Act
            var execution = await client.ExecuteWorkflowAsync<GeneratedV1>(
                "",
                new LocalInputSource(file));

            // Assert
            Assert.NotNull(execution);
            mindeeApi.Verify(api => api.ExecutionQueuePost<GeneratedV1>(
                It.IsAny<string>(),
                It.IsAny<PredictParameter>()), Times.Once);
        }

        [Fact]
        public async Task SendingADocumentToAnExecutionShouldDeserializeResponseCorrectly()
        {
            // Arrange
            var jsonFile = File.ReadAllText("src/test/resources/workflows/success.json");
            var mockResponse = JsonSerializer.Deserialize<WorkflowResponse<GeneratedV1>>(jsonFile);

            mockedClient.Setup(mindeeClient => mindeeClient.ExecuteWorkflowAsync<GeneratedV1>(
                    It.IsAny<string>(),
                    It.IsAny<LocalInputSource>(),
                    It.IsAny<PredictOptions>(),
                    It.IsAny<PageOptions>()))
                .ReturnsAsync(mockResponse);

            string workflowId = "07ebf237-ff27-4eee-b6a2-425df4a5cca6";
            string filePath = "src/test/resources/products/financial_document/default_sample.jpg";
            var inputSource = new LocalInputSource(filePath);

            // Act
            var response = await mockedClient.Object.ExecuteWorkflowAsync<GeneratedV1>(workflowId, inputSource);

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(response.ApiRequest);
            Assert.Null(response.Execution.BatchName);
            Assert.Null(response.Execution.CreatedAt);
            Assert.Null(response.Execution.File.Alias);
            Assert.Equal("default_sample.jpg", response.Execution.File.Name);
            Assert.Equal("8c75c035-e083-4e77-ba3b-7c3598bd1d8a", response.Execution.Id);
            Assert.Null(response.Execution.Inference);
            Assert.Equal("medium", response.Execution.Priority);
            Assert.Null(response.Execution.ReviewedAt);
            Assert.Null(response.Execution.ReviewedPrediction);
            Assert.Equal("processing", response.Execution.Status);
            Assert.Equal("manual", response.Execution.Type);
            Assert.Equal("2024-11-13T13:02:31.699190",
                response.Execution.UploadedAt?.ToString("yyyy-MM-ddTHH:mm:ss.ffffff"));
            Assert.Equal(workflowId, response.Execution.WorkflowId);

            mockedClient.Verify(mindeeClient => mindeeClient.ExecuteWorkflowAsync<GeneratedV1>(
                workflowId,
                It.Is<LocalInputSource>(source => source.Filename == inputSource.Filename),
                It.IsAny<PredictOptions>(),
                It.IsAny<PageOptions>()), Times.Once);
        }

        [Fact]
        public async Task SendingADocumentToAnExecutionWithPriorityAndAliasShouldDeserializeResponseCorrectly()
        {
            // Arrange
            var jsonFile = File.ReadAllText("src/test/resources/workflows/success_low_priority.json");
            var mockResponse = JsonSerializer.Deserialize<WorkflowResponse<GeneratedV1>>(jsonFile);

            mockedClient.Setup(mindeeClient => mindeeClient.ExecuteWorkflowAsync<GeneratedV1>(
                    It.IsAny<string>(),
                    It.IsAny<LocalInputSource>(),
                    It.IsAny<PredictOptions>(),
                    It.IsAny<PageOptions>()))
                .ReturnsAsync(mockResponse);

            string workflowId = "07ebf237-ff27-4eee-b6a2-425df4a5cca6";
            string filePath = "src/test/resources/products/financial_document/default_sample.jpg";
            var inputSource = new LocalInputSource(filePath);

            // Act
            var response = await mockedClient.Object.ExecuteWorkflowAsync<GeneratedV1>(workflowId, inputSource);

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(response.ApiRequest);
            Assert.Null(response.Execution.BatchName);
            Assert.Null(response.Execution.CreatedAt);
            Assert.Equal("low-priority-sample-test", response.Execution.File.Alias);
            Assert.Equal("default_sample.jpg", response.Execution.File.Name);
            Assert.Equal("b743e123-e18c-4b62-8a07-811a4f72afd3", response.Execution.Id);
            Assert.Null(response.Execution.Inference);
            Assert.Equal("low", response.Execution.Priority);
            Assert.Null(response.Execution.ReviewedAt);
            Assert.Null(response.Execution.ReviewedPrediction);
            Assert.Equal("processing", response.Execution.Status);
            Assert.Equal("manual", response.Execution.Type);
            Assert.Equal("2024-11-13T13:17:01.315179",
                response.Execution.UploadedAt?.ToString("yyyy-MM-ddTHH:mm:ss.ffffff"));
            Assert.Equal(workflowId, response.Execution.WorkflowId);

            mockedClient.Verify(mindeeClient => mindeeClient.ExecuteWorkflowAsync<GeneratedV1>(
                workflowId,
                It.Is<LocalInputSource>(source => source.Filename == inputSource.Filename),
                It.IsAny<PredictOptions>(),
                It.IsAny<PageOptions>()), Times.Once);
        }
    }
}
