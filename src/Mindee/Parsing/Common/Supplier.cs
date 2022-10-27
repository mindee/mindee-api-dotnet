using System.Text.Json.Serialization;

namespace Mindee.Parsing.Common
{
    public class Supplier : BaseField
    {
        /// <summary>
        /// The value of the field.
        /// </summary>
        [JsonPropertyName("value")]
        public string Value { get; set; }
    }
}
