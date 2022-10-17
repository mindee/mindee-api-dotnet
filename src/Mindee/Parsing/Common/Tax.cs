﻿using System.Text.Json.Serialization;

namespace Mindee.Parsing.Common
{
    public class Tax : BaseField
    {
        /// <summary>
        /// The rate of the taxe.
        /// </summary>
        /// <example>5</example>
        [JsonPropertyName("rate")]
        public double? Rate { get; set; }

        /// <summary>
        /// The total amount of the tax.
        /// </summary>
        /// <example>10.5</example>
        [JsonPropertyName("value")]
        public double? Value { get; set; }
    }
}