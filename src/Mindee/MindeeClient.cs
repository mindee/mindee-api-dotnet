using System.IO;
using System.Threading.Tasks;
using Mindee.Domain;
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

        /// <summary>
        /// Load the document to perform some checks.
        /// </summary>
        /// <param name="file"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public MindeeClient LoadDocument(Stream file, string filename)
        {
            DocumentClient = new DocumentClient(file, filename);

            return this;
        }

        /// <summary>
        /// Try to parse the current document as an invoice.
        /// </summary>
        /// <returns><see cref="InvoiceInference"/></returns>
        public async Task<InvoiceInference> ParseInvoiceAsync()
        {
            if(DocumentClient == null)
            {
                return null;
            }

            return await _documentParser.WithInvoiceType(new ParseParameter(DocumentClient));
        }

        /// <summary>
        /// Try to parse the current document as a receipt.
        /// </summary>
        /// <returns><see cref="ReceiptInference"/></returns>
        public async Task<ReceiptInference> ParseReceiptAsync()
        {
            if (DocumentClient == null)
            {
                return null;
            }

            return await _documentParser.WithReceiptType(new ParseParameter(DocumentClient));

        }

        /// <summary>
        /// Try to parse the current document as a passport.
        /// </summary>
        /// <returns><see cref="PassportInference"/></returns>
        public async Task<PassportInference> ParsePassportAsync()
        {
            if (DocumentClient == null)
            {
                return null;
            }

            return await _documentParser.WithPassportType(new ParseParameter(DocumentClient));
        }
    }
}
