using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Mindee.Parsing.Common
{
    /// <summary>
    /// Optional information about the document.
    /// </summary>
    public sealed class OptionsV2
    {
        /// <summary>
        /// Cropping result.
        /// </summary>
        [JsonPropertyName("cropper")]
        public Cropper Cropper { get; set; }

        /// <summary>
        /// Full text extraction of the ocr result.
        /// </summary>
        [JsonPropertyName("raw_text")]
        public List<string> RawText { get; set; }
    }
}
