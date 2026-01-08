using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing;
using Mindee.Parsing.Standard;

namespace Mindee.Product.Us.BankCheck
{
    /// <summary>
    ///     Bank Check API version 1.1 page data.
    /// </summary>
    public sealed class BankCheckV1Page : BankCheckV1Document
    {
        /// <summary>
        ///     The position of the check on the document.
        /// </summary>
        [JsonPropertyName("check_position")]
        public PositionField CheckPosition { get; set; }

        /// <summary>
        ///     List of signature positions
        /// </summary>
        [JsonPropertyName("signatures_positions")]
        public IList<PositionField> SignaturesPositions { get; set; } = new List<PositionField>();

        /// <summary>
        ///     A prettier representation of the current model values.
        /// </summary>
        public override string ToString()
        {
            var signaturesPositions = string.Join(
                "\n " + string.Concat(Enumerable.Repeat(" ", 21)),
                SignaturesPositions.Select(item => item));
            var result = new StringBuilder();
            result.Append($":Check Position: {CheckPosition}\n");
            result.Append($":Signature Positions: {signaturesPositions}\n");
            result.Append(base.ToString());
            return SummaryHelper.Clean(result.ToString());
        }
    }
}
