using System.Text.Json.Serialization;

namespace Mindee.Domain.Parsing.Common
{
    public class DocumentType
    {
        [JsonPropertyName("value")]
        public string Value { get; set; }
    }
}
