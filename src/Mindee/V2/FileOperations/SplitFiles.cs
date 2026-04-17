using System.Collections.Generic;
using System.IO;
using Mindee.Pdf;

namespace Mindee.V2.FileOperations
{
    /// <summary>
    /// Collection of split PDFs.
    /// </summary>
    public sealed class SplitFiles : List<ExtractedPdf>
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="collection"></param>
        public SplitFiles(IEnumerable<ExtractedPdf> collection) : base(collection)
        {
        }

        /// <summary>
        ///
        /// </summary>
        public SplitFiles() : base()
        {
        }

        /// <summary>
        /// Saves all the extracted pages to disk.
        /// </summary>
        /// <param name="path">Path for all files</param>
        /// <param name="prefix">Prefix for file names</param>
        public void SaveAllToDisk(string path, string prefix = "split")
        {
            Directory.CreateDirectory(path);

            int index = 1;
            foreach (var crop in this)
            {
                string fileName = $"{prefix}_{index:D3}.pdf";
                string filePath = Path.Combine(path, fileName);

                crop.WriteToFile(filePath);

                index++;
            }
        }
    }
}
