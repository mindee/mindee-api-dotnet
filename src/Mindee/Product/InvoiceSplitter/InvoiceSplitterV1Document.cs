using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing;

namespace Mindee.Product.InvoiceSplitter
{
    /// <summary>
    /// Document data for Invoice Splitter, API version 1.
    /// </summary>
    public sealed class InvoiceSplitterV1Document : IPrediction
    {
        /// <summary>
        /// List of page group.
        /// </summary>
        [JsonPropertyName("invoice_page_groups")]
        public IList<PageGroup> PageGroups { get; set; } = new List<PageGroup>();

        /// <summary>
        /// A prettier representation of the current model values.
        /// </summary>
        public override string ToString()
        {
            if (PageGroups.Count < 1)
            {
                return ":Invoice Page Groups:\n";
            }
            StringBuilder result = new StringBuilder();
            result.Append($":Invoice Page Groups:\n  {string.Join("\n  ", PageGroups.Select(pg => pg.ToString()))}\n");

            return SummaryHelper.Clean(result.ToString());
        }
    }

    /// <summary>
    /// Group of page indexes.
    /// </summary>
    public class PageGroup
    {
        /// <summary>
        /// List of page indexes that belong to the same invoice (group).
        /// </summary>
        [JsonPropertyName("page_indexes")]
        public int[] PageIndexes { get; set; }

        /// <summary>
        /// A prettier representation of the current model values.
        /// </summary>
        public override string ToString()
        {
            return $":Page indexes: {string.Join(", ", PageIndexes)}";
        }
    }
}
