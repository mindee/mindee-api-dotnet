using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing.V2.Field;

namespace Mindee.Parsing.V2
{
    /// <summary>
    /// Optional information about the document.
    /// </summary>
    public class InferenceActiveOptions
    {
        /// <summary>
        /// Enhance extraction accuracy with Retrieval-Augmented Generation.
        /// </summary>
        [JsonPropertyName("rag")]
        public bool Rag { get; set; }

        /// <summary>
        /// Extract the full text content from the document as strings, and fill the <see cref="InferenceResult.RawText"/> attribute.
        /// </summary>
        [JsonPropertyName("raw_text")]
        public bool RawText { get; set; }

        /// <summary>
        /// Calculate bounding box polygons for all fields, and fill their <see cref="BaseField.Locations"/> attribute
        /// </summary>
        [JsonPropertyName("polygon")]
        public bool Polygon { get; set; }

        /// <summary>
        /// Boost the precision and accuracy of all extractions.
        /// Calculate confidence scores for all fields, and fill their <see cref="BaseField.Confidence"/> attribute.
        /// </summary>
        [JsonPropertyName("confidence")]
        public bool Confidence { get; set; }

        /// <summary>
        /// Pretty-prints the file section exactly as expected by Inference.ToString().
        /// </summary>
        public override string ToString()
        {
            var sb = new StringBuilder("Active Options\n==============");
            sb.Append($"\n:Raw Text: {RawText}");
            sb.Append($"\n:Polygon: {Polygon}");
            sb.Append($"\n:Confidence: {Confidence}");
            sb.Append($"\n:RAG: {Rag}");
            return sb.ToString();
        }
    }
}
