using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing;
using Mindee.Parsing.Custom;

namespace Mindee.Product.Custom
{
    /// <summary>
    /// Document data for Custom Documents, API version 1.
    /// </summary>
    [Serializable]
    [JsonConverter(typeof(CustomV1DocumentJsonConverter))]
    public sealed class CustomV1Document : IPrediction
    {
        /// <summary>
        /// Classification fields.
        /// </summary>
        public Dictionary<string, ClassificationField> ClassificationFields { get; set; }

        /// <summary>
        /// Fields that are not classifications.
        /// </summary>
        public Dictionary<string, ListField> Fields { get; set; }

        /// <summary>
        /// A prettier reprensentation.
        /// </summary>
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            foreach (var classificationField in ClassificationFields)
            {
                result.Append($":{classificationField.Key}: {classificationField.Value}\n");
            }

            foreach (var listField in Fields)
            {
                result.Append($":{listField.Key}: {listField.Value}\n");
            }

            return SummaryHelper.Clean(result.ToString());
        }
    }
}
