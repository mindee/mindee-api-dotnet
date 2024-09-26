using System;
using System.IO;
using Docnet.Core;
using Docnet.Core.Models;
using SkiaSharp;

namespace Mindee.Input
{
    /// <summary>
    /// Compressor static class to handle image and PDF compression.
    /// </summary>
    public static class Compressor
    {
        private static byte[] CompressImage(SKBitmap original, int quality = 85, int? maxWidth = null,
            int? maxHeight = null)
        {
            int newWidth = original.Width;
            int newHeight = original.Height;

            if (maxWidth.HasValue || maxHeight.HasValue)
            {
                (newWidth, newHeight) = CalculateNewDimensions(original.Width, original.Height, maxWidth, maxHeight);
            }

            using var resized = original.Width == newWidth && original.Height == newHeight
                ? original
                : original.Resize(new SKImageInfo(newWidth, newHeight), SKFilterQuality.High);
            using var image = SKImage.FromBitmap(resized);
            return image.Encode(SKEncodedImageFormat.Jpeg, quality).ToArray();
        }


        /// <summary>
        /// Resize and/or compress an image using SkiaSharp.
        /// </summary>
        /// <param name="imageData">Byte sequence representing the content of the image.</param>
        /// <param name="quality">Quality of the final file.</param>
        /// <param name="maxWidth">Maximum width. If not specified, the horizontal ratio will remain the same.</param>
        /// <param name="maxHeight">Maximum height. If not specified, the vertical ratio will remain the same</param>
        /// <returns></returns>
        public static byte[] CompressImage(byte[] imageData, int quality = 85, int? maxWidth = null,
            int? maxHeight = null)
        {
            using var inputStream = new SKMemoryStream(imageData);
            using var original = SKBitmap.Decode(inputStream);
            return CompressImage(original, quality, maxWidth, maxHeight);
        }

        private static (int width, int height) CalculateNewDimensions(int originalWidth, int originalHeight,
            int? maxWidth, int? maxHeight)
        {
            double ratioX = maxWidth.HasValue ? (double)maxWidth.Value / originalWidth : double.PositiveInfinity;
            double ratioY = maxHeight.HasValue ? (double)maxHeight.Value / originalHeight : double.PositiveInfinity;
            double ratio = Math.Min(ratioX, ratioY);

            int newWidth = (int)(originalWidth * ratio);
            int newHeight = (int)(originalHeight * ratio);

            return (newWidth, newHeight);
        }

        /// <summary>
        /// Compresses a PDF file using DocLib.
        /// </summary>
        /// <param name="pdfData">Byte sequence representing the content of the PDF file.</param>
        /// <param name="imageQuality">Quality of the final file.</param>
        /// <returns></returns>
        public static byte[] CompressPdf(byte[] pdfData, int imageQuality = 85)
        {
            using var library = DocLib.Instance;
            using var docReader = library.GetDocReader(pdfData, new PageDimensions(1));

            using var outputStream = new MemoryStream();
            using var document = SKDocument.CreatePdf(outputStream);

            for (int i = 0; i < docReader.GetPageCount(); i++)
            {
                using var pageReader = docReader.GetPageReader(i);
                var width = pageReader.GetPageWidth();
                var height = pageReader.GetPageHeight();
                var rawBytes = pageReader.GetImage();

                var bmp = MindeeInputUtils.ArrayToImage(MindeeInputUtils.ConvertTo3DArray(rawBytes, width, height));
                var compressedImage = CompressImage(bmp, imageQuality);

                using var canvas = document.BeginPage(width, height);
                using var image = SKImage.FromEncodedData(compressedImage);
                canvas.DrawImage(image, 0, 0);

                document.EndPage();
            }

            return outputStream.ToArray();
        }
    }
}
