using System.Text.Json.Serialization;
using Mindee.Parsing.Common;

namespace Mindee.Parsing.Receipt
{
    public class Time : BaseField
    {
        [JsonPropertyName("raw")]
        public string Raw { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }
    }
}
