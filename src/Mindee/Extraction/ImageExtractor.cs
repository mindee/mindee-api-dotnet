using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
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
        /// LocalInputSource object used by the ImageExtractor.
        /// </summary>
        public readonly LocalInputSource LocalInput;

        /// <summary>
        /// Init from a Local Input Source.
        /// </summary>
        /// <param name="localInput">Locally loaded resource.</param>
        /// <param name="saveFormat">Format to save the resulting images as.</param>
        public ImageExtractor(LocalInputSource localInput, string saveFormat = null)
        {
            _filename = localInput.Filename;
            _pageImages = new List<SKBitmap>();
            LocalInput = localInput;
            if (saveFormat == null)
            {
                var extension = Path.GetExtension(localInput.Filename)?.Substring(1);
                if (extension != null && !extension.Equals("pdf", StringComparison.CurrentCultureIgnoreCase))
                {
                    _saveFormat = extension;
                }
                else
                {
                    _saveFormat = "jpg";
                }
            }
            else
            {
                _saveFormat = saveFormat;
            }

            if (localInput.IsPdf())
            {
                List<SKBitmap> pdfPageImages = PdfToImages(localInput.FileBytes);
                _pageImages.AddRange(pdfPageImages);
            }
            else
            {
                _pageImages.Add(SKBitmap.Decode(localInput.FileBytes));
            }
        }

        /// <summary>
        /// Init from a path.
        /// </summary>
        /// <param name="filePath">Path to the file.</param>
        public ImageExtractor(string filePath) : this(new LocalInputSource(filePath))
        {
        }

        private static SKBitmap ArrayToImage(byte[,,] pixelArray)
        {
            int width = pixelArray.GetLength(1);
            int height = pixelArray.GetLength(0);

            uint[] pixelValues = new uint[width * height];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    byte alpha = 255;
                    byte red = pixelArray[y, x, 0];
                    byte green = pixelArray[y, x, 1];
                    byte blue = pixelArray[y, x, 2];
                    uint pixelValue = (uint)red + (uint)(green << 8) + (uint)(blue << 16) + (uint)(alpha << 24);
                    pixelValues[y * width + x] = pixelValue;
                }
            }

            SKBitmap bitmap = new();
            GCHandle gcHandle = GCHandle.Alloc(pixelValues, GCHandleType.Pinned);
            SKImageInfo info = new(width, height, SKColorType.Rgba8888, SKAlphaType.Premul);

            IntPtr ptr = gcHandle.AddrOfPinnedObject();
            int rowBytes = info.RowBytes;
            bitmap.InstallPixels(info, ptr, rowBytes, delegate { gcHandle.Free(); });

            return bitmap;
        }

        static byte[,,] ConvertTo3DArray(byte[] input, int width, int height)
        {
            if (input.Length != width * height * 4)
            {
                throw new ArgumentException("Input array size does not match the given width and height.");
            }

            byte[,,] output = new byte[height, width, 4];

            for (int i = 0; i < input.Length; i += 4)
            {
                int pixelIndex = i / 4;
                int x = pixelIndex % width;
                int y = pixelIndex / width;

                output[y, x, 2] = input[i];
                output[y, x, 1] = input[i + 1];
                output[y, x, 0] = input[i + 2];
                output[y, x, 3] = input[i + 3];
            }

            return output;
        }

        /// <summary>
        /// Renders the input Pdf's pages as individual images.
        /// </summary>
        /// <param name="fileBytes">Input pdf.</param>
        /// <returns>A list of pages, as SKBitmap.</returns>
        public static List<SKBitmap> PdfToImages(byte[] fileBytes)
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
                            var width = pageReader.GetPageWidth();
                            var height = pageReader.GetPageHeight();
                            var bytes = pageReader.GetImage();
                            var bmp = ArrayToImage(ConvertTo3DArray(bytes, width, height));
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
            return
            [
                Path.GetFileNameWithoutExtension(filename),
                Path.GetExtension(filename).TrimStart('.')
            ];
        }

        /// <summary>
        /// Gets the number of pages in the file.
        /// </summary>
        /// <returns>The number of pages in the file.</returns>
        public int GetPageCount()
        {
            return _pageImages.Count;
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
            if (GetPageCount() > 1)
            {
                string[] splitName = SplitNameStrict(outputName);
                filename = $"{splitName[0]}.{_saveFormat}";
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
            if (GetPageCount() > 1)
            {
                string[] splitName = SplitNameStrict(outputName);
                filename = $"{splitName[0]}.{_saveFormat}";
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
            string filename = $"{splitName[0]}_page-{(pageIndex + 1):D3}.{_saveFormat}";

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
            string filename = $"{splitName[0]}_page-{(pageIndex + 1):D3}.{_saveFormat}";

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
            string fieldFilename = $"{splitName[0]}_{index:D3}.{_saveFormat}";
            return new ExtractedImage(ExtractImage(bbox, pageIndex), fieldFilename, _saveFormat);
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
            string fieldFilename = $"{splitName[0]}_{index:D3}.{_saveFormat}";
            return new ExtractedImage(ExtractImage(bbox, pageIndex), fieldFilename, _saveFormat);
        }

        private SKBitmap ExtractImage(Bbox bbox, int pageIndex)
        {
            SKBitmap image = _pageImages[pageIndex];
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
