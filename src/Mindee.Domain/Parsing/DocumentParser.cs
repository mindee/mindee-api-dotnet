using System.IO;
using System.Threading.Tasks;
using Mindee.Domain.Parsing.Invoice;
using Mindee.Domain.Parsing.Passport;
using Mindee.Domain.Parsing.Receipt;

namespace Mindee.Domain.Parsing
{
    /// <summary>
    /// Parse a document.
    /// </summary>
    public sealed class DocumentParser
    {
        private readonly IInvoiceParsing _invoiceParsing;
        private readonly IReceiptParsing _receiptParsing;
        private readonly IPassportParsing _passportParsing;

        public DocumentParser(IInvoiceParsing invoiceParsing
            , IReceiptParsing receiptParsing
            , IPassportParsing passportParsing)
        {
            _invoiceParsing = invoiceParsing;
            _receiptParsing = receiptParsing;
            _passportParsing = passportParsing;
        }

        public async Task<InvoiceInference> WithInvoiceType(Stream file, string filename)
        {
            return await _invoiceParsing.ExecuteAsync(file, filename);
        }

        public Task<ReceiptInference> WithReceiptType(Stream stream, string filename)
        {
            return _receiptParsing.ExecuteAsync(stream, filename);
        }

        public Task<PassportInference> WithPassportType(Stream stream, string filename)
        {
            return _passportParsing.ExecuteAsync(stream, filename);
        }
    }
}
