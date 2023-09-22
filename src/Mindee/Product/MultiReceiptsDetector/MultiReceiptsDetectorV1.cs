using Mindee.Http;
using Mindee.Parsing.Common;

namespace Mindee.Product.MultiReceiptsDetector
{
    /// <summary>
    /// The definition for Multi Receipts Detector, API version 1.
    /// </summary>
    [Endpoint("multi_receipts_detector", "1")]
    public sealed class MultiReceiptsDetectorV1 : Inference<MultiReceiptsDetectorV1Document, MultiReceiptsDetectorV1Document>
    {
    }
}
