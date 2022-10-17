﻿using System.Collections.Generic;
using System.Text.Json.Serialization;
using Mindee.Parsing.Common;

namespace Mindee.Parsing.Invoice
{
    [Endpoint("invoices", "3")]
    public sealed class InvoicePrediction : FinancialPredictionBase
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
}
