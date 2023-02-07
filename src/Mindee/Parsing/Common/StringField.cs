using System;
using System.Text.Json.Serialization;
using Mindee.Geometry;

namespace Mindee.Parsing.Common
{
    /// <summary>
    /// Represent a string field.
    /// </summary>
    public class StringField : BaseField
    {
        /// <summary>
        /// The value of the field.
        /// </summary>
        /// <example>food</example>
        [JsonPropertyName("value")]
        public string Value { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"><see cref="Value"/></param>
        /// <param name="confidence"><see cref="BaseField.Confidence"/></param>
        /// <param name="polygon"><see cref="BaseField.Polygon"/></param>
        /// <param name="pageId"><see cref="BaseField.PageId"/></param>
        public StringField(
            string value,
            double confidence,
            Polygon polygon,
            int? pageId = null) : base(confidence, polygon, pageId)
        {
            Value = value;
        }

        /// <summary>
        /// Prettier representation.
        /// </summary>
        public override string ToString()
        {
            return Value ?? "";
        }
    }
}
