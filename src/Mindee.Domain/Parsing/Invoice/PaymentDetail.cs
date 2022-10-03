using Mindee.Domain.Parsing.Common;

namespace Mindee.Domain.Parsing.Invoice
{
    public class PaymentDetail : BaseField
    {
        public string AccountNumber { get; set; }

        public string Iban { get; set; }

        public string RoutingNumber { get; set; }

        public string Swift { get; set; }
    }
}
