using System.Text.Json.Serialization;

namespace Mindee.Infrastructure.Api.Commun
{
    public class Orientation
    {
        [JsonPropertyName("confidence")]
        public double Confidence { get; set; }

        [JsonPropertyName("degrees")]
        public int Degrees { get; set; }
    }
}
