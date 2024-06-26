using System.Text.Json.Serialization;

namespace Mindee.Product.InvoiceSplitter
{
    /// <summary>
    /// Group of page indexes.
    /// </summary>
    public class InvoiceSplitterV1PageGroup
    {
        /// <summary>
        /// List of page indexes that belong to the same invoice (group).
        /// </summary>
        [JsonPropertyName("page_indexes")]
        public int[] PageIndexes { get; set; }

        /// <summary>
        /// The confidence about the zone of the value extracted.
        /// A value from 0 to 1.
        /// </summary>
        /// <example>0.9</example>
        [JsonPropertyName("confidence")]
        public double Confidence { get; set; }

        /// <summary>
        /// A prettier representation of the current model values.
        /// </summary>
        public override string ToString()
        {
            return $":Page indexes: {string.Join(", ", PageIndexes)}";
        }
    }
}
