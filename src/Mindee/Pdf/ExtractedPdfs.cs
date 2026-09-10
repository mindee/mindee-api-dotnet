using System.Collections.Generic;

namespace Mindee.Pdf
{
    /// <summary>
    /// List of <see cref="ExtractedPdf"/>
    /// </summary>
    public class ExtractedPdfs : List<ExtractedPdf>
    {
        /// <summary>
        /// Constructor with a collection passed.
        /// </summary>
        /// <param name="collection"></param>
        public ExtractedPdfs(IEnumerable<ExtractedPdf> collection) : base(collection)
        {
        }

        /// <summary>
        /// Empty constructor.
        /// </summary>
        public ExtractedPdfs() : base()
        {
        }

        /// <summary>
        /// Save all extracted images to disk.
        /// </summary>
        /// <param name="outputPath"></param>
        public void SaveAllToDisk(string outputPath)
        {
            foreach (var extractedPdf in this)
            {
                extractedPdf.WriteToFile(outputPath);
            }
        }
    }
}
