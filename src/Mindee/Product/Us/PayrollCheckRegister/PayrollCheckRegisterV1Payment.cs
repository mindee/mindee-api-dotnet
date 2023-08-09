using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing;
using Mindee.Parsing.Standard;

namespace Mindee.Product.Us.PayrollCheckRegister
{
    /// <summary>
    /// List of payments.
    /// </summary>
    public sealed class PayrollCheckRegisterV1Payment : ILineItemField
    {
        /// <summary>
        /// The deductions.
        /// </summary>
        [JsonPropertyName("deductions")]
        [JsonConverter(typeof(ObjectListJsonConverter<PayrollCheckRegisterV1Deductions, PayrollCheckRegisterV1Deduction>))]
        public PayrollCheckRegisterV1Deductions Deductions { get; set; }

        /// <summary>
        /// The earnings.
        /// </summary>
        [JsonPropertyName("earnings")]
        [JsonConverter(typeof(ObjectListJsonConverter<PayrollCheckRegisterV1Earnings, PayrollCheckRegisterV1Earning>))]
        public PayrollCheckRegisterV1Earnings Earnings { get; set; }

        /// <summary>
        /// The full name of the employee.
        /// </summary>
        [JsonPropertyName("employee_name")]
        public StringField EmployeeName { get; set; }

        /// <summary>
        /// The employee code or number.
        /// </summary>
        [JsonPropertyName("employee_number")]
        public StringField EmployeeNumber { get; set; }

        /// <summary>
        /// The net pay amount.
        /// </summary>
        [JsonPropertyName("net_pay")]
        public DecimalField NetPay { get; set; }

        /// <summary>
        /// The pay date in ISO format (YYYY-MM-DD).
        /// </summary>
        [JsonPropertyName("pay_date")]
        public DateField PayDate { get; set; }

        /// <summary>
        /// The payment number or identifier (i.e. check number).
        /// </summary>
        [JsonPropertyName("payment_number")]
        public StringField PaymentNumber { get; set; }

        /// <summary>
        /// The type of payment (Voucher or Check).
        /// </summary>
        [JsonPropertyName("payment_type")]
        public ClassificationField PaymentType { get; set; }

        /// <summary>
        /// The date at which the period ends in YYYY-MM-DD or MM-DD format.
        /// </summary>
        [JsonPropertyName("period_end")]
        public DateField PeriodEnd { get; set; }

        /// <summary>
        /// The date at which the period starts in YYYY-MM-DD or MM-DD format.
        /// </summary>
        [JsonPropertyName("period_start")]
        public DateField PeriodStart { get; set; }

        /// <summary>
        /// The taxes.
        /// </summary>
        [JsonPropertyName("taxes")]
        [JsonConverter(typeof(ObjectListJsonConverter<PayrollCheckRegisterV1Taxes, PayrollCheckRegisterV1Tax>))]
        public PayrollCheckRegisterV1Taxes Taxes { get; set; }

        /// <summary>
        /// The total amount of deductions.
        /// </summary>
        [JsonPropertyName("total_deductions")]
        public DecimalField TotalDeductions { get; set; }

        /// <summary>
        /// The total amount earned.
        /// </summary>
        [JsonPropertyName("total_earnings")]
        public DecimalField TotalEarnings { get; set; }

        /// <summary>
        /// The total amount of hours worked.
        /// </summary>
        [JsonPropertyName("total_hours")]
        public DecimalField TotalHours { get; set; }

        /// <summary>
        /// The total amount of taxes.
        /// </summary>
        [JsonPropertyName("total_tax")]
        public DecimalField TotalTax { get; set; }

        /// <summary>
        /// Output the line in a format suitable for inclusion in an rST table.
        /// </summary>
        public string ToTableLine()
        {
            Dictionary<string, string> printable = PrintableValues();
            return "| "
              + String.Format("{0,-10}", printable["Deductions"])
              + " | "
              + String.Format("{0,-8}", printable["Earnings"])
              + " | "
              + String.Format("{0,-13}", printable["EmployeeName"])
              + " | "
              + String.Format("{0,-15}", printable["EmployeeNumber"])
              + " | "
              + String.Format("{0,-7}", printable["NetPay"])
              + " | "
              + String.Format("{0,-8}", printable["PayDate"])
              + " | "
              + String.Format("{0,-14}", printable["PaymentNumber"])
              + " | "
              + String.Format("{0,-12}", printable["PaymentType"])
              + " | "
              + String.Format("{0,-10}", printable["PeriodEnd"])
              + " | "
              + String.Format("{0,-12}", printable["PeriodStart"])
              + " | "
              + String.Format("{0,-5}", printable["Taxes"])
              + " | "
              + String.Format("{0,-16}", printable["TotalDeductions"])
              + " | "
              + String.Format("{0,-14}", printable["TotalEarnings"])
              + " | "
              + String.Format("{0,-11}", printable["TotalHours"])
              + " | "
              + String.Format("{0,-9}", printable["TotalTax"])
              + " |";
        }

        private Dictionary<string, string> PrintableValues()
        {
            return new Dictionary<string, string>()
            {
                {"Deductions", Deductions.ToString()},
                {"Earnings", Earnings.ToString()},
                {"EmployeeName", EmployeeName.ToString()},
                {"EmployeeNumber", EmployeeNumber.ToString()},
                {"NetPay", NetPay.ToString()},
                {"PayDate", PayDate.ToString()},
                {"PaymentNumber", PaymentNumber.ToString()},
                {"PaymentType", PaymentType.ToString()},
                {"PeriodEnd", PeriodEnd.ToString()},
                {"PeriodStart", PeriodStart.ToString()},
                {"Taxes", Taxes.ToString()},
                {"TotalDeductions", TotalDeductions.ToString()},
                {"TotalEarnings", TotalEarnings.ToString()},
                {"TotalHours", TotalHours.ToString()},
                {"TotalTax", TotalTax.ToString()},
            };
        }
    }

    /// <summary>
    /// List of payments.
    /// </summary>
    public class PayrollCheckRegisterV1Payments : List<PayrollCheckRegisterV1Payment>
    {
        /// <summary>
        /// Default string representation.
        /// </summary>
        public override string ToString()
        {
            if (this.Count == 0)
            {
                return "\n";
            }
            int[] columnSizes = { 12, 10, 15, 17, 9, 10, 16, 14, 12, 14, 7, 18, 16, 13, 11 };
            StringBuilder outStr = new StringBuilder("\n");
            outStr.Append("  " + SummaryHelper.LineSeparator(columnSizes, '-') + "  ");
            outStr.Append("| Deductions ");
            outStr.Append("| Earnings ");
            outStr.Append("| Employee Name ");
            outStr.Append("| Employee Number ");
            outStr.Append("| Net Pay ");
            outStr.Append("| Pay Date ");
            outStr.Append("| Payment Number ");
            outStr.Append("| Payment Type ");
            outStr.Append("| Period End ");
            outStr.Append("| Period Start ");
            outStr.Append("| Taxes ");
            outStr.Append("| Total Deductions ");
            outStr.Append("| Total Earnings ");
            outStr.Append("| Total Hours ");
            outStr.Append("| Total Tax ");
            outStr.Append("|\n  " + SummaryHelper.LineSeparator(columnSizes, '='));
            outStr.Append(SummaryHelper.ArrayToString(this, columnSizes));
            return outStr.ToString();
        }
    }
}
