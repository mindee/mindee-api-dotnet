using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Docnet.Core;
using Docnet.Core.Models;
using Docnet.Core.Readers;
using Microsoft.Extensions.Logging;
using Mindee.Exceptions;
using SkiaSharp;

namespace Mindee.Input
{
    /// <summary>
    /// Compressor static class to handle image and PDF compression.
    /// </summary>
    public static class Compressor
    {
        private static byte[] CompressImage(SKBitmap original, int quality, int finalWidth, int finalHeight)
        {
            using var image = SKImage.FromBitmap(original);
            using var compressedData = image.Encode(SKEncodedImageFormat.Jpeg, quality);

            using var compressedBitmap = SKBitmap.Decode(compressedData);
            if (compressedBitmap.Width != finalWidth || compressedBitmap.Height != finalHeight)
            {
                using var finalImage =
                    compressedBitmap.Resize(new SKImageInfo(finalWidth, finalHeight), SKFilterQuality.High);
                return finalImage.Encode(SKEncodedImageFormat.Jpeg, quality).ToArray();
            }

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
            int newWidth = maxWidth ?? original.Width;
            int newHeight = maxHeight ?? original.Height;

            return CompressImage(original, quality, newWidth, newHeight);
        }

        /// <summary>
        /// Compresses a PDF file using DocLib.
        /// </summary>
        /// <param name="pdfData">Byte sequence representing the content of the PDF file.</param>
        /// <param name="imageQuality">Quality of the final file.</param>
        /// <param name="logger">Debug logger.</param>
        /// <returns></returns>
        public static byte[] CompressPdf(byte[] pdfData, int imageQuality = 85, ILogger logger = null)
        {
            using var library = DocLib.Instance;
            using var docReader = library.GetDocReader(pdfData, new PageDimensions(1));
            bool hasWarned = false;
            var outputStream = new MemoryStream();
            using (var document = SKDocument.CreatePdf(outputStream))
            {
                for (int i = 0; i < docReader.GetPageCount(); i++)
                {
                    using var pageReader = docReader.GetPageReader(i);
                    var width = pageReader.GetPageWidth();
                    var height = pageReader.GetPageHeight();

                    using var resizedBitmap = GenerateResizedBitmap(imageQuality, pageReader, width, height);


                    var canvas = document.BeginPage(width, height);
                    canvas.DrawBitmap(resizedBitmap, SKPoint.Empty);

                    var characters = CheckCharacters(logger, pageReader.GetCharacters(), ref hasWarned);

                    foreach (var character in characters)
                    {
                        WriteTextToCanvas(resizedBitmap, character, canvas);
                    }

                    document.EndPage();
                }
            }

            return outputStream.ToArray();
        }

        /// <summary>
        /// Generates a resized bitmap of the current read page. This operation rasterizes the contents.
        /// </summary>
        /// <param name="imageQuality">Target quality for the contents of the page.</param>
        /// <param name="pageReader">Page reader instance for the currently read page.</param>
        /// <param name="width">Width of the page.</param>
        /// <param name="height">Height of the page.</param>
        /// <returns>A resized bitmap of the contents of the rasterized page.</returns>
        private static SKBitmap GenerateResizedBitmap(int imageQuality, IPageReader pageReader, int width, int height)
        {
            SKBitmap resizedBitmap = null;
            try
            {
                var rawBytes = pageReader.GetImage();
                var initialBitmap = MindeeInputUtils.ArrayToImage(MindeeInputUtils.ConvertTo3DArray(rawBytes, width, height));
                var compressedImage = CompressImage(initialBitmap, imageQuality, width, height);

                using var compressedBitmap = SKBitmap.Decode(compressedImage);
                resizedBitmap = compressedBitmap.Resize(new SKImageInfo(width, height), SKFilterQuality.High);
                return resizedBitmap;
            }
            catch
            {
                resizedBitmap?.Dispose();
                throw new MindeeInputException("The extracted bitmap from the given object could not be resized.");
            }
        }

        /// <summary>
        /// Writes the source text of a page to the newly-created canvas (on top of images).
        /// </summary>
        /// <param name="bitmap">The input bitmap.</param>
        /// <param name="character">The currently read character.</param>
        /// <param name="canvas">The canvas of the page to insert the bitmap into.</param>
        private static void WriteTextToCanvas(SKBitmap bitmap, Character character, SKCanvas canvas)
        {
            using (var paint = new SKPaint())
            {
                SKColor textColor = MindeeInputUtils.InferTextColor(bitmap, character.Box);
                paint.TextSize = (float)character.FontSize * (72f / 96f);
                paint.Color = textColor;

                var fontManager = SKFontManager.Default;
                // A bit obscure, but Lucida Grande is the most average font, apparently: https://ben-tanen.com/projects/2019/06/23/most-font.html
                // Arial & Liberation Sans are the fallbacks for Windows & macOS, as well as Linux, respectively.
                string[] preferredFonts = ["Lucida Grande", "Arial", "Liberation Sans"];

                string fontName = preferredFonts.FirstOrDefault(font =>
                    fontManager.MatchFamily(font) != null &&
                    string.Equals(fontManager.MatchFamily(font).FamilyName, font, StringComparison.OrdinalIgnoreCase)
                ) ?? "Liberation Sans";

                paint.Typeface = SKTypeface.FromFamilyName(fontName);

                canvas.DrawText(character.Char.ToString(),
                    (character.Box.Left + character.Box.Right) / 2.0f - paint.TextSize / 4f,
                    (character.Box.Top + character.Box.Bottom) / 2.0f + paint.TextSize / 4f, paint);
            }
        }

        /// <summary>
        /// Checks whether the provided PDF file's content has any text items insides.
        /// </summary>
        /// <param name="logger">Current logger</param>
        /// <param name="textItems">Text items retrieved from the page.</param>
        /// <param name="warnedSize">Whether the warning has been raised already.</param>
        /// <returns></returns>
        private static Character[] CheckCharacters(ILogger logger, IEnumerable<Character> textItems, ref bool warnedSize)
        {
            var characters = textItems as Character[] ?? textItems.ToArray();
            if (!characters.Any() || warnedSize)
            {
                return characters;
            }

            logger?.LogWarning(
                "Found text inside of the provided PDF file. This tool will rewrite the found text in the new document, but it will not match the original.");
            // Note: bypassing the logger as well since this is **heavily** discouraged.
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(
                "MINDEE WARNING: Found text inside of the provided PDF file. This tool will rewrite the found text in the new document, but it will not match the original.");
            Console.ResetColor();
            warnedSize = true;

            return characters;
        }
    }
}
