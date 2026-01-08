using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing;
using Mindee.Parsing.Standard;

namespace Mindee.Product.BarcodeReader
{
    /// <summary>
    ///     Barcode Reader API version 1.0 document data.
    /// </summary>
    public class BarcodeReaderV1Document : IPrediction
    {
        /// <summary>
        ///     List of decoded 1D barcodes.
        /// </summary>
        [JsonPropertyName("codes_1d")]
        public IList<StringField> Codes1D { get; set; } = new List<StringField>();

        /// <summary>
        ///     List of decoded 2D barcodes.
        /// </summary>
        [JsonPropertyName("codes_2d")]
        public IList<StringField> Codes2D { get; set; } = new List<StringField>();

        /// <summary>
        ///     A prettier representation of the current model values.
        /// </summary>
        public override string ToString()
        {
            var codes1D = string.Join(
                "\n " + string.Concat(Enumerable.Repeat(" ", 13)),
                Codes1D.Select(item => item));
            var codes2D = string.Join(
                "\n " + string.Concat(Enumerable.Repeat(" ", 13)),
                Codes2D.Select(item => item));
            var result = new StringBuilder();
            result.Append($":Barcodes 1D: {codes1D}\n");
            result.Append($":Barcodes 2D: {codes2D}\n");
            return SummaryHelper.Clean(result.ToString());
        }
    }
}
