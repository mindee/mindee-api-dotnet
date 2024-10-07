using System;
using System.IO;
using Docnet.Core;
using Docnet.Core.Models;
using Docnet.Core.Readers;
using SkiaSharp;

namespace Mindee.Pdf
{
    /// <summary>
    /// Image compressor static class to handle PDF compression.
    /// </summary>
    public static class PdfCompressor
    {
        /// <summary>
        /// Compresses a PDF file using DocLib.
        /// </summary>
        /// <param name="pdfData">Byte array representing the content of the PDF file.</param>
        /// <param name="imageQuality">Quality of the final file.</param>
        /// <param name="forceSourceTextCompression">Whether to force the rendering of source pdf. If enabled, will attempt to re-write the detected text.</param>
        /// <param name="disableSourceText">If the PDF has source text, whether to re-apply it to the original or not.</param>
        /// <returns>A byte array containing a compressed PDF.</returns>
        public static byte[] CompressPdf(byte[] pdfData, int imageQuality = 85, bool forceSourceTextCompression = false,
            bool disableSourceText = true)
        {
            if (!forceSourceTextCompression && PdfUtils.HasSourceText(pdfData))
            {
                // Note: bypassing the logger since this is **heavily** discouraged.
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(
                    "MINDEE WARNING: Found text inside of the provided PDF file. Compression operation aborted since disableSourceText is set to 'true'.");
                Console.ResetColor();
                return pdfData;
            }

            lock (DocLib.Instance)
            {
                using var docReader = DocLib.Instance.GetDocReader(pdfData, new PageDimensions(1));
                var outputStream = new MemoryStream();

                using (var document = SKDocument.CreatePdf(outputStream))
                {
                    ProcessPages(docReader, document, imageQuality, disableSourceText);
                }

                return outputStream.ToArray();
            }
        }

        private static void ProcessPages(IDocReader docReader, SKDocument document, int imageQuality,
            bool disableSourceText)
        {
            for (int i = 0; i < docReader.GetPageCount(); i++)
            {
                ProcessSinglePage(docReader, document, i, imageQuality, disableSourceText);
            }
        }

        private static void ProcessSinglePage(IDocReader docReader, SKDocument document, int pageIndex,
            int imageQuality, bool disableSourceText)
        {
            using var pageReader = docReader.GetPageReader(pageIndex);
            var width = pageReader.GetPageWidth();
            var height = pageReader.GetPageHeight();

            using var resizedBitmap = PdfUtils.GeneratePageBitmap(imageQuality, pageReader, width, height);

            var canvas = document.BeginPage(width, height);
            PdfUtils.DrawPageContent(canvas, resizedBitmap, pageReader, disableSourceText);
            document.EndPage();
        }
    }
}
