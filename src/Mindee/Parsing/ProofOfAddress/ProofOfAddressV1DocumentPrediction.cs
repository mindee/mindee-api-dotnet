using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing.Common;

namespace Mindee.Parsing.ProofOfAddress
{
    /// <summary>
    /// The proof of address model for the v1.
    /// </summary>
    public sealed class ProofOfAddressV1DocumentPrediction : PredictionBase
    {
        /// <summary>
        /// ISO date yyyy-mm-dd. Works both for European and US dates.
        /// </summary>
        [JsonPropertyName("date")]
        public DateField IssuanceDate { get; set; }

        /// <summary>
        /// All extracted ISO date yyyy-mm-dd. Works both for European and US dates.
        /// </summary>
        [JsonPropertyName("dates")]
        public List<DateField> Dates { get; set; } = new List<DateField>();

        /// <summary>
        /// Name of the document's issuer.
        /// </summary>
        [JsonPropertyName("issuer_name")]
        public StringField IssuerName { get; set; }

        /// <summary>
        /// Address of the document's issuer.
        /// </summary>
        [JsonPropertyName("issuer_address")]
        public StringField IssuerAddress { get; set; }

        /// <summary>
        /// Generic: VAT NUMBER, TAX ID, COMPANY REGISTRATION NUMBER or country specific.
        /// </summary>
        [JsonPropertyName("issuer_company_registration")]
        public List<CompanyRegistration> IssuerCompanyRegistrations { get; set; } = new List<CompanyRegistration>();

        /// <summary>
        /// Name of the document's recipient.
        /// </summary>
        [JsonPropertyName("recipient_name")]
        public StringField RecipientName { get; set; }

        /// <summary>
        /// Address of the document's recipient.
        /// </summary>
        [JsonPropertyName("recipient_address")]
        public StringField RecipientAddress { get; set; }

        /// <summary>
        /// Generic: VAT NUMBER, TAX ID, COMPANY REGISTRATION NUMBER or country specific.
        /// </summary>
        [JsonPropertyName("recipient_company_registration")]
        public List<CompanyRegistration> RecipientCompanyRegistrations { get; set; } = new List<CompanyRegistration>();

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            result.Append($":Locale: {Locale}\n");
            result.Append($":Issuer name: {IssuerName.Value}\n");
            result.Append($":Issuer Address: {IssuerAddress.Value}\n");
            result.Append($":Issuer Company Registrations: {string.Join("; ", IssuerCompanyRegistrations.Select(c => c.Value))}\n");
            result.Append($":Recipient name: {RecipientName.Value}\n");
            result.Append($":Recipient Address: {RecipientAddress.Value}\n");
            result.Append($":Recipient Company Registrations: {string.Join("; ", RecipientCompanyRegistrations.Select(c => c.Value))}\n");
            result.Append($":Issuance date: {IssuanceDate.Value}\n");
            result.Append($":Dates: {string.Join("\n        ", Dates.Select(c => c.Value))}\n");

            return SummaryHelper.Clean(result.ToString());
        }
    }
}
