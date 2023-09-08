using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing;
using Mindee.Parsing.Standard;

namespace Mindee.Product.ProofOfAddress
{
    /// <summary>
    /// Document data for Proof of Address, API version 1.
    /// </summary>
    public class ProofOfAddressV1Document : IPrediction
    {
        /// <summary>
        /// The date the document was issued.
        /// </summary>
        [JsonPropertyName("date")]
        public DateField Date { get; set; }

        /// <summary>
        /// List of dates found on the document.
        /// </summary>
        [JsonPropertyName("dates")]
        public IList<DateField> Dates { get; set; } = new List<DateField>();

        /// <summary>
        /// The address of the document's issuer.
        /// </summary>
        [JsonPropertyName("issuer_address")]
        public StringField IssuerAddress { get; set; }

        /// <summary>
        /// List of company registrations found for the issuer.
        /// </summary>
        [JsonPropertyName("issuer_company_registration")]
        public IList<CompanyRegistration> IssuerCompanyRegistration { get; set; } = new List<CompanyRegistration>();

        /// <summary>
        /// The name of the person or company issuing the document.
        /// </summary>
        [JsonPropertyName("issuer_name")]
        public StringField IssuerName { get; set; }

        /// <summary>
        /// The locale detected on the document.
        /// </summary>
        [JsonPropertyName("locale")]
        public Locale Locale { get; set; }

        /// <summary>
        /// The address of the recipient.
        /// </summary>
        [JsonPropertyName("recipient_address")]
        public StringField RecipientAddress { get; set; }

        /// <summary>
        /// List of company registrations found for the recipient.
        /// </summary>
        [JsonPropertyName("recipient_company_registration")]
        public IList<CompanyRegistration> RecipientCompanyRegistration { get; set; } = new List<CompanyRegistration>();

        /// <summary>
        /// The name of the person or company receiving the document.
        /// </summary>
        [JsonPropertyName("recipient_name")]
        public StringField RecipientName { get; set; }

        /// <summary>
        /// A prettier representation of the current model values.
        /// </summary>
        public override string ToString()
        {
            string issuerCompanyRegistration = string.Join(
                "\n " + string.Concat(Enumerable.Repeat(" ", 30)),
                IssuerCompanyRegistration.Select(item => item));
            string recipientCompanyRegistration = string.Join(
                "\n " + string.Concat(Enumerable.Repeat(" ", 33)),
                RecipientCompanyRegistration.Select(item => item));
            string dates = string.Join(
                "\n " + string.Concat(Enumerable.Repeat(" ", 7)),
                Dates.Select(item => item));
            StringBuilder result = new StringBuilder();
            result.Append($":Locale: {Locale}\n");
            result.Append($":Issuer Name: {IssuerName}\n");
            result.Append($":Issuer Company Registrations: {issuerCompanyRegistration}\n");
            result.Append($":Issuer Address: {IssuerAddress}\n");
            result.Append($":Recipient Name: {RecipientName}\n");
            result.Append($":Recipient Company Registrations: {recipientCompanyRegistration}\n");
            result.Append($":Recipient Address: {RecipientAddress}\n");
            result.Append($":Dates: {dates}\n");
            result.Append($":Date of Issue: {Date}\n");
            return SummaryHelper.Clean(result.ToString());
        }
    }
}
