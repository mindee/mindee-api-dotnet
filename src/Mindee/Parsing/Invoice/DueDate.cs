using System.Text.Json.Serialization;
using Mindee.Parsing.Common;

namespace Mindee.Parsing.Invoice
{
    public class DueDate : BaseField
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
