using System.Text.Json.Serialization;

namespace Mindee.Infrastructure.Api.Common
{
    public abstract class PredictionBase
    {

        [JsonPropertyName("document_type")]
        public DocumentType DocumentType { get; set; }

        [JsonPropertyName("locale")]
        public Locale Locale { get; set; }

        [JsonPropertyName("orientation")]
        public Orientation Orientation { get; set; }
    }
}
