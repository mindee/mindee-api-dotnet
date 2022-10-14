using System.Text.Json.Serialization;
using Mindee.Domain.Parsing.Common;

namespace Mindee.Domain.Parsing.Invoice
{
    public class DueDate : BaseField
    {
        [JsonPropertyName("raw")]
        public string Raw { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }
    }
}
