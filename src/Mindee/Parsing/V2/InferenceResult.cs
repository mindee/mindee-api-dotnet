using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing.V2.Field;

namespace Mindee.Parsing.V2
{
    /// <summary>
    /// A generic feature which can represent any OTS Mindee return prediction.
    /// </summary>
    public class InferenceResult
    {
        /// <summary>
        /// ResultModel fields..
        /// </summary>
        [JsonPropertyName("fields")]
        public InferenceFields Fields { get; set; }

        /// <summary>
        /// ResultOptions.
        /// </summary>
        [JsonPropertyName("options")]
        public InferenceResultOptions ResultOptions { get; set; }

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
