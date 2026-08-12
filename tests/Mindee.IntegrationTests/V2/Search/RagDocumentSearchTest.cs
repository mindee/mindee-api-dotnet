using Mindee.V2;
using Mindee.V2.Parsing.Search;
using Mindee.V2.Search.RagDocuments;

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
            var response = await _client.SearchAsync<RagDocumentSearchResponse>(
                new RagDocumentSearchParameters(modelId: _findocModelId));
            Assert.NotNull(response);
            Assert.NotNull(response.RagDocuments);
            Assert.NotNull(response.Pagination);
            Assert.Equal(1, response.Pagination.Page);
        }
    }
}
