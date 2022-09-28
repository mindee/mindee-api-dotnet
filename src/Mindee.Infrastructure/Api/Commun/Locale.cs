using System.Text.Json.Serialization;

namespace Mindee.Infrastructure.Api.Commun
{
    public class Locale
    {
        [JsonPropertyName("confidence")]
        public double Confidence { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [JsonPropertyName("language")]
        public string Language { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }
    }
}
