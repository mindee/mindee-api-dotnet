using System.Collections.Generic;
using System.Text.Json.Serialization;
using Mindee.Parsing;

namespace Mindee.V2.Parsing.Search
{
    /// <summary>
    /// Models search response.
    /// </summary>
    public class ModelSearchResponse : BaseSearchResponse
    {
        /// <summary>
        /// List of all models matching the search query.
        /// </summary>
        [JsonPropertyName("models")]
        [JsonConverter(typeof(ObjectListJsonConverter<SearchModels, SearchModel>))]
        public SearchModels Models { get; set; }

        /// <inheritdoc />
        protected override List<string> BodyLines()
        {
            return ["Models\n######\n", Models.ToString()];
        }
    }
}
