using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing;
using Mindee.Parsing.Generated;

namespace Mindee.Product.Generated
{
    /// <summary>
    /// Document data for Generated Documents, API version 2.
    /// </summary>
    [Serializable]
    [JsonConverter(typeof(GeneratedV1DocumentJsonConverter))]
    public class GeneratedV2Document : IPrediction
    {
        /// <summary>
        /// Dictionary containing the fields of the document.
        /// </summary>
        public Dictionary<string, GeneratedFeature> Fields { get; set; }

        /// <summary>
        /// A prettier representation of the current model values.
        /// </summary>
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            foreach (KeyValuePair<string, GeneratedFeature> feature in Fields)
            {
                result.Append($":{feature.Key}: {feature.Value}\n");
            }
            return result.ToString();
        }
    }
}
