using System;
using System.Text.Json.Serialization;

namespace Mindee.Parsing.Common.Jobs
{
    /// <summary>
    /// Define an enqueued job.
    /// </summary>
    public class Job
    {
        /// <summary>
        /// Date and time the job was issued at.
        /// </summary>
        [JsonPropertyName("issued_at")]
        public DateTime IssuedAt { get; set; }

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
    }
}
