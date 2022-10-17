using System.Text.Json.Serialization;
using Mindee.Parsing.Common;

namespace Mindee.Parsing.Invoice
{
    public class InvoiceNumber : BaseField
    {
        [JsonPropertyName("value")]
        public string Value { get; set; }
    }
}
