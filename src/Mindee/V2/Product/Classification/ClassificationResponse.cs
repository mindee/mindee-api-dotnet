using System.Text.Json.Serialization;
using Mindee.V2.Parsing;

namespace Mindee.V2.Product.Classification
{
    /// <summary>
    ///   Response for a classification utility inference.
    /// </summary>
    [ProductAttributes("classification")]
    public class ClassificationResponse : BaseResponse
    {
        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("inference")]
        public ClassificationInference Inference { get; set; }

    }
}
