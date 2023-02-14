using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Mindee.Parsing.Common
{
    /// <summary>
    /// Information from Mindee about the api request.
    /// </summary>
    public class ApiRequest
    {
        /// <summary>
        /// <see cref="Common.Error"/>
        /// </summary>
        [JsonPropertyName("error")]
        public Error Error { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("resources")]
        public List<string> Resources { get; set; }

        /// <summary>
        /// The status of the request.
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; }

        /// <summary>
        /// Status code of the request.
        /// </summary>
        [JsonPropertyName("status_code")]
        public int StatusCode { get; set; }

        /// <summary>
        /// The original url.
        /// </summary>
        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}
