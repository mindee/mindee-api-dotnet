using Mindee.V2.Parsing;
using Mindee.V2.Parsing.Search;

namespace Mindee.UnitTests.V2.Parsing
{
    [Trait("Category", "V2")]
    [Trait("Category", "Search")]
    public class ModelSearchTest
    {
        [Fact]
        public void ModelSearchResponse_LoadsLocally()
        {
            var localResponse = new LocalResponse(
                new FileInfo(Constants.V2RootDir + "search/models.json"));
            SearchResponse response = localResponse.DeserializeResponse<SearchResponse>();

            Assert.NotNull(response);

            Assert.Equal(5, response.Models.Count);
            Assert.Equal(5, response.Pagination.TotalItems);
            Assert.Equal(1, response.Pagination.Page);
            Assert.Equal(50, response.Pagination.PerPage);
            Assert.Equal(1, response.Pagination.TotalPages);

            var firstItem = response.Models.First();
            Assert.Equal("Extraction With Webhooks", firstItem.Name);
            Assert.Equal("afde5151-aa11-aa11-9289-fa04e50ca3b9", firstItem.Id);
            Assert.Equal("extraction", firstItem.ModelType);

            Assert.Equal(2, firstItem.Webhooks.Count);
            Assert.Equal("a2286ed9-aa11-aa11-bdc5-2f8496c5641a", firstItem.Webhooks[0].Id);
            Assert.Equal("FAILURE", firstItem.Webhooks[0].Name);
            Assert.Equal("https://failure.mindee.com", firstItem.Webhooks[0].Url);

            var lastItem = response.Models.Last();
            Assert.Equal("Extraction Without Webhooks Key", lastItem.Name);
            Assert.Equal("e14e0923-ee55-ee55-a335-8d2110917d7b", lastItem.Id);
        }
    }
}
