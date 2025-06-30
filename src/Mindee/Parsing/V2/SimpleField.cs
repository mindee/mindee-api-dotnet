using System.Text.Json.Serialization;

namespace Mindee.Parsing.V2
{
    /// <summary>
    /// Field having a single value.
    /// </summary>
    public class SimpleField: BaseField
    {
        /// <summary>
        /// Field value, one of: string, bool, int, double, null.
        /// </summary>
        [JsonPropertyName("value")]
        public dynamic Value { get; set; }
    }
}
