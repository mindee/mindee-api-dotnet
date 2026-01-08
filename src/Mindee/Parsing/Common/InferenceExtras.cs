using System.Text.Json.Serialization;

namespace Mindee.Parsing.Common
{
    /// <summary>
    ///     Inference-level optional info.
    /// </summary>
    public sealed class InferenceExtras
    {
        /// <summary>
        ///     Full text extraction of the ocr result.
        /// </summary>
        public string FullTextOcr { get; set; }

        /// <summary>
        ///     Retrieval-Augmented Generation results object.
        /// </summary>
        [JsonPropertyName("rag")]
        public Rag Rag { get; set; }
    }
}
