using System;
using System.IO;
using Docnet.Core;
using Docnet.Core.Models;
using Mindee.Input;

namespace Mindee.Extraction
{
    /// <summary>
    /// An extracted sub-Pdf.
    /// </summary>
    public class ExtractedPdf
    {
        /// <summary>
        /// File object for an ExtractedPdf.
        /// </summary>
        public readonly byte[] PdfBytes;
        /// <summary>
        /// Name of the original file.
        /// </summary>
        public readonly string Filename;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExtractedPdf"/> class.
        /// </summary>
        /// <param name="pdfBytes">A byte array representation of the Pdf.</param>
        /// <param name="filename">Name of the original file.</param>
        public ExtractedPdf(byte[] pdfBytes, string filename)
        {
            this.PdfBytes = pdfBytes;
            this.Filename = filename;
        }

        /// <summary>
        /// Wrapper for pdf GetPageCount();
        /// </summary>
        /// <returns>The number of pages in the file.</returns>
        public int GetPageCount()
        {
            var docInstance = DocLib.Instance.GetDocReader(this.PdfBytes, new PageDimensions(1, 1));
            return docInstance.GetPageCount();
        }

        /// <summary>
        /// Write the Pdf to a file.
        /// </summary>
        /// <param name="outputPath">the output directory (must exist).</param>
        public void WriteToFile(string outputPath)
        {
            var pdfPath = Path.Combine(outputPath, this.Filename);
            if (Path.GetFileName(outputPath) != String.Empty)
            {
                pdfPath = Path.GetFullPath(outputPath);
            }
            File.WriteAllBytes(pdfPath, this.PdfBytes);
        }

        /// <summary>
        /// Return the file in a format suitable for sending to MindeeClient for parsing.
        /// </summary>
        /// <returns>an instance of <see cref="ExtractedPdf"/></returns>
        public LocalInputSource AsInputSource()
        {
            return new LocalInputSource(this.PdfBytes, this.Filename);
        }
    }
}
