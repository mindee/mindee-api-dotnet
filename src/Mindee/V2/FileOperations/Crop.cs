using System.Collections.Generic;
using System.Linq;
using Mindee.Extraction;
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
            var imageExtractor = new ImageExtractor(this._localInput);
            return imageExtractor.ExtractImage(crop.Location, crop.Location.Page, 0);
        }

        /// <summary>
        /// Extracts multiple crop zones from a file.
        /// </summary>
        /// <param name="crops">List of crops.</param>
        /// <returns></returns>
        public ExtractedImages ExtractMultipleCrops(List<CropItem> crops)
        {
            ExtractedImages extractedImages = [];
            if (crops.Count <= 0)
                return extractedImages;

            var imageExtractor = new ImageExtractor(this._localInput);

            // Group crops by page, preserving insertion order
            var cropsByPage = crops
                .GroupBy(c => c.Location.Page)
                .Select(g => new { Page = g.Key, CropItem = g.ToList() })
                .ToList();

            foreach (var pageGroup in cropsByPage)
            {
                var page = pageGroup.Page;
                var locations = pageGroup.CropItem.Select(c => c.Location).ToList();
                extractedImages.AddRange(imageExtractor.ExtractImagesFromPage(locations, page));
            }
            return extractedImages;
        }
    }
}
