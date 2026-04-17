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
        public int PageId;

        /// <summary>
        /// ID of the image.
        /// </summary>
        public int ElementId;

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
        ///     SKBitmap wrapper for the image.
        /// </summary>
        public SKBitmap Image { get; }

        /// <summary>
        ///     Name of the file.
        /// </summary>
        public string Filename { get; }

        /// <summary>
        ///     Writes the image to a file.
        ///     If outputPath has an extension, it is treated as a full file path.
        ///     Otherwise, it is treated as a directory and uses the default filename.
        /// </summary>
        /// <param name="outputPath">The output directory (must exist) or full file path.</param>
        /// <param name="quality">The quality of the image. Defaults to 100.</param>
        /// <param name="fileFormat">The desired format. If null, inferred from extension or default.</param>
        public void WriteToFile(string outputPath, int quality = 100, string fileFormat = null)
        {
            string imagePath;
            var targetFormat = fileFormat ?? _saveFormat;

            if (Path.HasExtension(outputPath))
            {
                imagePath = outputPath;
                if (string.IsNullOrWhiteSpace(fileFormat))
                {
                    var extension = Path.GetExtension(outputPath).TrimStart('.');
                    if (!string.IsNullOrWhiteSpace(extension))
                    {
                        targetFormat = extension.ToLower();
                    }
                }
            }
            else
            {
                var finalFilename = Filename;
                if (!string.IsNullOrWhiteSpace(fileFormat))
                {
                    var nameWithoutExtension = Path.GetFileNameWithoutExtension(Filename);
                    finalFilename = $"{nameWithoutExtension}.{targetFormat.ToLower()}";
                }
                imagePath = Path.Combine(outputPath, finalFilename);
            }

            var format = GetEncodedImageFormat(targetFormat);

            using var image = SKImage.FromBitmap(Image);
            using var data = image.Encode(format, quality);
            using var stream = File.OpenWrite(imagePath);
            data.SaveTo(stream);
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
