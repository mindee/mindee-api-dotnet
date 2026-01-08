using System.Collections.Generic;
using System.Text.Json.Serialization;
using Mindee.Parsing;

namespace Mindee.Product.NutritionFactsLabel
{
    /// <summary>
    ///     The size of a single serving of the product.
    /// </summary>
    public sealed class NutritionFactsLabelV1ServingSize
    {
        /// <summary>
        ///     The amount of a single serving.
        /// </summary>
        [JsonPropertyName("amount")]
        public double? Amount { get; set; }

        /// <summary>
        ///     The unit for the amount of a single serving.
        /// </summary>
        [JsonPropertyName("unit")]
        public string Unit { get; set; }

        /// <summary>
        ///     Output the object in a format suitable for inclusion in an rST field list.
        /// </summary>
        public string ToFieldList()
        {
            var printable = PrintableValues();
            return "\n"
                   + $"  :Amount: {printable["Amount"]}\n"
                   + $"  :Unit: {printable["Unit"]}\n";
        }

        /// <summary>
        ///     A prettier representation of the line values.
        /// </summary>
        public override string ToString()
        {
            var printable = PrintableValues();
            return "Amount: "
                   + printable["Amount"]
                   + ", Unit: "
                   + printable["Unit"].Trim();
        }

        private Dictionary<string, string> PrintableValues()
        {
            return new Dictionary<string, string>
            {
                { "Amount", SummaryHelper.FormatAmount(Amount) }, { "Unit", SummaryHelper.FormatString(Unit) }
            };
        }
    }
}
