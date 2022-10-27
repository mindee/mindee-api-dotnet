using System.Text.Json.Serialization;
using Mindee.Parsing.Common;

namespace Mindee.Parsing.Receipt
{
    /// <summary>
    /// Represent a time.
    /// </summary>
    public class Time : BaseField
    {
        /// <summary>
        /// The raw representation of the value
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
