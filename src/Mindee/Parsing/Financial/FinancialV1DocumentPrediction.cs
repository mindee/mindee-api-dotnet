using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing.Common;
using Mindee.Parsing.Receipt;

namespace Mindee.Parsing.Financial
{
    /// <summary>
    /// The financial model for the v1.
    /// </summary>
    public class FinancialV1DocumentPrediction : FinancialPredictionBase
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
        [JsonPropertyName("supplier_company_registration")]
        public List<CompanyRegistration> SupplierCompanyRegistrations { get; set; }

        /// <summary>
        /// List of payment details.
        /// </summary>
        [JsonPropertyName("payment_details")]
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
        [JsonPropertyName("customer_company_registration")]
        public List<CompanyRegistration> CustomerCompanyRegistrations { get; set; }

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
        /// Total taxes.
        /// </summary>
        [JsonPropertyName("total_tax")]
        public AmountField TotalTax { get; set; }

        /// <summary>
        /// Line items details.
        /// </summary>
        [JsonPropertyName("line_items")]
        public List<FinancialLineItem> LineItems { get; set; }

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
        /// The supplier name.
        /// </summary>
        [JsonPropertyName("supplier")]
        public StringField Supplier { get; set; }

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
            result.Append($":Document type: {DocumentType}\n");
            result.Append($":Category: {Category}\n");
            result.Append($":Subcategory: {SubCategory}\n");
            result.Append($":Locale: {Locale}\n");
            result.Append($":Date: {Date}\n");
            result.Append($":Due date: {DueDate}\n");
            result.Append($":Time: {Time}\n");
            result.Append($":Number: {InvoiceNumber}\n");
            result.Append($":Reference numbers: {string.Join(", ", ReferenceNumbers.Select(rn => rn))}\n");
            result.Append($":Date: {Date.Value}\n");
            result.Append($":Due date: {DueDate.Value}\n");
            result.Append($":Supplier name: {SupplierName}\n");
            result.Append($":Supplier address: {SupplierAddress}\n");
            result.Append($":Supplier company registrations: {string.Join("; ", SupplierCompanyRegistrations.Select(c => c.Value))}\n");
            result.Append($":Supplier payment details: {string.Join("; ", SupplierPaymentDetails.Select(p => p))}\n");
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
