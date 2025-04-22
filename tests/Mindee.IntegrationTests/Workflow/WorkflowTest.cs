using Mindee.Http;
using Mindee.Input;
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
        public async Task Given_AWorkflowIDShouldReturnACorrectWorkflowObject()
        {
            string currentDateTime = DateTime.Now.ToString("yyyy-MM-dd-HH:mm:ss");
            var alias = "dotnet-" + currentDateTime;
            WorkflowOptions options = new WorkflowOptions(alias, ExecutionPriority.Low, rag: true);
            var response = await client.ExecuteWorkflowAsync(Environment.GetEnvironmentVariable("Workflow__ID"),
                inputSource, options);

            Assert.Equal(ExecutionPriority.Low, response.Execution.Priority);
            Assert.Equal(alias, response.Execution.File.Alias);
        }

        [Fact]
        public async Task Given_AWorkflowIdShouldPollWithRag()
        {
            // Note: equivalent to just calling FinancialDocumentV1, but might as well test custom docs.
            CustomEndpoint endpoint = new CustomEndpoint("financial_document", "mindee");
            PredictOptions options = new PredictOptions(
                workflowId: Environment.GetEnvironmentVariable("Workflow__ID"),
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
        public async Task Given_AWorkflowIdShouldPollWithoutRag()
        {
            CustomEndpoint endpoint = new CustomEndpoint("financial_document", "mindee");
            PredictOptions options = new PredictOptions(workflowId: Environment.GetEnvironmentVariable("Workflow__ID"));
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
