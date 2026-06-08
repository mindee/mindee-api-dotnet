using Mindee.V2.Parsing;
using Mindee.V2.Parsing.Search;

namespace Mindee.UnitTests.V2.Parsing
{
    [Trait("Category", "V2")]
    [Trait("Category", "Search")]
    public class SearchTest
    {
        [Fact]
        public void SearchModels_LoadsLocally()
        {
            var localResponse = new LocalResponse(
                new FileInfo(Constants.V2RootDir + "search/models.json"));
            SearchResponse modelsResponse = localResponse.DeserializeResponse<SearchResponse>();

            Assert.NotNull(modelsResponse);

            Assert.Equal(5, modelsResponse.Models.Count);
            Assert.Equal(5, modelsResponse.Pagination.TotalItems);
            Assert.Equal(1, modelsResponse.Pagination.Page);
            Assert.Equal(50, modelsResponse.Pagination.PerPage);
            Assert.Equal(1, modelsResponse.Pagination.TotalPages);

            var firstModel = modelsResponse.Models.First();
            Assert.Equal("Extraction With Webhooks", firstModel.Name);
            Assert.Equal("afde5151-aa11-aa11-9289-fa04e50ca3b9", firstModel.Id);
            Assert.Equal("extraction", firstModel.ModelType);

            Assert.Equal(2, firstModel.Webhooks.Count);
            Assert.Equal("a2286ed9-aa11-aa11-bdc5-2f8496c5641a", firstModel.Webhooks[0].Id);
            Assert.Equal("FAILURE", firstModel.Webhooks[0].Name);
            Assert.Equal("https://failure.mindee.com", firstModel.Webhooks[0].Url);
            var lastModel = modelsResponse.Models.Last();
            Assert.Equal("Extraction Without Webhooks Key", lastModel.Name);
            Assert.Equal("e14e0923-ee55-ee55-a335-8d2110917d7b", lastModel.Id);
        }
    }
}
