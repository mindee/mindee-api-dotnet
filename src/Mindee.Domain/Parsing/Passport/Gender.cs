﻿using System.Text.Json.Serialization;
using Mindee.Domain.Parsing.Common;

namespace Mindee.Domain.Parsing.Passport
{
    public class Gender : BaseField
    {
        [JsonPropertyName("value")]
        public string Value { get; set; }
    }
}
