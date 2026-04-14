using System.Collections.Generic;
using System.IO;
using Mindee.Image;

namespace Mindee.V2.FileOperations
{
    /// <summary>
    /// Collection of cropped files.
    /// </summary>
    public class CropFiles : List<ExtractedImage>
    {
        /// <summary>
        /// Saves all cropped files to disk.
        /// </summary>
        /// <param name="path">Path for all files</param>
        /// <param name="prefix">Prefix for file names</param>
        /// <param name="fileFormat">File format for saving (default: null)</param>
        public void SaveAllToDisk(string path, string prefix = "crop", string fileFormat = null)
        {
            Directory.CreateDirectory(path);

            int index = 1;
            foreach (var crop in this)
            {
                string fileName = $"{prefix}_{index:D3}.jpg";
                string filePath = Path.Combine(path, fileName);

                crop.WriteToFile(filePath, fileFormat);

                index++;
            }
        }
    }
}
