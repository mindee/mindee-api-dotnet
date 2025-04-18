using Mindee.Input;
using Mindee.Product.FinancialDocument;

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
        public async Task Given_AWorkflowIdShouldPoll()
        {
            PredictOptions options = new PredictOptions(workflowId: Environment.GetEnvironmentVariable("Workflow__ID"));
            var response = await client.EnqueueAndParseAsync<FinancialDocumentV1>(
                inputSource,
                options
            );
            Assert.NotEmpty(response.Document.ToString());
        }
    }
}
