using System.IO;
using Docnet.Core;
using Docnet.Core.Models;
using Mindee.Input;

namespace Mindee.Extraction
{
    /// <summary>
    ///     An extracted sub-Pdf.
    /// </summary>
    public class ExtractedPdf
    {
        /// <summary>
        ///     Name of the original file.
        /// </summary>
        public readonly string Filename;

        /// <summary>
        ///     File object for an ExtractedPdf.
        /// </summary>
        public readonly byte[] PdfBytes;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ExtractedPdf" /> class.
        /// </summary>
        /// <param name="pdfBytes">A byte array representation of the Pdf.</param>
        /// <param name="filename">Name of the original file.</param>
        public ExtractedPdf(byte[] pdfBytes, string filename)
        {
            PdfBytes = pdfBytes;
            Filename = filename;
        }

        /// <summary>
        ///     Wrapper for pdf GetPageCount();
        /// </summary>
        /// <returns>The number of pages in the file.</returns>
        public int GetPageCount()
        {
            lock (DocLib.Instance)
            {
                using var docInstance = DocLib.Instance.GetDocReader(PdfBytes, new PageDimensions(1, 1));
                return docInstance.GetPageCount();
            }
        }

        /// <summary>
        ///     Write the Pdf to a file.
        /// </summary>
        /// <param name="outputPath">the output directory (must exist).</param>
        public void WriteToFile(string outputPath)
        {
            var pdfPath = Path.Combine(outputPath, Filename);
            if (Path.GetFileName(outputPath) != string.Empty)
            {
                pdfPath = Path.GetFullPath(outputPath);
            }

            File.WriteAllBytes(pdfPath, PdfBytes);
        }

        /// <summary>
        ///     Return the file in a format suitable for sending to MindeeClient for parsing.
        /// </summary>
        /// <returns>an instance of <see cref="ExtractedPdf" /></returns>
        public LocalInputSource AsInputSource()
        {
            return new LocalInputSource(PdfBytes, Filename);
        }
    }
}
