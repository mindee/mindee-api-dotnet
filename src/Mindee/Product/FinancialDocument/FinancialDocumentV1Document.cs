using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing;
using Mindee.Parsing.Standard;

namespace Mindee.Product.FinancialDocument
{
    /// <summary>
    /// The financial model for the v1.
    /// </summary>
    public class FinancialDocumentV1Document
    {
        /// <summary>
        /// <see cref="ClassificationField"/>
        /// </summary>
        [JsonPropertyName("document_type")]
        public ClassificationField DocumentType { get; set; }

        /// <summary>
        /// The date.
        /// </summary>
        [JsonPropertyName("date")]
        public DateField Date { get; set; }

        /// <summary>
        /// <see cref="Locale"/>
        /// </summary>
        [JsonPropertyName("locale")]
        public Locale Locale { get; set; }

        /// <summary>
        /// <see cref="Tax"/>
        /// </summary>
        [JsonPropertyName("taxes")]
        public List<Tax> Taxes { get; set; }

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
        public List<PaymentDetail> PaymentDetails { get; set; }

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
        public List<CompanyRegistration> CustomerCompanyRegistrations { get; set; }

        /// <summary>
        /// The address of the customer.
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
        /// Total taxes.
        /// </summary>
        [JsonPropertyName("total_tax")]
        public AmountField TotalTax { get; set; }

        /// <summary>
        /// Line items details.
        /// </summary>
        [JsonPropertyName("line_items")]
        public List<FinancialDocumentV1LineItem> LineItems { get; set; }

        /// <summary>
        /// The category of the receipt.
        /// </summary>
        [JsonPropertyName("category")]
        public StringField Category { get; set; }

        /// <summary>
        /// The subcategory of the receipt.
        /// </summary>
        [JsonPropertyName("subcategory")]
        public StringField SubCategory { get; set; }

        /// <summary>
        /// <see cref="Time"/>
        /// </summary>
        [JsonPropertyName("time")]
        public TimeField Time { get; set; }

        /// <summary>
        /// The tip.
        /// </summary>
        [JsonPropertyName("tip")]
        public AmountField Tip { get; set; }

        /// <summary>
        /// A prettier representation of the current model values.
        /// </summary>
        public override string ToString()
        {
            var lineItems = new StringBuilder();
            if (LineItems.Any())
            {
                lineItems.Append(
                    "\n====================== ======== ========= ========== ================== ====================================\n"
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
            result.Append($":Document type: {DocumentType}\n");
            result.Append($":Category: {Category}\n");
            result.Append($":Subcategory: {SubCategory}\n");
            result.Append($":Locale: {Locale}\n");
            result.Append($":Date: {Date}\n");
            result.Append($":Due date: {DueDate}\n");
            result.Append($":Time: {Time}\n");
            result.Append($":Number: {InvoiceNumber}\n");
            result.Append($":Reference numbers: {string.Join(", ", ReferenceNumbers.Select(rn => rn))}\n");
            result.Append($":Supplier name: {SupplierName}\n");
            result.Append($":Supplier address: {SupplierAddress}\n");
            result.Append($":Supplier company registrations: {string.Join("; ", SupplierCompanyRegistrations.Select(c => c.Value))}\n");
            result.Append($":Supplier payment details: {string.Join("; ", PaymentDetails.Select(p => p))}\n");
            result.Append($":Customer name: {CustomerName}\n");
            result.Append($":Customer address: {string.Join("; ", CustomerAddress)}\n");
            result.Append($":Customer company registrations: {string.Join("; ", CustomerCompanyRegistrations.Select(c => c.Value))}\n");
            result.Append($":Tip: {Tip}\n");
            result.Append($":Taxes: {string.Join("\n                 ", Taxes.Select(t => t))}\n");
            result.Append($":Total taxes: {TotalTax}\n");
            result.Append($":Total net: {TotalNet}\n");
            result.Append($":Total amount: {TotalAmount}\n");
            result.Append($"\n:Line Items: {lineItems}\n");

            return SummaryHelper.Clean(result.ToString());
        }
    }
}
