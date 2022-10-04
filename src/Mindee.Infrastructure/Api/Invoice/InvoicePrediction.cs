using System.Collections.Generic;
using System.Text.Json.Serialization;
using Mindee.Infrastructure.Api.Common;

namespace Mindee.Infrastructure.Api.Invoice
{
    internal class InvoicePrediction : FinancialPredictionBase
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

        [JsonPropertyName("total_excl")]
        public TotalExcl TotalExcl { get; set; }
    }

    public class CompanyRegistration
    {
        [JsonPropertyName("confidence")]
        public double Confidence { get; set; }

        [JsonPropertyName("polygon")]
        public List<List<double>> Polygon { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }

        [JsonPropertyName("page_id")]
        public int? PageId { get; set; }
    }

    public class Customer
    {
        [JsonPropertyName("confidence")]
        public double Confidence { get; set; }

        [JsonPropertyName("polygon")]
        public List<List<double>> Polygon { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }

        [JsonPropertyName("page_id")]
        public int? PageId { get; set; }
    }

    public class CustomerAddress
    {
        [JsonPropertyName("confidence")]
        public double Confidence { get; set; }

        [JsonPropertyName("polygon")]
        public List<List<double>> Polygon { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }

        [JsonPropertyName("page_id")]
        public int? PageId { get; set; }
    }

    public class CustomerCompanyRegistration
    {
        [JsonPropertyName("confidence")]
        public double Confidence { get; set; }

        [JsonPropertyName("polygon")]
        public List<List<double>> Polygon { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }

        [JsonPropertyName("page_id")]
        public int? PageId { get; set; }
    }

    public class DueDate
    {
        [JsonPropertyName("confidence")]
        public double Confidence { get; set; }

        [JsonPropertyName("polygon")]
        public List<List<double>> Polygon { get; set; }

        [JsonPropertyName("raw")]
        public string Raw { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }

        [JsonPropertyName("page_id")]
        public int? PageId { get; set; }
    }

    public class InvoiceNumber
    {
        [JsonPropertyName("confidence")]
        public double Confidence { get; set; }

        [JsonPropertyName("polygon")]
        public List<List<double>> Polygon { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }

        [JsonPropertyName("page_id")]
        public int? PageId { get; set; }
    }

    public class PaymentDetail
    {
        [JsonPropertyName("account_number")]
        public string AccountNumber { get; set; }

        [JsonPropertyName("confidence")]
        public double Confidence { get; set; }

        [JsonPropertyName("iban")]
        public string Iban { get; set; }

        [JsonPropertyName("polygon")]
        public List<List<double>> Polygon { get; set; }

        [JsonPropertyName("routing_number")]
        public string RoutingNumber { get; set; }

        [JsonPropertyName("swift")]
        public string Swift { get; set; }

        [JsonPropertyName("page_id")]
        public int? PageId { get; set; }
    }

    public class SupplierAddress
    {
        [JsonPropertyName("confidence")]
        public double Confidence { get; set; }

        [JsonPropertyName("polygon")]
        public List<List<double>> Polygon { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }

        [JsonPropertyName("page_id")]
        public int? PageId { get; set; }
    }

    public class TotalExcl
    {
        [JsonPropertyName("confidence")]
        public double Confidence { get; set; }

        [JsonPropertyName("polygon")]
        public List<List<double>> Polygon { get; set; }

        [JsonPropertyName("value")]
        public double? Value { get; set; }

        [JsonPropertyName("page_id")]
        public int? PageId { get; set; }
    }
}
