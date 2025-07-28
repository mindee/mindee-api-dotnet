using System.Globalization;
using System.Text.Json.Serialization;

namespace Mindee.Parsing.V2.Field
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
        /// String representation of the field.
        /// Checks that integers get displayed with proper formatting.
        /// </summary>
        public override string ToString()
        {
            return Value switch
            {
                double d => d % 1 == 0
                    ? d.ToString("0.0", CultureInfo.InvariantCulture)
                    : d.ToString(CultureInfo.InvariantCulture),

                bool b => b ? "True" : "False",

                _ => Value?.ToString() ?? string.Empty
            };
        }
    }
}
