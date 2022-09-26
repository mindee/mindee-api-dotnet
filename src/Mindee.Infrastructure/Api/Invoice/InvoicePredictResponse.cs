using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Mindee.Infrastructure.Api.Invoice
{
    public class InvoicePredictResponse
    {
        [JsonPropertyName("api_request")]
        public ApiRequest ApiRequest { get; set; }

        [JsonPropertyName("document")]
        public Document Document { get; set; }
    }

    public class Error
    {
    }

    public class Extras
    {
    }

    public class Inference
    {
        [JsonPropertyName("extras")]
        public Extras Extras { get; set; }

        [JsonPropertyName("finished_at")]
        public DateTime FinishedAt { get; set; }

        [JsonPropertyName("pages")]
        public List<Page> Pages { get; set; }

        [JsonPropertyName("prediction")]
        public Prediction Prediction { get; set; }

        [JsonPropertyName("processing_time")]
        public double ProcessingTime { get; set; }

        [JsonPropertyName("product")]
        public Product Product { get; set; }

        [JsonPropertyName("started_at")]
        public DateTime StartedAt { get; set; }
    }

    public class Ocr
    {
    }

    public class Orientation
    {
        [JsonPropertyName("confidence")]
        public double Confidence { get; set; }

        [JsonPropertyName("degrees")]
        public int Degrees { get; set; }
    }

    public class Page
    {
        [JsonPropertyName("extras")]
        public Extras Extras { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("prediction")]
        public Prediction Prediction { get; set; }
    }

    public class Prediction
    {
        [JsonPropertyName("company_registration")]
        public List<CompanyRegistration> CompanyRegistration { get; set; }

        [JsonPropertyName("customer")]
        public Customer Customer { get; set; }

        [JsonPropertyName("customer_address")]
        public CustomerAddress CustomerAddress { get; set; }

        [JsonPropertyName("customer_company_registration")]
        public List<CustomerCompanyRegistration> CustomerCompanyRegistration { get; set; }

        [JsonPropertyName("date")]
        public Date Date { get; set; }

        [JsonPropertyName("document_type")]
        public DocumentType DocumentType { get; set; }

        [JsonPropertyName("due_date")]
        public DueDate DueDate { get; set; }

        [JsonPropertyName("invoice_number")]
        public InvoiceNumber InvoiceNumber { get; set; }

        [JsonPropertyName("locale")]
        public Locale Locale { get; set; }

        [JsonPropertyName("orientation")]
        public Orientation Orientation { get; set; }

        [JsonPropertyName("payment_details")]
        public List<PaymentDetail> PaymentDetails { get; set; }

        [JsonPropertyName("supplier")]
        public Supplier Supplier { get; set; }

        [JsonPropertyName("supplier_address")]
        public SupplierAddress SupplierAddress { get; set; }

        [JsonPropertyName("taxes")]
        public List<Taxis> Taxes { get; set; }

        [JsonPropertyName("total_excl")]
        public TotalExcl TotalExcl { get; set; }

        [JsonPropertyName("total_incl")]
        public TotalIncl TotalIncl { get; set; }
    }

    public class Product
    {
        [JsonPropertyName("features")]
        public List<string> Features { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("version")]
        public string Version { get; set; }
    }

    public class Annotations
    {
        [JsonPropertyName("labels")]
        public List<object> Labels { get; set; }
    }

    public class ApiRequest
    {
        [JsonPropertyName("error")]
        public Error Error { get; set; }

        [JsonPropertyName("resources")]
        public List<string> Resources { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("status_code")]
        public int StatusCode { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }

    public class Document
    {
        [JsonPropertyName("annotations")]
        public Annotations Annotations { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("inference")]
        public Inference Inference { get; set; }

        [JsonPropertyName("n_pages")]
        public int NPages { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("ocr")]
        public Ocr Ocr { get; set; }
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

    public class Date
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

    public class DocumentType
    {
        [JsonPropertyName("value")]
        public string Value { get; set; }
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

    public class Supplier
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

    public class Taxis
    {
        [JsonPropertyName("confidence")]
        public double Confidence { get; set; }

        [JsonPropertyName("polygon")]
        public List<List<double>> Polygon { get; set; }

        [JsonPropertyName("rate")]
        public double? Rate { get; set; }

        [JsonPropertyName("value")]
        public double? Value { get; set; }

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

    public class TotalIncl
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

    public class Locale
    {
        [JsonPropertyName("confidence")]
        public double Confidence { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [JsonPropertyName("language")]
        public string Language { get; set; }
    }
}
