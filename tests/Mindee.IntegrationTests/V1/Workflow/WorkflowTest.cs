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
                "Resources/v1/products/financial_document/default_sample.jpg");
            _ragNoMatchInputSource = new LocalInputSource(
                "Resources/v1/products/invoices/default_sample.jpg");
            _workflowId = Environment.GetEnvironmentVariable("Workflow__ID") ?? "";
        }

        [Fact]
        public async Task Given_AWorkflowIdUpload_ShouldReturnACorrectWorkflowObject()
        {
            string currentDateTime = DateTime.Now.ToString("yyyy-MM-dd-HH:mm:ss");
            var alias = "dotnet-" + currentDateTime;
            WorkflowOptions options = new WorkflowOptions(alias, ExecutionPriority.Low, rag: true);
            var response = await _client.ExecuteWorkflowAsync(
                _workflowId, _ragMatchInputSource, options);

            Assert.Equal(ExecutionPriority.Low, response.Execution.Priority);
            Assert.Equal(alias, response.Execution.File.Alias);
        }

        [Fact]
        public async Task Given_AWorkflowIdPredictCustom_ShouldPollAndMatchRag()
        {
            CustomEndpoint endpoint = new CustomEndpoint("financial_document", "mindee");
            PredictOptions options = new PredictOptions(
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
            PredictOptions options = new PredictOptions(
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
            PredictOptions options = new PredictOptions(
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
            CustomEndpoint endpoint = new CustomEndpoint("financial_document", "mindee");
            PredictOptions options = new PredictOptions(
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
            PredictOptions options = new PredictOptions(
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
