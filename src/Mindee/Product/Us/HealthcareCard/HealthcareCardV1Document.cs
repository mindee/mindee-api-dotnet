using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing;
using Mindee.Parsing.Standard;

namespace Mindee.Product.Us.HealthcareCard
{
    /// <summary>
    /// Healthcare Card API version 1.0 document data.
    /// </summary>
    public class HealthcareCardV1Document : IPrediction
    {
        /// <summary>
        /// The name of the company that provides the healthcare plan.
        /// </summary>
        [JsonPropertyName("company_name")]
        public StringField CompanyName { get; set; }

        /// <summary>
        /// Is a fixed amount for a covered service.
        /// </summary>
        [JsonPropertyName("copays")]
        [JsonConverter(typeof(ObjectListJsonConverter<HealthcareCardV1Copays, HealthcareCardV1Copay>))]
        public HealthcareCardV1Copays Copays { get; set; }

        /// <summary>
        /// The list of dependents covered by the healthcare plan.
        /// </summary>
        [JsonPropertyName("dependents")]
        public IList<StringField> Dependents { get; set; } = new List<StringField>();

        /// <summary>
        /// The date when the member enrolled in the healthcare plan.
        /// </summary>
        [JsonPropertyName("enrollment_date")]
        public DateField EnrollmentDate { get; set; }

        /// <summary>
        /// The group number associated with the healthcare plan.
        /// </summary>
        [JsonPropertyName("group_number")]
        public StringField GroupNumber { get; set; }

        /// <summary>
        /// The organization that issued the healthcare plan.
        /// </summary>
        [JsonPropertyName("issuer_80840")]
        public StringField Issuer80840 { get; set; }

        /// <summary>
        /// The unique identifier for the member in the healthcare system.
        /// </summary>
        [JsonPropertyName("member_id")]
        public StringField MemberId { get; set; }

        /// <summary>
        /// The name of the member covered by the healthcare plan.
        /// </summary>
        [JsonPropertyName("member_name")]
        public StringField MemberName { get; set; }

        /// <summary>
        /// The unique identifier for the payer in the healthcare system.
        /// </summary>
        [JsonPropertyName("payer_id")]
        public StringField PayerId { get; set; }

        /// <summary>
        /// The BIN number for prescription drug coverage.
        /// </summary>
        [JsonPropertyName("rx_bin")]
        public StringField RxBin { get; set; }

        /// <summary>
        /// The group number for prescription drug coverage.
        /// </summary>
        [JsonPropertyName("rx_grp")]
        public StringField RxGrp { get; set; }

        /// <summary>
        /// The PCN number for prescription drug coverage.
        /// </summary>
        [JsonPropertyName("rx_pcn")]
        public StringField RxPcn { get; set; }

        /// <summary>
        /// A prettier representation of the current model values.
        /// </summary>
        public override string ToString()
        {
            string dependents = string.Join(
                "\n " + string.Concat(Enumerable.Repeat(" ", 12)),
                Dependents.Select(item => item));
            StringBuilder result = new StringBuilder();
            result.Append($":Company Name: {CompanyName}\n");
            result.Append($":Member Name: {MemberName}\n");
            result.Append($":Member ID: {MemberId}\n");
            result.Append($":Issuer 80840: {Issuer80840}\n");
            result.Append($":Dependents: {dependents}\n");
            result.Append($":Group Number: {GroupNumber}\n");
            result.Append($":Payer ID: {PayerId}\n");
            result.Append($":RX BIN: {RxBin}\n");
            result.Append($":RX GRP: {RxGrp}\n");
            result.Append($":RX PCN: {RxPcn}\n");
            result.Append($":copays:{Copays}");
            result.Append($":Enrollment Date: {EnrollmentDate}\n");
            return SummaryHelper.Clean(result.ToString());
        }
    }
}
