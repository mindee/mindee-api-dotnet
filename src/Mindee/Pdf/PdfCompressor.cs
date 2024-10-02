using System;
using System.Collections.Generic;
using System.IO;
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
    /// Image compressor static class to handle PDF compression.
    /// </summary>
    public static class PdfCompressor
    {
        /// <summary>
        /// Compresses a PDF file using DocLib.
        /// </summary>
        /// <param name="pdfData">Byte array representing the content of the PDF file.</param>
        /// <param name="imageQuality">Quality of the final file.</param>
        /// <param name="forceSourceTextCompression">Whether to force the rendering of source pdf. If enabled, will attempt to re-write the detected text.</param>
        /// <param name="disableSourceText">If the PDF has source text, whether to re-apply it to the original or not.</param>
        /// <returns>A byte array containing a compressed PDF.</returns>
        public static byte[] CompressPdf(byte[] pdfData, int imageQuality = 85, bool forceSourceTextCompression = false,
            bool disableSourceText = true)
        {
            if (!forceSourceTextCompression && HasSourceText(pdfData))
            {
                // Note: bypassing the logger since this is **heavily** discouraged.
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(
                    "MINDEE WARNING: Found text inside of the provided PDF file. Compression operation aborted.");
                Console.ResetColor();
            }

            lock (DocLib.Instance)
            {
                using var docReader = DocLib.Instance.GetDocReader(pdfData, new PageDimensions(1));
                var outputStream = new MemoryStream();

                using (var document = SKDocument.CreatePdf(outputStream))
                {
                    ProcessPages(docReader, document, imageQuality, disableSourceText);
                }

                return outputStream.ToArray();
            }
        }

        private static void ProcessPages(IDocReader docReader, SKDocument document, int imageQuality, bool disableSourceText)
        {
            for (int i = 0; i < docReader.GetPageCount(); i++)
            {
                ProcessSinglePage(docReader, document, i, imageQuality, disableSourceText);
            }
        }

        private static void ProcessSinglePage(IDocReader docReader, SKDocument document, int pageIndex,
            int imageQuality, bool disableSourceText)
        {
            using var pageReader = docReader.GetPageReader(pageIndex);
            var width = pageReader.GetPageWidth();
            var height = pageReader.GetPageHeight();

            using var resizedBitmap = GeneratePageBitmap(imageQuality, pageReader, width, height);

            var canvas = document.BeginPage(width, height);
            DrawPageContent(canvas, resizedBitmap, pageReader, disableSourceText);
            document.EndPage();
        }

        private static void DrawPageContent(SKCanvas canvas, SKBitmap resizedBitmap, IPageReader pageReader, bool disableSourceText)
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
        /// Generates a bitmap of the current read page. This operation rasterizes the contents.
        /// </summary>
        /// <param name="imageQuality">Target quality for the contents of the page.</param>
        /// <param name="pageReader">Page reader instance for the currently read page.</param>
        /// <param name="width">Width of the page.</param>
        /// <param name="height">Height of the page.</param>
        /// <returns>A resized bitmap of the contents of the rasterized page.</returns>
        private static SKBitmap GeneratePageBitmap(int imageQuality, IPageReader pageReader, int width, int height)
        {
            SKBitmap resizedBitmap = null;
            try
            {
                var rawBytes = pageReader.GetImage();
                var initialBitmap =
                    MindeeImageUtils.ArrayToImage(MindeeImageUtils.ConvertTo3DArray(rawBytes, width, height));

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
        /// Writes the source text of a page to the newly-created canvas (on top of images).
        /// Does not extract font-family due to Skiasharp limitations.
        /// Also approximates the positioning of letters since vertical anchor is not extracted from the text and
        /// SkiaSharp places letters from bounding boxes.
        /// </summary>
        /// <param name="bitmap">The input bitmap.</param>
        /// <param name="character">The currently read character.</param>
        /// <param name="canvas">The canvas of the page to insert the bitmap into.</param>
        private static void WriteTextToCanvas(SKBitmap bitmap, Character character, SKCanvas canvas)
        {
            using var paint = new SKPaint();
            SKColor textColor = MindeeImageUtils.InferTextColor(bitmap, character.Box);
            paint.TextSize = (float)character.FontSize * (72f / 96f);
            paint.Color = textColor;

            var fontManager = SKFontManager.Default;
            string[] preferredFonts = ["Lucida Grande", "Arial", "Liberation Sans"];

            string fontName = preferredFonts.FirstOrDefault(tmpFontName =>
                fontManager.MatchFamily(tmpFontName) != null &&
                string.Equals(fontManager.MatchFamily(tmpFontName).FamilyName, tmpFontName,
                    StringComparison.OrdinalIgnoreCase)
            ) ?? "Liberation Sans";
            paint.Typeface = SKTypeface.FromFamilyName(fontName);

            paint.TextAlign = SKTextAlign.Left;

            string text = character.Char.ToString();
            string[] lines = text.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);

            float lineHeight = paint.FontSpacing;
            float boxCenterX = (character.Box.Left + character.Box.Right) / 2f;
            float boxBottom = character.Box.Bottom;

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                if (string.IsNullOrEmpty(line))
                {
                    continue;
                }

                SKRect lineBounds = new SKRect();
                paint.MeasureText(line, ref lineBounds);

                float x = boxCenterX - (lineBounds.Width / 2f);
                float y = boxBottom - ((lines.Length - i) * lineHeight);

                foreach (char c in line)
                {
                    if (char.IsControl(c))
                    {
                        // Necessary, otherwise it prints a ￾ at line returns (not quite sure why).
                        continue;
                    }

                    string charString = c.ToString();
                    canvas.DrawText(charString, x, y, paint);
                    x += paint.MeasureText(charString);
                }
            }
        }

        /// <summary>
        /// Returns true if the source PDF has source text inside. Returns false for images.
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
                    for (int i = 0; i < docReader.GetPageCount(); i++)
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
