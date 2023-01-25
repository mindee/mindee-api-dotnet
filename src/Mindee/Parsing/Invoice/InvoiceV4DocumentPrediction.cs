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
    public sealed class InvoiceV4DocumentPrediction : FinancialPredictionBase
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
        public DateField DueDate { get; set; }

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
            StringBuilder lineItems = new StringBuilder("\n");
            if (LineItems.Any())
            {
                lineItems.Append(
                    "====================== ======== ========= ========== ================== ====================================\n"
                    );
                lineItems.Append(
                    "Code                   QTY      Price     Amount     Tax (Rate)         Description\n"
                    );
                lineItems.Append(
                    "====================== ======== ========= ========== ================== ====================================\n"
                    );
                lineItems.Append(string.Join("\n", LineItems.Select(item => item.ToString())));
                lineItems.Append(
                    "\n====================== ======== ========= ========== ================== ===================================="
                    );
            }

            StringBuilder result = new StringBuilder();
            result.Append($":Locale: {Locale}\n");
            result.Append($":Document type: {DocumentType}\n");
            result.Append($":Invoice number: {InvoiceNumber}\n");
            result.Append($":Reference numbers: {string.Join(", ", ReferenceNumbers.Select(rn => rn))}\n");
            result.Append($":Invoice date: {Date.Value}\n");
            result.Append($":Invoice due date: {DueDate.Value}\n");
            result.Append($":Supplier name: {SupplierName}\n");
            result.Append($":Supplier address: {SupplierAddress}\n");
            result.Append($":Supplier company registrations: {string.Join("; ", SupplierCompanyRegistrations.Select(c => c.Value))}\n");
            result.Append($":Supplier payment details: {string.Join("; ", SupplierPaymentDetails.Select(p => p))}\n");
            result.Append($":Customer name: {CustomerName}\n");
            result.Append($":Customer address: {string.Join("; ", CustomerAddress)}\n");
            result.Append($":Customer company registrations: {string.Join("; ", CustomerCompanyRegistrations.Select(c => c.Value))}\n");
            result.Append($":Taxes: {string.Join("\n                 ", Taxes.Select(t => t))}\n");
            result.Append($":Total net: {TotalNet}\n");
            result.Append($":Total taxes: {SummaryHelper.FormatAmount(TotalTaxes)}\n");
            result.Append($":Total amount: {TotalAmount}\n");
            result.Append($"\n:Line Items: {lineItems}\n");

            return SummaryHelper.Clean(result.ToString());
        }
    }
}
