using System.Text.Json.Serialization;
using Mindee.Domain.Parsing.Common;

namespace Mindee.Domain.Parsing.Invoice
{
    public class SupplierAddress : BaseField
    {
        [JsonPropertyName("value")]
        public string Value { get; set; }
    }
}
