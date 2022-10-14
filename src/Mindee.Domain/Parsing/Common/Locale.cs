using System.Text.Json.Serialization;

namespace Mindee.Domain.Parsing.Common
{
    /// <summary>
    /// The local of the page.
    /// </summary>
    public class Locale
    {
        /// <summary>
        /// The confidence about the zone of the value extracted.
        /// A value from 0 to 1.
        /// </summary>
        /// <example>0.9</example>
        [JsonPropertyName("confidence")]
        public double Confidence { get; set; }

        /// <summary>
        /// The currency which has been detected.
        /// </summary>
        /// <example>EUR</example>
        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        /// <summary>
        /// The langage which has been detected.
        /// </summary>
        /// <example>fr</example>
        [JsonPropertyName("language")]
        public string Language { get; set; }

        /// <summary>
        /// The country which has been detected.
        /// </summary>
        /// <example>FR</example>
        [JsonPropertyName("country")]
        public string Country { get; set; }
    }
}
