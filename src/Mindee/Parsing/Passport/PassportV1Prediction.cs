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
        /// <see cref="BirthDate"/>
        /// </summary>
        [JsonPropertyName("birth_date")]
        public BirthDate BirthDate { get; set; }

        /// <summary>
        /// <see cref="BirthPlace"/>
        /// </summary>
        [JsonPropertyName("birth_place")]
        public BirthPlace BirthPlace { get; set; }

        /// <summary>
        /// <see cref="Country"/>
        /// </summary>
        [JsonPropertyName("country")]
        public Country Country { get; set; }

        /// <summary>
        /// <see cref="ExpiryDate"/>
        /// </summary>
        [JsonPropertyName("expiry_date")]
        public ExpiryDate ExpiryDate { get; set; }

        /// <summary>
        /// <see cref="Gender"/>
        /// </summary>
        [JsonPropertyName("gender")]
        public Gender Gender { get; set; }

        /// <summary>
        /// <see cref="GivenName"/>
        /// </summary>
        [JsonPropertyName("given_names")]
        public List<GivenName> GivenNames { get; set; }

        /// <summary>
        /// <see cref="IdNumber"/>
        /// </summary>
        [JsonPropertyName("id_number")]
        public IdNumber IdNumber { get; set; }

        /// <summary>
        /// <see cref="IssuanceDate"/>
        /// </summary>
        [JsonPropertyName("issuance_date")]
        public IssuanceDate IssuanceDate { get; set; }

        /// <summary>
        /// <see cref="Mrz1"/>
        /// </summary>
        [JsonPropertyName("mrz1")]
        public Mrz1 Mrz1 { get; set; }

        /// <summary>
        /// <see cref="Mrz2"/>
        /// </summary>
        [JsonPropertyName("mrz2")]
        public Mrz2 Mrz2 { get; set; }

        /// <summary>
        /// <see cref="Surname"/>
        /// </summary>
        [JsonPropertyName("surname")]
        public Surname Surname { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder result = new StringBuilder("-----Passport data-----\n");
            result.Append($"Given names: {string.Join(" ", GivenNames.Select(gn => gn.Value))}\n");
            result.Append($"Surname: {Surname.Value}\n");
            result.Append($"Country: {Country.Value}\n");
            result.Append($"ID Number: {IdNumber.Value}\n");
            result.Append($"Issuance date: {IssuanceDate.Value}\n");
            result.Append($"Birth date: {BirthDate.Value}\n");
            result.Append($"Expiry date: {ExpiryDate.Value}\n");
            result.Append($"MRZ 1: {Mrz1.Value}\n");
            result.Append($"MRZ 2: {Mrz2.Value}\n");

            result.Append("----------------------");

            return result.ToString();
        }
    }
}
