using Mindee.Http;
using Mindee.Parsing.Common;

namespace Mindee.Product.Receipt
{
    /// <summary>
    /// The definition for expense_receipts v5.
    /// </summary>
    [Endpoint("expense_receipts", "5")]
    public sealed class ReceiptV5 : Inference<ReceiptV5Document, ReceiptV5Document>
    {
    }
}
