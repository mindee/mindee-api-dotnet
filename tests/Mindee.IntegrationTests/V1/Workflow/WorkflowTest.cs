using Mindee.Http;
using Mindee.Input;
using Mindee.Product.FinancialDocument;
using Mindee.Product.Generated;

namespace Mindee.IntegrationTests.V1.Workflow
{
    [Trait("Category", "Integration")]
    public class WorkflowTest
    {
        private readonly MindeeClient _client;
        private readonly LocalInputSource _ragMatchInputSource;
        private readonly LocalInputSource _ragNoMatchInputSource;
        private readonly string _workflowId;

        public WorkflowTest()
        {
            var apiKey1 = Environment.GetEnvironmentVariable("Mindee__ApiKey");
            _client = TestingUtilities.GetOrGenerateMindeeClient(apiKey1);
            _ragMatchInputSource = new LocalInputSource(
                Constants.V1ProductDir + "financial_document/default_sample.jpg");
            _ragNoMatchInputSource = new LocalInputSource(
                Constants.V1ProductDir + "invoices/default_sample.jpg");
            _workflowId = Environment.GetEnvironmentVariable("Workflow__ID") ?? "";
        }

        [Fact]
        public async Task Given_AWorkflowIdUpload_ShouldReturnACorrectWorkflowObject()
        {
            var currentDateTime = DateTime.Now.ToString("yyyy-MM-dd-HH:mm:ss");
            var alias = "dotnet-" + currentDateTime;
            var options = new WorkflowOptions(alias, ExecutionPriority.Low, rag: true);
            var response = await _client.ExecuteWorkflowAsync(
                _workflowId, _ragMatchInputSource, options);

            Assert.Equal(ExecutionPriority.Low, response.Execution.Priority);
            Assert.Equal(alias, response.Execution.File.Alias);
        }

        [Fact]
        public async Task Given_AWorkflowIdPredictCustom_ShouldPollAndMatchRag()
        {
            var endpoint = new CustomEndpoint("financial_document", "mindee");
            var options = new PredictOptions(
                workflowId: _workflowId, rag: true);
            var response = await _client.EnqueueAndParseAsync<GeneratedV1>(
                _ragMatchInputSource,
                endpoint,
                options
            );
            Assert.NotEmpty(response.Document.ToString());
            Assert.NotEmpty(response.Document.Inference.Extras.Rag.MatchingDocumentId);
        }

        [Fact]
        public async Task Given_AWorkflowIdPredictOTS_ShouldPollAndMatchRag()
        {
            var options = new PredictOptions(
                workflowId: _workflowId, rag: true);
            var response = await _client.EnqueueAndParseAsync<FinancialDocumentV1>(
                _ragMatchInputSource,
                options
            );
            Assert.NotEmpty(response.Document.ToString());
            Assert.NotEmpty(response.Document.Inference.Extras.Rag.MatchingDocumentId);
        }

        [Fact]
        public async Task Given_AWorkflowIdPredictOTS_ShouldPollAndNotMatchRag()
        {
            var options = new PredictOptions(
                workflowId: _workflowId, rag: true);
            var response = await _client.EnqueueAndParseAsync<FinancialDocumentV1>(
                _ragNoMatchInputSource,
                options
            );
            Assert.NotEmpty(response.Document.ToString());
            Assert.Null(response.Document.Inference.Extras.Rag.MatchingDocumentId);
        }

        [Fact]
        public async Task Given_AWorkflowIdPredictCustom_ShouldPollWithoutRag()
        {
            var endpoint = new CustomEndpoint("financial_document", "mindee");
            var options = new PredictOptions(
                workflowId: _workflowId);
            var response = await _client.EnqueueAndParseAsync<GeneratedV1>(
                _ragMatchInputSource,
                endpoint,
                options
            );
            Assert.NotEmpty(response.Document.ToString());
            Assert.Null(response.Document.Inference.Extras.Rag);
        }

        [Fact]
        public async Task Given_AWorkflowIdPredictOTS_ShouldPollWithoutRag()
        {
            var options = new PredictOptions(
                workflowId: _workflowId);
            var response = await _client.EnqueueAndParseAsync<FinancialDocumentV1>(
                _ragMatchInputSource,
                options
            );
            Assert.NotEmpty(response.Document.ToString());
            Assert.Null(response.Document.Inference.Extras.Rag);
        }
    }
}
