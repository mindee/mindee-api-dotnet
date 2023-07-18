using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing;
using Mindee.Parsing.Standard;

namespace Mindee.Product.Fr.BankAccountDetails
{
    /// <summary>
    /// Document data for Bank Account Details, API version 2.
    /// </summary>
    public class BankAccountDetailsV2Document : IPrediction
    {
        /// <summary>
        /// Full extraction of the account holders names.
        /// </summary>
        [JsonPropertyName("account_holders_names")]
        public StringField AccountHoldersNames { get; set; }

        /// <summary>
        /// Full extraction of BBAN, including: branch code, bank code, account and key.
        /// </summary>
        [JsonPropertyName("bban")]
        public BankAccountDetailsV2Bban Bban { get; set; }

        /// <summary>
        /// Full extraction of the IBAN number.
        /// </summary>
        [JsonPropertyName("iban")]
        public StringField Iban { get; set; }

        /// <summary>
        /// Full extraction of the SWIFT code.
        /// </summary>
        [JsonPropertyName("swift_code")]
        public StringField SwiftCode { get; set; }

        /// <summary>
        /// A prettier representation of the current model values.
        /// </summary>
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.Append($":Account Holder's Names: {AccountHoldersNames}\n");
            result.Append($":Basic Bank Account Number:{Bban.ToFieldList()}");
            result.Append($":IBAN: {Iban}\n");
            result.Append($":SWIFT Code: {SwiftCode}\n");
            return SummaryHelper.Clean(result.ToString());
        }
    }
}
