using System.IO;
using System.Threading.Tasks;
using Mindee.Domain;
using Mindee.Domain.Parsing;
using Mindee.Domain.Parsing.Common;
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
        /// <param name="withFullText">To get all the words in the current document.By default, set to false.</param>
        /// <returns><see cref="Document{InvoicePrediction}"/></returns>
        public async Task<Document<InvoicePrediction>> ParseInvoiceAsync(bool withFullText = false)
        {
            if(DocumentClient == null)
            {
                return null;
            }

            return await _documentParser.WithInvoiceType(new ParseParameter(DocumentClient, withFullText));
        }

        /// <summary>
        /// Try to parse the current document as a receipt.
        /// </summary>
        /// <param name="withFullText">To get all the words in the current document.By default, set to false.</param>
        /// <returns><see cref="Document{ReceiptPrediction}"/></returns>
        public async Task<Document<ReceiptPrediction>> ParseReceiptAsync(bool withFullText = false)
        {
            if (DocumentClient == null)
            {
                return null;
            }

            return await _documentParser.WithReceiptType(new ParseParameter(DocumentClient, withFullText));

        }

        /// <summary>
        /// Try to parse the current document as a passport.
        /// </summary>
        /// <param name="withFullText">To get all the words in the current document.By default, set to false.</param>
        /// <returns><see cref="Document{PassportPrediction}"/></returns>
        public async Task<Document<PassportPrediction>> ParsePassportAsync(bool withFullText = false)
        {
            if (DocumentClient == null)
            {
                return null;
            }

            return await _documentParser.WithPassportType(new ParseParameter(DocumentClient, withFullText));
        }
    }
}
