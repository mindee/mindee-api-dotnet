using System.Collections.Generic;
using Mindee.Prediction.Commun;

namespace Mindee.Prediction.Invoice
{
    public sealed class InvoicePrediction : FinancialPredictionBase
    {
        public List<CompanyRegistration> CompanyRegistration { get; set; }

        public Customer Customer { get; set; }

        public CustomerAddress CustomerAddress { get; set; }

        public List<CustomerCompanyRegistration> CustomerCompanyRegistration { get; set; }

        public DueDate DueDate { get; set; }

        public InvoiceNumber InvoiceNumber { get; set; }

        public List<PaymentDetail> PaymentDetails { get; set; }

        public SupplierAddress SupplierAddress { get; set; }

        public TotalExcl TotalExcl { get; set; }
    }
}
