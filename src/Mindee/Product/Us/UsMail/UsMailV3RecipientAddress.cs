using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing;
using Mindee.Parsing.Standard;

namespace Mindee.Product.Us.UsMail
{
    /// <summary>
    /// The addresses of the recipients.
    /// </summary>
    public sealed class UsMailV3RecipientAddress : LineItemField
    {
        /// <summary>
        /// The city of the recipient's address.
        /// </summary>
        [JsonPropertyName("city")]
        public string City { get; set; }

        /// <summary>
        /// The complete address of the recipient.
        /// </summary>
        [JsonPropertyName("complete")]
        public string Complete { get; set; }

        /// <summary>
        /// Indicates if the recipient's address is a change of address.
        /// </summary>
        [JsonPropertyName("is_address_change")]
        public bool? IsAddressChange { get; set; }

        /// <summary>
        /// The postal code of the recipient's address.
        /// </summary>
        [JsonPropertyName("postal_code")]
        public string PostalCode { get; set; }

        /// <summary>
        /// The private mailbox number of the recipient's address.
        /// </summary>
        [JsonPropertyName("private_mailbox_number")]
        public string PrivateMailboxNumber { get; set; }

        /// <summary>
        /// Second part of the ISO 3166-2 code, consisting of two letters indicating the US State.
        /// </summary>
        [JsonPropertyName("state")]
        public string State { get; set; }

        /// <summary>
        /// The street of the recipient's address.
        /// </summary>
        [JsonPropertyName("street")]
        public string Street { get; set; }

        /// <summary>
        /// The unit number of the recipient's address.
        /// </summary>
        [JsonPropertyName("unit")]
        public string Unit { get; set; }

        private Dictionary<string, string> TablePrintableValues()
        {
            return new Dictionary<string, string>()
            {
                {"City", SummaryHelper.FormatString(City, 15)},
                {"Complete", SummaryHelper.FormatString(Complete, 35)},
                {"IsAddressChange", SummaryHelper.FormatBool(IsAddressChange)},
                {"PostalCode", SummaryHelper.FormatString(PostalCode)},
                {"PrivateMailboxNumber", SummaryHelper.FormatString(PrivateMailboxNumber)},
                {"State", SummaryHelper.FormatString(State)},
                {"Street", SummaryHelper.FormatString(Street, 25)},
                {"Unit", SummaryHelper.FormatString(Unit, 15)},
            };
        }

        /// <summary>
        /// Output the line in a format suitable for inclusion in an rST table.
        /// </summary>
        public override string ToTableLine()
        {
            Dictionary<string, string> printable = TablePrintableValues();
            return "| "
              + String.Format("{0,-15}", printable["City"])
              + " | "
              + String.Format("{0,-35}", printable["Complete"])
              + " | "
              + String.Format("{0,-17}", printable["IsAddressChange"])
              + " | "
              + String.Format("{0,-11}", printable["PostalCode"])
              + " | "
              + String.Format("{0,-22}", printable["PrivateMailboxNumber"])
              + " | "
              + String.Format("{0,-5}", printable["State"])
              + " | "
              + String.Format("{0,-25}", printable["Street"])
              + " | "
              + String.Format("{0,-15}", printable["Unit"])
              + " |";
        }

        /// <summary>
        /// A prettier representation of the line values.
        /// </summary>
        public override string ToString()
        {
            Dictionary<string, string> printable = PrintableValues();
            return "City: "
              + printable["City"]
              + ", Complete Address: "
              + printable["Complete"]
              + ", Is Address Change: "
              + printable["IsAddressChange"]
              + ", Postal Code: "
              + printable["PostalCode"]
              + ", Private Mailbox Number: "
              + printable["PrivateMailboxNumber"]
              + ", State: "
              + printable["State"]
              + ", Street: "
              + printable["Street"]
              + ", Unit: "
              + printable["Unit"].Trim();
        }

        private Dictionary<string, string> PrintableValues()
        {
            return new Dictionary<string, string>()
            {
                {"City", SummaryHelper.FormatString(City)},
                {"Complete", SummaryHelper.FormatString(Complete)},
                {"IsAddressChange", SummaryHelper.FormatBool(IsAddressChange)},
                {"PostalCode", SummaryHelper.FormatString(PostalCode)},
                {"PrivateMailboxNumber", SummaryHelper.FormatString(PrivateMailboxNumber)},
                {"State", SummaryHelper.FormatString(State)},
                {"Street", SummaryHelper.FormatString(Street)},
                {"Unit", SummaryHelper.FormatString(Unit)},
            };
        }
    }

    /// <summary>
    /// The addresses of the recipients.
    /// </summary>
    public class UsMailV3RecipientAddresses : List<UsMailV3RecipientAddress>
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
            int[] columnSizes = { 17, 37, 19, 13, 24, 7, 27, 17 };
            StringBuilder outStr = new StringBuilder("\n");
            outStr.Append("  " + SummaryHelper.LineSeparator(columnSizes, '-') + "  ");
            outStr.Append("| City            ");
            outStr.Append("| Complete Address                    ");
            outStr.Append("| Is Address Change ");
            outStr.Append("| Postal Code ");
            outStr.Append("| Private Mailbox Number ");
            outStr.Append("| State ");
            outStr.Append("| Street                    ");
            outStr.Append("| Unit            ");
            outStr.Append("|\n  " + SummaryHelper.LineSeparator(columnSizes, '='));
            outStr.Append(SummaryHelper.ArrayToString(this, columnSizes));
            return outStr.ToString();
        }
    }
}
