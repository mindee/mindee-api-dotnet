﻿using System.Text.Json.Serialization;
using Mindee.Parsing.Common;

namespace Mindee.Parsing.Passport
{
    public class Mrz1 : BaseField
    {
        [JsonPropertyName("value")]
        public string Value { get; set; }
    }
}