using System.Text.Json.Serialization;
using Mindee.Geometry;

namespace Mindee.Parsing.Standard
{
    /// <summary>
    ///     Represent a string field.
    /// </summary>
    public class BooleanField : BaseField
    {
        /// <summary>
        /// </summary>
        /// <param name="value">
        ///     <see cref="Value" />
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
        public BooleanField(
            bool? value,
            double? confidence,
            Polygon polygon,
            int? pageId = null) : base(confidence, polygon, pageId)
        {
            Value = value;
        }

        /// <summary>
        ///     The value of the field.
        /// </summary>
        [JsonPropertyName("value")]
        public bool? Value { get; set; }

        /// <summary>
        ///     Prettier representation.
        /// </summary>
        public override string ToString()
        {
            return SummaryHelper.FormatBool(Value);
        }
    }
}
