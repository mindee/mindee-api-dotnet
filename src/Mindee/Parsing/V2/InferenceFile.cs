using System.Text.Json.Serialization;

namespace Mindee.Parsing.V2
{
    /// <summary>
    /// File info for V2 API.
    /// </summary>
    public class InferenceFile
    {
        /// <summary>
        /// File name.
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
