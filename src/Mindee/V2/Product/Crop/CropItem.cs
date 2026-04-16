using System.Collections.Generic;
using System.Text.Json.Serialization;
using Mindee.Extraction;
using Mindee.Geometry;
using Mindee.Image;
using Mindee.Input;
using Mindee.V2.Parsing.Inference.Field;

namespace Mindee.V2.Product.Crop
{
    /// <summary>
    /// Result of a cropped document region.
    /// </summary>
    public class CropItem
    {
        /// <summary>
        /// Type or classification of the detected object.
        /// </summary>
        [JsonPropertyName("object_type")]
        public string ObjectType { get; set; }

        /// <summary>
        /// Location which includes cropping coordinates for the detected object, within the source document.
        /// </summary>
        [JsonPropertyName("location")]
        public FieldLocation Location { get; set; }

        /// <summary>
        /// String representation of the crop item.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"* :Location: {Location}\n  :Object Type: {ObjectType}";
        }

        /// <summary>
        /// Extract the crop from the source document.
        /// </summary>
        /// <param name="inputSource"></param>
        /// <returns></returns>
        public ExtractedImage ExtractFromFile(LocalInputSource inputSource)
        {
            var crop = new FileOperations.Crop(inputSource);
            return crop.ExtractSingleCrop(this);
        }
    }
}
