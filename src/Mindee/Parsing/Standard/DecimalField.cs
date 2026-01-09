using System.Text.Json.Serialization;
using Mindee.Geometry;

namespace Mindee.Parsing.Standard
{
    /// <summary>
    ///     Represent an amount.
    /// </summary>
    public class DecimalField : BaseField
    {
        /// <summary>
        ///     Empty constructor.
        /// </summary>
        public DecimalField() { }

        /// <summary>
        ///     Default constructor.
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
        public DecimalField(
            decimal? value,
            double? confidence,
            Polygon polygon,
            int? pageId = null) : base(confidence, polygon, pageId)
        {
            Value = value;
        }

        /// <summary>
        ///     An amount value.
        /// </summary>
        /// <example>5.89</example>
        [JsonPropertyName("value")]
        [JsonConverter(typeof(DecimalJsonConverter))]
        public decimal? Value { get; set; }

        /// <summary>
        ///     A pretty summary of the value.
        /// </summary>
        public override string ToString()
        {
            return SummaryHelper.FormatAmount(Value);
        }
    }
}
