using System.IO;
using Mindee.Exceptions;
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
        /// Original filename.
        /// </summary>
        public readonly string Filename;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ExtractedPdf" /> class.
        /// </summary>
        /// <param name="fileBytes">A byte array representation of the Pdf.</param>
        /// <param name="filename">Name of the original file.</param>
        public ExtractedPdf(byte[] fileBytes, string filename)
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
            PageCount = LocalInput.GetPageCount();
            Filename = LocalInput.Filename;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ExtractedPdf" /> class.
        /// </summary>
        /// <param name="localInput">LocalInputSource containing the Pdf bytes and filename.</param>
        public ExtractedPdf(LocalInputSource localInput)
        {
            LocalInput = localInput;
            if (!localInput.IsPdf())
            {
                throw new MindeeInputException("The input file is not a PDF.");
            }
            PageCount = LocalInput.GetPageCount();
            Filename = LocalInput.Filename;
        }

        /// <summary>
        ///     Wrapper for pdf GetPageCount();
        /// </summary>
        /// <returns>The number of pages in the file.</returns>
        public int GetPageCount()
        {
            return LocalInput.GetPageCount();
        }

        /// <summary>
        ///     Write the Pdf to a file.
        /// </summary>
        /// <param name="outputPath">the output directory (must exist).</param>
        public void WriteToFile(string outputPath)
        {
            var pdfPath = Path.Combine(outputPath, LocalInput.Filename);
            if (Path.GetFileName(outputPath) != string.Empty)
            {
                pdfPath = Path.GetFullPath(outputPath);
            }

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
