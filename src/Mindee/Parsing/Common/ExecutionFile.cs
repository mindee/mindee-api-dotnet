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
        /// Identifier for the execution.
        /// </summary>
        [JsonPropertyName("alias")]
        public string Alias { get; set; }
    }
}
