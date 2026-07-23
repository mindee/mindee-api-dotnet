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
        public async Task RagDocumentSearch_mustHaveResults()
        {
            var response = await _client.SearchRagDocuments(
                new RagDocumentSearchParameters(modelId: _findocModelId));
            Assert.NotNull(response);
            Assert.NotNull(response.RagDocuments);
            Assert.NotNull(response.Pagination);
            Assert.Equal(1, response.Pagination.Page);
        }
    }
}
