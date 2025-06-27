using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Mindee.Product.V2;

namespace Mindee.Parsing.V2
{
    /// <summary>
    /// A generic feature which can represent any OTS Mindee return prediction.
    /// </summary>
    public class InferenceResult
    {
        /// <summary>
        /// Model fields..
        /// </summary>
        [JsonPropertyName("fields")]
        [JsonConverter(typeof(InferenceFieldsJsonConverter))]
        public InferenceFields Fields { get; set; }

        /// <summary>
        /// Options.
        /// </summary>
        [JsonPropertyName("options")]
        public InferenceOptions Options { get; set; }

        /// <summary>
        /// A prettier representation of the feature values.
        /// </summary>

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            foreach (KeyValuePair<string, DynamicField> feature in Fields)
            {
                result.Append($":{feature.Key}: {feature.Value}\n");
            }
            return result.ToString();
        }
    }
}
