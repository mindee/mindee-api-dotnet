using System.Text.Json.Serialization;
using Mindee.Domain.Parsing.Common;

namespace Mindee.Domain.Parsing.Receipt
{
    public class Category : BaseField
    {
        [JsonPropertyName("value")]
        public string Value { get; set; }
    }
}
