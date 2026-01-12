using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Docnet.Core;
using Docnet.Core.Models;
using Microsoft.Extensions.Logging.Abstractions;
using Mindee.Input;
using Mindee.Pdf;
using Mindee.Product.InvoiceSplitter;
using SkiaSharp;

namespace Mindee.Extraction
{
    /// <summary>
    ///     PDF extraction class.
    /// </summary>
    public class PdfExtractor
    {
        private readonly string Filename;
        private readonly byte[] SourcePdf;

        /// <summary>
        ///     Initializes a new instance of the <see cref="PdfExtractor" /> class.
        /// </summary>
        /// <param name="localInput">Instance of a LocalInputSource, provided by the user.</param>
        public PdfExtractor(LocalInputSource localInput)
        {
            Filename = localInput.Filename;

            if (localInput.IsPdf())
            {
                SourcePdf = localInput.FileBytes;
            }
            else
            {
                var memoryStream = new MemoryStream();
                var image = SKImage.FromEncodedData(localInput.FileBytes);
                var bmp = SKBitmap.FromImage(image);
                var pageSize = new SKSize(bmp.Width, bmp.Height);
                using (var document = SKDocument.CreatePdf(memoryStream))
                {
                    var canvas = document.BeginPage(pageSize.Width, pageSize.Height);
                    canvas.DrawBitmap(bmp, SKPoint.Empty);
                }

                SourcePdf = memoryStream.ToArray();
            }
        }

        /// <summary>
        ///     Wrapper for pdf GetPageCount();
        /// </summary>
        /// <returns>The number of pages in the file.</returns>
        public int GetPageCount()
        {
            var docInstance = DocLib.Instance.GetDocReader(SourcePdf, new PageDimensions(1, 1));
            return docInstance.GetPageCount();
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
                if (!pageIndexElem.Any())
                {
                    throw new ArgumentException("Empty indexes not allowed for extraction.");
                }

                var extension = Path.GetExtension(Filename);
                var prefix = Path.GetFileNameWithoutExtension(Filename);
                var fieldFilename =
                    $"{prefix}_{pageIndexElem[0] + 1:D3}-{pageIndexElem[pageIndexElem.Count - 1] + 1:D3}{extension}";

                var splitQuery = new SplitQuery(
                    SourcePdf,
                    new PageOptions(pageIndexElem.ConvertAll(item => (short)item).ToArray()));
                var pdfOperation = new DocNetApi(new NullLogger<DocNetApi>());
                var mergedPdfBytes = pdfOperation.Split(splitQuery).File;
                extractedPdfs.Add(new ExtractedPdf(mergedPdfBytes, fieldFilename));
            }

            return extractedPdfs;
        }

        /// <summary>
        ///     Extracts invoices as complete PDFs from the document. Include cuts for confidence scores below 1.0.
        /// </summary>
        /// <param name="pageIndexes">List of sub-lists of pages to keep.</param>
        /// <returns>A list of extracted invoices.</returns>
        public List<ExtractedPdf> ExtractInvoices(List<InvoiceSplitterV1InvoicePageGroup> pageIndexes)
        {
            var indexes = pageIndexes.Select(pi => pi.PageIndexes.ToList()).ToList();
            return ExtractSubDocuments(indexes.ToList());
        }

        /// <summary>
        ///     Extracts invoices as complete PDFs from the document.
        /// </summary>
        /// <param name="pageIndexes">List of sub-lists of pages to keep.</param>
        /// <param name="strict">Whether to trust confidence scores of 1.0 only or not.</param>
        /// <returns>A list of extracted invoices.</returns>
        public List<ExtractedPdf> ExtractInvoices(IList<InvoiceSplitterV1InvoicePageGroup> pageIndexes, bool strict)
        {
            if (!strict)
            {
                return ExtractInvoices(pageIndexes.ToList());
            }

            var correctPageIndexes = new List<List<int>>();
            var iterator = pageIndexes.GetEnumerator();
            using var iterator1 = (IDisposable)iterator;
            var currentList = new List<int>();
            double? previousConfidence = null;

            while (iterator.MoveNext())
            {
                var pageIndex = iterator.Current;
                Debug.Assert(pageIndex != null, nameof(pageIndex) + " != null");
                var confidence = pageIndex.Confidence ?? 0.0;
                var pageList = pageIndex.PageIndexes;

                if (Math.Abs(confidence - 1.0) < 0.01 && previousConfidence == null)
                {
                    currentList = new List<int>(pageList);
                }
                else if (Math.Abs(confidence - 1.0) < 0.01)
                {
                    correctPageIndexes.Add(currentList);
                    currentList = new List<int>(pageList);
                }
                else if (confidence == 0.0 && !iterator.MoveNext())
                {
                    currentList.AddRange(pageList);
                    correctPageIndexes.Add(currentList);
                }
                else
                {
                    correctPageIndexes.Add(currentList);
                    correctPageIndexes.Add(pageList.ToList());
                }

                previousConfidence = confidence;
            }

            return ExtractSubDocuments(correctPageIndexes);
        }
    }
}
