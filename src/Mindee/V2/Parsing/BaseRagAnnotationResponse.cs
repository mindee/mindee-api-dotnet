using System;
using System.Text.Json.Serialization;

namespace Mindee.V2.Parsing
{
    /// <summary>
    /// Base class for all RAG document responses from the V2 API.
    /// </summary>
    public class BaseRagAnnotationResponse : BaseResponse
    {
        /// <summary>
        /// Unique identifier of the RAG document.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

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
        /// Current status of the RAG document.
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; }
    }
}
