using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing;
using Mindee.Parsing.Standard;

namespace Mindee.Product.Passport
{
    /// <summary>
    /// Document data for Passport, API version 1.
    /// </summary>
    public class PassportV1Document : IPrediction
    {
        /// <summary>
        /// The date of birth of the passport holder.
        /// </summary>
        [JsonPropertyName("birth_date")]
        public DateField BirthDate { get; set; }

        /// <summary>
        /// The place of birth of the passport holder.
        /// </summary>
        [JsonPropertyName("birth_place")]
        public StringField BirthPlace { get; set; }

        /// <summary>
        /// The country's 3 letter code (ISO 3166-1 alpha-3).
        /// </summary>
        [JsonPropertyName("country")]
        public StringField Country { get; set; }

        /// <summary>
        /// The expiry date of the passport.
        /// </summary>
        [JsonPropertyName("expiry_date")]
        public DateField ExpiryDate { get; set; }

        /// <summary>
        /// The gender of the passport holder.
        /// </summary>
        [JsonPropertyName("gender")]
        public StringField Gender { get; set; }

        /// <summary>
        /// The given name(s) of the passport holder.
        /// </summary>
        [JsonPropertyName("given_names")]
        public IList<StringField> GivenNames { get; set; } = new List<StringField>();

        /// <summary>
        /// The passport's identification number.
        /// </summary>
        [JsonPropertyName("id_number")]
        public StringField IdNumber { get; set; }

        /// <summary>
        /// The date the passport was issued.
        /// </summary>
        [JsonPropertyName("issuance_date")]
        public DateField IssuanceDate { get; set; }

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
        /// The surname of the passport holder.
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
            result.Append($":Country Code: {Country}\n");
            result.Append($":ID Number: {IdNumber}\n");
            result.Append($":Given Name(s): {givenNames}\n");
            result.Append($":Surname: {Surname}\n");
            result.Append($":Date of Birth: {BirthDate}\n");
            result.Append($":Place of Birth: {BirthPlace}\n");
            result.Append($":Gender: {Gender}\n");
            result.Append($":Date of Issue: {IssuanceDate}\n");
            result.Append($":Expiry Date: {ExpiryDate}\n");
            result.Append($":MRZ Line 1: {Mrz1}\n");
            result.Append($":MRZ Line 2: {Mrz2}\n");
            return SummaryHelper.Clean(result.ToString());
        }
    }
}
