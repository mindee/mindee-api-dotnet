﻿using System.Text.Json.Serialization;
using Mindee.Domain.Parsing.Common;

namespace Mindee.Domain.Parsing.Invoice
{
    public class PaymentDetail : BaseField
    {
        [JsonPropertyName("account_number")]
        public string AccountNumber { get; set; }

        [JsonPropertyName("iban")]
        public string Iban { get; set; }

        [JsonPropertyName("routing_number")]
        public string RoutingNumber { get; set; }

        [JsonPropertyName("swift")]
        public string Swift { get; set; }
    }
}
