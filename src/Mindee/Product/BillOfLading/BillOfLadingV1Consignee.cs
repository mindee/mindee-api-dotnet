using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing;
using Mindee.Parsing.Standard;

namespace Mindee.Product.BillOfLading
{
    /// <summary>
    /// The party to whom the goods are being shipped.
    /// </summary>
    public sealed class BillOfLadingV1Consignee
    {
        /// <summary>
        /// The address of the consignee.
        /// </summary>
        [JsonPropertyName("address")]
        public string Address { get; set; }

        /// <summary>
        /// The  email of the shipper.
        /// </summary>
        [JsonPropertyName("email")]
        public string Email { get; set; }

        /// <summary>
        /// The name of the consignee.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// The phone number of the consignee.
        /// </summary>
        [JsonPropertyName("phone")]
        public string Phone { get; set; }

        /// <summary>
        /// Output the object in a format suitable for inclusion in an rST field list.
        /// </summary>
        public string ToFieldList()
        {
            Dictionary<string, string> printable = PrintableValues();
            return "\n"
                + $"  :Address: {printable["Address"]}\n"
                + $"  :Email: {printable["Email"]}\n"
                + $"  :Name: {printable["Name"]}\n"
                + $"  :Phone: {printable["Phone"]}\n";
        }

        /// <summary>
        /// A prettier representation of the line values.
        /// </summary>
        public override string ToString()
        {
            Dictionary<string, string> printable = PrintableValues();
            return "Address: "
              + printable["Address"]
              + ", Email: "
              + printable["Email"]
              + ", Name: "
              + printable["Name"]
              + ", Phone: "
              + printable["Phone"].Trim();
        }

        private Dictionary<string, string> PrintableValues()
        {
            return new Dictionary<string, string>()
            {
                {"Address", SummaryHelper.FormatString(Address)},
                {"Email", SummaryHelper.FormatString(Email)},
                {"Name", SummaryHelper.FormatString(Name)},
                {"Phone", SummaryHelper.FormatString(Phone)},
            };
        }
    }
}
