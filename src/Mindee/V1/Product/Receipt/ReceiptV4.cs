using Mindee.Http;
using Mindee.V1.Parsing.Common;

namespace Mindee.V1.Product.Receipt
{
    /// <summary>
    /// The definition for Receipt, API version 4.
    /// </summary>
    [Endpoint("expense_receipts", "4")]
    public sealed class ReceiptV4 : Inference<ReceiptV4Document, ReceiptV4Document>
    {
    }
}
