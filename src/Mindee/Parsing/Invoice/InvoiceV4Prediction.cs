using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing.Common;

namespace Mindee.Parsing.Invoice
{
    /// <summary>
    /// The invoice model for the v4.
    /// </summary>
    [Endpoint("invoices", "4")]
    public sealed class InvoiceV4Prediction : FinancialPredictionBase
    {
        /// <summary>
        /// List of Reference numbers including PO number.
        /// </summary>
        [JsonPropertyName("reference_numbers")]
        public List<StringField> ReferenceNumbers { get; set; }

        /// <summary>
        /// The supplier name.
        /// </summary>
        [JsonPropertyName("supplier_name")]
        public StringField SupplierName { get; set; }

        /// <summary>
        /// List of <see cref="CompanyRegistration"/>
        /// </summary>
        [JsonPropertyName("supplier_company_registrations")]
        public List<CompanyRegistration> SupplierCompanyRegistrations { get; set; }

        /// <summary>
        /// List of payment details.
        /// </summary>
        [JsonPropertyName("supplier_payment_details")]
        public List<PaymentDetail> SupplierPaymentDetails { get; set; }

        /// <summary>
        /// The supplier address.
        /// </summary>
        [JsonPropertyName("supplier_address")]
        public StringField SupplierAddress { get; set; }

        /// <summary>
        /// The customer.
        /// </summary>
        [JsonPropertyName("customer_name")]
        public StringField CustomerName { get; set; }

        /// <summary>
        /// List of customer company registrations.
        /// </summary>
        [JsonPropertyName("customer_company_registrations")]
        public List<CustomerCompanyRegistration> CustomerCompanyRegistrations { get; set; }

        /// <summary>
        /// The adress of the customer.
        /// </summary>
        [JsonPropertyName("customer_address")]
        public StringField CustomerAddress { get; set; }

        /// <summary>
        /// The due date of the invoice.
        /// </summary>
        [JsonPropertyName("due_date")]
        public Date DueDate { get; set; }

        /// <summary>
        /// The invoice number.
        /// </summary>
        [JsonPropertyName("invoice_number")]
        public StringField InvoiceNumber { get; set; }

        /// <summary>
        /// Total amount including taxes.
        /// </summary>
        [JsonPropertyName("total_amount")]
        public AmountField TotalAmount { get; set; }

        /// <summary>
        /// Total amount excluding taxes.
        /// </summary>
        [JsonPropertyName("total_net")]
        public AmountField TotalNet { get; set; }

        /// <summary>
        /// Line items details.
        /// </summary>
        [JsonPropertyName("line_items")]
        public List<InvoiceLineItem> LineItems { get; set; }

        /// <summary>
        /// The total amount of taxes.
        /// </summary>
        public double? TotalTaxes => Taxes?.Sum(t => t.Value);

        /// <summary>
        /// A prettier reprensentation of the current model values.
        /// </summary>
        public override string ToString()
        {
            string lineItems = "\n";
            if (LineItems.Any())
            {
                lineItems =
                    "\n  Code           | QTY    | Price   | Amount   | Tax (Rate)       | Description\n  ";
                lineItems += string.Join("\n  ", LineItems.Select(item => item.ToString()));
            }

            StringBuilder result = new StringBuilder("----- Invoice V4 -----\n");
            result.Append($"Locale: {Locale}\n");
            result.Append($"Invoice number: {InvoiceNumber.Value}\n");
            result.Append($"Reference numbers: {string.Join(", ", ReferenceNumbers.Select(rn => rn.Value))}\n");
            result.Append($"Invoice date: {Date.Value}\n");
            result.Append($"Invoice due date: {DueDate.Value}\n");
            result.Append($"Supplier name: {SupplierName.Value}\n");
            result.Append($"Supplier address: {SupplierAddress.Value}\n");
            result.Append($"Supplier company registrations: {string.Join("; ", SupplierCompanyRegistrations.Select(c => c.Value))}\n");
            result.Append($"Supplier payment details: {string.Join("; ", SupplierPaymentDetails.Select(p => p))}\n");
            result.Append($"Customer name: {CustomerName.Value}\n");
            result.Append($"Customer company registrations: {string.Join("; ", CustomerCompanyRegistrations.Select(c => c.Value))}\n");
            result.Append($"Customer address: {string.Join("; ", CustomerAddress.Value)}\n");
            result.Append($"Line Items: {lineItems}\n");
            result.Append($"Taxes: {string.Join("\n                 ", Taxes.Select(t => t))}\n");
            result.Append($"Total taxes: {TotalTaxes}\n");
            result.Append($"Total amount excluding taxes: {TotalNet.Value}\n");
            result.Append($"Total amount including taxes: {TotalAmount.Value}\n");
            result.Append("----------------------\n");

            return SummaryHelper.Clean(result.ToString());
        }
    }
}
