using System.IO;
using Mindee.Input;
using SkiaSharp;

namespace Mindee.Image
{
    /// <summary>
    ///     An extracted sub-image.
    /// </summary>
    public class ExtractedImage
    {
        /// <summary>
        ///     String representation of the save format.
        /// </summary>
        private readonly string _saveFormat;

        /// <summary>
        /// Page number the image was extracted from.
        /// </summary>
        public readonly int PageId;

        /// <summary>
        /// ID of the image.
        /// </summary>
        public readonly int ElementId;

        /// <summary>
        ///     SKBitmap wrapper for the image.
        /// </summary>
        public SKBitmap Image { get; }

        /// <summary>
        ///     Name of the file.
        /// </summary>
        public string Filename { get; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ExtractedImage" /> class.
        /// </summary>
        /// <param name="image">The extracted image.</param>
        /// <param name="filename">The filename for the image.</param>
        /// <param name="saveFormat">The format to save the image.</param>
        /// <param name="pageId">The page number the image was extracted from.</param>
        /// <param name="elementId">The ID of the image.</param>
        public ExtractedImage(SKBitmap image, string filename, string saveFormat, int pageId, int elementId)
        {
            Image = image;
            Filename = filename;
            _saveFormat = saveFormat;
            PageId = pageId;
            ElementId = elementId;
        }

        /// <summary>
        ///     Writes the image to a file.
        ///     If outputPath has an extension, it is treated as a full file path.
        ///     Otherwise, it is treated as a directory and uses the default filename.
        /// </summary>
        /// <param name="outputPath">The output directory (must exist) or full file path.</param>
        public void WriteToFile(string outputPath)
        {
            if (!Directory.Exists(outputPath))
            {
                throw new DirectoryNotFoundException($"Directory does not exist: {outputPath}");
            }

            var nameWithoutExtension = Path.GetFileNameWithoutExtension(Filename);
            var finalFilename = $"{nameWithoutExtension}.{_saveFormat.ToLower()}";
            var imagePath = Path.Combine(outputPath, finalFilename);

            using var stream = File.OpenWrite(imagePath);
            Image.Encode(stream, GetEncodedImageFormat(_saveFormat), 100);
        }

        /// <summary>
        ///     Returns the image in a format suitable for sending to a client for parsing.
        /// </summary>
        /// <param name="quality">The quality of the image. Defaults to 100.</param>
        /// <returns>An instance of <see cref="LocalInputSource" />.</returns>
        public LocalInputSource AsInputSource(int quality = 100)
        {
            using var image = SKImage.FromBitmap(Image);
            using var data = image.Encode(GetEncodedImageFormat(_saveFormat), quality);
            using var output = new MemoryStream();
            data.SaveTo(output);
            return new LocalInputSource(output.ToArray(), Filename);
        }

        private static SKEncodedImageFormat GetEncodedImageFormat(string saveFormat)
        {
            return saveFormat.ToLower() switch
            {
                "jpg" or "jpeg" => SKEncodedImageFormat.Jpeg,
                "png" => SKEncodedImageFormat.Png,
                "bmp" => SKEncodedImageFormat.Bmp,
                "gif" => SKEncodedImageFormat.Gif,
                "webp" => SKEncodedImageFormat.Webp,
                _ => SKEncodedImageFormat.Jpeg
            };
        }
    }
}
