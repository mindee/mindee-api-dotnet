using System.Text.Json.Serialization;

namespace Mindee.Parsing.Common
{
    public class Date : BaseField
    {
        /// <summary>
        /// The raw value of it.
        /// </summary>
        [JsonPropertyName("raw")]
        public string Raw { get; set; }

        /// <summary>
        /// The value of the field.
        /// </summary>
        [JsonPropertyName("value")]
        public string Value { get; set; }
    }
}
