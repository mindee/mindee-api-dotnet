using System.IO;
using Mindee.Input;
using SkiaSharp;

namespace Mindee.Extraction
{
    /// <summary>
    /// An extracted sub-image.
    /// </summary>
    public class ExtractedImage
    {
        /// <summary>
        /// SKBitmap wrapper for the image.
        /// </summary>
        public SKBitmap Image { get; }

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
        public ExtractedImage(SKBitmap image, string filename, string saveFormat)
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
            SKEncodedImageFormat format = GetEncodedImageFormat(this._saveFormat);

            using (var image = SKImage.FromBitmap(this.Image))
            using (var data = image.Encode(format, 100))
            using (var stream = File.OpenWrite(imagePath))
            {
                data.SaveTo(stream);
            }
        }

        /// <summary>
        /// Returns the image in a format suitable for sending to a client for parsing.
        /// </summary>
        /// <returns>An instance of <see cref="LocalInputSource"/>.</returns>
        public LocalInputSource AsInputSource()
        {
            using (var image = SKImage.FromBitmap(this.Image))
            using (var data = image.Encode(SKEncodedImageFormat.Jpeg, 100))
            using (MemoryStream output = new MemoryStream())
            {
                data.SaveTo(output);
                return new LocalInputSource(output.ToArray(), this.Filename);
            }
        }

        private SKEncodedImageFormat GetEncodedImageFormat(string saveFormat)
        {
            return saveFormat.ToLower() switch
            {
                "png" => SKEncodedImageFormat.Png,
                "bmp" => SKEncodedImageFormat.Bmp,
                "gif" => SKEncodedImageFormat.Gif,
                "webp" => SKEncodedImageFormat.Webp,
                _ => SKEncodedImageFormat.Jpeg
            };
        }
    }
}
