using Mindee.Http;
using Mindee.Parsing.Common;

namespace Mindee.Product.Cropper
{
    /// <summary>
    /// The cropper v1 definition.
    /// </summary>
    [Endpoint("cropper", "1")]
    public sealed class CropperV1 : Inference<CropperV1Document, CropperV1Document>
    {
    }
}
