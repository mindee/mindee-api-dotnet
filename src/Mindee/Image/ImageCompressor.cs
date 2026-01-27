using SkiaSharp;

namespace Mindee.Image
{
    /// <summary>
    ///     Image compressor static class to handle image compression.
    /// </summary>
    public static class ImageCompressor
    {
        /// <summary>
        ///     Resize and/or compress an SKBitmap. This assumes the ratio was provided before hands.
        /// </summary>
        /// <param name="original">Original, unedited bitmap.</param>
        /// <param name="quality">Quality of the final file.</param>
        /// <param name="finalWidth">Maximum width. If not specified, the horizontal ratio will remain the same.</param>
        /// <param name="finalHeight">Maximum height. If not specified, the vertical ratio will remain the same</param>
        /// <returns>A byte array holding a compressed image.</returns>
        public static byte[] CompressImage(SKBitmap original, int quality, int finalWidth, int finalHeight)
        {
            using var image = SKImage.FromBitmap(original);
            using var compressedBitmap = SKBitmap.FromImage(image);
            var samplingOptions = new SKSamplingOptions(SKFilterMode.Linear, SKMipmapMode.None);
            using var finalImage =
                compressedBitmap.Resize(new SKImageInfo(finalWidth, finalHeight), samplingOptions);
            return finalImage.Encode(SKEncodedImageFormat.Jpeg, quality).ToArray();
        }


        /// <summary>
        ///     Resize and/or compress an image using SkiaSharp. This maintains the provided ratio.
        /// </summary>
        /// <param name="imageData">Byte array representing the content of the image.</param>
        /// <param name="quality">Quality of the final file.</param>
        /// <param name="maxWidth">Maximum width. If not specified, the horizontal ratio will remain the same.</param>
        /// <param name="maxHeight">Maximum height. If not specified, the vertical ratio will remain the same.</param>
        /// <returns>A byte array holding a compressed image.</returns>
        public static byte[] CompressImage(byte[] imageData, int quality = 85, int? maxWidth = null,
            int? maxHeight = null)
        {
            using var original = SKBitmap.Decode(imageData);
            var (newWidth, newHeight) = ImageUtils.CalculateNewDimensions(original, maxWidth, maxHeight);

            return CompressImage(original, quality, newWidth, newHeight);
        }
    }
}
