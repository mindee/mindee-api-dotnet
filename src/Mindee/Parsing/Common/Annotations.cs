using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Mindee.Parsing.Common
{
    public class Annotations
    {
        [JsonPropertyName("labels")]
        public List<object> Labels { get; set; }
    }
}
