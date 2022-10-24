using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Mindee.Parsing.Common
{
    /// <summary>
    /// Define the inference model of values.
    /// </summary>
    /// <typeparam name="TPredictionModel">The prediction model which defines values.</typeparam>
    public class Inference<TPredictionModel>
        where TPredictionModel : class, new()
    {
        /// <summary>
        /// The pages and the associated values which was detected on the document.
        /// </summary>
        [JsonPropertyName("pages")]
        public List<Page<TPredictionModel>> Pages { get; set; }

        /// <summary>
        /// The prediction model values.
        /// </summary>
        [JsonPropertyName("prediction")]
        public TPredictionModel Prediction { get; set; }
    }
}
