using System.Text.Json.Serialization;

namespace Mindee.Parsing.Common
{
    /// <summary>
    /// Optional information about the document.
    /// </summary>
    public sealed class Extras
    {
        /// <summary>
        /// Cropping result.
        /// </summary>
        [JsonPropertyName("cropper")]
        public Cropper Cropper { get; set; }

        /// <summary>
        /// Full text extraction of the ocr result.
        /// </summary>
        [JsonPropertyName("full_text_ocr")]
        public FullTextOcr FullTextOcr { get; set; }

        /// <summary>
        /// Retrieval-Augmented Generation.
        /// </summary>
        [JsonPropertyName("rag")]
        public Rag Rag { get; set; }
    }
}
