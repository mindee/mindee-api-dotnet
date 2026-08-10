using System;
using System.Text.Json.Serialization;
using Mindee.V2.Parsing;

namespace Mindee.V2.Product.Extraction.RagDocuments
{
    /// <summary>
    /// Response for a RAG document.
    /// </summary>
    public class ExtractionRagAnnotationResponse : BaseRagAnnotationResponse
    {
        /// <summary>
        /// Model identifier linked to the RAG document.
        /// </summary>
        [JsonPropertyName("model_id")]
        public string ModelId { get; set; }

        /// <summary>
        /// Number of times this document was used in an inference.
        /// </summary>
        [JsonPropertyName("total_matches")]
        public int TotalMatches { get; set; }

        /// <summary>
        /// Date and time of the latest matching inference, if any.
        /// </summary>
        [JsonPropertyName("last_match_at")]
        [JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime? LastMatchAt { get; set; }

        /// <summary>
        /// Annotation metadata associated with the document.
        /// </summary>
        [JsonPropertyName("annotation")]
        public RagAnnotation Annotation { get; set; }
    }
}
