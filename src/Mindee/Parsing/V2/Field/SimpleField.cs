using System.Collections.Generic;
using System.Globalization;
using System.Text.Json.Serialization;

namespace Mindee.Parsing.V2.Field
{
    /// <summary>
    ///     Field having a single value.
    /// </summary>
    [JsonConverter(typeof(SimpleFieldJsonConverter))]
    public class SimpleField : BaseField
    {
        /// <summary>
        ///     Represents a field with a single value.
        ///     Inherits from the <see cref="BaseField" /> class.
        ///     <param name="value">
        ///         <see cref="Value" />
        ///     </param>
        ///     <param name="confidence">
        ///         <see cref="BaseField.Confidence" />
        ///     </param>
        ///     <param name="locations">
        ///         <see cref="BaseField.Locations" />
        ///     </param>
        /// </summary>
        public SimpleField(dynamic value, FieldConfidence? confidence, List<FieldLocation> locations) : base(confidence,
            locations)
        {
            Value = value;
        }

        /// <summary>
        ///     Field value, one of: string, bool, int, double, null.
        /// </summary>
        [JsonPropertyName("value")]
        public dynamic Value { get; set; }

        /// <summary>
        ///     String representation of the field.
        ///     Checks that integers get displayed with proper formatting.
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
