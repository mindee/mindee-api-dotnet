using System;
using System.Text.Json.Serialization;

namespace Mindee.Parsing.Common
{
    /// <summary>
    /// Define an enqueued job.
    /// </summary>
    public class Job
    {
        /// <summary>
        /// Issuance date of the job.
        /// </summary>
        [JsonPropertyName("issued_at")]
        public DateTime IssuedAt { get; set; }

        /// <summary>
        /// Unique identifier of the job.
        /// </summary>
        [JsonPropertyName("job_id")]
        public string JobId { get; set; }

        /// <summary>
        /// Status of the job.
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; }
    }
}
