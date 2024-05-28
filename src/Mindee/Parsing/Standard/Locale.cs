using System.Text;
using System.Text.Json.Serialization;

namespace Mindee.Parsing.Standard
{
    /// <summary>
    /// The locale of the page.
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
        /// The language which has been detected.
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

        /// <summary>
        /// Concatenation of lang and country codes.
        /// </summary>
        /// <example>en-GB</example>
        [JsonPropertyName("value")]
        public string Value { get; set; }

        /// <summary>
        /// The locale of the page.
        /// </summary>
        /// <param name="confidence"><see cref="Confidence"/></param>
        /// <param name="currency"><see cref="Currency"/></param>
        /// <param name="language"><see cref="Language"/></param>
        /// <param name="country"><see cref="Country"/></param>
        /// <param name="value"><see cref="Value"/></param>
        [JsonConstructor]
        public Locale(
            double confidence,
            string currency,
            string language,
            string country,
            string value)
        {
            (Confidence, Currency, Country, Language, Value) = (confidence, currency, country, language, value);
            if (string.IsNullOrWhiteSpace(value) && !string.IsNullOrWhiteSpace(language))
                Value = language;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns>A pretty summary of the value.</returns>
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(Value))
            {
                result.Append($"{Value}; ");
            }
            if (!string.IsNullOrWhiteSpace(Language))
            {
                result.Append($"{Language}; ");
            }
            if (!string.IsNullOrWhiteSpace(Country))
            {
                result.Append($"{Country}; ");
            }
            if (!string.IsNullOrWhiteSpace(Currency))
            {
                result.Append($"{Currency}; ");
            }

            return result.ToString().Trim();
        }
    }
}
