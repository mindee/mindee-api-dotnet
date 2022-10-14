using System.Text.Json.Serialization;
using Mindee.Parsing.Common;

namespace Mindee.Parsing.Invoice
{
    public class Customer : BaseField
    {
        [JsonPropertyName("value")]
        public string Value { get; set; }
    }
}
