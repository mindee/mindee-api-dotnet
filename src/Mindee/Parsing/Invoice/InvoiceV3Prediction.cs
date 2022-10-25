using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing.Common;

namespace Mindee.Parsing.Invoice
{
    [Endpoint("invoices", "3")]
    public sealed class InvoiceV3Prediction : FinancialPredictionBase
    {
        [JsonPropertyName("company_registration")]
        public List<CompanyRegistration> CompanyRegistration { get; set; }

        [JsonPropertyName("customer")]
        public Customer Customer { get; set; }

        [JsonPropertyName("customer_address")]
        public CustomerAddress CustomerAddress { get; set; }

        [JsonPropertyName("customer_company_registration")]
        public List<CustomerCompanyRegistration> CustomerCompanyRegistration { get; set; }

        [JsonPropertyName("due_date")]
        public DueDate DueDate { get; set; }

        [JsonPropertyName("invoice_number")]
        public InvoiceNumber InvoiceNumber { get; set; }

        [JsonPropertyName("payment_details")]
        public List<PaymentDetail> PaymentDetails { get; set; }

        [JsonPropertyName("supplier_address")]
        public SupplierAddress SupplierAddress { get; set; }

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
            result.Append($"Customer company registration: {string.Join("; ", CustomerCompanyRegistration.Select(c => c.Value))}\n");
            result.Append($"Customer address: {string.Join("; ", CustomerAddress.Value)}\n");
            result.Append($"Payment details: {string.Join("\n                 ", PaymentDetails.Select(p => p))}\n");
            result.Append($"Company numbers: {string.Join("\n                 ", CompanyRegistration.Select(c => c.Value))}\n");
            result.Append($"Taxes: {string.Join("\n                 ", Taxes.Select(t => t))}\n");
            result.Append($"Locale: {Locale}\n");

            result.Append("----------------------");

            return result.ToString();
        }
    }
}
