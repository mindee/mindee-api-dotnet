using System.Text.Json.Serialization;
using Mindee.Parsing.Common;

namespace Mindee.Parsing.Invoice
{
    public class DueDate : BaseField
    {
        [JsonPropertyName("raw")]
        public string Raw { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }
    }
}
