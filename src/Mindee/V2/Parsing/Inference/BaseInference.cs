using System;
using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing;

namespace Mindee.V2.Parsing.Inference
{
    /// <summary>
    /// Base for all inference-based V2 products.
    /// </summary>
    public abstract class BaseInference
    {

        /// <summary>
        ///     UUID of the ExtractionInference.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        ///     Model used for the inference.
        /// </summary>
        [JsonPropertyName("model")]
        public InferenceModel Model { get; set; }

        /// <summary>
        ///     File used for the inference.
        /// </summary>
        [JsonPropertyName("file")]
        public InferenceFile File { get; set; }

        /// <summary>
        /// Job the inference belongs to.
        /// </summary>
        [JsonPropertyName("job")]
        public InferenceJob Job { get; set; }

        /// <summary>
        /// Type of the product's response.
        /// </summary>
        public virtual Type ResponseType { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var stringBuilder = new StringBuilder("Inference\n#########\n");
            stringBuilder.Append(Job);
            stringBuilder.Append("\n\n");
            stringBuilder.Append(Model);
            stringBuilder.Append("\n\n");
            stringBuilder.Append(File);
            stringBuilder.Append("\n\n");

            return SummaryHelper.Clean(stringBuilder.ToString());
        }
    }
}
