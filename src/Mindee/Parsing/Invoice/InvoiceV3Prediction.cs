using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing.Common;

namespace Mindee.Parsing.Invoice
{
    /// <summary>
    /// The invoice model for the v3.
    /// </summary>
    [Endpoint("invoices", "3")]
    public sealed class InvoiceV3Prediction : FinancialPredictionBase
    {
        /// <summary>
        /// List of <see cref="CompanyRegistration"/>
        /// </summary>
        [JsonPropertyName("company_registration")]
        public List<CompanyRegistration> CompanyRegistrations { get; set; }

        /// <summary>
        /// <see cref="Customer"/>
        /// </summary>
        [JsonPropertyName("customer")]
        public Customer Customer { get; set; }

        /// <summary>
        /// <see cref="Invoice.CustomerAddress"/>
        /// </summary>
        [JsonPropertyName("customer_address")]
        public CustomerAddress CustomerAddress { get; set; }

        /// <summary>
        /// List of customer company registrations.
        /// </summary>
        [JsonPropertyName("customer_company_registration")]
        public List<CustomerCompanyRegistration> CustomerCompanyRegistrations { get; set; }

        /// <summary>
        /// <see cref="Invoice.DueDate"/>
        /// </summary>
        [JsonPropertyName("due_date")]
        public DueDate DueDate { get; set; }

        /// <summary>
        /// <see cref="Invoice.InvoiceNumber"/>
        /// </summary>
        [JsonPropertyName("invoice_number")]
        public InvoiceNumber InvoiceNumber { get; set; }

        /// <summary>
        /// List of payment details.
        /// </summary>
        [JsonPropertyName("payment_details")]
        public List<PaymentDetail> PaymentDetails { get; set; }

        /// <summary>
        /// <see cref="Invoice.SupplierAddress"/>
        /// </summary>
        [JsonPropertyName("supplier_address")]
        public SupplierAddress SupplierAddress { get; set; }

        /// <summary>
        /// <see cref="Common.TotalIncl"/>
        /// </summary>
        [JsonPropertyName("total_incl")]
        public TotalIncl TotalIncl { get; set; }

        /// <summary>
        /// <see cref="Common.TotalExcl"/>
        /// </summary>
        [JsonPropertyName("total_excl")]
        public TotalExcl TotalExcl { get; set; }

        /// <summary>
        /// A prettier reprensentation of the current model values.
        /// </summary>
        public override string ToString()
        {
            StringBuilder result = new StringBuilder("-----Invoice data-----\n");
            result.Append($"Invoice number: {InvoiceNumber.Value}\n");
            result.Append($"Total amount including taxes: {TotalIncl.Value}\n");
            result.Append($"Total amount excluding taxes: {TotalExcl.Value}\n");
            result.Append($"Invoice date: {Date.Value}\n");
            result.Append($"Invoice due date: {DueDate.Value}\n");
            result.Append($"Supplier name: {Supplier.Value}\n");
            result.Append($"Supplier address: {SupplierAddress.Value}\n");
            result.Append($"Customer name: {Customer.Value}\n");
            result.Append($"Customer company registration: {string.Join("; ", CustomerCompanyRegistrations.Select(c => c.Value))}\n");
            result.Append($"Customer address: {string.Join("; ", CustomerAddress.Value)}\n");
            result.Append($"Payment details: {string.Join("\n                 ", PaymentDetails.Select(p => p))}\n");
            result.Append($"Company numbers: {string.Join("\n                 ", CompanyRegistrations.Select(c => c.Value))}\n");
            result.Append($"Taxes: {string.Join("\n                 ", Taxes.Select(t => t))}\n");
            result.Append($"Locale: {Locale}\n");

            result.Append("----------------------");

            return result.ToString();
        }
    }
}
