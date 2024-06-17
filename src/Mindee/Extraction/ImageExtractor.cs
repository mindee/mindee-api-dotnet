using System;
using System.Collections.Generic;
using System.IO;
using Docnet.Core;
using Docnet.Core.Models;
using Mindee.Exceptions;
using Mindee.Geometry;
using Mindee.Input;
using Mindee.Parsing.Standard;
using SkiaSharp;

namespace Mindee.Extraction
{
    /// <summary>
    /// Extract sub-images from an image.
    /// </summary>
    public class ImageExtractor
    {
        private readonly List<SKBitmap> _pageImages;
        private readonly string _filename;
        private readonly string _saveFormat;

        /// <summary>
        /// Init from a Local Input Source.
        /// </summary>
        /// <param name="localInput"></param>
        public ImageExtractor(LocalInputSource localInput)
        {
            this._filename = localInput.Filename;
            this._pageImages = new List<SKBitmap>();
            if (localInput.IsPdf())
            {
                this._saveFormat = "jpg";
                List<SKBitmap> pdfPageImages = PdfToImages(localInput.FileBytes);
                this._pageImages.AddRange(pdfPageImages);
            }
            else
            {
                this._pageImages.Add(SKBitmap.Decode(localInput.FileBytes));
                var extension = Path.GetExtension(localInput.Filename);
                this._saveFormat = extension == null ? ".jpg" : extension.Substring(1);
            }
        }


        private static List<SKBitmap> PdfToImages(byte[] fileBytes)
        {
            List<SKBitmap> images = new List<SKBitmap>();

            using (var library = DocLib.Instance)
            {
                using (var docReader = library.GetDocReader(fileBytes, new PageDimensions(1)))
                {
                    for (int i = 0; i < docReader.GetPageCount(); i++)
                    {
                        using (var pageReader = docReader.GetPageReader(i))
                        {
                            var rawBytes = pageReader.GetImage();
                            var width = pageReader.GetPageWidth();
                            var height = pageReader.GetPageHeight();
                            var bmp = new SKBitmap(width, height, SKColorType.Bgra8888, SKAlphaType.Premul);

                            images.Add(bmp);
                        }
                    }

                }
            }

            return images;
        }

        /// <summary>
        /// Splits the filename into name and extension.
        /// </summary>
        private static string[] SplitNameStrict(string filename)
        {
            return new string[]
            {
                Path.GetFileNameWithoutExtension(filename), Path.GetExtension(filename).TrimStart('.')
            };
        }

        /// <summary>
        /// Gets the number of pages in the file.
        /// </summary>
        /// <returns>The number of pages in the file.</returns>
        public int GetPageCount()
        {
            return this._pageImages.Count;
        }

        /// <summary>
        /// Extract multiple images on a given page from a list of fields having position data.
        /// </summary>
        /// <param name="fields">List of Fields to extract.</param>
        /// <param name="pageIndex">The page index to extract, begins at 0.</param>
        /// <returns>A list of extracted images.</returns>
        public IList<ExtractedImage> ExtractImagesFromPage(IList<PositionField> fields, int pageIndex)
        {
            return ExtractFromPage(fields, pageIndex, _filename);
        }

        /// <summary>
        /// Extract multiple images on a given page from a list of fields having position data.
        /// </summary>
        /// <param name="fields">List of Fields to extract.</param>
        /// <param name="pageIndex">The page index to extract, begins at 0.</param>
        /// <param name="outputName">The base output filename, must have an image extension.</param>
        /// <returns>A list of extracted images.</returns>
        public IList<ExtractedImage> ExtractImagesFromPage<TBaseField>(IList<TBaseField> fields, int pageIndex,
            string outputName) where TBaseField : BaseField
        {
            string filename;
            if (this.GetPageCount() > 1)
            {
                string[] splitName = SplitNameStrict(outputName);
                filename = $"{splitName[0]}.{this._saveFormat}";
            }
            else
            {
                filename = outputName;
            }

            return ExtractFromPage(fields, pageIndex, filename);
        }

        /// <summary>
        /// Extract multiple images on a given page from a list of fields having position data.
        /// </summary>
        /// <param name="fields">List of Fields to extract.</param>
        /// <param name="pageIndex">The page index to extract, begins at 0.</param>
        /// <param name="outputName">The base output filename, must have an image extension.</param>
        /// <returns>A list of extracted images.</returns>
        public IList<ExtractedImage> ExtractImagesFromPage(IList<PositionField> fields, int pageIndex,
            string outputName)
        {
            string filename;
            if (this.GetPageCount() > 1)
            {
                string[] splitName = SplitNameStrict(outputName);
                filename = $"{splitName[0]}.{this._saveFormat}";
            }
            else
            {
                filename = outputName;
            }

            return ExtractFromPage(fields, pageIndex, filename);
        }

