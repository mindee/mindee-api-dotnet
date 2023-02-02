﻿using System.Text.Json.Serialization;

namespace Mindee.Parsing.Common
{
    /// <summary>
    /// Represent a company registration.
    /// </summary>
    public class CompanyRegistration : BaseField
    {
        /// <summary>
        /// Type of the company registration number.
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; }

        /// <summary>
        /// The value of the field.
        /// </summary>

        [JsonPropertyName("value")]
        public string Value { get; set; }
    }
}