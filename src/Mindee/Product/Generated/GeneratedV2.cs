using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing;
using Mindee.Parsing.Common;
using Mindee.Parsing.Generated;

namespace Mindee.Product.Generated
{
    /// <summary>
    /// Document data for Generated Documents, API version 2.
    /// </summary>
    [Serializable]
    [JsonConverter(typeof(GeneratedV2JsonConverter))]
    public class GeneratedV2 : IPrediction
    {
        /// <summary>
        /// Dictionary containing the fields of the document.
        /// </summary>
        [JsonPropertyName("fields")]
        public Dictionary<string, GeneratedFeatureV2> Fields { get; set; }

        /// <summary>
        /// <see cref="OptionsV2"/>
        /// </summary>
        [JsonPropertyName("options")]
        public OptionsV2 Options { get; set; }

        /// <summary>
        /// A prettier representation of the current model values.
        /// </summary>
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            foreach (KeyValuePair<string, GeneratedFeatureV2> feature in Fields)
            {
                result.Append($":{feature.Key}: {feature.Value}\n");
            }
            return result.ToString();
        }
    }
}
