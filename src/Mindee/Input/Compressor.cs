using SkiaSharp;

namespace Mindee.Input
{
    /// <summary>
    /// Compressor static class to handle image & PDF compression.
    /// </summary>
    public static class Compressor
    {
        /// <summary>
        /// Compress an Image using Skiasharp.
        /// </summary>
        /// <param name="imageData"></param>
        /// <param name="quality"></param>
        /// <returns></returns>
        public static byte[] CompressImage(byte[] imageData, int quality = 85)
        {
            using (var inputStream = new SKMemoryStream(imageData))
            using (var original = SKBitmap.Decode(inputStream))
            using (var image = SKImage.FromBitmap(original))
            {
                return image.Encode(SKEncodedImageFormat.Jpeg, quality).ToArray();
            }
        }

        public static byte[] CompressPdf(byte[] pdfData, int imageQuality = 85)
        {
            using (var docReader = DocLib.Instance.GetDocReader(pdfData, new PageDimensions()))
            {
                using (var outputStream = new MemoryStream())
                {
                    using (var document = SKDocument.CreatePdf(outputStream))
                    {
                        for (int i = 0; i < docReader.GetPageCount(); i++)
                        {
                            using (var pageReader = docReader.GetPageReader(i))
                            {
                                var width = pageReader.GetPageWidth();
                                var height = pageReader.GetPageHeight();
                                var rawBytes = pageReader.GetImage();

                                // Compress the page image
                                var compressedImage = CompressImage(rawBytes, imageQuality);

                                // Create a new page in the output PDF
                                using (var canvas = document.BeginPage(width, height))
                                {
                                    using (var image = SKImage.FromEncodedData(compressedImage))
                                    {
                                        canvas.DrawImage(image, 0, 0);
                                    }
                                    document.EndPage();
                                }
                            }
                        }
                    }
                    return outputStream.ToArray();
                }
            }
        }

        public static byte[] CompressImage(byte[] imageData, int quality = 85)
        {
            using (var inputStream = new SKMemoryStream(imageData))
            using (var original = SKBitmap.Decode(inputStream))
            using (var image = SKImage.FromBitmap(original))
            using (var outputStream = new SKDynamicMemoryWStream())
            {
                var encodedImage = image.Encode(SKEncodedImageFormat.Jpeg, quality);
                encodedImage.SaveTo(outputStream);
                return outputStream.ToArray();
            }
        }
    }
}
