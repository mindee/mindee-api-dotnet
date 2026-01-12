using System.Text.Json.Serialization;

namespace Mindee.Parsing.Common
{
    /// <summary>
    ///     Retrieval-Augmented Generation info class.
    /// </summary>
    public class Rag
    {
        /// <summary>
        ///     Retrieval-Augmented Generation results object.
        /// </summary>
        [JsonPropertyName("matching_document_id")]
        public string MatchingDocumentId { get; set; }
    }
}
