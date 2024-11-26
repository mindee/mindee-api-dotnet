using Mindee.Input;

namespace Mindee.IntegrationTests.Workflow
{
    [Trait("Category", "Integration tests")]
    public class WorkflowTest
    {
        [Fact]
        public async Task Given_AWorkflowIDShouldReturnACorrectWorkflowObject()
        {
            var apiKey = Environment.GetEnvironmentVariable("Mindee__ApiKey");
            var client = TestingUtilities.GetOrGenerateMindeeClient(apiKey);
            var inputSource = new LocalInputSource("Resources/products/financial_document/default_sample.jpg");

            string currentDateTime = DateTime.Now.ToString("yyyy-MM-dd-HH:mm:ss");
            var alias = "dotnet-" + currentDateTime;
            WorkflowOptions options = new WorkflowOptions(alias, ExecutionPriority.Low);
            var response = await client.ExecuteWorkflowAsync(Environment.GetEnvironmentVariable("Workflow__ID"), inputSource, options);

            Assert.Equal(ExecutionPriority.Low, response.Execution.Priority);
            Assert.Equal(alias, response.Execution.File.Alias);

        }
    }
}
