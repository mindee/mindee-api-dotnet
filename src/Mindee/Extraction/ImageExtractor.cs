using System;
using System.Collections.Generic;
using System.IO;
using Docnet.Core;
using Docnet.Core.Models;
using Mindee.Geometry;
using Mindee.Image;
using Mindee.Input;
using SkiaSharp;

namespace Mindee.Extraction
{
    /// <summary>
    ///     Extract sub-images from an image.
    /// </summary>
    public class ImageExtractor
    {
        /// <summary>
        /// Name of the file.
        /// </summary>
        protected readonly string _filename;
        /// <summary>
        /// List of SKBitmap representing the pages of the file.
        /// </summary>
        private readonly List<SKBitmap> _pageImages;
        /// <summary>
        /// Format to save the resulting images as.
        /// </summary>
        protected readonly string SaveFormat;

        /// <summary>
        ///     LocalInputSource object used by the ImageExtractor.
        /// </summary>
        public readonly LocalInputSource LocalInput;

        /// <summary>
        ///     Init from a Local Input Source.
        /// </summary>
        /// <param name="localInput">Locally loaded resource.</param>
        public ImageExtractor(LocalInputSource localInput)
        {
            _filename = localInput.Filename;
            _pageImages = [];
            LocalInput = localInput;

            if (localInput.IsPdf())
            {
                SaveFormat = "jpg";
                var pdfPageImages = PdfToImages(localInput.FileBytes);
                _pageImages.AddRange(pdfPageImages);
            }
            else
            {
                var extension = Path.GetExtension(localInput.Filename)?.Substring(1);
                SaveFormat = extension ?? "jpg";
                _pageImages.Add(SKBitmap.Decode(localInput.FileBytes));
            }
        }

        /// <summary>
        ///     Init from a path.
        /// </summary>
        /// <param name="filePath">Path to the file.</param>
        public ImageExtractor(string filePath) : this(new LocalInputSource(filePath))
        {
        }

        /// <summary>
        ///     Renders the input PDF's pages as individual images.
        /// </summary>
        /// <param name="fileBytes">Input pdf.</param>
        /// <returns>A list of pages, as SKBitmap.</returns>
        private static List<SKBitmap> PdfToImages(byte[] fileBytes)
        {
            var images = new List<SKBitmap>();
            lock (DocLib.Instance)
            {
                using var docReader = DocLib.Instance.GetDocReader(fileBytes, new PageDimensions(1));
                for (var i = 0; i < docReader.GetPageCount(); i++)
                {
                    using var pageReader = docReader.GetPageReader(i);
                    var width = pageReader.GetPageWidth();
                    var height = pageReader.GetPageHeight();
                    var bytes = pageReader.GetImage();
                    var bmp = ImageUtils.ArrayToImage(ImageUtils.ConvertTo3DArray(bytes, width, height));
                    images.Add(bmp);
                }

                return images;
            }
        }

        /// <summary>
        ///     Splits the filename into name and extension.
        /// </summary>
        protected static string[] SplitNameStrict(string filename)
        {
            return
            [
                Path.GetFileNameWithoutExtension(filename),
                Path.GetExtension(filename).TrimStart('.')
            ];
        }

        /// <summary>
        ///     Gets the number of pages in the file.
        /// </summary>
        /// <returns>The number of pages in the file.</returns>
        public int GetPageCount()
        {
            return _pageImages.Count;
        }

        /// <summary>
        /// Extract multiple images on a given page from a list of fields having position data.
        /// </summary>
        /// <typeparam name="TField">Type of field (needs to support positioning data).</typeparam>
        /// <param name="fields">List of fields to extract.</param>
        /// <param name="pageId">The page index to extract, begins at 0.</param>
        /// <returns>A list of extracted images.</returns>
        public List<ExtractedImage> ExtractImagesFromPage<TField>(
            List<TField> fields,
            int pageId
        ) where TField : IPositionDataField
        {
            return ExtractFromPage(fields, pageId, _filename);
        }

        private List<ExtractedImage> ExtractFromPage<TField>(
            List<TField> fields,
            int pageId,
            string outputName
        ) where TField : IPositionDataField
        {
            var extractedImages = new ExtractedImages();
            for (int elementId = 0; elementId < fields.Count; elementId++)
            {
                var extractedImage = ExtractImage(
                    fields[elementId], pageId, elementId, outputName);

                if (extractedImage != null)
                    extractedImages.Add(extractedImage);
            }
            return extractedImages;
        }

        /// <summary>
        /// Extract a single image from a field having position data.
        /// </summary>
        /// <typeparam name="TField">Type of field (needs to support positioning data).</typeparam>
        /// <param name="field">The field to extract.</param>
        /// <param name="pageId">The page index to extract, begins at 0.</param>
        /// <param name="elementId">The index to use for naming the extracted image.</param>
        /// <returns>The extracted image, or null if the field does not have valid position data.</returns>
        public ExtractedImage ExtractImage<TField>(
            TField field,
            int pageId,
            int elementId
        ) where TField : IPositionDataField
        {
            return ExtractImage(field, pageId, elementId, _filename);
        }

        /// <summary>
        /// Extract a single image from a field having position data.
        /// </summary>
        /// <typeparam name="TField">Type of field (needs to support positioning data).</typeparam>
        /// <param name="field">The field to extract.</param>
        /// <param name="pageId">The page index to extract, begins at 0.</param>
        /// <param name="elementId">The index to use for naming the extracted image.</param>
        /// <param name="filename">Name of the file.</param>
        /// <returns>The extracted image, or null if the field does not have valid position data.</returns>
        public ExtractedImage ExtractImage<TField>(
            TField field,
            int pageId,
            int elementId,
            string filename
        ) where TField : IPositionDataField
        {
            var splitName = SplitNameStrict(filename);
            var polygon = field.Polygon;
            if (polygon == null)
                return null;

            var fieldFilename = string.Format("{0}_page-{1:000}-item-{2:000}.{3}",
                splitName[0],
                pageId + 1,
                elementId + 1,
                SaveFormat);

            return new ExtractedImage(
                ExtractImage(Utils.BboxFromPolygon(polygon), pageId),
                fieldFilename,
                SaveFormat,
                pageId,
                elementId
            );
        }

        /// <summary>
        /// Extracts a single image from a field having position data.
        /// </summary>
        /// <param name="bbox">Bounding box of the field.</param>
        /// <param name="pageIndex">Index of the page containing the field.</param>
        /// <returns>Extracted image as an SKBitmap.</returns>
        protected SKBitmap ExtractImage(Bbox bbox, int pageIndex)
        {
            var image = _pageImages[pageIndex];
            var width = image.Width;
            var height = image.Height;
            var minX = (int)Math.Round(bbox.MinX * width);
            var maxX = (int)Math.Round(bbox.MaxX * width);
            var minY = (int)Math.Round(bbox.MinY * height);
            var maxY = (int)Math.Round(bbox.MaxY * height);

            var croppedBitmap = new SKBitmap(maxX - minX, maxY - minY);
            using var canvas = new SKCanvas(croppedBitmap);
            var destRect = new SKRect(0, 0, croppedBitmap.Width, croppedBitmap.Height);
            var sourceRect = new SKRect(minX, minY, maxX, maxY);
            canvas.DrawBitmap(image, sourceRect, destRect);

            return croppedBitmap;
        }
    }
}
