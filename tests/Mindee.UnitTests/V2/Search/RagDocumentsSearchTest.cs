using Mindee.V2.Parsing;
using Mindee.V2.Parsing.Search;

namespace Mindee.UnitTests.V2.Parsing
{
    [Trait("Category", "V2")]
    [Trait("Category", "Search")]
    public class RagDocumentSearchTest
    {
        [Fact]
        public void RagDocumentSearchResponse_LoadsLocally()
        {
            var localResponse = new LocalResponse(
                new FileInfo(Constants.V2RootDir + "search/rag_documents.json"));
            RagDocumentSearchResponse response = localResponse.DeserializeResponse<RagDocumentSearchResponse>();

            Assert.NotNull(response);

            Assert.Equal(3, response.RagDocuments.Count);
            Assert.Equal(3, response.Pagination.TotalItems);
            Assert.Equal(1, response.Pagination.Page);
            Assert.Equal(50, response.Pagination.PerPage);
            Assert.Equal(1, response.Pagination.TotalPages);

            var firstItem = response.RagDocuments.First();
            Assert.Equal("cc831599-c545-48b7-aa27-6d7ccd5b8d32", firstItem.Id);
            Assert.Equal("12345678-1234-1234-1234-123456789abc", firstItem.ModelId);
            Assert.Equal("invoice_01.pdf", firstItem.Filename);
            Assert.Equal(
                DateTime.Parse(
                    "2026-06-30T13:13:46.168586Z",
                    null,
                    System.Globalization.DateTimeStyles.RoundtripKind),
                firstItem.CreatedAt);
            Assert.Equal(0, firstItem.TotalMatches);
            Assert.Null(firstItem.LastMatchAt);
            Assert.Equal("Processing", firstItem.Status);

            var secondItem = response.RagDocuments.ElementAt(1);
            Assert.Equal("27467e4c-5602-4315-90d9-3d2da69b05ab", secondItem.Id);
            Assert.Equal("12345678-1234-1234-1234-123456789abc", secondItem.ModelId);
            Assert.Equal("invoice_02.pdf", secondItem.Filename);
            Assert.Equal(
                DateTime.Parse(
                    "2026-06-30T13:13:46.168586Z",
                    null,
                    System.Globalization.DateTimeStyles.RoundtripKind),
                secondItem.CreatedAt);
            Assert.Equal(0, secondItem.TotalMatches);
            Assert.Null(secondItem.LastMatchAt);
            Assert.Equal("Draft", secondItem.Status);

            var thirdItem = response.RagDocuments.ElementAt(2);
            Assert.Equal("a6bcae7d-0439-476b-8a63-5a39ec05dc21", thirdItem.Id);
            Assert.Equal("12345678-1234-1234-1234-jobid1234567", thirdItem.ModelId);
            Assert.Equal("invoice_03.pdf", thirdItem.Filename);
            Assert.Equal(
                DateTime.Parse(
                    "2026-06-17T14:35:46.228006Z",
                    null,
                    System.Globalization.DateTimeStyles.RoundtripKind),
                thirdItem.CreatedAt);
            Assert.Equal(5, thirdItem.TotalMatches);
            Assert.Equal(
                DateTime.Parse(
                    "2026-06-18T14:35:46.248006Z",
                    null,
                    System.Globalization.DateTimeStyles.RoundtripKind),
                thirdItem.LastMatchAt);
            Assert.Equal("Active", thirdItem.Status);
        }
    }
}
