using System.Text.Json.Serialization;
using Mindee.Parsing.Common;

namespace Mindee.Parsing.Receipt
{
    public class Category : BaseField
    {
        [JsonPropertyName("value")]
        public string Value { get; set; }
    }
}
