using System.Text.Json.Serialization;

namespace Mindee.Infrastructure.Api.Commun
{
    public class DocumentType
    {
        [JsonPropertyName("value")]
        public string Value { get; set; }
    }
}
