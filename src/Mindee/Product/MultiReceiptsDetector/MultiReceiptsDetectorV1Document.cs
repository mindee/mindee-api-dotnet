using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing;
using Mindee.Parsing.Standard;

namespace Mindee.Product.MultiReceiptsDetector
{
    /// <summary>
    ///     Multi Receipts Detector API version 1.1 document data.
    /// </summary>
    public class MultiReceiptsDetectorV1Document : IPrediction
    {
        /// <summary>
        ///     Positions of the receipts on the document.
        /// </summary>
        [JsonPropertyName("receipts")]
        public IList<PositionField> Receipts { get; set; } = new List<PositionField>();

        /// <summary>
        ///     A prettier representation of the current model values.
        /// </summary>
        public override string ToString()
        {
            var receipts = string.Join(
                "\n " + string.Concat(Enumerable.Repeat(" ", 18)),
                Receipts.Select(item => item));
            var result = new StringBuilder();
            result.Append($":List of Receipts: {receipts}\n");
            return SummaryHelper.Clean(result.ToString());
        }
    }
}
