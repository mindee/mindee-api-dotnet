using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing;
using Mindee.Parsing.Standard;

namespace Mindee.Product.InvoiceSplitter
{
    /// <summary>
    /// List of page groups. Each group represents a single invoice within a multi-invoice document.
    /// </summary>
    public sealed class InvoiceSplitterV1InvoicePageGroup : LineItemField
    {
        /// <summary>
        /// List of page indexes that belong to the same invoice (group).
        /// </summary>
        [JsonPropertyName("page_indexes")]
        public List<int> PageIndexes { get; set; }

        private Dictionary<string, string> TablePrintableValues()
        {
            return new Dictionary<string, string>()
            {
                {"PageIndexes", String.Join(", ", PageIndexes)},
            };
        }

        /// <summary>
        /// Output the line in a format suitable for inclusion in an rST table.
        /// </summary>
        public override string ToTableLine()
        {
            Dictionary<string, string> printable = TablePrintableValues();
            return "| "
              + String.Format("{0,-72}", printable["PageIndexes"])
              + " |";
        }

        /// <summary>
        /// A prettier representation of the line values.
        /// </summary>
        public override string ToString()
        {
            Dictionary<string, string> printable = PrintableValues();
            return "Page Indexes: "
              + printable["PageIndexes"].Trim();
        }

        private Dictionary<string, string> PrintableValues()
        {
            return new Dictionary<string, string>()
            {
                {"PageIndexes", String.Join(", ", PageIndexes)},
            };
        }
    }

    /// <summary>
    /// List of page groups. Each group represents a single invoice within a multi-invoice document.
    /// </summary>
    public class InvoiceSplitterV1InvoicePageGroups : List<InvoiceSplitterV1InvoicePageGroup>
    {
        /// <summary>
        /// Default string representation.
        /// </summary>
        public override string ToString()
        {
            if (this.Count == 0)
            {
                return "\n";
            }
            int[] columnSizes = { 74 };
            StringBuilder outStr = new StringBuilder("\n");
            outStr.Append("  " + SummaryHelper.LineSeparator(columnSizes, '-') + "  ");
            outStr.Append("| Page Indexes                                                             ");
            outStr.Append("|\n  " + SummaryHelper.LineSeparator(columnSizes, '='));
            outStr.Append(SummaryHelper.ArrayToString(this, columnSizes));
            return outStr.ToString();
        }
    }
}
