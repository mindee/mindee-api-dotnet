using System.Collections.Generic;
using System.Globalization;
using System.Text.Json.Serialization;

namespace Mindee.V2.Parsing.Inference.Field
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
        ///     Field value cast as a string when available; otherwise <c>null</c>.
        /// </summary>
        [JsonIgnore]
        public string StringValue => Value as string;

        /// <summary>
        ///     Field value cast as a boolean when available; otherwise <c>null</c>.
        /// </summary>
        [JsonIgnore]
        public bool? BooleanValue => Value is bool b ? b : null;

        /// <summary>
        ///     Field value cast as a number when available; otherwise <c>null</c>.
        /// </summary>
        [JsonIgnore]
        public double? DoubleValue => Value is double d ? d : null;

        /// <summary>
        ///     Field value cast as an integer when available; otherwise <c>null</c>.
        /// </summary>
        [JsonIgnore]
        public int? IntegerValue
        {
            get
            {
                if (Value is int i)
                {
                    return i;
                }

                if (Value is double d && d % 1 == 0 && d >= int.MinValue && d <= int.MaxValue)
                {
                    return (int)d;
                }

                return null;
            }
        }

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
