using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Mindee.Parsing.Common
{
    public class ApiRequest
    {
        [JsonPropertyName("error")]
        public Error Error { get; set; }

        [JsonPropertyName("resources")]
        public List<string> Resources { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("status_code")]
        public int StatusCode { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}
