using System.Text.Json.Serialization;
using Mindee.Geometry;

namespace Mindee.V2.Product.Ocr
{
    /// <summary>
    /// OCR result for a single word extracted from the document page.
    /// </summary>
    public class OcrWord
    {
        /// <summary>
        /// Text content of the word.
        /// </summary>
        [JsonPropertyName("content")]
        public string Content { get; set; }


        /// <summary>
        /// Location which includes cropping coordinates for the detected object, within the source document.
        /// </summary>
        [JsonPropertyName("polygon")]
        [JsonConverter(typeof(PolygonJsonConverter))]
        public Polygon Polygon { get; set; }

        /// <summary>
        ///     A prettier representation.
        /// </summary>
        public override string ToString()
        {
            return Content;
        }
    }
}
