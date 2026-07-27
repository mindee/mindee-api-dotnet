using System.Text.Json.Serialization;
using Mindee.V2.Parsing;

namespace Mindee.V2.Product.Classification
{
    /// <summary>
    ///   Response for a classification utility inference.
    /// </summary>
    [Product("classification")]
    public class ClassificationResponse : BaseResponse
    {
        /// <summary>
        /// Inference for a classification utility.
        /// </summary>
        [JsonPropertyName("inference")]
        public ClassificationInference Inference { get; set; }

    }
}
