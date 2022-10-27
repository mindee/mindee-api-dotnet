﻿using System.Text.Json.Serialization;
using Mindee.Parsing.Common;

namespace Mindee.Parsing.Invoice
{
    public class Customer : BaseField
    {
        /// <summary>
        /// The value of the field.
        /// </summary>
        [JsonPropertyName("value")]
        public string Value { get; set; }
    }
}
