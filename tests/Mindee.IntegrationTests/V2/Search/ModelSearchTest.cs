using Mindee.V2;
using Mindee.V2.Search.Models;

namespace Mindee.IntegrationTests.V2.Search
{
    [Trait("Category", "V2")]
    [Trait("Category", "Integration")]
    public class ModelSearchTest
    {
        private readonly Client _client;

        public ModelSearchTest()
        {
            var apiKey = Environment.GetEnvironmentVariable("MindeeV2__ApiKey");
            _client = TestingUtilities.GetOrGenerateMindeeClientV2(apiKey);
        }

        [Fact(Timeout = 180000)]
        public async Task ModelSearch_mustReturnModels()
        {
            var response = await _client.SearchModels();
            Assert.NotNull(response);
            Assert.NotNull(response.Models);
            Assert.NotEmpty(response.Models);
            Assert.NotNull(response.Pagination);
            Assert.True(response.Pagination.TotalItems > 1);
            Assert.Equal(1, response.Pagination.Page);
        }

        [Fact(Timeout = 180000)]
        public async Task ModelSearch_mustReturnEmpty()
        {
            var response = await _client.SearchModels(
                new ModelSearchParameters(name: "je n'existe pas tralala"));
            Assert.NotNull(response);
            Assert.NotNull(response.Models);
            Assert.Empty(response.Models);
            Assert.NotNull(response.Pagination);
            Assert.Equal(0, response.Pagination.TotalItems);
            Assert.Equal(1, response.Pagination.Page);
        }

        [Fact(Timeout = 180000)]
        public async Task ModelSearch_mustReturnEmptyObsolete()
        {
            var response = await _client.SearchModels(name: "je n'existe pas tralala");
            Assert.NotNull(response);
            Assert.NotNull(response.Models);
            Assert.Empty(response.Models);
            Assert.NotNull(response.Pagination);
            Assert.Equal(0, response.Pagination.TotalItems);
            Assert.Equal(1, response.Pagination.Page);
        }
    }
}
