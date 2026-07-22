using Mindee.V2;
using Mindee.V2.Search.Model;

namespace Mindee.IntegrationTests.V2.Search
{
    [Trait("Category", "V2")]
    [Trait("Category", "Integration")]
    public class RagDocumentSearchTest
    {
        private readonly Client _client;
        private readonly string? _findocModelId;

        public RagDocumentSearchTest()
        {
            var apiKey = Environment.GetEnvironmentVariable("MindeeV2__ApiKey");
            _client = TestingUtilities.GetOrGenerateMindeeClientV2(apiKey);
            _findocModelId = Environment.GetEnvironmentVariable("MindeeV2__Findoc__Model__Id");
        }

        [Fact(Timeout = 180000)]
        public async Task ModelSearch_mustReturnModels()
        {
            var response = await _client.SearchRagDocuments(
                new RagDocumentSearchParameters(modelId: _findocModelId));
            Assert.NotNull(response);
            Assert.NotNull(response.RagDocuments);
            Assert.NotEmpty(response.RagDocuments);
            Assert.NotNull(response.Pagination);
            Assert.True(response.Pagination.TotalItems > 1);
            Assert.Equal(1, response.Pagination.Page);
        }
    }
}
