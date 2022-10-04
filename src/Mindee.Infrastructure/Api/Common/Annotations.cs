using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Mindee.Infrastructure.Api.Common
{
    public class Annotations
    {
        [JsonPropertyName("labels")]
        public List<object> Labels { get; set; }
    }
}
