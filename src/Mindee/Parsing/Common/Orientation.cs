using System.Text.Json.Serialization;
using Mindee.Parsing.Standard;

namespace Mindee.Parsing.Common
{
    /// <summary>
    /// The orientation which was applied from the original page.
    /// </summary>
    public class Orientation
    {
        /// <summary>
        /// Degrees of the rotation
        /// </summary>
        /// <example>90</example>
        [JsonPropertyName("value")]
        public int? Value { get; set; }
    }
}
