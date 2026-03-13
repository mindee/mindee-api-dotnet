using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Mindee.V2.Product.Split
{
    /// <summary>
    /// Result of a split utility inference.
    /// </summary>
    public class SplitResult
    {
        /// <summary>
        /// A single document as identified when splitting a multi-document source file.
        /// </summary>
        [JsonPropertyName("splits")]
        public List<SplitRange> Splits { get; set; }

        /// <summary>
        /// String representation.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string splits = string.Join("\n", this.Splits.Select(item => item.ToString()));

            return $"Splits\n======\n{splits}";
        }
    }
}
