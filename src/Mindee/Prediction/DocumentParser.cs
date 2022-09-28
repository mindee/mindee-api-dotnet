using System.IO;
using System.Threading.Tasks;
using Mindee.Prediction.Invoice;
using Mindee.Prediction.Receipt;

namespace Mindee.Prediction
{
    /// <summary>
    /// Parse a document.
    /// </summary>
    public sealed class DocumentParser
    {
        private readonly IInvoiceParsing _invoiceParsing;
        private readonly IReceiptParsing _receiptParsing;

        public DocumentParser(IInvoiceParsing invoiceParsing
            , IReceiptParsing receiptParsing)
        {
            _invoiceParsing = invoiceParsing;
            _receiptParsing = receiptParsing;
        }

        public async Task<InvoicePrediction> FromInvoice(Stream file, string filename)
        {
            return await _invoiceParsing.ExecuteAsync(file, filename);
        }

        public Task<ReceiptPrediction> FromReceipt(Stream stream, string filename)
        {
            return _receiptParsing.ExecuteAsync(stream, filename);
        }
    }
}
