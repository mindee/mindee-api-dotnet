using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing;
using Mindee.Parsing.Standard;

namespace Mindee.Product.Invoice
{
    /// <summary>
    /// Invoice API version 4.11 document data.
    /// </summary>
    public class InvoiceV4Document : IPrediction
    {
        /// <summary>
        /// The customer billing address.
        /// </summary>
        [JsonPropertyName("billing_address")]
        public AddressField BillingAddress { get; set; }

        /// <summary>
        /// The purchase category.
        /// </summary>
        [JsonPropertyName("category")]
        public ClassificationField Category { get; set; }

        /// <summary>
        /// The address of the customer.
        /// </summary>
        [JsonPropertyName("customer_address")]
        public AddressField CustomerAddress { get; set; }

        /// <summary>
        /// List of company registration numbers associated to the customer.
        /// </summary>
        [JsonPropertyName("customer_company_registrations")]
        public IList<CompanyRegistration> CustomerCompanyRegistrations { get; set; } = new List<CompanyRegistration>();

        /// <summary>
        /// The customer account number or identifier from the supplier.
        /// </summary>
        [JsonPropertyName("customer_id")]
        public StringField CustomerId { get; set; }

        /// <summary>
        /// The name of the customer or client.
        /// </summary>
        [JsonPropertyName("customer_name")]
        public StringField CustomerName { get; set; }

        /// <summary>
        /// The date the purchase was made.
        /// </summary>
        [JsonPropertyName("date")]
        public DateField Date { get; set; }

        /// <summary>
        /// Document type: INVOICE or CREDIT NOTE.
        /// </summary>
        [JsonPropertyName("document_type")]
        public ClassificationField DocumentType { get; set; }

        /// <summary>
        /// Document type extended.
        /// </summary>
        [JsonPropertyName("document_type_extended")]
        public ClassificationField DocumentTypeExtended { get; set; }

        /// <summary>
        /// The date on which the payment is due.
        /// </summary>
        [JsonPropertyName("due_date")]
        public DateField DueDate { get; set; }

        /// <summary>
        /// The invoice number or identifier.
        /// </summary>
        [JsonPropertyName("invoice_number")]
        public StringField InvoiceNumber { get; set; }

        /// <summary>
        /// List of all the line items present on the invoice.
        /// </summary>
        [JsonPropertyName("line_items")]
        [JsonConverter(typeof(ObjectListJsonConverter<InvoiceV4LineItems, InvoiceV4LineItem>))]
        public InvoiceV4LineItems LineItems { get; set; }

        /// <summary>
        /// The locale of the document.
        /// </summary>
        [JsonPropertyName("locale")]
        public Locale Locale { get; set; }

        /// <summary>
        /// The date on which the payment is due / was full-filled.
        /// </summary>
        [JsonPropertyName("payment_date")]
        public DateField PaymentDate { get; set; }

        /// <summary>
        /// The purchase order number.
        /// </summary>
        [JsonPropertyName("po_number")]
        public StringField PoNumber { get; set; }

        /// <summary>
        /// List of all reference numbers on the invoice, including the purchase order number.
        /// </summary>
        [JsonPropertyName("reference_numbers")]
        public IList<StringField> ReferenceNumbers { get; set; } = new List<StringField>();

        /// <summary>
        /// Customer's delivery address.
        /// </summary>
        [JsonPropertyName("shipping_address")]
        public AddressField ShippingAddress { get; set; }

        /// <summary>
        /// The purchase subcategory for transport, food and shopping.
        /// </summary>
        [JsonPropertyName("subcategory")]
        public ClassificationField Subcategory { get; set; }

        /// <summary>
        /// The address of the supplier or merchant.
        /// </summary>
        [JsonPropertyName("supplier_address")]
        public AddressField SupplierAddress { get; set; }

        /// <summary>
        /// List of company registration numbers associated to the supplier.
        /// </summary>
        [JsonPropertyName("supplier_company_registrations")]
        public IList<CompanyRegistration> SupplierCompanyRegistrations { get; set; } = new List<CompanyRegistration>();

        /// <summary>
        /// The email address of the supplier or merchant.
        /// </summary>
        [JsonPropertyName("supplier_email")]
        public StringField SupplierEmail { get; set; }

        /// <summary>
        /// The name of the supplier or merchant.
        /// </summary>
        [JsonPropertyName("supplier_name")]
        public StringField SupplierName { get; set; }

        /// <summary>
        /// List of payment details associated to the supplier of the invoice.
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
        /// List of taxes. Each item contains the detail of the tax.
        /// </summary>
        [JsonPropertyName("taxes")]
        [JsonConverter(typeof(ObjectListJsonConverter<Taxes, Tax>))]
        public Taxes Taxes { get; set; }

        /// <summary>
        /// The total amount of the invoice: includes taxes, tips, fees, and other charges.
        /// </summary>
        [JsonPropertyName("total_amount")]
        public AmountField TotalAmount { get; set; }

        /// <summary>
        /// The net amount of the invoice: does not include taxes, fees, and discounts.
        /// </summary>
        [JsonPropertyName("total_net")]
        public AmountField TotalNet { get; set; }

        /// <summary>
        /// The total tax: the sum of all the taxes for this invoice.
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
            result.Append($":Purchase Order Number: {PoNumber}\n");
            result.Append($":Reference Numbers: {referenceNumbers}\n");
            result.Append($":Purchase Date: {Date}\n");
            result.Append($":Due Date: {DueDate}\n");
            result.Append($":Payment Date: {PaymentDate}\n");
            result.Append($":Total Net: {TotalNet}\n");
            result.Append($":Total Amount: {TotalAmount}\n");
            result.Append($":Total Tax: {TotalTax}\n");
            result.Append($":Taxes:{Taxes}");
            result.Append($":Supplier Payment Details: {supplierPaymentDetails}\n");
            result.Append($":Supplier Name: {SupplierName}\n");
            result.Append($":Supplier Company Registrations: {supplierCompanyRegistrations}\n");
            result.Append($":Supplier Address: {SupplierAddress}\n");
            result.Append($":Supplier Phone Number: {SupplierPhoneNumber}\n");
            result.Append($":Supplier Website: {SupplierWebsite}\n");
            result.Append($":Supplier Email: {SupplierEmail}\n");
            result.Append($":Customer Name: {CustomerName}\n");
            result.Append($":Customer Company Registrations: {customerCompanyRegistrations}\n");
            result.Append($":Customer Address: {CustomerAddress}\n");
            result.Append($":Customer ID: {CustomerId}\n");
            result.Append($":Shipping Address: {ShippingAddress}\n");
            result.Append($":Billing Address: {BillingAddress}\n");
            result.Append($":Document Type: {DocumentType}\n");
            result.Append($":Document Type Extended: {DocumentTypeExtended}\n");
            result.Append($":Purchase Subcategory: {Subcategory}\n");
            result.Append($":Purchase Category: {Category}\n");
            result.Append($":Line Items:{LineItems}");
            return SummaryHelper.Clean(result.ToString());
        }
    }
}
