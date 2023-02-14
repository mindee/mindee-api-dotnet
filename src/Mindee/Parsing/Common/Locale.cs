using System.Text;
using System.Text.Json.Serialization;

namespace Mindee.Parsing.Common
{
    /// <summary>
    /// The local of the page.
    /// </summary>
    public class Locale
    {
        private string _language;

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
        public string Language
        {
            get
            {
                return _language;
            }
            set
            {
                _language = value;
                Value = value;
            }
        }

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
