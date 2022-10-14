using System.Text.Json.Serialization;

namespace Mindee.Parsing.Common
{
    public class DocumentType
    {
        [JsonPropertyName("value")]
        public string Value { get; set; }
    }
}
