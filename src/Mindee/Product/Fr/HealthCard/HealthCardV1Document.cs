using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing;
using Mindee.Parsing.Standard;

namespace Mindee.Product.Fr.HealthCard
{
    /// <summary>
    /// Health Card API version 1.0 document data.
    /// </summary>
    public class HealthCardV1Document : IPrediction
    {
        /// <summary>
        /// The given names of the card holder.
        /// </summary>
        [JsonPropertyName("given_names")]
        public IList<StringField> GivenNames { get; set; } = new List<StringField>();

        /// <summary>
        /// The date when the carte vitale document was issued.
        /// </summary>
        [JsonPropertyName("issuance_date")]
        public DateField IssuanceDate { get; set; }

        /// <summary>
        /// The social security number of the card holder.
        /// </summary>
        [JsonPropertyName("social_security")]
        public StringField SocialSecurity { get; set; }

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
            result.Append($":Given Name(s): {givenNames}\n");
            result.Append($":Surname: {Surname}\n");
            result.Append($":Social Security Number: {SocialSecurity}\n");
            result.Append($":Issuance Date: {IssuanceDate}\n");
            return SummaryHelper.Clean(result.ToString());
        }
    }
}
