using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing.Common;

namespace Mindee.Parsing.Passport
{
    [Endpoint("passport", "1")]
    public sealed class PassportPrediction : PredictionBase
    {
        [JsonPropertyName("birth_date")]
        public BirthDate BirthDate { get; set; }

        [JsonPropertyName("birth_place")]
        public BirthPlace BirthPlace { get; set; }

        [JsonPropertyName("country")]
        public Country Country { get; set; }

        [JsonPropertyName("expiry_date")]
        public ExpiryDate ExpiryDate { get; set; }

        [JsonPropertyName("gender")]
        public Gender Gender { get; set; }

        [JsonPropertyName("given_names")]
        public List<GivenName> GivenNames { get; set; }

        [JsonPropertyName("id_number")]
        public IdNumber IdNumber { get; set; }

        [JsonPropertyName("issuance_date")]
        public IssuanceDate IssuanceDate { get; set; }

        [JsonPropertyName("mrz1")]
        public Mrz1 Mrz1 { get; set; }

        [JsonPropertyName("mrz2")]
        public Mrz2 Mrz2 { get; set; }

        [JsonPropertyName("surname")]
        public Surname Surname { get; set; }

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
