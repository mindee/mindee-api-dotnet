using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing;
using Mindee.Parsing.Standard;

namespace Mindee.Product.Us.PayrollCheckRegister
{
    /// <summary>
    ///     Document data for Payroll Check Register, API version 1.
    /// </summary>
    public class PayrollCheckRegisterV1Document : IPrediction
    {
        /// <summary>
        ///     The name of the company.
        /// </summary>
        [JsonPropertyName("company_name")]
        public StringField CompanyName { get; set; }

        /// <summary>
        ///     The date on which the payment was made.
        /// </summary>
        [JsonPropertyName("pay_date")]
        public DateField PayDate { get; set; }

        /// <summary>
        ///     List of payments.
        /// </summary>
        [JsonPropertyName("payments")]
        [JsonConverter(typeof(ObjectListJsonConverter<PayrollCheckRegisterV1Payments, PayrollCheckRegisterV1Payment>))]
        public PayrollCheckRegisterV1Payments Payments { get; set; }

        /// <summary>
        ///     The date at which the period ends.
        /// </summary>
        [JsonPropertyName("period_end")]
        public DateField PeriodEnd { get; set; }

        /// <summary>
        ///     The date at which the period starts.
        /// </summary>
        [JsonPropertyName("period_start")]
        public DateField PeriodStart { get; set; }

        /// <summary>
        ///     A prettier representation of the current model values.
        /// </summary>
        public override string ToString()
        {
            var result = new StringBuilder();
            result.Append($":Company Name: {CompanyName}\n");
            result.Append($":Period Start: {PeriodStart}\n");
            result.Append($":Period End: {PeriodEnd}\n");
            result.Append($":Pay Date: {PayDate}\n");
            result.Append($":Payments:{Payments}");
            return SummaryHelper.Clean(result.ToString());
        }
    }
}
