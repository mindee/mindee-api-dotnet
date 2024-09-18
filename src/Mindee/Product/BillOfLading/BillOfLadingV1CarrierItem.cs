using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing;
using Mindee.Parsing.Standard;

namespace Mindee.Product.BillOfLading
{
    /// <summary>
    /// The goods being shipped.
    /// </summary>
    public sealed class BillOfLadingV1CarrierItem : LineItemField
    {
        /// <summary>
        /// A description of the item.
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        /// The gross weight of the item.
        /// </summary>
        [JsonPropertyName("gross_weight")]
        public double? GrossWeight { get; set; }

        /// <summary>
        /// The measurement of the item.
        /// </summary>
        [JsonPropertyName("measurement")]
        public double? Measurement { get; set; }

        /// <summary>
        /// The unit of measurement for the measurement.
        /// </summary>
        [JsonPropertyName("measurement_unit")]
        public string MeasurementUnit { get; set; }

        /// <summary>
        /// The quantity of the item being shipped.
        /// </summary>
        [JsonPropertyName("quantity")]
        public double? Quantity { get; set; }

        /// <summary>
        /// The unit of measurement for weights.
        /// </summary>
        [JsonPropertyName("weight_unit")]
        public string WeightUnit { get; set; }

        /// <summary>
        /// Output the line in a format suitable for inclusion in an rST table.
        /// </summary>
        public override string ToTableLine()
        {
            Dictionary<string, string> printable = PrintableValues();
            return "| "
              + String.Format("{0,-36}", printable["Description"])
              + " | "
              + String.Format("{0,-12}", printable["GrossWeight"])
              + " | "
              + String.Format("{0,-11}", printable["Measurement"])
              + " | "
              + String.Format("{0,-16}", printable["MeasurementUnit"])
              + " | "
              + String.Format("{0,-8}", printable["Quantity"])
              + " | "
              + String.Format("{0,-11}", printable["WeightUnit"])
              + " |";
        }

        /// <summary>
        /// A prettier representation of the line values.
        /// </summary>
        public override string ToString()
        {
            Dictionary<string, string> printable = PrintableValues();
            return "Description: "
              + printable["Description"]
              + ", Gross Weight: "
              + printable["GrossWeight"]
              + ", Measurement: "
              + printable["Measurement"]
              + ", Measurement Unit: "
              + printable["MeasurementUnit"]
              + ", Quantity: "
              + printable["Quantity"]
              + ", Weight Unit: "
              + printable["WeightUnit"].Trim();
        }

        private Dictionary<string, string> PrintableValues()
        {
            return new Dictionary<string, string>()
            {
                {"Description", SummaryHelper.FormatString(Description, 36)},
                {"GrossWeight", SummaryHelper.FormatAmount(GrossWeight)},
                {"Measurement", SummaryHelper.FormatAmount(Measurement)},
                {"MeasurementUnit", SummaryHelper.FormatString(MeasurementUnit)},
                {"Quantity", SummaryHelper.FormatAmount(Quantity)},
                {"WeightUnit", SummaryHelper.FormatString(WeightUnit)},
            };
        }
    }

    /// <summary>
    /// The goods being shipped.
    /// </summary>
    public class BillOfLadingV1CarrierItems : List<BillOfLadingV1CarrierItem>
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
            int[] columnSizes = { 38, 14, 13, 18, 10, 13 };
            StringBuilder outStr = new StringBuilder("\n");
            outStr.Append("  " + SummaryHelper.LineSeparator(columnSizes, '-') + "  ");
            outStr.Append("| Description                          ");
            outStr.Append("| Gross Weight ");
            outStr.Append("| Measurement ");
            outStr.Append("| Measurement Unit ");
            outStr.Append("| Quantity ");
            outStr.Append("| Weight Unit ");
            outStr.Append("|\n  " + SummaryHelper.LineSeparator(columnSizes, '='));
            outStr.Append(SummaryHelper.ArrayToString(this, columnSizes));
            return outStr.ToString();
        }
    }
}
