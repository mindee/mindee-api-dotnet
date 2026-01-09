using System;
using System.Text.Json.Serialization;

namespace Mindee.Parsing.Common
{
    /// <summary>
    ///     Define an enqueued job.
    /// </summary>
    public class Job
    {
        /// <summary>
        ///     Date and time the job result was available at.
        /// </summary>
        [JsonPropertyName("available_at")]
        [JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime? AvailableAt { get; set; }

        /// <summary>
        ///     Date and time the job was issued at.
        /// </summary>
        [JsonPropertyName("issued_at")]
        [JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime IssuedAt { get; set; }

        /// <summary>
        ///     Unique identifier of the job.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        ///     Status of the job.
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; }

        /// <summary>
        ///     An error encountered while processing the job.
        /// </summary>
        [JsonPropertyName("error")]
        public Error Error { get; set; }
    }
}
