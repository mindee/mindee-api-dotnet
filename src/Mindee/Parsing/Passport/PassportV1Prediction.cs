using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing.Common;

namespace Mindee.Parsing.Passport
{
    /// <summary>
    /// The passport model for the v1.
    /// </summary>
    [Endpoint("passport", "1")]
    public sealed class PassportV1Prediction : PredictionBase
    {
        /// <summary>
        /// The birth date of the person.
        /// </summary>
        [JsonPropertyName("birth_date")]
        public StringField BirthDate { get; set; }

        /// <summary>
        /// The birth place  of the person
        /// </summary>
        [JsonPropertyName("birth_place")]
        public StringField BirthPlace { get; set; }

        /// <summary>
        /// The country.
        /// </summary>
        [JsonPropertyName("country")]
        public StringField Country { get; set; }

        /// <summary>
        /// The expiry date.
        /// </summary>
        [JsonPropertyName("expiry_date")]
        public StringField ExpiryDate { get; set; }

        /// <summary>
        /// The gender.
        /// </summary>
        [JsonPropertyName("gender")]
        public StringField Gender { get; set; }

        /// <summary>
        /// The list of the person given names.
        /// </summary>
        [JsonPropertyName("given_names")]
        public List<StringField> GivenNames { get; set; }

        /// <summary>
        /// The id of the passport.
        /// </summary>
        [JsonPropertyName("id_number")]
        public StringField IdNumber { get; set; }

        /// <summary>
        /// The date of issuance of the passport.
        /// </summary>
        [JsonPropertyName("issuance_date")]
        public StringField IssuanceDate { get; set; }

        /// <summary>
        /// The first MRZ line.
        /// </summary>
        [JsonPropertyName("mrz1")]
        public StringField Mrz1 { get; set; }

        /// <summary>
        ///The second MRZ line.
        /// </summary>
        [JsonPropertyName("mrz2")]
        public StringField Mrz2 { get; set; }

        /// <summary>
        /// Combine the MRZ lines.
        /// </summary>
        public string Mrz => $"{Mrz1.Value}{Mrz2.Value}";

        /// <summary>
        /// The surname of the person.
        /// </summary>
        [JsonPropertyName("surname")]
        public StringField Surname { get; set; }

        /// <summary>
        /// The full name.
        /// </summary>
        public string FullName => $"{string.Join(" ", GivenNames.Select(gn => gn.Value))} {Surname.Value}";

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder result = new StringBuilder("----- Passport V1 -----\n");
            result.Append($"Full name: {FullName}\n");
            result.Append($"Given names: {string.Join(" ", GivenNames.Select(gn => gn.Value))}\n");
            result.Append($"Surname: {Surname.Value}\n");
            result.Append($"Country: {Country.Value}\n");
            result.Append($"ID Number: {IdNumber.Value}\n");
            result.Append($"Issuance date: {IssuanceDate.Value}\n");
            result.Append($"Birth date: {BirthDate.Value}\n");
            result.Append($"Expiry date: {ExpiryDate.Value}\n");
            result.Append($"MRZ 1: {Mrz1.Value}\n");
            result.Append($"MRZ 2: {Mrz2.Value}\n");
            result.Append($"MRZ: {Mrz}\n");

            result.Append("----------------------\n");

            return SummaryHelper.Clean(result.ToString());
        }
    }
}
