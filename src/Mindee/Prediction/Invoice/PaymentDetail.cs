using Mindee.Prediction.Commun;

namespace Mindee.Prediction.Invoice
{
    public class PaymentDetail : BaseField
    {
        public string AccountNumber { get; set; }

        public string Iban { get; set; }

        public string RoutingNumber { get; set; }

        public string Swift { get; set; }
    }
}
