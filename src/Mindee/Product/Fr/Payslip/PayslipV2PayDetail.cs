using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing;
using Mindee.Parsing.Standard;

namespace Mindee.Product.Fr.Payslip
{
    /// <summary>
    /// Detailed information about the pay.
    /// </summary>
    public sealed class PayslipV2PayDetail
    {
        /// <summary>
        /// The gross salary of the employee.
        /// </summary>
        [JsonPropertyName("gross_salary")]
        public double? GrossSalary { get; set; }

        /// <summary>
        /// The year-to-date gross salary of the employee.
        /// </summary>
        [JsonPropertyName("gross_salary_ytd")]
        public double? GrossSalaryYtd { get; set; }

        /// <summary>
        /// The income tax rate of the employee.
        /// </summary>
        [JsonPropertyName("income_tax_rate")]
        public double? IncomeTaxRate { get; set; }

        /// <summary>
        /// The income tax withheld from the employee's pay.
        /// </summary>
        [JsonPropertyName("income_tax_withheld")]
        public double? IncomeTaxWithheld { get; set; }

        /// <summary>
        /// The net paid amount of the employee.
        /// </summary>
        [JsonPropertyName("net_paid")]
        public double? NetPaid { get; set; }

        /// <summary>
        /// The net paid amount before tax of the employee.
        /// </summary>
        [JsonPropertyName("net_paid_before_tax")]
        public double? NetPaidBeforeTax { get; set; }

        /// <summary>
        /// The net taxable amount of the employee.
        /// </summary>
        [JsonPropertyName("net_taxable")]
        public double? NetTaxable { get; set; }

        /// <summary>
        /// The year-to-date net taxable amount of the employee.
        /// </summary>
        [JsonPropertyName("net_taxable_ytd")]
        public double? NetTaxableYtd { get; set; }

        /// <summary>
        /// The total cost to the employer.
        /// </summary>
        [JsonPropertyName("total_cost_employer")]
        public double? TotalCostEmployer { get; set; }

        /// <summary>
        /// The total taxes and deductions of the employee.
        /// </summary>
        [JsonPropertyName("total_taxes_and_deductions")]
        public double? TotalTaxesAndDeductions { get; set; }

        /// <summary>
        /// Output the object in a format suitable for inclusion in an rST field list.
        /// </summary>
        public string ToFieldList()
        {
            Dictionary<string, string> printable = PrintableValues();
            return "\n"
                + $"  :Gross Salary: {printable["GrossSalary"]}\n"
                + $"  :Gross Salary YTD: {printable["GrossSalaryYtd"]}\n"
                + $"  :Income Tax Rate: {printable["IncomeTaxRate"]}\n"
                + $"  :Income Tax Withheld: {printable["IncomeTaxWithheld"]}\n"
                + $"  :Net Paid: {printable["NetPaid"]}\n"
                + $"  :Net Paid Before Tax: {printable["NetPaidBeforeTax"]}\n"
                + $"  :Net Taxable: {printable["NetTaxable"]}\n"
                + $"  :Net Taxable YTD: {printable["NetTaxableYtd"]}\n"
                + $"  :Total Cost Employer: {printable["TotalCostEmployer"]}\n"
                + $"  :Total Taxes and Deductions: {printable["TotalTaxesAndDeductions"]}\n";
        }

        /// <summary>
        /// A prettier representation of the line values.
        /// </summary>
        public override string ToString()
        {
            Dictionary<string, string> printable = PrintableValues();
            return "Gross Salary: "
              + printable["GrossSalary"]
              + ", Gross Salary YTD: "
              + printable["GrossSalaryYtd"]
              + ", Income Tax Rate: "
              + printable["IncomeTaxRate"]
              + ", Income Tax Withheld: "
              + printable["IncomeTaxWithheld"]
              + ", Net Paid: "
              + printable["NetPaid"]
              + ", Net Paid Before Tax: "
              + printable["NetPaidBeforeTax"]
              + ", Net Taxable: "
              + printable["NetTaxable"]
              + ", Net Taxable YTD: "
              + printable["NetTaxableYtd"]
              + ", Total Cost Employer: "
              + printable["TotalCostEmployer"]
              + ", Total Taxes and Deductions: "
              + printable["TotalTaxesAndDeductions"].Trim();
        }

        private Dictionary<string, string> PrintableValues()
        {
            return new Dictionary<string, string>()
            {
                {"GrossSalary", SummaryHelper.FormatAmount(GrossSalary)},
                {"GrossSalaryYtd", SummaryHelper.FormatAmount(GrossSalaryYtd)},
                {"IncomeTaxRate", SummaryHelper.FormatAmount(IncomeTaxRate)},
                {"IncomeTaxWithheld", SummaryHelper.FormatAmount(IncomeTaxWithheld)},
                {"NetPaid", SummaryHelper.FormatAmount(NetPaid)},
                {"NetPaidBeforeTax", SummaryHelper.FormatAmount(NetPaidBeforeTax)},
                {"NetTaxable", SummaryHelper.FormatAmount(NetTaxable)},
                {"NetTaxableYtd", SummaryHelper.FormatAmount(NetTaxableYtd)},
                {"TotalCostEmployer", SummaryHelper.FormatAmount(TotalCostEmployer)},
                {"TotalTaxesAndDeductions", SummaryHelper.FormatAmount(TotalTaxesAndDeductions)},
            };
        }
    }
}
