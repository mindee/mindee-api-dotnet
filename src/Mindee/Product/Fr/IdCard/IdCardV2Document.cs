using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing;
using Mindee.Parsing.Standard;

namespace Mindee.Product.Fr.IdCard
{
    /// <summary>
    /// Carte Nationale d'Identité API version 2.0 document data.
    /// </summary>
    public class IdCardV2Document : IPrediction
    {
        /// <summary>
        /// The alternate name of the card holder.
        /// </summary>
        [JsonPropertyName("alternate_name")]
        public StringField AlternateName { get; set; }

        /// <summary>
        /// The name of the issuing authority.
        /// </summary>
        [JsonPropertyName("authority")]
        public StringField Authority { get; set; }

        /// <summary>
        /// The date of birth of the card holder.
        /// </summary>
        [JsonPropertyName("birth_date")]
        public DateField BirthDate { get; set; }

        /// <summary>
        /// The place of birth of the card holder.
        /// </summary>
        [JsonPropertyName("birth_place")]
        public StringField BirthPlace { get; set; }

        /// <summary>
        /// The card access number (CAN).
        /// </summary>
        [JsonPropertyName("card_access_number")]
        public StringField CardAccessNumber { get; set; }

        /// <summary>
        /// The document number.
        /// </summary>
        [JsonPropertyName("document_number")]
        public StringField DocumentNumber { get; set; }

        /// <summary>
        /// The expiry date of the identification card.
        /// </summary>
        [JsonPropertyName("expiry_date")]
        public DateField ExpiryDate { get; set; }

        /// <summary>
        /// The gender of the card holder.
        /// </summary>
        [JsonPropertyName("gender")]
        public StringField Gender { get; set; }

        /// <summary>
        /// The given name(s) of the card holder.
        /// </summary>
        [JsonPropertyName("given_names")]
        public IList<StringField> GivenNames { get; set; } = new List<StringField>();

        /// <summary>
        /// The date of issue of the identification card.
        /// </summary>
        [JsonPropertyName("issue_date")]
        public DateField IssueDate { get; set; }

        /// <summary>
        /// The Machine Readable Zone, first line.
        /// </summary>
        [JsonPropertyName("mrz1")]
        public StringField Mrz1 { get; set; }

        /// <summary>
        /// The Machine Readable Zone, second line.
        /// </summary>
        [JsonPropertyName("mrz2")]
        public StringField Mrz2 { get; set; }

        /// <summary>
        /// The Machine Readable Zone, third line.
        /// </summary>
        [JsonPropertyName("mrz3")]
        public StringField Mrz3 { get; set; }

        /// <summary>
        /// The nationality of the card holder.
        /// </summary>
        [JsonPropertyName("nationality")]
        public StringField Nationality { get; set; }

        /// <summary>
        /// The surname of the card holder.
        /// </summary>
        [JsonPropertyName("surname")]
        public StringField Surname { get; set; }

        /// <summary>
        /// A prettier representation of the current model values.
        /// </summary>
        public override string ToString()
        {
            string givenNames = string.Join(
                "\n " + string.Concat(Enumerable.Repeat(" ", 15)),
                GivenNames.Select(item => item));
            StringBuilder result = new StringBuilder();
            result.Append($":Nationality: {Nationality}\n");
            result.Append($":Card Access Number: {CardAccessNumber}\n");
            result.Append($":Document Number: {DocumentNumber}\n");
            result.Append($":Given Name(s): {givenNames}\n");
            result.Append($":Surname: {Surname}\n");
            result.Append($":Alternate Name: {AlternateName}\n");
            result.Append($":Date of Birth: {BirthDate}\n");
            result.Append($":Place of Birth: {BirthPlace}\n");
            result.Append($":Gender: {Gender}\n");
            result.Append($":Expiry Date: {ExpiryDate}\n");
            result.Append($":Mrz Line 1: {Mrz1}\n");
            result.Append($":Mrz Line 2: {Mrz2}\n");
            result.Append($":Mrz Line 3: {Mrz3}\n");
            result.Append($":Date of Issue: {IssueDate}\n");
            result.Append($":Issuing Authority: {Authority}\n");
            return SummaryHelper.Clean(result.ToString());
        }
    }
}
