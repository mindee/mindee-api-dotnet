using System.Text.Json.Serialization;

namespace Mindee.Parsing.Common
{
    /// <summary>
    /// File info for V2 API.
    /// </summary>
    public class FileV2
    {
        /// <summary>
        /// File name.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Optional file alias..
        /// </summary>
        [JsonPropertyName("alias")]
        public string Alias { get; set; }
    }
}
