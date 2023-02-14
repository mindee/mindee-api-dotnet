using Mindee.Parsing.Common;

namespace Mindee.Parsing.Invoice
{
    /// <summary>
    /// The invoice v4 definition.
    /// </summary>
    [Endpoint("invoices", "4")]
    public class InvoiceV4Inference : Inference<InvoiceV4DocumentPrediction, InvoiceV4DocumentPrediction>
    {
    }
}
