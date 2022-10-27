using System.Text.Json.Serialization;
using Mindee.Parsing.Common;

namespace Mindee.Parsing.Receipt
{
    /// <summary>
    /// Represent a category.
    /// </summary>
    public class Category : BaseField
    {
        /// <summary>
        /// The value of the field.
        /// </summary>
        [JsonPropertyName("value")]
        public string Value { get; set; }
    }
}
