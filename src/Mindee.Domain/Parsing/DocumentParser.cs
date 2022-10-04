using System.Threading.Tasks;
using System.Xml.Linq;
using Mindee.Domain.Parsing.Common;
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

        public DocumentParser(
            IInvoiceParsing invoiceParsing
            , IReceiptParsing receiptParsing
            , IPassportParsing passportParsing)
        {
            _invoiceParsing = invoiceParsing;
            _receiptParsing = receiptParsing;
            _passportParsing = passportParsing;
        }

        public async Task<Document<InvoicePrediction>> WithInvoiceType(ParseParameter parseParameter)
        {
            return await _invoiceParsing.ExecuteAsync(parseParameter);
        }

        public Task<ReceiptInference> WithReceiptType(ParseParameter parseParameter)
        {
            return _receiptParsing.ExecuteAsync(parseParameter);
        }

        public Task<Document<PassportPrediction>> WithPassportType(ParseParameter parseParameter)
        {
            return _passportParsing.ExecuteAsync(parseParameter);
        }
    }
}
