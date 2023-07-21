using Mindee.Http;
using Mindee.Parsing.Common;

namespace Mindee.Product.Receipt
{
    /// <summary>
    /// The definition for Receipt, API version 5.
    /// </summary>
    [Endpoint("expense_receipts", "5")]
    public sealed class ReceiptV5 : Inference<ReceiptV5Document, ReceiptV5Document>
    {
    }
}
