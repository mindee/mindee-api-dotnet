﻿using System.Text.Json.Serialization;
using Mindee.Parsing.Common;

namespace Mindee.Parsing.Invoice
{
    public class CompanyRegistration : BaseField
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }
    }
}