using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing;

namespace Mindee.V2.Parsing.Search
{
    /// <summary>
    /// Models search response.
    /// </summary>
    public abstract class BaseSearchResponse : BaseResponse
    {
        /// <summary>
        /// Pagination metadata.
        /// </summary>
        [JsonPropertyName("pagination")]
        public PaginationMetadata Pagination { get; set; }

        /// <summary>
        /// String representation.
        /// </summary>
        /// <returns></returns>
        protected string ToString(StringBuilder stringBuilder)
        {
            stringBuilder.Append("\nPagination Metadata\n");
            stringBuilder.Append("###################\n");
            stringBuilder.Append(Pagination);
            stringBuilder.Append('\n');

            return SummaryHelper.Clean(stringBuilder.ToString());
        }
    }
}
