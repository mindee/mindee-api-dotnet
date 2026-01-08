using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing;

namespace Mindee.Product.Fr.Payslip
{
    /// <summary>
    ///     Payslip API version 2.0 document data.
    /// </summary>
    public class PayslipV2Document : IPrediction
    {
        /// <summary>
        ///     Information about the employee's bank account.
        /// </summary>
        [JsonPropertyName("bank_account_details")]
        public PayslipV2BankAccountDetail BankAccountDetails { get; set; }

        /// <summary>
        ///     Information about the employee.
        /// </summary>
        [JsonPropertyName("employee")]
        public PayslipV2Employee Employee { get; set; }

        /// <summary>
        ///     Information about the employer.
        /// </summary>
        [JsonPropertyName("employer")]
        public PayslipV2Employer Employer { get; set; }

        /// <summary>
        ///     Information about the employment.
        /// </summary>
        [JsonPropertyName("employment")]
        public PayslipV2Employment Employment { get; set; }

        /// <summary>
        ///     Detailed information about the pay.
        /// </summary>
        [JsonPropertyName("pay_detail")]
        public PayslipV2PayDetail PayDetail { get; set; }

        /// <summary>
        ///     Information about the pay period.
        /// </summary>
        [JsonPropertyName("pay_period")]
        public PayslipV2PayPeriod PayPeriod { get; set; }

        /// <summary>
        ///     Information about paid time off.
        /// </summary>
        [JsonPropertyName("pto")]
        public PayslipV2Pto Pto { get; set; }

        /// <summary>
        ///     Detailed information about the earnings.
        /// </summary>
        [JsonPropertyName("salary_details")]
        [JsonConverter(typeof(ObjectListJsonConverter<PayslipV2SalaryDetails, PayslipV2SalaryDetail>))]
        public PayslipV2SalaryDetails SalaryDetails { get; set; }

        /// <summary>
        ///     A prettier representation of the current model values.
        /// </summary>
        public override string ToString()
        {
            var result = new StringBuilder();
            result.Append($":Employee:{Employee.ToFieldList()}");
            result.Append($":Employer:{Employer.ToFieldList()}");
            result.Append($":Bank Account Details:{BankAccountDetails.ToFieldList()}");
            result.Append($":Employment:{Employment.ToFieldList()}");
            result.Append($":Salary Details:{SalaryDetails}");
            result.Append($":Pay Detail:{PayDetail.ToFieldList()}");
            result.Append($":PTO:{Pto.ToFieldList()}");
            result.Append($":Pay Period:{PayPeriod.ToFieldList()}");
            return SummaryHelper.Clean(result.ToString());
        }
    }
}
