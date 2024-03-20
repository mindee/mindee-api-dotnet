using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Mindee.Parsing;
using Mindee.Parsing.Generated;

namespace Mindee.Product.Generated
{
    /// <summary>
    /// Document data for Generated Documents, API version 1.
    /// </summary>
    [Serializable]
    [JsonConverter(typeof(GeneratedV1DocumentJsonConverter))]
    public class GeneratedV1Document : IPrediction
    {
        /// <summary>
        /// Dictionary containing the fields of the document.
        /// </summary>
        public Dictionary<string, GeneratedFeature> Fields { get; set; }
    }
}
