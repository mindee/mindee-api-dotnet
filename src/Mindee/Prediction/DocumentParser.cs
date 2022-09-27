using System.IO;
using System.Threading.Tasks;

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

        public async Task FromInvoice(Stream file, string filename)
        {
            await _invoiceParsing.ExecuteAsync(file, filename);
        }
    }
}
