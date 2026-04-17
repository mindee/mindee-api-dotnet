using System.Collections.Generic;
using System.Linq;
using Mindee.Exceptions;
using Mindee.Geometry;
using Mindee.Image;
using Mindee.Input;
using Mindee.V1.Parsing.Standard;

namespace Mindee.V1.Image
{
    /// <summary>
    ///     Legacy V1 Wrapper for ImageExtractor.
    /// </summary>
    public sealed class ImageExtractor : Mindee.Extraction.ImageExtractor
    {
        /// <summary>
        ///     Init from a Local Input Source.
        /// </summary>
        /// <param name="localInput">Locally loaded resource.</param>
        /// <param name="saveFormat">Format to save the resulting images as.</param>
        public ImageExtractor(LocalInputSource localInput, string saveFormat = null)
            : base(localInput, saveFormat)
        { }

        /// <summary>
        ///     Init from a path.
        /// </summary>
        /// <param name="filePath">Path to the file.</param>
        public ImageExtractor(string filePath)
            : base(filePath)
        { }


        /// <summary>
        ///     Extract multiple images on a given page from a list of fields having position data.
        /// </summary>
        /// <param name="fields">List of Fields to extract.</param>
        /// <param name="pageIndex">The page index to extract, begins at 0.</param>
        /// <returns>A list of extracted images.</returns>
        public IList<ExtractedImage> ExtractImagesFromPage(IList<PositionField> fields, int pageIndex)
        {
            return ExtractFromPage(fields, pageIndex, _filename);
        }

        /// <summary>
        ///     Extract multiple images on a given page from a list of fields having position data.
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
                var splitName = SplitNameStrict(outputName);
                filename = $"{splitName[0]}.{SaveFormat}";
            }
            else
            {
                filename = outputName;
            }

            return ExtractFromPage(fields, pageIndex, filename);
        }

        /// <summary>
        ///     Extract multiple images on a given page from a list of fields having position data.
        /// </summary>
        /// <param name="fields">List of Fields to extract.</param>
        /// <param name="pageIndex">The page index to extract. Begins at 0.</param>
        /// <param name="outputName">The base output filename. Must have an image extension.</param>
        /// <returns>A list of extracted images.</returns>
        public IList<ExtractedImage> ExtractImagesFromPage(IList<PositionField> fields, int pageIndex,
            string outputName)
        {
            string filename;
            if (GetPageCount() > 1)
            {
                var splitName = SplitNameStrict(outputName);
                filename = $"{splitName[0]}.{SaveFormat}";
            }
            else
            {
                filename = outputName;
            }

            return ExtractFromPage(fields, pageIndex, filename);
        }

        /// <summary>
        ///     Extract multiple images on a given page from a list of fields having position data.
        /// </summary>
        /// <param name="fields">List of Fields to extract.</param>
        /// <param name="pageIndex">The page index to extract, begins at 0.</param>
        /// <param name="outputName">The base output filename, must have an image extension.</param>
        /// <returns>A list of extracted images.</returns>
        private List<ExtractedImage> ExtractFromPage<TBaseField>(IList<TBaseField> fields, int pageIndex,
            string outputName) where TBaseField : BaseField
        {
            var splitName = SplitNameStrict(outputName);
            var filename = $"{splitName[0]}_page-{pageIndex + 1:D3}.{SaveFormat}";

            return fields.Select((t, i) => ExtractImage(t, pageIndex, i + 1, filename)).Where(extractedImage => extractedImage != null).ToList();
        }

        private List<ExtractedImage> ExtractFromPage(IList<PositionField> fields, int pageIndex, string outputName)
        {
            var splitName = SplitNameStrict(outputName);
            var filename = $"{splitName[0]}_page-{pageIndex + 1:D3}.{SaveFormat}";

            var extractedImages = new List<ExtractedImage>();
            for (var i = 0; i < fields.Count; i++)
            {
                var extractedImage = ExtractImage(fields[i], pageIndex, i + 1, filename);
                if (extractedImage != null)
                {
                    extractedImages.Add(extractedImage);
                }
            }

            return extractedImages;
        }

        /// <summary>
        ///     Extracts a single image from a Position field.
        /// </summary>
        /// <param name="field">The field to extract.</param>
        /// <param name="pageIndex">The page index to extract, begins at 0.</param>
        /// <param name="index">The index to use for naming the extracted image.</param>
        /// <param name="filename">The output filename.</param>
        /// <returns>The extracted image, or null if the field does not have valid position data.</returns>
        public ExtractedImage ExtractImage(PositionField field, int pageIndex, int index, string filename)
        {
            var splitName = SplitNameStrict(filename);
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

            var bbox = Utils.BboxFromPolygon(boundingBox);
            var fieldFilename = $"{splitName[0]}_{index:D3}.{SaveFormat}";
            return new ExtractedImage(ExtractImage(bbox, pageIndex), fieldFilename, SaveFormat, pageIndex, index);
        }

        /// <summary>
        ///     Extracts a single image from a field having position data.
        /// </summary>
        /// <param name="field">The field to extract.</param>
        /// <param name="pageIndex">The page index to extract, begins at 0.</param>
        /// <param name="index">The index to use for naming the extracted image.</param>
        /// <param name="filename">The output filename.</param>
        /// <returns>The extracted image, or null if the field does not have valid position data.</returns>
        public ExtractedImage ExtractImage(BaseField field, int pageIndex, int index, string filename)
        {
            var splitName = SplitNameStrict(filename);
            Polygon boundingBox;

            if (field.Polygon.Count > 0)
            {
                boundingBox = field.Polygon;
            }
            else
            {
                throw new MindeeException("Provided field has no valid position data.");
            }

            var bbox = Utils.BboxFromPolygon(boundingBox);
            var fieldFilename = $"{splitName[0]}_{index:D3}.{SaveFormat}";
            return new ExtractedImage(ExtractImage(bbox, pageIndex), fieldFilename, SaveFormat, pageIndex, index);
        }
    }
}
