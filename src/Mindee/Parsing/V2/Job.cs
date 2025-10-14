using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Mindee.Parsing.V2
{
    /// <summary>
    /// Defines an enqueued job.
    /// </summary>
    public class Job
    {
        /// <summary>
        /// Date and time the job was created at.
        /// </summary>
        [JsonPropertyName("created_at")]
        [JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Unique identifier of the job.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// Status of the job.
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; }

        /// <summary>
        /// An error encountered while processing the job.
        /// </summary>
        [JsonPropertyName("error")]
        public ErrorResponse Error { get; set; }

        /// <summary>
        /// ID of the model.
        /// </summary>
        [JsonPropertyName("model_id")]
        public string ModelId { get; set; }

        /// <summary>
        /// Name of the file.
        /// </summary>
        [JsonPropertyName("file_name")]
        public string FileName { get; set; }

        /// <summary>
        /// Optional Alias for the file.
        /// </summary>
        [JsonPropertyName("file_alias")]
        public string FileAlias { get; set; }

        /// <summary>
        /// URL to use for polling.
        /// </summary>
        [JsonPropertyName("polling_url")]
        public string PollingUrl { get; set; }

        /// <summary>
        /// URL to follow for the final result.
        /// </summary>
        [JsonPropertyName("result_url")]
        public string ResultUrl { get; set; }

        /// <summary>
        /// Webhooks to call.
        /// </summary>
        [JsonPropertyName("webhooks")]
        public List<JobWebhook> Webhooks { get; set; }
    }
}
