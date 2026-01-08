using System.Text.Json.Serialization;

namespace Mindee.Parsing.V2
{
    /// <summary>
    ///     Metadata about the RAG operation.
    /// </summary>
    public class RagMetadata
    {
        /// <summary>
        ///     The UUID of the matched document used during the RAG operation.
        /// </summary>
        [JsonPropertyName("retrieved_document_id")]
        public string RetrievedDocumentId { get; set; }
    }
}
