using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Mindee.Parsing.Common
{
    /// <summary>
    /// Define the inference model of values.
    /// </summary>
    /// <typeparam name="TPagePrediction">Page prediction (could be the same that TDocumentPrediction).</typeparam>
    /// <typeparam name="TDocumentPrediction">Document prediction (could be the same that TPagePrediction).</typeparam>
    public abstract class Inference<TPagePrediction, TDocumentPrediction>
        where TPagePrediction : class, new()
        where TDocumentPrediction : class, new()
    {
        /// <summary>
        /// The pages and the associated values which was detected on the document.
        /// </summary>
        [JsonPropertyName("pages")]
        public List<Page<TPagePrediction>> Pages { get; set; }

        /// <summary>
        /// The prediction model values.
        /// </summary>
        [JsonPropertyName("prediction")]
        public TDocumentPrediction Prediction { get; set; }
    }
}
