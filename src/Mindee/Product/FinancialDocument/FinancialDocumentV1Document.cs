using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing;
using Mindee.Parsing.Standard;

namespace Mindee.Product.FinancialDocument
{
    /// <summary>
    /// Financial Document API version 1.7 document data.
    /// </summary>
    public class FinancialDocumentV1Document : IPrediction
    {
        /// <summary>
        /// The customer's address used for billing.
        /// </summary>
        [JsonPropertyName("billing_address")]
        public StringField BillingAddress { get; set; }

        /// <summary>
        /// The purchase category among predefined classes.
        /// </summary>
        [JsonPropertyName("category")]
        public ClassificationField Category { get; set; }

        /// <summary>
        /// The address of the customer.
        /// </summary>
        [JsonPropertyName("customer_address")]
        public StringField CustomerAddress { get; set; }

        /// <summary>
        /// List of company registrations associated to the customer.
        /// </summary>
        [JsonPropertyName("customer_company_registrations")]
        public IList<CompanyRegistration> CustomerCompanyRegistrations { get; set; } = new List<CompanyRegistration>();

        /// <summary>
        /// The customer account number or identifier from the supplier.
        /// </summary>
        [JsonPropertyName("customer_id")]
        public StringField CustomerId { get; set; }

        /// <summary>
        /// The name of the customer.
        /// </summary>
        [JsonPropertyName("customer_name")]
        public StringField CustomerName { get; set; }

        /// <summary>
        /// The date the purchase was made.
        /// </summary>
        [JsonPropertyName("date")]
        public DateField Date { get; set; }

        /// <summary>
        /// The document number or identifier.
        /// </summary>
        [JsonPropertyName("document_number")]
        public StringField DocumentNumber { get; set; }

        /// <summary>
        /// One of: 'INVOICE', 'CREDIT NOTE', 'CREDIT CARD RECEIPT', 'EXPENSE RECEIPT'.
        /// </summary>
        [JsonPropertyName("document_type")]
        public ClassificationField DocumentType { get; set; }

        /// <summary>
        /// The date on which the payment is due.
        /// </summary>
        [JsonPropertyName("due_date")]
        public DateField DueDate { get; set; }

        /// <summary>
        /// The invoice number or identifier only if document is an invoice.
        /// </summary>
        [JsonPropertyName("invoice_number")]
        public StringField InvoiceNumber { get; set; }

        /// <summary>
        /// List of line item details.
        /// </summary>
        [JsonPropertyName("line_items")]
        [JsonConverter(typeof(ObjectListJsonConverter<FinancialDocumentV1LineItems, FinancialDocumentV1LineItem>))]
        public FinancialDocumentV1LineItems LineItems { get; set; }

        /// <summary>
        /// The locale detected on the document.
        /// </summary>
        [JsonPropertyName("locale")]
        public Locale Locale { get; set; }

        /// <summary>
        /// The receipt number or identifier only if document is a receipt.
        /// </summary>
        [JsonPropertyName("receipt_number")]
        public StringField ReceiptNumber { get; set; }

        /// <summary>
        /// List of Reference numbers, including PO number.
        /// </summary>
        [JsonPropertyName("reference_numbers")]
        public IList<StringField> ReferenceNumbers { get; set; } = new List<StringField>();

        /// <summary>
        /// The customer's address used for shipping.
        /// </summary>
        [JsonPropertyName("shipping_address")]
        public StringField ShippingAddress { get; set; }

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
        public IList<CompanyRegistration> SupplierCompanyRegistrations { get; set; } = new List<CompanyRegistration>();

        /// <summary>
        /// The email of the supplier or merchant.
        /// </summary>
        [JsonPropertyName("supplier_email")]
        public StringField SupplierEmail { get; set; }

        /// <summary>
        /// The name of the supplier or merchant.
        /// </summary>
        [JsonPropertyName("supplier_name")]
        public StringField SupplierName { get; set; }

        /// <summary>
        /// List of payment details associated to the supplier.
        /// </summary>
        [JsonPropertyName("supplier_payment_details")]
        public IList<PaymentDetail> SupplierPaymentDetails { get; set; } = new List<PaymentDetail>();

        /// <summary>
        /// The phone number of the supplier or merchant.
        /// </summary>
        [JsonPropertyName("supplier_phone_number")]
        public StringField SupplierPhoneNumber { get; set; }

        /// <summary>
        /// The website URL of the supplier or merchant.
        /// </summary>
        [JsonPropertyName("supplier_website")]
        public StringField SupplierWebsite { get; set; }

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
        /// The total amount of tip and gratuity
        /// </summary>
        [JsonPropertyName("tip")]
        public AmountField Tip { get; set; }

        /// <summary>
        /// The total amount paid: includes taxes, tips, fees, and other charges.
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
            string referenceNumbers = string.Join(
                "\n " + string.Concat(Enumerable.Repeat(" ", 19)),
                ReferenceNumbers.Select(item => item));
            string supplierPaymentDetails = string.Join(
                "\n " + string.Concat(Enumerable.Repeat(" ", 26)),
                SupplierPaymentDetails.Select(item => item));
            string supplierCompanyRegistrations = string.Join(
                "\n " + string.Concat(Enumerable.Repeat(" ", 32)),
                SupplierCompanyRegistrations.Select(item => item));
            string customerCompanyRegistrations = string.Join(
                "\n " + string.Concat(Enumerable.Repeat(" ", 32)),
                CustomerCompanyRegistrations.Select(item => item));
            StringBuilder result = new StringBuilder();
            result.Append($":Locale: {Locale}\n");
            result.Append($":Invoice Number: {InvoiceNumber}\n");
            result.Append($":Receipt Number: {ReceiptNumber}\n");
            result.Append($":Document Number: {DocumentNumber}\n");
            result.Append($":Reference Numbers: {referenceNumbers}\n");
            result.Append($":Purchase Date: {Date}\n");
            result.Append($":Due Date: {DueDate}\n");
            result.Append($":Total Net: {TotalNet}\n");
            result.Append($":Total Amount: {TotalAmount}\n");
            result.Append($":Taxes:{Taxes}");
            result.Append($":Supplier Payment Details: {supplierPaymentDetails}\n");
            result.Append($":Supplier Name: {SupplierName}\n");
            result.Append($":Supplier Company Registrations: {supplierCompanyRegistrations}\n");
            result.Append($":Supplier Address: {SupplierAddress}\n");
            result.Append($":Supplier Phone Number: {SupplierPhoneNumber}\n");
            result.Append($":Customer Name: {CustomerName}\n");
            result.Append($":Supplier Website: {SupplierWebsite}\n");
            result.Append($":Supplier Email: {SupplierEmail}\n");
            result.Append($":Customer Company Registrations: {customerCompanyRegistrations}\n");
            result.Append($":Customer Address: {CustomerAddress}\n");
            result.Append($":Customer ID: {CustomerId}\n");
            result.Append($":Shipping Address: {ShippingAddress}\n");
            result.Append($":Billing Address: {BillingAddress}\n");
            result.Append($":Document Type: {DocumentType}\n");
            result.Append($":Purchase Subcategory: {Subcategory}\n");
            result.Append($":Purchase Category: {Category}\n");
            result.Append($":Total Tax: {TotalTax}\n");
            result.Append($":Tip and Gratuity: {Tip}\n");
            result.Append($":Purchase Time: {Time}\n");
            result.Append($":Line Items:{LineItems}");
            return SummaryHelper.Clean(result.ToString());
        }
    }
}
