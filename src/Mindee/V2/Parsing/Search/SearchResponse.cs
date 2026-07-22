using System;
using System.Text.Json.Serialization;

namespace Mindee.V2.Parsing.Search
{
    /// <summary>
    /// Models search response.
    /// </summary>
    public class SearchResponse : ModelSearchResponse
    {
        /// <summary>
        /// Pagination metadata (Obsolete).
        /// </summary>
        [JsonIgnore]
        [Obsolete("Use Pagination instead.")]
        public PaginationMetadata PaginationMetadata
        {
            get => Pagination;
            set => Pagination = value;
        }
    }
}
