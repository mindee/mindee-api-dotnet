using System.Text.Json.Serialization;

namespace Mindee.Parsing.Common
{
    /// <summary>
    /// File name.
    /// </summary>
    public class ExecutionFile
    {
        /// <summary>
        /// File name.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Optional alias for the file.
        /// </summary>
        [JsonPropertyName("alias")]
        public string Alias { get; set; }
    }
}
