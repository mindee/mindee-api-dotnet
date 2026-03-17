using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing;
using Mindee.V1.Parsing;

namespace Mindee.V2.Parsing.Search
{
    /// <summary>
    /// Models search response.
    /// </summary>
    public class SearchResponse
    {
        /// <summary>
        /// List of all models matching the search query.
        /// </summary>
        [JsonPropertyName("models")]
        [JsonConverter(typeof(ObjectListJsonConverter<SearchModels, SearchModel>))]
        public SearchModels Models { get; set; }

        /// <summary>
        /// PaginationMetadata metadata.
        /// </summary>
        [JsonPropertyName("pagination")]
        public PaginationMetadata PaginationMetadata { get; set; }


        /// <summary>
        /// String representation.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var stringBuilder = new StringBuilder("Models\n######\n");
            stringBuilder.Append(Models);
            stringBuilder.Append('\n');
            stringBuilder.Append("Pagination Metadata\n");
            stringBuilder.Append("###################\n");
            stringBuilder.Append(PaginationMetadata);
            stringBuilder.Append('\n');

            return SummaryHelper.Clean(stringBuilder.ToString());
        }
    }
}
