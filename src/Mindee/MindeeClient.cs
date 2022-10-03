using System.IO;
using System.Threading.Tasks;
using Mindee.Domain.Parsing;
using Mindee.Domain.Parsing.Invoice;
using Mindee.Domain.Parsing.Passport;
using Mindee.Domain.Parsing.Receipt;

namespace Mindee
{
    public sealed class MindeeClient
    {
        private readonly DocumentParser _documentParser;
        public DocumentClient DocumentClient { get; private set; }

        public MindeeClient(DocumentParser documentParser)
        {
            _documentParser = documentParser;
        }

        public MindeeClient LoadDocument(Stream file, string filename)
        {
            DocumentClient = new DocumentClient(file, filename);

            return this;
        }

        public async Task<InvoiceInference> ParseInvoiceAsync()
        {
            if(DocumentClient == null)
            {
                return null;
            }

            return await _documentParser.WithInvoiceType(DocumentClient.File, DocumentClient.Filename);
        }

        public async Task<ReceiptInference> ParseReceiptAsync()
        {
            if (DocumentClient == null)
            {
                return null;
            }

            return await _documentParser.WithReceiptType(DocumentClient.File, DocumentClient.Filename);

        }

        public async Task<PassportInference> ParsePassportAsync()
        {
            if (DocumentClient == null)
            {
                return null;
            }

            return await _documentParser.WithPassportType(DocumentClient.File, DocumentClient.Filename);
        }
    }
}
