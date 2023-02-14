using Mindee.Parsing.Common;

namespace Mindee.Parsing.Cropper
{
    /// <summary>
    /// The cropper v1 definition.
    /// </summary>
    [Endpoint("cropper", "1")]
    public class CropperV1Inference : Inference<CropperV1DocumentPrediction, CropperV1DocumentPrediction>
    {
    }
}
