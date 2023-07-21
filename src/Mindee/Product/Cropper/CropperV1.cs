using Mindee.Http;
using Mindee.Parsing.Common;

namespace Mindee.Product.Cropper
{
    /// <summary>
    /// The definition for Cropper, API version 1.
    /// </summary>
    [Endpoint("cropper", "1")]
    public sealed class CropperV1 : Inference<CropperV1Document, CropperV1Document>
    {
    }
}
