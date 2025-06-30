using System;
using System.Text.Json.Serialization;

namespace Mindee.Parsing.V2
{
    /// <summary>
    /// Webhook info response for the V2 API.
    /// </summary>
    public class Webhook
    {
        /// <summary>
        /// Date and time the webhook was created at.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// An error encountered while processing the webhook.
        /// </summary>
        [JsonPropertyName("error")]
        public ErrorResponse Error { get; set; }

        /// <summary>
        /// Date and time the webhook was sent at.
        /// </summary>
        [JsonPropertyName("created_at")]
        [JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Status of the webhook.
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; }
    }
}
