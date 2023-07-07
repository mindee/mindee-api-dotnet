using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing;
using Mindee.Parsing.Standard;

namespace Mindee.Product.Fr.BankAccountDetails
{
    /// <summary>
    /// Document data for Bank Account Details, API version 1.
    /// </summary>
    public class BankAccountDetailsV1Document : IPrediction
    {
        /// <summary>
        /// The name of the account holder as seen on the document.
        /// </summary>
        [JsonPropertyName("account_holder_name")]
        public StringField AccountHolderName { get; set; }

        /// <summary>
        /// The International Bank Account Number (IBAN).
        /// </summary>
        [JsonPropertyName("iban")]
        public StringField Iban { get; set; }

        /// <summary>
        /// The bank's SWIFT Business Identifier Code (BIC).
        /// </summary>
        [JsonPropertyName("swift")]
        public StringField Swift { get; set; }

        /// <summary>
        /// A prettier representation of the current model values.
        /// </summary>
        public override string ToString()
        {

            StringBuilder result = new StringBuilder();
            result.Append($":IBAN: {Iban}\n");
            result.Append($":Account Holder's Name: {AccountHolderName}\n");
            result.Append($":SWIFT Code: {Swift}\n");

            return SummaryHelper.Clean(result.ToString());
        }
    }
}
