using System.Text.Json.Serialization;

namespace Mindee.Parsing.Common
{
    /// <summary>
    /// Represent a predict response from Mindee V2 API.
    /// </summary>
    public class AsyncPredictResponseV2 : CommonResponseV2
    {
        /// <summary>
        /// <see cref="Common.ApiRequest"/>
        /// </summary>
        [JsonPropertyName("api_request")]
        public ApiRequest ApiRequest { get; set; }

        /// <summary>
        /// Set the prediction model used to parse the document.
        /// The response object will be instantiated based on this parameter.
        /// </summary>
        [JsonPropertyName("inference")]
        public InferenceV2 Inference { get; set; }
    }
}
