﻿using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Mindee.Exceptions;
using Mindee.Input;
using Mindee.Parsing;
using Mindee.Parsing.Common;
using Mindee.Parsing.CustomBuilder;
using Mindee.Pdf;

namespace Mindee
{
    /// <summary>
    /// The entrypoint to use the Mindee features.
    /// </summary>
    public sealed class MindeeClient
    {
        private readonly IPdfOperation _pdfOperation;
        private readonly MindeeApi _mindeeApi;

        /// <summary>
        /// <see cref="Mindee.DocumentClient"/>
        /// </summary>
        public DocumentClient DocumentClient { get; private set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"><see cref="ILogger"/></param>
        /// <param name="pdfOperation"><see cref="IPdfOperation"/></param>
        /// <param name="configuration"><see cref="IConfiguration"/></param>
        public MindeeClient(
            ILogger<MindeeClient> logger,
            IPdfOperation pdfOperation,
            IConfiguration configuration)
        {
            _pdfOperation = pdfOperation;
            _mindeeApi = new MindeeApi(logger, configuration);
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
        /// Try to parse the current document using custom builder API.
        /// </summary>
        /// <param name="endpoint"><see cref="Endpoint"/></param>
        /// <returns><see cref="Document{CustomPrediction}"/></returns>
        /// <exception cref="MindeeException"></exception>
        public async Task<Document<CustomPrediction>> ParseAsync(Endpoint endpoint)
        {
            if (DocumentClient == null)
            {
                return null;
            }

            return await _mindeeApi.PredictAsync<CustomPrediction>(
                endpoint,
                new PredictParameter(
                    DocumentClient.File,
                    DocumentClient.Filename));
        }

        /// <summary>
        /// Try to parse the current document using custom builder API.
        /// </summary>
        /// <param name="endpoint"><see cref="Endpoint"/></param>
        /// <param name="pageOptions"><see cref="PageOptions"/></param>
        /// <returns><see cref="Document{CustomPrediction}"/></returns>
        /// <exception cref="MindeeException"></exception>
        public async Task<Document<CustomPrediction>> ParseAsync(
            Endpoint endpoint
            , PageOptions pageOptions)
        {
            if (DocumentClient == null)
            {
                return null;
            }

            if (DocumentClient.Extension.Equals(
            ".pdf",
                StringComparison.InvariantCultureIgnoreCase))
            {
                DocumentClient.File = _pdfOperation.Split(new SplitQuery(DocumentClient.File, pageOptions)).File;
            }

            return await _mindeeApi.PredictAsync<CustomPrediction>(
                endpoint,
                new PredictParameter(
                    DocumentClient.File,
                    DocumentClient.Filename));
        }

        /// <summary>
        /// Try to parse the current document.
        /// </summary>
        /// <param name="withFullText">To get all the words in the current document.By default, set to false.</param>
        /// <typeparam name="TPredictionModel">Define the targeted expected type of the parsing.</typeparam>
        /// <returns><see cref="Document{TPredictionModel}"/></returns>
        /// <exception cref="MindeeException"></exception>
        /// <remarks>With full text doesn't work for all the types.</remarks>
        public async Task<Document<TPredictionModel>> ParseAsync<TPredictionModel>(bool withFullText = false)
            where TPredictionModel : class, new()
        {
            if (DocumentClient == null)
            {
                return null;
            }

            return await _mindeeApi.PredictAsync<TPredictionModel>(
                new PredictParameter(
                    DocumentClient.File,
                    DocumentClient.Filename,
                    withFullText));
        }

        /// <summary>
        /// Try to parse the current document.
        /// </summary>
        /// <param name="withFullText">To get all the words in the current document.By default, set to false.</param>
        /// <param name="pageOptions"><see cref="PageOptions"/></param>
        /// <typeparam name="TPredictionModel">Define the targeted expected type of the parsing.</typeparam>
        /// <returns><see cref="Document{TPredictionModel}"/></returns>
        /// <exception cref="MindeeException"></exception>
        /// <remarks>With full text doesn't work for all the types. Check the API documentation first.</remarks>
        public async Task<Document<TPredictionModel>> ParseAsync<TPredictionModel>(
            PageOptions pageOptions
            , bool withFullText = false)
            where TPredictionModel : class, new()
        {
            if (DocumentClient == null)
            {
                return null;
            }

            if (DocumentClient.Extension.Equals(
                ".pdf",
                StringComparison.InvariantCultureIgnoreCase))
            {
                DocumentClient.File = _pdfOperation.Split(new SplitQuery(DocumentClient.File, pageOptions)).File;
            }

            return await _mindeeApi.PredictAsync<TPredictionModel>(
                new PredictParameter(
                    DocumentClient.File,
                    DocumentClient.Filename,
                    withFullText));
        }
    }
}
