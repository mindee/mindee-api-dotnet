using System.Collections.Generic;
using System.IO;
using Mindee.Input;

namespace Mindee.Pdf
{
    /// <summary>
    ///     An extracted sub-Pdf.
    /// </summary>
    public class ExtractedPdf
    {
        /// <summary>
        /// Local input source.
        /// </summary>
        public readonly LocalInputSource LocalInput;

        /// <summary>
        /// Page count.
        /// </summary>
        public int PageCount { get; set; }

        /// <summary>
        /// Name of the original file.
        /// </summary>
        public readonly string Filename;

        /// <summary>
        /// 0-based indexes of all pages taken from the original PDF.
        /// </summary>
        public readonly List<int> PageIndexes;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ExtractedPdf" /> class.
        /// </summary>
        /// <param name="fileBytes">A byte array representation of the PDF.</param>
        /// <param name="filename"></param>
        /// <param name="pageIndexes"></param>
        public ExtractedPdf(byte[] fileBytes, string filename, List<int> pageIndexes)
        {
            var tmpInput = new LocalInputSource(fileBytes, filename);
            if (tmpInput.IsPdf())
            {
                LocalInput = tmpInput;
            }
            else
            {
                byte[] pdfBytes = PdfUtils.ConvertImageToPdf(fileBytes, filename);
                string newFilename = Path.ChangeExtension(filename, ".pdf");
                LocalInput = new LocalInputSource(pdfBytes, newFilename);
            }
            PageCount = pageIndexes.Count;
            Filename = LocalInput.Filename;
            PageIndexes = pageIndexes;
        }

        /// <summary>
        ///     Write the PDF to a file.
        /// </summary>
        /// <param name="outputPath">the output directory (must exist).</param>
        public void WriteToFile(string outputPath)
        {
            if (!Directory.Exists(outputPath))
                throw new DirectoryNotFoundException($"Directory does not exist: {outputPath}");

            var pdfPath = Path.Combine(outputPath, LocalInput.Filename);
            File.WriteAllBytes(pdfPath, LocalInput.FileBytes);

        }

        /// <summary>
        ///     Return the file in a format suitable for sending to Mindee Client for parsing.
        /// </summary>
        /// <returns>an instance of <see cref="ExtractedPdf" /></returns>
        public LocalInputSource AsInputSource()
        {
            return LocalInput;
        }
    }
}
