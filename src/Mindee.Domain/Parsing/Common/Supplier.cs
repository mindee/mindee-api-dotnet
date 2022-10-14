using System.Text.Json.Serialization;

namespace Mindee.Domain.Parsing.Common
{
    public class Supplier : BaseField
    {
        [JsonPropertyName("value")]
        public string Value { get; set; }
    }
}
