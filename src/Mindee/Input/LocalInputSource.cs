using System;
using System.IO;
using System.Linq;
using Docnet.Core;
using Docnet.Core.Models;
using Microsoft.Extensions.DependencyInjection;
using Mindee.Exceptions;
using Mindee.Image;
using Mindee.Pdf;

namespace Mindee.Input
{
    /// <summary>
    ///     Represent a document to parse.
    /// </summary>
    public sealed class LocalInputSource
    {
        private static readonly string[] AuthorizedFileExtensions =
        [
            ".heic", ".jpg", ".jpga", ".jpeg", ".pdf", ".png", ".tiff", ".tif", ".webp"
        ];

        private DocNetApi _pdfOperation;

        /// <summary>
        ///     Construct from bytes.
        /// </summary>
        /// <param name="fileBytes">The file contents as bytes.</param>
        /// <param name="filename">The name of the file.</param>
        /// <exception cref="MindeeInputException"></exception>
        public LocalInputSource(byte[] fileBytes, string filename)
        {
            FileBytes = fileBytes;
            SetFileName(filename);
        }

        /// <summary>
        ///     Construct from string.
        /// </summary>
        /// <param name="filePath">The file's local path as a string.</param>
        /// <exception cref="MindeeInputException"></exception>
        public LocalInputSource(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentNullException(filePath);
            }

            var fileInfo = new FileInfo(filePath);
            FileBytes = File.ReadAllBytes(fileInfo.FullName);
            SetFileName(fileInfo.Name);
        }

        /// <summary>
        ///     Construct from stream.
        /// </summary>
        /// <param name="fileStream">The file contents as a stream.</param>
        /// <param name="filename">The file name.</param>
        /// <exception cref="MindeeInputException"></exception>
        public LocalInputSource(Stream fileStream, string filename)
        {
            using (var memoryStream = new MemoryStream())
            {
                fileStream.CopyTo(memoryStream);
                FileBytes = memoryStream.ToArray();
            }

            SetFileName(filename);
        }

        /// <summary>
        ///     Load the document to perform some checks.
        /// </summary>
        /// <param name="fileinfo">ResultFile information from the file to load from disk.</param>
        /// <exception cref="MindeeInputException"></exception>
        public LocalInputSource(FileInfo fileinfo)
        {
            FileBytes = File.ReadAllBytes(fileinfo.FullName);
            SetFileName(fileinfo.Name);
        }

        /// <summary>
        ///     Load a file from a given base64 string.
        /// </summary>
        /// <param name="base64Data"></param>
        /// <param name="filename"></param>
        public LocalInputSource(string base64Data, string filename)
        {
            FileBytes = Convert.FromBase64String(base64Data);
            SetFileName(filename);
        }

        /// <summary>
        ///     The file as a stream.
        /// </summary>
        public byte[] FileBytes { get; set; }

        /// <summary>
        ///     The name of the file.
        /// </summary>
        public string Filename { get; set; }

        /// <summary>
        ///     Extension file's.
        /// </summary>
        public string Extension { get; set; }

        /// <param name="filename"></param>
        /// <exception cref="MindeeInputException"></exception>
        private void SetFileName(string filename)
        {
            if (string.IsNullOrWhiteSpace(filename))
            {
                throw new MindeeInputException($"The filename '{filename}' requires an extension");
            }

            Filename = filename;
            Extension = Path.GetExtension(Filename);
            if (!IsExtensionValid())
            {
                throw new MindeeInputException($"The file extension '{Extension}' is not supported.");
            }
        }

        /// <summary>
        ///     Determine if the file extension is valid.
        /// </summary>
        public bool IsExtensionValid()
        {
            return AuthorizedFileExtensions.Any(f => f.Equals(Extension, StringComparison.InvariantCultureIgnoreCase)
            );
        }

        /// <summary>
        ///     Determine if the file is a PDF.
        /// </summary>
        public bool IsPdf()
        {
            return Extension.Equals(".pdf", StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        ///     Returns the page count for PDF files, or '1' otherwise.
        /// </summary>
        /// <returns>The page count, as an integer.</returns>
        public int GetPageCount()
        {
            if (!IsPdf())
            {
                return 1;
            }

            lock (DocLib.Instance)
            {
                var docInstance = DocLib.Instance.GetDocReader(FileBytes, new PageDimensions(1, 1));
                return docInstance.GetPageCount();
            }
        }

        /// <summary>
        ///     Compresses the file, according to the provided info.
        /// </summary>
        /// <param name="quality">Quality of the output file.</param>
        /// <param name="maxWidth">Maximum width (Ignored for PDFs)</param>
        /// <param name="maxHeight">Maximum height (Ignored for PDFs)</param>
        /// <param name="forceSourceText">
        ///     Whether to force the operation on PDFs with source text. This will attempt to
        ///     re-render PDF text over the rasterized original. If disabled, ignored the operation.
        ///     WARNING: this operation is strongly discouraged.
        /// </param>
        /// <param name="disableSourceText">If the PDF has source text, whether to re-apply it to the original or not.</param>
        public void Compress(int quality = 85, int? maxWidth = null, int? maxHeight = null,
            bool forceSourceText = false, bool disableSourceText = true)
        {
            if (IsPdf())
            {
                FileBytes = PdfCompressor.CompressPdf(FileBytes, quality, forceSourceText, disableSourceText);
            }
            else
            {
                FileBytes = ImageCompressor.CompressImage(FileBytes, quality, maxWidth, maxHeight);
            }
        }

        /// <summary>
        ///     Apply cut and merge options on multipage documents.
        /// </summary>
        /// <param name="pageOptions"></param>
        public void ApplyPageOptions(PageOptions pageOptions)
        {
            var serviceCollection = new ServiceCollection();
            var serviceProvider = serviceCollection.BuildServiceProvider();
            _pdfOperation = serviceProvider.GetService<DocNetApi>();
            if (_pdfOperation == null)
            {
                _pdfOperation = new DocNetApi();
                serviceCollection.AddSingleton(_pdfOperation);
            }

            if (pageOptions != null && IsPdf())
            {
                FileBytes = _pdfOperation.Split(
                    new SplitQuery(FileBytes, pageOptions)).File;
            }
        }

        /// <summary>
        ///     Returns true if the source PDF has source text inside. Returns false for images.
        /// </summary>
        /// <returns>True if at least one character exists in one page.</returns>
        public bool HasSourceText()
        {
            if (!IsPdf())
            {
                return false;
            }

            return PdfUtils.HasSourceText(FileBytes);
        }
    }
}
