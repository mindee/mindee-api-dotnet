using System.Text.Json.Serialization;

namespace Mindee.Infrastructure.Api.Common
{
    public class Orientation
    {
        [JsonPropertyName("value")]
        public int Value { get; set; }
    }
}
