using System.IO;
using System.Threading.Tasks;
using Mindee.Prediction.Invoice;

namespace Mindee.Prediction
{
    /// <summary>
    /// Parse a document.
    /// </summary>
    public sealed class DocumentParser
    {
        private readonly IInvoiceParsing _invoiceParsing;

        public DocumentParser(IInvoiceParsing invoiceParsing)
        {
            _invoiceParsing = invoiceParsing;
        }

        public async Task<InvoicePrediction> FromInvoice(Stream file, string filename)
        {
            return await _invoiceParsing.ExecuteAsync(file, filename);
        }
    }
}
