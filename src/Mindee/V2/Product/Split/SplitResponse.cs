using System.Text.Json.Serialization;
using Mindee.V2.Parsing;

namespace Mindee.V2.Product.Split
{
    /// <summary>
    ///     Represent a split response from Mindee V2 API.
    /// </summary>
    [ProductSlug("split")]
    public class SplitResponse : BaseResponse
    {
        /// <summary>
        ///     Contents of the inference.
        /// </summary>
        [JsonPropertyName("inference")]
        public SplitInference Inference { get; set; }
    }
}
