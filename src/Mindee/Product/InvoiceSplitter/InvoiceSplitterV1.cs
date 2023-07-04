using Mindee.Http;
using Mindee.Parsing.Common;

namespace Mindee.Product.InvoiceSplitter
{
    /// <summary>
    /// The invoice splitter v1 definition.
    /// </summary>
    [Endpoint("invoice_splitter", "1")]
    public class InvoiceSplitterV1 : Inference<InvoiceSplitterV1Document, InvoiceSplitterV1Document>
    {
    }
}
