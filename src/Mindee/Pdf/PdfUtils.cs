using System;
using System.Linq;
using Docnet.Core;
using Docnet.Core.Models;
using Docnet.Core.Readers;
using Mindee.Exceptions;
using Mindee.Image;
using SkiaSharp;

namespace Mindee.Pdf
{
    /// <summary>
    ///     PDF Utility class.
    /// </summary>
    public static class PdfUtils
    {
        /// <summary>
        ///     Draws the content of a bitmap onto a canvas. Optionally writes the text from a source PDF onto said canvas.
        /// </summary>
        /// <param name="canvas">SkiaSharp canvas object.</param>
        /// <param name="resizedBitmap">Resized bitmap to transpose.</param>
        /// <param name="pageReader">DocLib pagerader object to optionally extract the text.</param>
        /// <param name="disableSourceText">If true, the text transposition is skipped.</param>
        public static void DrawPageContent(
            SKCanvas canvas,
            SKBitmap resizedBitmap,
            IPageReader pageReader,
            bool disableSourceText)
        {
            canvas.DrawBitmap(resizedBitmap, SKPoint.Empty);
            if (disableSourceText)
            {
                return;
            }

            var characters = pageReader.GetCharacters();

            foreach (var character in characters)
            {
                WriteTextToCanvas(resizedBitmap, character, canvas);
            }
        }

        /// <summary>
        ///     Generates a bitmap of the current read page. This operation rasterizes the contents.
        /// </summary>
        /// <param name="imageQuality">Target quality for the contents of the page.</param>
        /// <param name="pageReader">Page reader instance for the currently read page.</param>
        /// <param name="width">Width of the page.</param>
        /// <param name="height">Height of the page.</param>
        /// <returns>A resized bitmap of the contents of the rasterized page.</returns>
        public static SKBitmap GeneratePageBitmap(int imageQuality, IPageReader pageReader, int width, int height)
        {
            SKBitmap resizedBitmap = null;
            try
            {
                var rawBytes = pageReader.GetImage();
                var initialBitmap =
                    ImageUtils.ArrayToImage(ImageUtils.ConvertTo3DArray(rawBytes, width, height));

                var compressedImage = ImageCompressor.CompressImage(initialBitmap, imageQuality, width, height);

                var colorType = SKColorType.Argb4444;
                using var compressedBitmap = SKBitmap.Decode(compressedImage);
                if (imageQuality > 85)
                {
                    colorType = SKColorType.Rgb565;
                }

                using var surface = SKSurface.Create(new SKImageInfo(width, height, colorType));

                surface.Canvas.DrawBitmap(compressedBitmap, 0, 0);
                resizedBitmap = SKBitmap.FromImage(surface.Snapshot());

                return resizedBitmap;
            }
            catch
            {
                resizedBitmap?.Dispose();
                throw new MindeeInputException("The extracted bitmap from the given object could not be resized.");
            }
        }

        /// <summary>
        ///     Writes the source text of a page to the newly-created canvas (on top of images).
        ///     Does not extract font-family due to Skiasharp limitations.
        ///     Also approximates the positioning of letters since vertical anchor is not extracted from the text and
        ///     SkiaSharp places letters from bounding boxes.
        /// </summary>
        /// <param name="bitmap">The input bitmap.</param>
        /// <param name="character">The currently read character.</param>
        /// <param name="canvas">The canvas of the page to insert the bitmap into.</param>
        private static void WriteTextToCanvas(SKBitmap bitmap, Character character, SKCanvas canvas)
        {
            using var paint = new SKPaint();
            using var font = new SKFont();
            var textColor = ImageUtils.InferTextColor(bitmap, character.Box);
            font.Size = (float)character.FontSize * (72f / 96f);
            paint.Color = textColor;

            var fontManager = SKFontManager.Default;
            var preferredFonts = new[] { "Lucida Grande", "Arial", "Liberation Sans" };

            var fontName = preferredFonts.FirstOrDefault(tmpFontName =>
                fontManager.MatchFamily(tmpFontName) != null &&
                string.Equals(fontManager.MatchFamily(tmpFontName).FamilyName, tmpFontName,
                    StringComparison.OrdinalIgnoreCase)
            ) ?? "Liberation Sans";
            font.Typeface = SKTypeface.FromFamilyName(fontName);

            var text = character.Char.ToString();
            var lines = text.Split(["\r\n", "\n"], StringSplitOptions.None);

            var lineHeight = font.Spacing;
            var boxCenterX = (character.Box.Left + character.Box.Right) / 2f;
            float boxBottom = character.Box.Bottom;

            for (var i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                if (string.IsNullOrEmpty(line))
                {
                    continue;
                }

                font.MeasureText(line, out var lineBounds);

                var x = boxCenterX - (lineBounds.Width / 2f);
                var y = boxBottom - ((lines.Length - i) * lineHeight);

                foreach (var c in line)
                {
                    if (char.IsControl(c))
                    {
                        // Necessary, otherwise it prints a ￾ at line returns due to SkiaSharp rendering issues.
                        continue;
                    }

                    var charString = c.ToString();
                    canvas.DrawText(charString, x, y, font, paint);
                    x += font.MeasureText(charString);
                }
            }
        }

        /// <summary>
        ///     Returns true if the source PDF has source text inside. Returns false for images.
        /// </summary>
        /// <param name="fileBytes">A byte array representing a PDF.</param>
        /// <returns>True if at least one character exists in one page.</returns>
        public static bool HasSourceText(byte[] fileBytes)
        {
            try
            {
                lock
                    (DocLib.Instance)
                {
                    using var docReader = DocLib.Instance.GetDocReader(fileBytes, new PageDimensions(1));
                    for (var i = 0; i < docReader.GetPageCount(); i++)
                    {
                        using var pageReader = docReader.GetPageReader(i);
                        if (pageReader.GetText().Length > 0)
                        {
                            return true;
                        }
                    }
                }
            }
            catch (Exception)
            {
                try
                {
                    // Image files will result in a failure to read from PDF, but can still be valid, so we try from Skiasharp.
                    using var original = SKBitmap.Decode(fileBytes);
                    return false;
                }
                catch (Exception exc)
                {
                    throw new MindeeInputException("The file could not be read.", exc);
                }
            }

            return false;
        }
    }
}
