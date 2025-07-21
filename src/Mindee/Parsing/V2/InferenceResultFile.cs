using System.Text.Json.Serialization;

namespace Mindee.Parsing.V2
{
    /// <summary>
    /// ResultFile info for V2 API.
    /// </summary>
    public class InferenceResultFile
    {
        /// <summary>
        /// ResultFile name.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Optional file alias.
        /// </summary>
        [JsonPropertyName("alias")]
        public string Alias { get; set; }
    }
}
