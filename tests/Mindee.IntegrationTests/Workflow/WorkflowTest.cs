using Mindee.Http;
using Mindee.Input;
using Mindee.Product.FinancialDocument;
using Mindee.Product.Generated;

namespace Mindee.IntegrationTests.Workflow
{
    [Trait("Category", "Integration tests")]
    public class WorkflowTest
    {
        private readonly MindeeClient client;
        private readonly LocalInputSource inputSource;

        public WorkflowTest()
        {
            var apiKey1 = Environment.GetEnvironmentVariable("Mindee__ApiKey");
            client = TestingUtilities.GetOrGenerateMindeeClient(apiKey1);
            inputSource = new LocalInputSource("Resources/products/financial_document/default_sample.jpg");
        }

        [Fact]
        public async Task Given_AWorkflowIdUpload_ShouldReturnACorrectWorkflowObject()
        {
            string currentDateTime = DateTime.Now.ToString("yyyy-MM-dd-HH:mm:ss");
            var alias = "dotnet-" + currentDateTime;
            WorkflowOptions options = new WorkflowOptions(alias, ExecutionPriority.Low, rag: true);
            var response = await client.ExecuteWorkflowAsync(Environment.GetEnvironmentVariable("Mindee__WorkflowID"),
                inputSource, options);

            Assert.Equal(ExecutionPriority.Low, response.Execution.Priority);
            Assert.Equal(alias, response.Execution.File.Alias);
        }

        [Fact]
        public async Task Given_AWorkflowIdPredictCustom_ShouldPollWithRag()
        {
            CustomEndpoint endpoint = new CustomEndpoint("financial_document", "mindee");
            PredictOptions options = new PredictOptions(
                workflowId: Environment.GetEnvironmentVariable("Mindee__WorkflowID"),
                rag: true
            );
            var response = await client.EnqueueAndParseAsync<GeneratedV1>(
                inputSource,
                endpoint,
                options
            );
            Assert.NotEmpty(response.Document.ToString());
            Assert.NotEmpty(response.Document.Inference.Extras.Rag.MatchingDocumentId);
        }

        [Fact]
        public async Task Given_AWorkflowIdPredictOTS_ShouldPollWithRag()
        {
            PredictOptions options = new PredictOptions(
                workflowId: Environment.GetEnvironmentVariable("Mindee__WorkflowID"),
                rag: true
            );
            var response = await client.EnqueueAndParseAsync<FinancialDocumentV1>(
                inputSource,
                options
            );
            Assert.NotEmpty(response.Document.ToString());
            Assert.NotEmpty(response.Document.Inference.Extras.Rag.MatchingDocumentId);
        }

        [Fact]
        public async Task Given_AWorkflowIdPredictCustom_ShouldPollWithoutRag()
        {
            CustomEndpoint endpoint = new CustomEndpoint("financial_document", "mindee");
            PredictOptions options = new PredictOptions(
                workflowId: Environment.GetEnvironmentVariable("Mindee__WorkflowID"));
            var response = await client.EnqueueAndParseAsync<GeneratedV1>(
                inputSource,
                endpoint,
                options
            );
            Assert.NotEmpty(response.Document.ToString());
            Assert.Null(response.Document.Inference.Extras.Rag);
        }
    }
}
