using System;
using System.Text.Json.Serialization;

namespace Mindee.Parsing.Common
{
    /// <summary>
    /// Webhook info response for the V2 API.
    /// </summary>
    public class Webhook
    {
        /// <summary>
        /// Date and time the job was created at.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// An error encountered while processing the job.
        /// </summary>
        [JsonPropertyName("error")]
        public Error Error { get; set; }

        /// <summary>
        /// Date and time the webhook was sent at.
        /// </summary>
        [JsonPropertyName("sent_at")]
        [JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime SentAt { get; set; }

        /// <summary>
        /// Status of the job.
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; }
    }
}
