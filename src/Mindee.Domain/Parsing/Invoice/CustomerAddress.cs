using System.Text.Json.Serialization;
using Mindee.Domain.Parsing.Common;

namespace Mindee.Domain.Parsing.Invoice
{
    public class CustomerAddress : BaseField
    {
        [JsonPropertyName("value")]
        public string Value { get; set; }
    }
}
