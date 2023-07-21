using Mindee.Http;
using Mindee.Parsing.Common;

namespace Mindee.Product.InvoiceSplitter
{
    /// <summary>
    /// The definition for Invoice Splitter, API version 1.
    /// </summary>
    [Endpoint("invoice_splitter", "1")]
    public sealed class InvoiceSplitterV1 : Inference<InvoiceSplitterV1Document, InvoiceSplitterV1Document>
    {
    }
}
