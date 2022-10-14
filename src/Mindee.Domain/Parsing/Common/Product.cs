using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Mindee.Domain.Parsing.Common
{
    public class Product
    {
        [JsonPropertyName("features")]
        public List<string> Features { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("version")]
        public string Version { get; set; }
    }
}
