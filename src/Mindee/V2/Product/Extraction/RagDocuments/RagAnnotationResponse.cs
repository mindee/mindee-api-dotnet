using System;
using System.Text.Json.Serialization;
using Mindee.V2.Parsing;

namespace Mindee.V2.Product.Extraction.RagDocuments
{
    /// <summary>
    /// Response for a RAG document.
    /// </summary>
    public class RagAnnotationResponse
    {
        /// <summary>
        /// Unique identifier of the RAG document.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// Model identifier linked to the RAG document.
        /// </summary>
        [JsonPropertyName("model_id")]
        public string ModelId { get; set; }

        /// <summary>
        /// Original filename of the uploaded document.
        /// </summary>
        [JsonPropertyName("filename")]
        public string Filename { get; set; }

        /// <summary>
        /// Date and time of the document creation.
        /// </summary>
        [JsonPropertyName("created_at")]
        [JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime CreatedAt { get; set; }

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
        /// Current status of the RAG document.
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; }

        /// <summary>
        /// Annotation metadata associated with the document.
        /// </summary>
        [JsonPropertyName("annotation")]
        public RagAnnotation Annotation { get; set; }
    }
}
