using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing;
using Mindee.Parsing.Standard;

namespace Mindee.Product.Fr.Payslip
{
    /// <summary>
    /// Payslip API version 3.0 document data.
    /// </summary>
    public class PayslipV3Document : IPrediction
    {
        /// <summary>
        /// Information about the employee's bank account.
        /// </summary>
        [JsonPropertyName("bank_account_details")]
        public PayslipV3BankAccountDetail BankAccountDetails { get; set; }

        /// <summary>
        /// Information about the employee.
        /// </summary>
        [JsonPropertyName("employee")]
        public PayslipV3Employee Employee { get; set; }

        /// <summary>
        /// Information about the employer.
        /// </summary>
        [JsonPropertyName("employer")]
        public PayslipV3Employer Employer { get; set; }

        /// <summary>
        /// Information about the employment.
        /// </summary>
        [JsonPropertyName("employment")]
        public PayslipV3Employment Employment { get; set; }

        /// <summary>
        /// Information about paid time off.
        /// </summary>
        [JsonPropertyName("paid_time_off")]
        [JsonConverter(typeof(ObjectListJsonConverter<PayslipV3PaidTimeOffs, PayslipV3PaidTimeOff>))]
        public PayslipV3PaidTimeOffs PaidTimeOff { get; set; }

        /// <summary>
        /// Detailed information about the pay.
        /// </summary>
        [JsonPropertyName("pay_detail")]
        public PayslipV3PayDetail PayDetail { get; set; }

        /// <summary>
        /// Information about the pay period.
        /// </summary>
        [JsonPropertyName("pay_period")]
        public PayslipV3PayPeriod PayPeriod { get; set; }

        /// <summary>
        /// Detailed information about the earnings.
        /// </summary>
        [JsonPropertyName("salary_details")]
        [JsonConverter(typeof(ObjectListJsonConverter<PayslipV3SalaryDetails, PayslipV3SalaryDetail>))]
        public PayslipV3SalaryDetails SalaryDetails { get; set; }

        /// <summary>
        /// A prettier representation of the current model values.
        /// </summary>
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.Append($":Pay Period:{PayPeriod.ToFieldList()}");
            result.Append($":Employee:{Employee.ToFieldList()}");
            result.Append($":Employer:{Employer.ToFieldList()}");
            result.Append($":Bank Account Details:{BankAccountDetails.ToFieldList()}");
            result.Append($":Employment:{Employment.ToFieldList()}");
            result.Append($":Salary Details:{SalaryDetails}");
            result.Append($":Pay Detail:{PayDetail.ToFieldList()}");
            result.Append($":Paid Time Off:{PaidTimeOff}");
            return SummaryHelper.Clean(result.ToString());
        }
    }
}
