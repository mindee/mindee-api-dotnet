using System.Collections.Generic;
using System.Text.Json.Serialization;
using Mindee.Parsing;

namespace Mindee.Product.NutritionFactsLabel
{
    /// <summary>
    ///     The amount of trans fat in the product.
    /// </summary>
    public sealed class NutritionFactsLabelV1TransFat
    {
        /// <summary>
        ///     DVs are the recommended amounts of trans fat to consume or not to exceed each day.
        /// </summary>
        [JsonPropertyName("daily_value")]
        public double? DailyValue { get; set; }

        /// <summary>
        ///     The amount of trans fat per 100g of the product.
        /// </summary>
        [JsonPropertyName("per_100g")]
        public double? Per100G { get; set; }

        /// <summary>
        ///     The amount of trans fat per serving of the product.
        /// </summary>
        [JsonPropertyName("per_serving")]
        public double? PerServing { get; set; }

        /// <summary>
        ///     Output the object in a format suitable for inclusion in an rST field list.
        /// </summary>
        public string ToFieldList()
        {
            var printable = PrintableValues();
            return "\n"
                   + $"  :Daily Value: {printable["DailyValue"]}\n"
                   + $"  :Per 100g: {printable["Per100G"]}\n"
                   + $"  :Per Serving: {printable["PerServing"]}\n";
        }

        /// <summary>
        ///     A prettier representation of the line values.
        /// </summary>
        public override string ToString()
        {
            var printable = PrintableValues();
            return "Daily Value: "
                   + printable["DailyValue"]
                   + ", Per 100g: "
                   + printable["Per100G"]
                   + ", Per Serving: "
                   + printable["PerServing"].Trim();
        }

        private Dictionary<string, string> PrintableValues()
        {
            return new Dictionary<string, string>
            {
                { "DailyValue", SummaryHelper.FormatAmount(DailyValue) },
                { "Per100G", SummaryHelper.FormatAmount(Per100G) },
                { "PerServing", SummaryHelper.FormatAmount(PerServing) }
            };
        }
    }
}
