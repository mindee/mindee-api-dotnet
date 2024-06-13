using System.IO;
using FreeImageAPI;
using Mindee.Input;

namespace Mindee.Extraction
{
    /// <summary>
    /// An extracted sub-image.
    /// </summary>
    public class ExtractedImage
    {
        /// <summary>
        /// FreeImageBitmap wrapper for the image.
        /// </summary>
        public FreeImageBitmap Image { get; }
        /// <summary>
        /// Name of the file.
        /// </summary>
        private string Filename { get; }
        /// <summary>
        /// String representation of the save format.
        /// </summary>
        private readonly string _saveFormat;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExtractedImage"/> class.
        /// </summary>
        /// <param name="image">The extracted image.</param>
        /// <param name="filename">The filename for the image.</param>
        /// <param name="saveFormat">The format to save the image.</param>
        public ExtractedImage(FreeImageBitmap image, string filename, string saveFormat)
        {
            this.Image = image;
            this.Filename = filename;
            this._saveFormat = saveFormat;
        }

        /// <summary>
        /// Writes the image to a file.
        /// Uses the default image format and filename.
        /// </summary>
        /// <param name="outputPath">The output directory (must exist).</param>
        public void WriteToFile(string outputPath)
        {
            string imagePath = Path.Combine(outputPath, this.Filename);
            string formatStr;
            if (this._saveFormat != null)
            {
                formatStr = this._saveFormat;
            }
            else
            {
                formatStr = Path.GetExtension(imagePath).Length > 0 ? Path.GetExtension(imagePath) : null;
            }

            var format = ImageFormatExtensions.FromString(formatStr);

            if (format != null)
            {
                this.Image.Save(imagePath, (FREE_IMAGE_FORMAT)format);
            }
            else
            {
                this.Image.Save(imagePath, FREE_IMAGE_FORMAT.FIF_JPEG, FREE_IMAGE_SAVE_FLAGS.JPEG_QUALITYSUPERB);
            }
        }

        /// <summary>
        /// Returns the image in a format suitable for sending to a client for parsing.
        /// </summary>
        /// <returns>An instance of <see cref="LocalInputSource"/>.</returns>
        public LocalInputSource AsInputSource()
        {
            using (MemoryStream output = new MemoryStream())
            {
                this.Image.Save(output, FREE_IMAGE_FORMAT.FIF_JPEG);
                return new LocalInputSource(output.ToArray(), this.Filename);
            }
        }
    }
}
