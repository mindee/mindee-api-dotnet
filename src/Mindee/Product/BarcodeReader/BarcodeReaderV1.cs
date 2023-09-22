using Mindee.Http;
using Mindee.Parsing.Common;

namespace Mindee.Product.BarcodeReader
{
    /// <summary>
    /// The definition for Barcode Reader, API version 1.
    /// </summary>
    [Endpoint("barcode_reader", "1")]
    public sealed class BarcodeReaderV1 : Inference<BarcodeReaderV1Document, BarcodeReaderV1Document>
    {
    }
}
