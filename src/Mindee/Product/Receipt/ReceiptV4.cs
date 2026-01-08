using Mindee.Http;
using Mindee.Parsing.Common;

namespace Mindee.Product.Receipt
{
    /// <summary>
    ///     The definition for Receipt, API version 4.
    /// </summary>
    [Endpoint("expense_receipts", "4")]
    public sealed class ReceiptV4 : Inference<ReceiptV4Document, ReceiptV4Document>
    {
    }
}