        /// <summary>
        /// Extract multiple images on a given page from a list of fields having position data.
        /// </summary>
        /// <param name="fields">List of Fields to extract.</param>
        /// <param name="pageIndex">The page index to extract, begins at 0.</param>
        /// <param name="outputName">The base output filename, must have an image extension.</param>
        /// <returns>A list of extracted images.</returns>
        private List<ExtractedImage> ExtractFromPage<TBaseField>(IList<TBaseField> fields, int pageIndex,
            string outputName) where TBaseField : BaseField
        {
            string[] splitName = SplitNameStrict(outputName);
            string filename = $"{splitName[0]}_page-{(pageIndex + 1):D3}.{this._saveFormat}";

            List<ExtractedImage> extractedImages = new List<ExtractedImage>();
            for (int i = 0; i < fields.Count; i++)
            {
                ExtractedImage extractedImage = ExtractImage(fields[i], pageIndex, i + 1, filename);
                if (extractedImage != null)
                {
                    extractedImages.Add(extractedImage);
                }
            }

            return extractedImages;
        }

        private List<ExtractedImage> ExtractFromPage(IList<PositionField> fields, int pageIndex, string outputName)
        {
            string[] splitName = SplitNameStrict(outputName);
            string filename = $"{splitName[0]}_page-{(pageIndex + 1):D3}.{this._saveFormat}";

            List<ExtractedImage> extractedImages = new List<ExtractedImage>();
            for (int i = 0; i < fields.Count; i++)
            {
                ExtractedImage extractedImage = ExtractImage(fields[i], pageIndex, i + 1, filename);
                if (extractedImage != null)
                {
                    extractedImages.Add(extractedImage);
                }
            }

            return extractedImages;
        }

        /// <summary>
        /// Extracts a single image from a Position field.
        /// </summary>
        /// <param name="field">The field to extract.</param>
        /// <param name="pageIndex">The page index to extract, begins at 0.</param>
        /// <param name="index">The index to use for naming the extracted image.</param>
        /// <param name="filename">The output filename.</param>
        /// <returns>The extracted image, or null if the field does not have valid position data.</returns>
        public ExtractedImage ExtractImage(PositionField field, int pageIndex, int index, string filename)
        {
            string[] splitName = SplitNameStrict(filename);
            string saveFormat = splitName[1].ToLower();
            Polygon boundingBox;

            if (field.Polygon is { Count: > 0 })
            {
                boundingBox = field.Polygon;
            }
            else if (field.BoundingBox is { Count: > 0 })
            {
                boundingBox = field.BoundingBox;
            }
            else if (field.Quadrangle is { Count: > 0 })
            {
                boundingBox = field.Quadrangle;
            }
            else if (field.Rectangle is { Count: > 0 })
            {
                boundingBox = field.Rectangle;
            }
            else
            {
                throw new MindeeException("Provided field {field} has no valid position data.");
            }

            Bbox bbox = Utils.BboxFromPolygon(boundingBox);
            string fieldFilename = $"{splitName[0]}_{index:D3}.{saveFormat}";
            return new ExtractedImage(ExtractImage(bbox, pageIndex), fieldFilename, saveFormat);
        }

        /// <summary>
        /// Extracts a single image from a field having position data.
        /// </summary>
        /// <param name="field">The field to extract.</param>
        /// <param name="pageIndex">The page index to extract, begins at 0.</param>
        /// <param name="index">The index to use for naming the extracted image.</param>
        /// <param name="filename">The output filename.</param>
        /// <returns>The extracted image, or null if the field does not have valid position data.</returns>
        public ExtractedImage ExtractImage(BaseField field, int pageIndex, int index, string filename)
        {
            string[] splitName = SplitNameStrict(filename);
            string saveFormat = splitName[1].ToLower();
            Polygon boundingBox;

            if (field.Polygon.Count > 0)
            {
                boundingBox = field.Polygon;
            }
            else
            {
                throw new MindeeException("Provided field has no valid position data.");
            }

            Bbox bbox = Utils.BboxFromPolygon(boundingBox);
            string fieldFilename = $"{splitName[0]}_{index:D3}.{saveFormat}";
            return new ExtractedImage(ExtractImage(bbox, pageIndex), fieldFilename, saveFormat);
        }

        private SKBitmap ExtractImage(Bbox bbox, int pageIndex)
        {
            SKBitmap image = this._pageImages[pageIndex];
            int width = image.Width;
            int height = image.Height;
            int minX = (int)Math.Round(bbox.MinX * width);
            int maxX = (int)Math.Round(bbox.MaxX * width);
            int minY = (int)Math.Round(bbox.MinY * height);
            int maxY = (int)Math.Round(bbox.MaxY * height);

            SKBitmap croppedBitmap = new SKBitmap(maxX - minX, maxY - minY);
            using (SKCanvas canvas = new SKCanvas(croppedBitmap))
            {
                SKRect destRect = new SKRect(0, 0, croppedBitmap.Width, croppedBitmap.Height);
                SKRect sourceRect = new SKRect(minX, minY, maxX, maxY);
                canvas.DrawBitmap(image, sourceRect, destRect);
            }

            return croppedBitmap;
        }
    }
}
