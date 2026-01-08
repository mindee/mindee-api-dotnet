using System.Text.Json.Serialization;
using Mindee.Geometry;

namespace Mindee.Parsing.Standard
{
    /// <summary>
    ///     Represent a postal address broken down into its individual components,
    ///     while still exposing the complete address string via <see cref="StringField.Value" />.
    /// </summary>
    public class AddressField : StringField
    {
        /// <summary>
        ///     Complete constructor used by the JSON deserializer and by advanced callers.
        /// </summary>
        /// <param name="value">
        ///     <see cref="StringField.Value" />
        /// </param>
        /// <param name="rawValue">
        ///     <see cref="StringField.RawValue" />
        /// </param>
        /// <param name="streetNumber">
        ///     <see cref="StreetNumber" />
        /// </param>
        /// <param name="streetName">
        ///     <see cref="StreetName" />
        /// </param>
        /// <param name="poBox">
        ///     <see cref="PoBox" />
        /// </param>
        /// <param name="addressComplement">
        ///     <see cref="AddressComplement" />
        /// </param>
        /// <param name="city">
        ///     <see cref="City" />
        /// </param>
        /// <param name="postalCode">
        ///     <see cref="PostalCode" />
        /// </param>
        /// <param name="state">
        ///     <see cref="State" />
        /// </param>
        /// <param name="country">
        ///     <see cref="Country" />
        /// </param>
        /// <param name="confidence">
        ///     <see cref="BaseField.Confidence" />
        /// </param>
        /// <param name="polygon">
        ///     <see cref="BaseField.Polygon" />
        /// </param>
        /// <param name="pageId">
        ///     <see cref="BaseField.PageId" />
        /// </param>
        [JsonConstructor]
        public AddressField(
            string value,
            string rawValue,
            string streetNumber,
            string streetName,
            string poBox,
            string addressComplement,
            string city,
            string postalCode,
            string state,
            string country,
            double? confidence,
            Polygon polygon,
            int? pageId = null
        ) : base(value, rawValue, confidence, polygon, pageId)
        {
            StreetNumber = string.IsNullOrEmpty(streetNumber) ? null : streetNumber;
            StreetName = string.IsNullOrEmpty(streetName) ? null : streetName;
            PoBox = string.IsNullOrEmpty(poBox) ? null : poBox;
            AddressComplement = string.IsNullOrEmpty(addressComplement) ? null : addressComplement;
            City = string.IsNullOrEmpty(city) ? null : city;
            PostalCode = string.IsNullOrEmpty(postalCode) ? null : postalCode;
            State = string.IsNullOrEmpty(state) ? null : state;
            Country = string.IsNullOrEmpty(country) ? null : country;
        }

        /// <summary>
        ///     Regular constructor.
        /// </summary>
        /// <param name="value">
        ///     <see cref="StringField.Value" />
        /// </param>
        /// <param name="confidence">
        ///     <see cref="BaseField.Confidence" />
        /// </param>
        /// <param name="polygon">
        ///     <see cref="BaseField.Polygon" />
        /// </param>
        /// <param name="pageId">
        ///     <see cref="BaseField.PageId" />
        /// </param>
        public AddressField(
            string value,
            double? confidence,
            Polygon polygon,
            int? pageId = null
        ) : this(
            value,
            null,
            null, null, null, null, null, null, null, null,
            confidence,
            polygon,
            pageId
        )
        {
        }

        /// <summary>
        ///     Street number.
        /// </summary>
        [JsonPropertyName("street_number")]
        public string StreetNumber { get; set; }

        /// <summary>
        ///     Street name.
        /// </summary>
        [JsonPropertyName("street_name")]
        public string StreetName { get; set; }

        /// <summary>
        ///     PO-box number.
        /// </summary>
        [JsonPropertyName("po_box")]
        public string PoBox { get; set; }

        /// <summary>
        ///     Additional address complement.
        /// </summary>
        [JsonPropertyName("address_complement")]
        public string AddressComplement { get; set; }

        /// <summary>
        ///     City or locality.
        /// </summary>
        [JsonPropertyName("city")]
        public string City { get; set; }

        /// <summary>
        ///     Postal / ZIP code.
        /// </summary>
        [JsonPropertyName("postal_code")]
        public string PostalCode { get; set; }

        /// <summary>
        ///     State, province or region.
        /// </summary>
        [JsonPropertyName("state")]
        public string State { get; set; }

        /// <summary>
        ///     Country.
        /// </summary>
        [JsonPropertyName("country")]
        public string Country { get; set; }
    }
}
