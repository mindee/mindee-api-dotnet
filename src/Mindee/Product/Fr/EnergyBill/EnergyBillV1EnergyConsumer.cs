using System.Collections.Generic;
using System.Text.Json.Serialization;
using Mindee.Parsing;

namespace Mindee.Product.Fr.EnergyBill
{
    /// <summary>
    ///     The entity that consumes the energy.
    /// </summary>
    public sealed class EnergyBillV1EnergyConsumer
    {
        /// <summary>
        ///     The address of the energy consumer.
        /// </summary>
        [JsonPropertyName("address")]
        public string Address { get; set; }

        /// <summary>
        ///     The name of the energy consumer.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        ///     Output the object in a format suitable for inclusion in an rST field list.
        /// </summary>
        public string ToFieldList()
        {
            var printable = PrintableValues();
            return "\n"
                   + $"  :Address: {printable["Address"]}\n"
                   + $"  :Name: {printable["Name"]}\n";
        }

        /// <summary>
        ///     A prettier representation of the line values.
        /// </summary>
        public override string ToString()
        {
            var printable = PrintableValues();
            return "Address: "
                   + printable["Address"]
                   + ", Name: "
                   + printable["Name"].Trim();
        }

        private Dictionary<string, string> PrintableValues()
        {
            return new Dictionary<string, string>
            {
                { "Address", SummaryHelper.FormatString(Address) }, { "Name", SummaryHelper.FormatString(Name) }
            };
        }
    }
}
