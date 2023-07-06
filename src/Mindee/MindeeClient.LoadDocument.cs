using System.IO;
using Microsoft.Extensions.Options;
using Mindee.Exceptions;
using Mindee.Http;
using Mindee.Input;
using Mindee.Pdf;

namespace Mindee
{
    /// <summary>
    /// The entry point to use the Mindee features.
    /// </summary>
    public sealed partial class MindeeClient
    {
        private readonly IPdfOperation _pdfOperation;
        private readonly IHttpApi _mindeeApi;

        /// <summary>
        /// <see cref="Mindee.DocumentClient"/>
        /// </summary>
        public DocumentClient DocumentClient { get; private set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="apiKey">The required API key to use Mindee.</param>
        public MindeeClient(string apiKey)
        {
            var mindeeSettings = new MindeeSettings
            {
                ApiKey = apiKey
            };
            _pdfOperation = new DocNetApi();
            _mindeeApi = new MindeeApi(Options.Create(mindeeSettings));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="mindeeSettings"><see cref="MindeeSettings"/></param>
        public MindeeClient(MindeeSettings mindeeSettings)
        {
            _pdfOperation = new DocNetApi();
            _mindeeApi = new MindeeApi(Options.Create(mindeeSettings));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="pdfOperation"><see cref="IPdfOperation"/></param>
        /// <param name="httpApi"><see cref="IHttpApi"/></param>
        public MindeeClient(
            IPdfOperation pdfOperation,
            IHttpApi httpApi)
        {
            _pdfOperation = pdfOperation;
            _mindeeApi = httpApi;
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
    }
}
