using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing.Common;

namespace Mindee.Parsing.InvoiceSplitter
{
    /// <summary>
    /// The invoice splitter model for the v1.
    /// </summary>
    public sealed class InvoiceSplitterV1DocumentPrediction
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
