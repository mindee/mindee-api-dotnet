using System.Collections.Generic;
using System.Text.Json.Serialization;
using Mindee.Domain.Parsing.Common;

namespace Mindee.Domain.Parsing.Passport
{
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
    }
}
