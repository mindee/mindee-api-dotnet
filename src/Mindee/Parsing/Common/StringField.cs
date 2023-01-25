using System;
using System.Text.Json.Serialization;

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
        /// Prettier representation.
        /// </summary>
        public override string ToString()
        {
            return Value ?? "";
        }
    }
}
