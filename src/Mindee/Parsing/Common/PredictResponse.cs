using System.Text.Json.Serialization;

namespace Mindee.Parsing.Common
{
    /// <summary>
    /// Represent a predict response from Mindee API.
    /// </summary>
    /// <typeparam name="TModel">Set the prediction model used to parse the document.
    /// The response object will be instantiated based on this parameter.
    /// </typeparam>
    public class PredictResponse<TModel>
        where TModel : class, new()
    {
        [JsonPropertyName("api_request")]
        internal ApiRequest ApiRequest { get; set; }

        /// <summary>
        /// Set the prediction model used to parse the document.
        /// The response object will be instantiated based on this parameter.
        /// </summary>
        [JsonPropertyName("document")]
        public Document<TModel> Document { get; set; }
    }
}
