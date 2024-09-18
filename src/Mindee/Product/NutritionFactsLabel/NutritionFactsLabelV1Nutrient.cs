using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing;
using Mindee.Parsing.Standard;

namespace Mindee.Product.NutritionFactsLabel
{
    /// <summary>
    /// The amount of nutrients in the product.
    /// </summary>
    public sealed class NutritionFactsLabelV1Nutrient : LineItemField
    {
        /// <summary>
        /// DVs are the recommended amounts of nutrients to consume or not to exceed each day.
        /// </summary>
        [JsonPropertyName("daily_value")]
        public double? DailyValue { get; set; }

        /// <summary>
        /// The name of nutrients of the product.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// The amount of nutrients per 100g of the product.
        /// </summary>
        [JsonPropertyName("per_100g")]
        public double? Per100G { get; set; }

        /// <summary>
        /// The amount of nutrients per serving of the product.
        /// </summary>
        [JsonPropertyName("per_serving")]
        public double? PerServing { get; set; }

        /// <summary>
        /// The unit of measurement for the amount of nutrients.
        /// </summary>
        [JsonPropertyName("unit")]
        public string Unit { get; set; }

        /// <summary>
        /// Output the line in a format suitable for inclusion in an rST table.
        /// </summary>
        public override string ToTableLine()
        {
            Dictionary<string, string> printable = PrintableValues();
            return "| "
              + String.Format("{0,-11}", printable["DailyValue"])
              + " | "
              + String.Format("{0,-20}", printable["Name"])
              + " | "
              + String.Format("{0,-8}", printable["Per100G"])
              + " | "
              + String.Format("{0,-11}", printable["PerServing"])
              + " | "
              + String.Format("{0,-4}", printable["Unit"])
              + " |";
        }

        /// <summary>
        /// A prettier representation of the line values.
        /// </summary>
        public override string ToString()
        {
            Dictionary<string, string> printable = PrintableValues();
            return "Daily Value: "
              + printable["DailyValue"]
              + ", Name: "
              + printable["Name"]
              + ", Per 100g: "
              + printable["Per100G"]
              + ", Per Serving: "
              + printable["PerServing"]
              + ", Unit: "
              + printable["Unit"].Trim();
        }

        private Dictionary<string, string> PrintableValues()
        {
            return new Dictionary<string, string>()
            {
                {"DailyValue", SummaryHelper.FormatAmount(DailyValue)},
                {"Name", SummaryHelper.FormatString(Name, 20)},
                {"Per100G", SummaryHelper.FormatAmount(Per100G)},
                {"PerServing", SummaryHelper.FormatAmount(PerServing)},
                {"Unit", SummaryHelper.FormatString(Unit)},
            };
        }
    }

    /// <summary>
    /// The amount of nutrients in the product.
    /// </summary>
    public class NutritionFactsLabelV1Nutrients : List<NutritionFactsLabelV1Nutrient>
    {
        /// <summary>
        /// Default string representation.
        /// </summary>
        public override string ToString()
        {
            if (this.Count == 0)
            {
                return "\n";
            }
            int[] columnSizes = { 13, 22, 10, 13, 6 };
            StringBuilder outStr = new StringBuilder("\n");
            outStr.Append("  " + SummaryHelper.LineSeparator(columnSizes, '-') + "  ");
            outStr.Append("| Daily Value ");
            outStr.Append("| Name                 ");
            outStr.Append("| Per 100g ");
            outStr.Append("| Per Serving ");
            outStr.Append("| Unit ");
            outStr.Append("|\n  " + SummaryHelper.LineSeparator(columnSizes, '='));
            outStr.Append(SummaryHelper.ArrayToString(this, columnSizes));
            return outStr.ToString();
        }
    }
}
