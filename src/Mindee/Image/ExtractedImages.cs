using System.Collections.Generic;

namespace Mindee.Image
{
    /// <summary>
    /// List of <see cref="ExtractedImage"/>.
    /// </summary>
    public class ExtractedImages : List<ExtractedImage>
    {
        /// <summary>
        /// Constructor with a collection passed.
        /// </summary>
        /// <param name="collection"></param>
        public ExtractedImages(IEnumerable<ExtractedImage> collection) : base(collection)
        {
        }

        /// <summary>
        /// Empty constructor.
        /// </summary>
        public ExtractedImages() : base()
        {
        }

        /// <summary>
        /// Save all extracted images to disk.
        /// </summary>
        /// <param name="outputPath"></param>
        public void SaveAllToDisk(string outputPath)
        {
            foreach (var extractedImage in this)
            {
                extractedImage.WriteToFile(outputPath);
            }
        }
    }
}
