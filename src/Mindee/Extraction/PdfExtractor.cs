using System;
using System.Collections.Generic;
using System.IO;
using Docnet.Core;
using Microsoft.Extensions.Logging.Abstractions;
using Mindee.Exceptions;
using Mindee.Input;
using Mindee.Pdf;
using SkiaSharp;

namespace Mindee.Extraction
{
    /// <summary>
    ///     PDF extraction class.
    /// </summary>
    public class PdfExtractor
    {
        /// <summary>
        /// Local input source.
        /// </summary>
        protected readonly LocalInputSource LocalInput;

        /// <summary>
        /// Source PDF bytes.
        /// </summary>
        protected byte[] SourcePdf;

        /// <summary>
        ///     Initializes a new instance of the <see cref="PdfExtractor" /> class.
        /// </summary>
        /// <param name="localInput">Instance of a LocalInputSource, provided by the user.</param>
        public PdfExtractor(LocalInputSource localInput)
        {
            LocalInput = localInput;
        }

        /// <summary>
        ///     Wrapper for PDF GetPageCount();
        /// </summary>
        /// <returns>The number of pages in the file.</returns>
        public int GetPageCount()
        {
            return LocalInput.GetPageCount();
        }

        /// <summary>
        /// Extract the PDF bytes.
        /// </summary>
        /// <returns></returns>
        protected byte[] PdfBytes()
        {
            if (SourcePdf != null)
            {
                return this.SourcePdf;
            }
            if (LocalInput.IsPdf())
            {
                SourcePdf = LocalInput.FileBytes;
            }
            else
            {
                var memoryStream = new MemoryStream();
                using var image = SKImage.FromEncodedData(LocalInput.FileBytes);
                using var bmp = SKBitmap.FromImage(image);
                var pageSize = new SKSize(bmp.Width, bmp.Height);
                using (var document = SKDocument.CreatePdf(memoryStream))
                {
                    var canvas = document.BeginPage(pageSize.Width, pageSize.Height);
                    canvas.DrawBitmap(bmp, SKPoint.Empty);
                    document.EndPage();
                }

                SourcePdf = memoryStream.ToArray();
            }

            return SourcePdf;
        }

        /// <summary>
        ///     Extracts sub-documents from the source document using list of page indexes.
        /// </summary>
        /// <param name="pageIndexes">List of sub-lists of pages to keep.</param>
        /// <returns>Extracted documents.</returns>
        /// <exception cref="ArgumentException"></exception>
        public List<ExtractedPdf> ExtractSubDocuments(List<List<int>> pageIndexes)
        {
            var extractedPdfs = new List<ExtractedPdf>();

            foreach (var pageIndexElem in pageIndexes)
            {
                if (pageIndexElem.Count == 0)
                {
                    throw new MindeeInputException("Empty indexes not allowed for extraction.");
                }

                var extension = Path.GetExtension(LocalInput.Filename);
                var prefix = Path.GetFileNameWithoutExtension(LocalInput.Filename);
                var fieldFilename =
                    $"{prefix}_{pageIndexElem[0] + 1:D3}-{pageIndexElem[pageIndexElem.Count - 1] + 1:D3}{extension}";

                var splitQuery = new SplitQuery(
                    PdfBytes(),
                    new PageOptions(pageIndexElem.ConvertAll(item => (short)item).ToArray()));
                lock (DocLib.Instance)
                {
                    var pdfOperation = new DocNetApi(new NullLogger<DocNetApi>());
                    var mergedPdfBytes = pdfOperation.Split(splitQuery).File;
                    extractedPdfs.Add(new ExtractedPdf(mergedPdfBytes, fieldFilename));
                }
            }

            return extractedPdfs;
        }
    }
}
