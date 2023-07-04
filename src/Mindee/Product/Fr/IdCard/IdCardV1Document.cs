using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing;
using Mindee.Parsing.Standard;

namespace Mindee.Product.Fr.IdCard
{
    /// <summary>
    /// Document data for Carte Nationale d'Identité, API version 1.
    /// </summary>
    public class IdCardV1Document
    {
        /// <summary>
        /// The identification card number.
        /// </summary>
        [JsonPropertyName("id_number")]
        public StringField IdNumber { get; set; }

        /// <summary>
        /// The given name(s) of the card holder.
        /// </summary>
        [JsonPropertyName("given_names")]
        public List<StringField> GivenNames { get; set; }

        /// <summary>
        /// The surname of the card holder.
        /// </summary>
        [JsonPropertyName("surname")]
        public StringField Surname { get; set; }

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
        /// The expiry date of the identification card.
        /// </summary>
        [JsonPropertyName("expiry_date")]
        public DateField ExpiryDate { get; set; }

        /// <summary>
        /// The name of the issuing authority.
        /// </summary>
        [JsonPropertyName("authority")]
        public StringField Authority { get; set; }

        /// <summary>
        /// The gender of the card holder.
        /// </summary>
        [JsonPropertyName("gender")]
        public StringField Gender { get; set; }

        /// <summary>
        /// Machine Readable Zone, first line
        /// </summary>
        [JsonPropertyName("mrz1")]
        public StringField Mrz1 { get; set; }

        /// <summary>
        /// Machine Readable Zone, second line
        /// </summary>
        [JsonPropertyName("mrz2")]
        public StringField Mrz2 { get; set; }

        /// <summary>
        /// A prettier representation of the current model values.
        /// </summary>
        public override string ToString()
        {
            string givenNames = string.Join(
                "\n " + string.Concat(Enumerable.Repeat(" ", 13)),
                GivenNames.Select(item => item));

            StringBuilder result = new StringBuilder();
            result.Append($":Identity Number: {IdNumber}\n");
            result.Append($":Given Name(s): {givenNames}\n");
            result.Append($":Surname: {Surname}\n");
            result.Append($":Date of Birth: {BirthDate}\n");
            result.Append($":Place of Birth: {BirthPlace}\n");
            result.Append($":Expiry Date: {ExpiryDate}\n");
            result.Append($":Issuing Authority: {Authority}\n");
            result.Append($":Gender: {Gender}\n");
            result.Append($":MRZ Line 1: {Mrz1}\n");
            result.Append($":MRZ Line 2: {Mrz2}\n");

            return SummaryHelper.Clean(result.ToString());
        }
    }
}
