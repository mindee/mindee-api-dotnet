using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Mindee.V1.Parsing.Common
{
    internal class Annotations
    {
        [JsonPropertyName("labels")] public List<object> Labels { get; set; }
    }
}
