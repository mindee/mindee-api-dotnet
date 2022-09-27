using System.Collections.Generic;
using Mindee.Prediction.Commun;

namespace Mindee.Prediction.Invoice
{
    public sealed class InvoicePrediction : PredictionBase
    {
        public List<CompanyRegistration> CompanyRegistration { get; set; }

        public Customer Customer { get; set; }

        public CustomerAddress CustomerAddress { get; set; }

        public List<CustomerCompanyRegistration> CustomerCompanyRegistration { get; set; }

        public Date Date { get; set; }

        public DueDate DueDate { get; set; }

        public InvoiceNumber InvoiceNumber { get; set; }

        public List<PaymentDetail> PaymentDetails { get; set; }

        public Supplier Supplier { get; set; }

        public SupplierAddress SupplierAddress { get; set; }

        public List<Taxis> Taxes { get; set; }

        public TotalExcl TotalExcl { get; set; }

        public TotalIncl TotalIncl { get; set; }
    }

    public class CompanyRegistration : BaseField
    {
        public string Type { get; set; }

        public string Value { get; set; }
    }

    public class Customer : BaseField
    {
        public string Value { get; set; }
    }

    public class CustomerAddress : BaseField
    {
        public string Value { get; set; }
    }

    public class CustomerCompanyRegistration : BaseField
    {
        public string Type { get; set; }

        public string Value { get; set; }
    }

    public class Date : BaseField
    {
        public string Raw { get; set; }

        public string Value { get; set; }
    }

    public class DueDate : BaseField
    {
        public string Raw { get; set; }

        public string Value { get; set; }
    }

    public class InvoiceNumber : BaseField
    {
        public string Value { get; set; }
    }

    public class PaymentDetail : BaseField
    {
        public string AccountNumber { get; set; }

        public string Iban { get; set; }

        public string RoutingNumber { get; set; }

        public string Swift { get; set; }
    }

    public class Supplier : BaseField
    {
        public string Value { get; set; }
    }

    public class SupplierAddress : BaseField
    {
        public string Value { get; set; }
    }

    public class Taxis : BaseField
    {
        public double? Rate { get; set; }

        public double? Value { get; set; }
    }

    public class TotalExcl : BaseField
    {
        public double? Value { get; set; }
    }

    public class TotalIncl : BaseField
    {
        public double? Value { get; set; }
    }
}
