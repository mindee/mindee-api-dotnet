﻿using System.Text.Json.Serialization;

namespace Mindee.Parsing.Common
{
    public class Date : BaseField
    {
        [JsonPropertyName("raw")]
        public string Raw { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }
    }
}