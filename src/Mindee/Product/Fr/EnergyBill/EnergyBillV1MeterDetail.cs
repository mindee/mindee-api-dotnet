using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing;
using Mindee.Parsing.Standard;

namespace Mindee.Product.Fr.EnergyBill
{
    /// <summary>
    /// Information about the energy meter.
    /// </summary>
    public sealed class EnergyBillV1MeterDetail
    {
        /// <summary>
        /// The unique identifier of the energy meter.
        /// </summary>
        [JsonPropertyName("meter_number")]
        public string MeterNumber { get; set; }

        /// <summary>
        /// The type of energy meter.
        /// </summary>
        [JsonPropertyName("meter_type")]
        public string MeterType { get; set; }

        /// <summary>
        /// The unit of power for energy consumption.
        /// </summary>
        [JsonPropertyName("unit")]
        public string Unit { get; set; }

        /// <summary>
        /// Output the object in a format suitable for inclusion in an rST field list.
        /// </summary>
        public string ToFieldList()
        {
            Dictionary<string, string> printable = PrintableValues();
            return "\n"
                + $"  :Meter Number: {printable["MeterNumber"]}\n"
                + $"  :Meter Type: {printable["MeterType"]}\n"
                + $"  :Unit of Power: {printable["Unit"]}\n";
        }

        /// <summary>
        /// A prettier representation of the line values.
        /// </summary>
        public override string ToString()
        {
            Dictionary<string, string> printable = PrintableValues();
            return "Meter Number: "
              + printable["MeterNumber"]
              + ", Meter Type: "
              + printable["MeterType"]
              + ", Unit of Power: "
              + printable["Unit"].Trim();
        }

        private Dictionary<string, string> PrintableValues()
        {
            return new Dictionary<string, string>()
            {
                {"MeterNumber", SummaryHelper.FormatString(MeterNumber)},
                {"MeterType", SummaryHelper.FormatString(MeterType)},
                {"Unit", SummaryHelper.FormatString(Unit)},
            };
        }
    }
}
