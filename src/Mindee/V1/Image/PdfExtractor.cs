using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Mindee.Input;
using Mindee.Pdf;
using Mindee.V1.Product.InvoiceSplitter;

namespace Mindee.V1.Image
{
    /// <summary>
    ///     V1 wrapper for the PDF extraction class.
    /// </summary>
    public class PdfExtractor : Mindee.Extraction.PdfExtractor
    {
        /// <inheritdoc />
        public PdfExtractor(LocalInputSource localInput) : base(localInput)
        {
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
