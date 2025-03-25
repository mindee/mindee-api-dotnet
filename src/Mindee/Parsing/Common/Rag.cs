using System.Text.Json.Serialization;

namespace Mindee.Parsing.Common
{
    /// <summary>
    /// Retrieval-Augmented Generation result.
    /// </summary>
    public class Rag
    {
        /// <summary>
        /// Matching document ID for the RAG result.
        /// </summary>
        [JsonPropertyName("matching_document_id")]
        public string MatchingDocumentId { get; set; }
    }
}
