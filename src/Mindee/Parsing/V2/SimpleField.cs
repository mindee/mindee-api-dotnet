using System.Text.Json.Serialization;

namespace Mindee.Parsing.V2
{
    /// <summary>
    /// Field having a single value.
    /// </summary>
    [JsonConverter(typeof(SimpleFieldJsonConverter))]
    public class SimpleField : BaseField
    {
        /// <summary>
        /// Field value, one of: string, bool, int, double, null.
        /// </summary>
        [JsonPropertyName("value")]
        public dynamic Value { get; set; }

        /// <summary>
        /// Represents a field with a single value.
        /// Inherits from the <see cref="BaseField"/> class.
        /// </summary>
        public SimpleField(dynamic value)
        {
            Value = value;
        }

        /// <summary>
        /// String representation of the field..
        /// </summary>
        public override string ToString()
        {
            return $"{Value}";
        }
    }
}
