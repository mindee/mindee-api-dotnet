using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Mindee.Parsing.V2
{
    /// <summary>
    /// Information on the processing of a file sent to the Mindee API.
    /// </summary>
    public class Job
    {
        /// <summary>
        /// Date and time of the Job creation.
        /// </summary>
        [JsonPropertyName("created_at")]
        [JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// UUID of the Job.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// Status of the job.
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; }

        /// <summary>
        /// If an error occurred during processing, contains the problem details.
        /// </summary>
        [JsonPropertyName("error")]
        public ErrorResponse Error { get; set; }

        /// <summary>
        /// UUID of the model to be used for the inference.
        /// </summary>
        [JsonPropertyName("model_id")]
        public string ModelId { get; set; }

        /// <summary>
        /// Name of the file sent.
        /// </summary>
        [JsonPropertyName("file_name")]
        public string FileName { get; set; }

        /// <summary>
        /// Optional alias sent for the file.
        /// </summary>
        [JsonPropertyName("file_alias")]
        public string FileAlias { get; set; }

        /// <summary>
        /// URL to poll for the Job status.
        /// </summary>
        [JsonPropertyName("polling_url")]
        public string PollingUrl { get; set; }

        /// <summary>
        /// URL to retrieve the inference results. Will be filled once the inference is ready.
        /// </summary>
        [JsonPropertyName("result_url")]
        public string ResultUrl { get; set; }

        /// <summary>
        /// List of responses from webhooks called. Empty until processing is finished.
        /// </summary>
        [JsonPropertyName("webhooks")]
        public List<JobWebhook> Webhooks { get; set; }
    }
}
