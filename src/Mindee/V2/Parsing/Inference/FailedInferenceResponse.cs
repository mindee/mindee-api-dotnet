using System;
using System.Text.Json.Serialization;

namespace Mindee.V2.Parsing.Inference
{
    /// <summary>
    ///     Webhook payload returned when an inference fails before producing a result.
    /// </summary>
    public class FailedInferenceResponse : BaseResponse
    {
        /// <summary>
        ///     UUID of the failed inference.
        /// </summary>
        [JsonPropertyName("inference_id")]
        public string InferenceId { get; set; }

        /// <summary>
        ///     UUID of the model used.
        /// </summary>
        [JsonPropertyName("model_id")]
        public string ModelId { get; set; }

        /// <summary>
        ///     Name of the input file
        /// </summary>
        [JsonPropertyName("file_name")]
        public string FileName { get; set; }

        /// <summary>
        ///     Alias sent for the file, if any.
        /// </summary>
        [JsonPropertyName("file_alias")]
        public string FileAlias { get; set; }

        /// <summary>
        ///     Problem details for the failure, if available.
        /// </summary>
        [JsonPropertyName("error")]
        public ErrorResponse Error { get; set; }


        /// <summary>
        ///     Date and time when the inference was started.
        /// </summary>
        [JsonPropertyName("created_at")]
        [JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime CreatedAt { get; set; }
    }
}
