using System.Collections.Generic;
using System.Linq;
using Mindee.Extraction;
using Mindee.Geometry;
using Mindee.Image;
using Mindee.Input;
using Mindee.V2.Product.Crop;

namespace Mindee.V2.FileOperations
{
    /// <summary>
    /// V2 Crop operation utility.
    /// </summary>
    public sealed class Crop
    {
        /// <summary>
        ///     LocalInputSource object.
        /// </summary>
        private readonly LocalInputSource _localInput;

        /// <summary>
        ///
        /// </summary>
        /// <param name="inputSource"></param>
        public Crop(LocalInputSource inputSource)
        {
            this._localInput = inputSource;
        }

        /// <summary>
        /// Extract a single crop item from a file.
        /// </summary>
        /// <param name="crop"></param>
        /// <returns></returns>
        public ExtractedImage ExtractSingleCrop(CropItem crop)
        {
            var polygons = new List<Polygon> { crop.Location.Polygon };
            var imageExtractor = new ImageExtractor(this._localInput);
            return imageExtractor.ExtractMultipleImagesFromSource(crop.Location.Page, polygons)[0];
        }

        /// <summary>
        /// Extracts multiple crop zones from a file.
        /// </summary>
        /// <param name="crops">List of crops.</param>
        /// <returns></returns>
        public CropFiles ExtractCrops(List<CropItem> crops)
        {
            var imageExtractor = new ImageExtractor(this._localInput);
            CropFiles extractedImages = [];
            var cropsPerPage = crops.GroupBy(c => c.Location.Page).ToList();
            foreach (var pageCrops in cropsPerPage)
            {
                extractedImages.AddRange(imageExtractor.ExtractMultipleImagesFromSource(pageCrops.Key, pageCrops.Select(c => c.Location.Polygon).ToList()));
            }
            return extractedImages;
        }
    }
}
