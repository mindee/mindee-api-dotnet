using System.Text;
using System.Text.Json.Serialization;

namespace Mindee.V2.Parsing.Search
{
    /// <summary>
    /// PaginationMetadata data associated with model search.
    /// </summary>
    public class PaginationMetadata
    {
        /// <summary>
        /// Number of items per page.
        /// </summary>
        [JsonPropertyName("per_page")]
        public int PerPage { get; set; }

        /// <summary>
        /// 1-indexed page number.
        /// </summary>
        [JsonPropertyName("page")]
        public int Page { get; set; }

        /// <summary>
        /// Total items.
        /// </summary>
        [JsonPropertyName("total_items")]
        public int TotalItems { get; set; }

        /// <summary>
        /// Total number of pages.
        /// </summary>
        [JsonPropertyName("total_pages")]
        public int TotalPages { get; set; }

        /// <summary>
        /// String representation of the pagination metadata.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append($":Per Page: {PerPage}");
            stringBuilder.Append('\n');
            stringBuilder.Append($":Page: {Page}");
            stringBuilder.Append('\n');
            stringBuilder.Append($":Total Items: {TotalItems}");
            stringBuilder.Append('\n');
            stringBuilder.Append($":Total Pages: {TotalPages}");
            stringBuilder.Append('\n');
            return stringBuilder.ToString();
        }
    }
}
