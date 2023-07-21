using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing;
using Mindee.Parsing.Standard;

namespace Mindee.Product.Invoice
{
    /// <summary>
    /// Document data for Invoice, API version 4.
    /// </summary>
    public sealed class InvoiceV4Document : IPrediction
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
        [JsonConverter(typeof(ObjectListJsonConverter<Taxes, Tax>))]
        public Taxes Taxes { get; set; }

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
        /// Line items details.
        /// </summary>
        [JsonPropertyName("line_items")]
        [JsonConverter(typeof(ObjectListJsonConverter<InvoiceV4LineItems, InvoiceV4LineItem>))]
        public InvoiceV4LineItems LineItems { get; set; }

        /// <summary>
        /// The total amount of taxes.
        /// </summary>
        public double? TotalTaxes => Taxes?.Sum(t => t.Value);

        /// <summary>
        /// A prettier reprensentation of the current model values.
        /// </summary>
        public override string ToString()
        {
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
            result.Append($":Taxes:{Taxes}");
            result.Append($":Total net: {TotalNet}\n");
            result.Append($":Total tax: {SummaryHelper.FormatAmount(TotalTaxes)}\n");
            result.Append($":Total amount: {TotalAmount}\n");
            result.Append($":Line Items:{LineItems}");
            return SummaryHelper.Clean(result.ToString());
        }
    }
}
