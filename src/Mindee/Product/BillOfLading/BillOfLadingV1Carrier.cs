using System.Collections.Generic;
using System.Text.Json.Serialization;
using Mindee.Parsing;

namespace Mindee.Product.BillOfLading
{
    /// <summary>
    ///     The shipping company responsible for transporting the goods.
    /// </summary>
    public sealed class BillOfLadingV1Carrier
    {
        /// <summary>
        ///     The name of the carrier.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        ///     The professional number of the carrier.
        /// </summary>
        [JsonPropertyName("professional_number")]
        public string ProfessionalNumber { get; set; }

        /// <summary>
        ///     The Standard Carrier Alpha Code (SCAC) of the carrier.
        /// </summary>
        [JsonPropertyName("scac")]
        public string Scac { get; set; }

        /// <summary>
        ///     Output the object in a format suitable for inclusion in an rST field list.
        /// </summary>
        public string ToFieldList()
        {
            var printable = PrintableValues();
            return "\n"
                   + $"  :Name: {printable["Name"]}\n"
                   + $"  :Professional Number: {printable["ProfessionalNumber"]}\n"
                   + $"  :SCAC: {printable["Scac"]}\n";
        }

        /// <summary>
        ///     A prettier representation of the line values.
        /// </summary>
        public override string ToString()
        {
            var printable = PrintableValues();
            return "Name: "
                   + printable["Name"]
                   + ", Professional Number: "
                   + printable["ProfessionalNumber"]
                   + ", SCAC: "
                   + printable["Scac"].Trim();
        }

        private Dictionary<string, string> PrintableValues()
        {
            return new Dictionary<string, string>
            {
                { "Name", SummaryHelper.FormatString(Name) },
                { "ProfessionalNumber", SummaryHelper.FormatString(ProfessionalNumber) },
                { "Scac", SummaryHelper.FormatString(Scac) }
            };
        }
    }
}
