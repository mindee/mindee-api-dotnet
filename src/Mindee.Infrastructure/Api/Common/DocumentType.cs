using System.Text.Json.Serialization;

namespace Mindee.Infrastructure.Api.Common
{
    public class DocumentType
    {
        [JsonPropertyName("value")]
        public string Value { get; set; }
    }
}
