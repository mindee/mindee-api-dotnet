using System.Text.Json.Serialization;
using Mindee.Geometry;

namespace Mindee.Parsing.Standard
{
    /// <summary>
    ///     Represent a string field.
    /// </summary>
    public class StringField : BaseField
    {
        /// <summary>
        /// </summary>
        /// <param name="value">
        ///     <see cref="Value" />
        /// </param>
        /// <param name="rawValue">
        ///     <see cref="RawValue" />
        /// </param>
        /// <param name="confidence">
        ///     <see cref="BaseField.Confidence" />
        /// </param>
        /// <param name="polygon">
        ///     <see cref="BaseField.Polygon" />
        /// </param>
        /// <param name="pageId">
        ///     <see cref="BaseField.PageId" />
        /// </param>
        public StringField(
            string value,
            string rawValue,
            double? confidence,
            Polygon polygon,
            int? pageId = null) : base(confidence, polygon, pageId)
        {
            Value = value == "" ? null : value;
            RawValue = rawValue == "" ? null : rawValue;
        }

        /// <summary>
        ///     The value of the field.
        /// </summary>
        /// <example>Mindee is cool!</example>
        [JsonPropertyName("value")]
        public string Value { get; set; }

        /// <summary>
        ///     The value as it appears on the document.
        /// </summary>
        /// <example>Mindee is cool!</example>
        [JsonPropertyName("raw_value")]
        public string RawValue { get; set; }

        /// <summary>
        ///     Prettier representation.
        /// </summary>
        public override string ToString()
        {
            return SummaryHelper.FormatString(Value);
        }
    }
}
