using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Mindee.Domain.Exceptions;
using Mindee.Domain.Parsing;
using Mindee.Domain.Parsing.Common;
using Mindee.Domain.Parsing.Invoice;
using Mindee.Domain.Parsing.Passport;
using Mindee.Domain.Parsing.Receipt;
using Mindee.Domain.Pdf;

namespace Mindee.Domain
{
    public sealed class MindeeClient
    {
        private readonly ILogger _logger;
        private readonly IPdfOperation _pdfOperation;
        private readonly MindeeApi _mindeeApi;

        public DocumentClient DocumentClient { get; private set; }

        public MindeeClient(
            ILogger<MindeeClient> logger, 
            IPdfOperation pdfOperation,
            IConfiguration configuration)
        {
            _logger = logger;
            _pdfOperation = pdfOperation;
            _mindeeApi = new MindeeApi(_logger, configuration);
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

        /// <summary>
        /// Load the document to perform some checks.
        /// </summary>
        /// <param name="fileinfo">File information from the file to load from disk.</param>
        /// <exception cref="MindeeException"></exception>
        public MindeeClient LoadDocument(FileInfo fileinfo)
        {
            DocumentClient = new DocumentClient(File.ReadAllBytes(fileinfo.FullName), fileinfo.Name);

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

            return await _mindeeApi.PredictInvoiceAsync(
                new PredictParameter(
                    DocumentClient.File,
                    DocumentClient.Filename,
                    withFullText));
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

            return await _mindeeApi.PredictReceiptAsync(
                new PredictParameter(
                    DocumentClient.File,
                    DocumentClient.Filename,
                    withFullText));
        }

        /// <summary>
        /// Try to parse the current document as a passport.
        /// </summary>
        /// <param name="withFullText">To get all the words in the current document.By default, set to false.</param>
        /// <returns><see cref="Document{PassportPrediction}"/></returns>
        /// <exception cref="MindeeException"></exception>
        public async Task<Document<PassportPrediction>> ParsePassportAsync()
        {
            if (DocumentClient == null)
            {
                return null;
            }

            return await _mindeeApi.PredictPassportAsync(
                new PredictParameter(
                    DocumentClient.File,
                    DocumentClient.Filename));
        }
    }
}
