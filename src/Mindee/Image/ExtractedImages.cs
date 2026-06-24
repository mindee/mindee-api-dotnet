using System.Collections.Generic;
using System.IO;
using Mindee.Image;

namespace Mindee.V2.FileOperations
{
    /// <summary>
    /// Collection of cropped files.
    /// </summary>
    public class ExtractedImages : List<ExtractedImage>
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="collection"></param>
        public ExtractedImages(IEnumerable<ExtractedImage> collection) : base(collection)
        {
        }

        /// <summary>
        ///
        /// </summary>
        public ExtractedImages() : base()
        {
        }

        /// <summary>
        /// Saves all cropped files to disk.
        /// </summary>
        /// <param name="outputPath">Path for all files</param>
        /// <param name="quality">Quality of the output image</param>
        /// <param name="fileFormat">File format for saving (default: null)</param>
        public void SaveAllToDisk(string outputPath, int quality = 100, string fileFormat = null)
        {
            foreach (var crop in this)
            {
                crop.WriteToFile(outputPath, quality, fileFormat);
            }
        }
    }
}
