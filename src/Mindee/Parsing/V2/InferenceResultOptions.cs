using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Mindee.Parsing.V2
{
    /// <summary>
    /// Optional information about the document.
    /// </summary>
    public sealed class InferenceResultOptions
    {
        /// <summary>
        /// Full text extraction of the ocr result.
        /// </summary>
        [JsonPropertyName("raw_text")]
        public List<string> RawText { get; set; }
    }
}
