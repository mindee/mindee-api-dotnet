using Mindee.Http;
using Mindee.Parsing.Common;

namespace Mindee.Product.Invoice
{
    /// <summary>
    /// The invoice v4 definition.
    /// </summary>
    [Endpoint("invoices", "4")]
    public class InvoiceV4 : Inference<InvoiceV4Document, InvoiceV4Document>
    {
    }
}
