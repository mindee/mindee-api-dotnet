using System;
using System.IO;
using System.Threading.Tasks;
using Mindee.Domain;
using Mindee.Domain.Exceptions;
using Mindee.Domain.Parsing;
using Mindee.Domain.Parsing.Common;
using Mindee.Domain.Parsing.Invoice;
using Mindee.Domain.Parsing.Passport;
using Mindee.Domain.Parsing.Receipt;
using Mindee.Domain.Pdf;

namespace Mindee
{
    public sealed class MindeeClient
    {
        private readonly IPdfOperation _pdfOperation;
        private readonly IInvoiceParsing _invoiceParsing;
        private readonly IReceiptParsing _receiptParsing;
        private readonly IPassportParsing _passportParsing;

        public DocumentClient DocumentClient { get; private set; }

        public MindeeClient(
            IPdfOperation pdfOperation, 
            IInvoiceParsing invoiceParsing,
            IReceiptParsing receiptParsing, 
            IPassportParsing passportParsing)
        {
            _pdfOperation = pdfOperation;
            _invoiceParsing = invoiceParsing;
            _receiptParsing = receiptParsing;
            _passportParsing = passportParsing;
        }

        /// <summary>
        /// Load the document to perform some checks.
        /// </summary>
        /// <param name="file">The stream file.</param>
        /// <param name="filename">The file name.</param>
        /// <exception cref="MindeeException"></exception>
        public MindeeClient LoadDocument(Stream file, string filename)
        {
            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                DocumentClient = new DocumentClient(memoryStream.ToArray(), filename);
            }

            LoadDocument();

            return this;
        }

        /// <summary>
        /// Load the document to perform some checks.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="filename">The file name.</param>
        /// <exception cref="MindeeException"></exception>
        public MindeeClient LoadDocument(byte[] file, string filename)
        {
            DocumentClient = new DocumentClient(file, filename);

            LoadDocument();

            return this;
        }

        private void LoadDocument()
        {
            if (!FileVerification.IsFileNameExtensionRespectLimitation(DocumentClient.Filename))
            {
                throw new MindeeException($"The file type '{Path.GetExtension(DocumentClient.Extension)}' is not supported.");
            }

            if (DocumentClient.Extension.Equals(
            ".pdf",
                StringComparison.InvariantCultureIgnoreCase)
                && !_pdfOperation.CanBeOpen(DocumentClient.File))
            {
                throw new MindeeException($"This document is not recognized as a PDF file.");
            }
        }

        /// <summary>
        /// Try to parse the current document as an invoice.
        /// </summary>
        /// <param name="withFullText">To get all the words in the current document.By default, set to false.</param>
        /// <returns><see cref="Document{InvoicePrediction}"/></returns>
        /// <exception cref="MindeeException"></exception>
        public async Task<Document<InvoicePrediction>> ParseInvoiceAsync(bool withFullText = false)
        {
            if(DocumentClient == null)
            {
                return null;
            }

            return await _invoiceParsing.ExecuteAsync(new ParseParameter(DocumentClient, withFullText));
        }

        /// <summary>
        /// Try to parse the current document as a receipt.
        /// </summary>
        /// <param name="withFullText">To get all the words in the current document.By default, set to false.</param>
        /// <returns><see cref="Document{ReceiptPrediction}"/></returns>
        /// <exception cref="MindeeException"></exception>
        public async Task<Document<ReceiptPrediction>> ParseReceiptAsync(bool withFullText = false)
        {
            if (DocumentClient == null)
            {
                return null;
            }

            return await _receiptParsing.ExecuteAsync(new ParseParameter(DocumentClient, withFullText));

        }

        /// <summary>
        /// Try to parse the current document as a passport.
        /// </summary>
        /// <param name="withFullText">To get all the words in the current document.By default, set to false.</param>
        /// <returns><see cref="Document{PassportPrediction}"/></returns>
        /// <exception cref="MindeeException"></exception>
        public async Task<Document<PassportPrediction>> ParsePassportAsync(bool withFullText = false)
        {
            if (DocumentClient == null)
            {
                return null;
            }

            return await _passportParsing.ExecuteAsync(new ParseParameter(DocumentClient, withFullText));
        }
    }
}
