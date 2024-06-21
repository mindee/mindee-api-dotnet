using System.IO;
using Mindee.Input;

namespace Mindee.Extraction
{
    /// <summary>
    /// An extracted sub-Pdf.
    /// </summary>
    public class ExtractedPdf
    {
        private readonly byte[] PdfBytes;
        private readonly string Filename;

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
        /// Write the Pdf to a file.
        /// </summary>
        /// <param name="outputPath">the output directory (must exist).</param>
        public void WriteToFile(string outputPath)
        {
            var pdfPath = Path.Combine(outputPath, this.Filename);
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
