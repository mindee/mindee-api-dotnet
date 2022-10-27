using System.Text.Json.Serialization;

namespace Mindee.Parsing.Common
{
    /// <summary>
    /// The type of the parsed document.
    /// </summary>
    public class DocumentType
    {
        /// <summary>
        /// Value of it.
        /// </summary>
        [JsonPropertyName("value")]
        public string Value { get; set; }
    }
}
