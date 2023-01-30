using System;
using System.IO;
using System.Threading.Tasks;
using Mindee.Exceptions;
using Mindee.Input;
using Mindee.Parsing;
using Mindee.Parsing.Common;
using Mindee.Parsing.CustomBuilder;
using Mindee.Pdf;

namespace Mindee
{
    /// <summary>
    /// The entry point to use the Mindee features.
    /// </summary>
    public sealed class MindeeClient
    {
        private readonly IPdfOperation _pdfOperation;
        private readonly IPredictable _mindeeApi;

        /// <summary>
        /// <see cref="Mindee.DocumentClient"/>
        /// </summary>
        public DocumentClient DocumentClient { get; private set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="pdfOperation"><see cref="IPdfOperation"/></param>
        /// <param name="predictable"><see cref="IPredictable"/></param>
        public MindeeClient(
            IPdfOperation pdfOperation,
            IPredictable predictable)
        {
            _pdfOperation = pdfOperation;
            _mindeeApi = predictable;
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
        }

        /// <summary>
        /// Try to parse the current document using custom builder API.
        /// </summary>
        /// <param name="endpoint"><see cref="CustomEndpoint"/></param>
        /// <returns><see cref="Document{CustomV1Inference}"/></returns>
        /// <exception cref="MindeeException"></exception>
        public async Task<Document<CustomV1Inference>> ParseAsync(CustomEndpoint endpoint)
        {
            if (DocumentClient == null)
            {
                return null;
            }

            return await _mindeeApi.PredictAsync<CustomV1Inference>(
                endpoint,
                new PredictParameter(
                    DocumentClient.File,
                    DocumentClient.Filename));
        }

        /// <summary>
        /// Try to parse the current document using custom builder API.
        /// </summary>
        /// <param name="endpoint"><see cref="CustomEndpoint"/></param>
        /// <param name="pageOptions"><see cref="PageOptions"/></param>
        /// <returns><see cref="Document{CustomPrediction}"/></returns>
        /// <exception cref="MindeeException"></exception>
        public async Task<Document<CustomV1Inference>> ParseAsync(
            CustomEndpoint endpoint
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

            return await _mindeeApi.PredictAsync<CustomV1Inference>(
                endpoint,
                new PredictParameter(
                    DocumentClient.File,
                    DocumentClient.Filename));
        }

        /// <summary>
        /// Try to parse the current document.
        /// </summary>
        /// <param name="withFullText">Get all the words in the current document.By default, set to false.</param>
        /// <param name="withCropper">To get the cropper information about the current document.By default, set to false.</param>
        /// <typeparam name="TInferenceModel">Set the prediction model used to parse the document.
        /// The response object will be instantiated based on this parameter.</typeparam>
        /// <returns><see cref="Document{TInferenceModel}"/></returns>
        /// <exception cref="MindeeException"></exception>
        /// <remarks>With full text doesn't work for all the types.</remarks>
        public async Task<Document<TInferenceModel>> ParseAsync<TInferenceModel>(
            bool withFullText = false
            , bool withCropper = false)
            where TInferenceModel : class, new()
        {
            if (DocumentClient == null)
            {
                return null;
            }

            return await _mindeeApi.PredictAsync<TInferenceModel>(
                new PredictParameter(
                    DocumentClient.File,
                    DocumentClient.Filename,
                    withFullText,
                    withCropper));
        }

        /// <summary>
        /// Try to parse the current document.
        /// </summary>
        /// <param name="withFullText">To get all the words in the current document.By default, set to false.</param>
        /// <param name="withCropper">To get the cropping results about the current document.By default, set to false.</param>
        /// <param name="pageOptions"><see cref="PageOptions"/></param>
        /// <typeparam name="TInferenceModel">Set the prediction model used to parse the document.
        /// The response object will be instantiated based on this parameter.</typeparam>
        /// <returns><see cref="Document{TInferenceModel}"/></returns>
        /// <exception cref="MindeeException"></exception>
        /// <remarks>With full text doesn't work for all the types. Check the API documentation first.</remarks>
        public async Task<Document<TInferenceModel>> ParseAsync<TInferenceModel>(
            PageOptions pageOptions
            , bool withFullText = false
            , bool withCropper = false)
            where TInferenceModel : class, new()
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

            return await _mindeeApi.PredictAsync<TInferenceModel>(
                new PredictParameter(
                    DocumentClient.File,
                    DocumentClient.Filename,
                    withFullText,
                    withCropper));
        }
    }
}
