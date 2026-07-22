using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing;

namespace Mindee.V2.Parsing.Search
{
    /// <summary>
    /// Models search response.
    /// </summary>
    public class ModelsResponse : BaseSearchResponse
    {
        /// <summary>
        /// List of all models matching the search query.
        /// </summary>
        [JsonPropertyName("models")]
        [JsonConverter(typeof(ObjectListJsonConverter<SearchModels, SearchModel>))]
        public SearchModels Models { get; set; }

        /// <summary>
        /// String representation.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var stringBuilder = new StringBuilder("Models\n######\n");
            stringBuilder.Append(Models);
            return ToString(stringBuilder);
        }
    }
}
