using System.Collections.Generic;
using System.Text.Json.Serialization;
using Mindee.Infrastructure.Api.Commun;

namespace Mindee.Infrastructure.Api.Passport
{
    internal class PassportPrediction : PredictionBase
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

    public class BirthDate
    {
        [JsonPropertyName("confidence")]
        public double Confidence { get; set; }

        [JsonPropertyName("polygon")]
        public List<List<double>> Polygon { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }

        [JsonPropertyName("page_id")]
        public int PageId { get; set; }
    }

    public class BirthPlace
    {
        [JsonPropertyName("confidence")]
        public double Confidence { get; set; }

        [JsonPropertyName("polygon")]
        public List<List<double>> Polygon { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }

        [JsonPropertyName("page_id")]
        public int PageId { get; set; }
    }

    public class Country
    {
        [JsonPropertyName("confidence")]
        public double Confidence { get; set; }

        [JsonPropertyName("polygon")]
        public List<List<double>> Polygon { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }

        [JsonPropertyName("page_id")]
        public int PageId { get; set; }
    }

    public class ExpiryDate
    {
        [JsonPropertyName("confidence")]
        public double Confidence { get; set; }

        [JsonPropertyName("polygon")]
        public List<List<double>> Polygon { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }

        [JsonPropertyName("page_id")]
        public int PageId { get; set; }
    }

    public class Gender
    {
        [JsonPropertyName("confidence")]
        public double Confidence { get; set; }

        [JsonPropertyName("polygon")]
        public List<List<double>> Polygon { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }

        [JsonPropertyName("page_id")]
        public int PageId { get; set; }
    }

    public class GivenName
    {
        [JsonPropertyName("confidence")]
        public double Confidence { get; set; }

        [JsonPropertyName("polygon")]
        public List<List<double>> Polygon { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }

        [JsonPropertyName("page_id")]
        public int PageId { get; set; }
    }

    public class IdNumber
    {
        [JsonPropertyName("confidence")]
        public double Confidence { get; set; }

        [JsonPropertyName("polygon")]
        public List<List<double>> Polygon { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }

        [JsonPropertyName("page_id")]
        public int PageId { get; set; }
    }

    public class IssuanceDate
    {
        [JsonPropertyName("confidence")]
        public double Confidence { get; set; }

        [JsonPropertyName("polygon")]
        public List<List<double>> Polygon { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }

        [JsonPropertyName("page_id")]
        public int PageId { get; set; }
    }

    public class Mrz1
    {
        [JsonPropertyName("confidence")]
        public double Confidence { get; set; }

        [JsonPropertyName("polygon")]
        public List<List<double>> Polygon { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }

        [JsonPropertyName("page_id")]
        public int PageId { get; set; }
    }

    public class Mrz2
    {
        [JsonPropertyName("confidence")]
        public double Confidence { get; set; }

        [JsonPropertyName("polygon")]
        public List<List<double>> Polygon { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }

        [JsonPropertyName("page_id")]
        public int PageId { get; set; }
    }

    public class Surname
    {
        [JsonPropertyName("confidence")]
        public double Confidence { get; set; }

        [JsonPropertyName("polygon")]
        public List<List<double>> Polygon { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }

        [JsonPropertyName("page_id")]
        public int PageId { get; set; }
    }
}
