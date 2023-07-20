using Mindee.Http;
using Mindee.Parsing.Common;

namespace Mindee.Product.Invoice
{
    /// <summary>
    /// The definition for invoices v4.
    /// </summary>
    [Endpoint("invoices", "4")]
    public sealed class InvoiceV4 : Inference<InvoiceV4Document, InvoiceV4Document>
    {
    }
}
