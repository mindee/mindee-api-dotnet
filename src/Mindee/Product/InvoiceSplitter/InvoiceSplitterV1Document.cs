using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing;
using Mindee.Parsing.Standard;

namespace Mindee.Product.InvoiceSplitter
{
    /// <summary>
    /// Invoice Splitter API version 1.4 document data.
    /// </summary>
    public class InvoiceSplitterV1Document : IPrediction
    {
        /// <summary>
        /// List of page groups. Each group represents a single invoice within a multi-invoice document.
        /// </summary>
        [JsonPropertyName("invoice_page_groups")]
        [JsonConverter(typeof(ObjectListJsonConverter<InvoiceSplitterV1InvoicePageGroups, InvoiceSplitterV1InvoicePageGroup>))]
        public InvoiceSplitterV1InvoicePageGroups InvoicePageGroups { get; set; }

        /// <summary>
        /// A prettier representation of the current model values.
        /// </summary>
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.Append($":Invoice Page Groups:{InvoicePageGroups}");
            return SummaryHelper.Clean(result.ToString());
        }
    }
}
