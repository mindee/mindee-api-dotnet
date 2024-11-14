using System;
using System.Text.Json.Serialization;
using Mindee.Product.Generated;

namespace Mindee.Parsing.Common
{
    /// <summary>
    /// Representation of a workflow execution.
    /// </summary>
    public class Execution<TInferenceModel> where TInferenceModel : class, new()
    {
        /// <summary>
        /// Identifier for the batch to which the execution belongs.
        /// </summary>
        [JsonPropertyName("batch_name")]
        public string BatchName { get; set; }

        /// <summary>
        /// The time at which the execution started.
        /// </summary>
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// File representation within a workflow execution.
        /// </summary>
        [JsonPropertyName("file")]
        public ExecutionFile File { get; set; }

        /// <summary>
        /// Identifier for the execution.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// Deserialized inference object.
        /// </summary>
        [JsonPropertyName("inference")]
        public TInferenceModel Inference { get; set; }

        /// <summary>
        /// Priority of the execution.
        /// </summary>
        [JsonPropertyName("priority")]
        public string Priority { get; set; }

        /// <summary>
        /// The time at which the file was tagged as reviewed.
        /// </summary>
        [JsonPropertyName("reviewed_at")]
        public DateTime ReviewedAt { get; set; }

        /// <summary>
        /// The time at which the file was uploaded to a workflow.
        /// </summary>
        [JsonPropertyName("available_at")]
        public DateTime AvailableAt { get; set; }

        /// <summary>
        /// Reviewed fields and values.
        /// </summary>
        [JsonPropertyName("reviewed_prediction")]
        public GeneratedV1Document ReviewedPrediction { get; set; }

        /// <summary>
        /// Execution Status.
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; }

        /// <summary>
        /// Execution type.
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; }

        /// <summary>
        /// The time at which the file was uploaded to a workflow.
        /// </summary>
        [JsonPropertyName("uploaded_at")]
        public DateTime UploadedAt { get; set; }

        /// <summary>
        /// Identifier for the workflow.
        /// </summary>
        [JsonPropertyName("workflow_id")]
        public string WorkflowId { get; set; }
    }
}
