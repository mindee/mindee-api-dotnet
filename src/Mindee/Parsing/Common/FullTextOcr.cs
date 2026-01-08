using System.Text.Json.Serialization;

namespace Mindee.Parsing.Common
{
    /// <summary>
    ///     Cropping result.
    /// </summary>
    public sealed class FullTextOcr
    {
        /// <summary>
        ///     Text content of the extraction.
        /// </summary>
        [JsonPropertyName("content")]
        public string Content { get; set; }

        /// <summary>
        ///     Language used in the text.
        /// </summary>
        [JsonPropertyName("language")]
        public string Language { get; set; }
    }
}
