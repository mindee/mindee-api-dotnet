using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing;
using Mindee.Parsing.Standard;

namespace Mindee.Product.Receipt
{
    /// <summary>
    /// Document data for Receipt, API version 5.
    /// </summary>
    public class ReceiptV5Document : IPrediction
    {
        /// <summary>
        /// The purchase category among predefined classes.
        /// </summary>
        [JsonPropertyName("category")]
        public ClassificationField Category { get; set; }

        /// <summary>
        /// The date the purchase was made.
        /// </summary>
        [JsonPropertyName("date")]
        public DateField Date { get; set; }

        /// <summary>
        /// One of: 'CREDIT CARD RECEIPT', 'EXPENSE RECEIPT'.
        /// </summary>
        [JsonPropertyName("document_type")]
        public ClassificationField DocumentType { get; set; }

        /// <summary>
        /// List of line item details.
        /// </summary>
        [JsonPropertyName("line_items")]
        [JsonConverter(typeof(ObjectListJsonConverter<ReceiptV5LineItems, ReceiptV5LineItem>))]
        public ReceiptV5LineItems LineItems { get; set; }

        /// <summary>
        /// The locale detected on the document.
        /// </summary>
        [JsonPropertyName("locale")]
        public Locale Locale { get; set; }

        /// <summary>
        /// The purchase subcategory among predefined classes for transport and food.
        /// </summary>
        [JsonPropertyName("subcategory")]
        public ClassificationField Subcategory { get; set; }

        /// <summary>
        /// The address of the supplier or merchant.
        /// </summary>
        [JsonPropertyName("supplier_address")]
        public StringField SupplierAddress { get; set; }

        /// <summary>
        /// List of company registrations associated to the supplier.
        /// </summary>
        [JsonPropertyName("supplier_company_registrations")]
        public List<CompanyRegistration> SupplierCompanyRegistrations { get; set; }

        /// <summary>
        /// The name of the supplier or merchant.
        /// </summary>
        [JsonPropertyName("supplier_name")]
        public StringField SupplierName { get; set; }

        /// <summary>
        /// The phone number of the supplier or merchant.
        /// </summary>
        [JsonPropertyName("supplier_phone_number")]
        public StringField SupplierPhoneNumber { get; set; }

        /// <summary>
        /// List of tax lines information.
        /// </summary>
        [JsonPropertyName("taxes")]
        [JsonConverter(typeof(ObjectListJsonConverter<Taxes, Tax>))]
        public Taxes Taxes { get; set; }

        /// <summary>
        /// The time the purchase was made.
        /// </summary>
        [JsonPropertyName("time")]
        public StringField Time { get; set; }

        /// <summary>
        /// The total amount of tip and gratuity.
        /// </summary>
        [JsonPropertyName("tip")]
        public AmountField Tip { get; set; }

        /// <summary>
        /// The total amount paid: includes taxes, discounts, fees, tips, and gratuity.
        /// </summary>
        [JsonPropertyName("total_amount")]
        public AmountField TotalAmount { get; set; }

        /// <summary>
        /// The net amount paid: does not include taxes, fees, and discounts.
        /// </summary>
        [JsonPropertyName("total_net")]
        public AmountField TotalNet { get; set; }

        /// <summary>
        /// The total amount of taxes.
        /// </summary>
        [JsonPropertyName("total_tax")]
        public AmountField TotalTax { get; set; }

        /// <summary>
        /// A prettier representation of the current model values.
        /// </summary>
        public override string ToString()
        {
            string supplierCompanyRegistrations = string.Join(
                "\n " + string.Concat(Enumerable.Repeat(" ", 32)),
                SupplierCompanyRegistrations.Select(item => item));
            StringBuilder result = new StringBuilder();
            result.Append($":Expense Locale: {Locale}\n");
            result.Append($":Purchase Category: {Category}\n");
            result.Append($":Purchase Subcategory: {Subcategory}\n");
            result.Append($":Document Type: {DocumentType}\n");
            result.Append($":Purchase Date: {Date}\n");
            result.Append($":Purchase Time: {Time}\n");
            result.Append($":Total Amount: {TotalAmount}\n");
            result.Append($":Total Net: {TotalNet}\n");
            result.Append($":Total Tax: {TotalTax}\n");
            result.Append($":Tip and Gratuity: {Tip}\n");
            result.Append($":Taxes:{Taxes}");
            result.Append($":Supplier Name: {SupplierName}\n");
            result.Append($":Supplier Company Registrations: {supplierCompanyRegistrations}\n");
            result.Append($":Supplier Address: {SupplierAddress}\n");
            result.Append($":Supplier Phone Number: {SupplierPhoneNumber}\n");
            result.Append($":Line Items:{LineItems}");
            return SummaryHelper.Clean(result.ToString());
        }
    }
}
