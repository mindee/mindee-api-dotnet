using System.Collections.Generic;
using System.Text.Json.Serialization;
using Mindee.Parsing;

namespace Mindee.Product.Us.UsMail
{
    /// <summary>
    ///     The address of the sender.
    /// </summary>
    public sealed class UsMailV3SenderAddress
    {
        /// <summary>
        ///     The city of the sender's address.
        /// </summary>
        [JsonPropertyName("city")]
        public string City { get; set; }

        /// <summary>
        ///     The complete address of the sender.
        /// </summary>
        [JsonPropertyName("complete")]
        public string Complete { get; set; }

        /// <summary>
        ///     The postal code of the sender's address.
        /// </summary>
        [JsonPropertyName("postal_code")]
        public string PostalCode { get; set; }

        /// <summary>
        ///     Second part of the ISO 3166-2 code, consisting of two letters indicating the US State.
        /// </summary>
        [JsonPropertyName("state")]
        public string State { get; set; }

        /// <summary>
        ///     The street of the sender's address.
        /// </summary>
        [JsonPropertyName("street")]
        public string Street { get; set; }

        /// <summary>
        ///     Output the object in a format suitable for inclusion in an rST field list.
        /// </summary>
        public string ToFieldList()
        {
            var printable = PrintableValues();
            return "\n"
                   + $"  :City: {printable["City"]}\n"
                   + $"  :Complete Address: {printable["Complete"]}\n"
                   + $"  :Postal Code: {printable["PostalCode"]}\n"
                   + $"  :State: {printable["State"]}\n"
                   + $"  :Street: {printable["Street"]}\n";
        }

        /// <summary>
        ///     A prettier representation of the line values.
        /// </summary>
        public override string ToString()
        {
            var printable = PrintableValues();
            return "City: "
                   + printable["City"]
                   + ", Complete Address: "
                   + printable["Complete"]
                   + ", Postal Code: "
                   + printable["PostalCode"]
                   + ", State: "
                   + printable["State"]
                   + ", Street: "
                   + printable["Street"].Trim();
        }

        private Dictionary<string, string> PrintableValues()
        {
            return new Dictionary<string, string>
            {
                { "City", SummaryHelper.FormatString(City) },
                { "Complete", SummaryHelper.FormatString(Complete) },
                { "PostalCode", SummaryHelper.FormatString(PostalCode) },
                { "State", SummaryHelper.FormatString(State) },
                { "Street", SummaryHelper.FormatString(Street) }
            };
        }
    }
}
