using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Mindee.Parsing.Common
{
    /// <summary>
    /// Information from Mindee about the API V2 request.
    /// </summary>
    public class ApiRequestV2 : ApiRequest
    {
        /// <summary>
        /// <see cref="Common.Error"/>
        /// </summary>
        [JsonPropertyName("error")]
        public new ErrorV2 Error { get; set; }
    }
}
