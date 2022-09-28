﻿using System.Text.Json.Serialization;

namespace Mindee.Infrastructure.Api.Commun
{
    public class PredictResponse<TModel>
        where TModel : class, new()
    {
        [JsonPropertyName("api_request")]
        public ApiRequest ApiRequest { get; set; }

        [JsonPropertyName("document")]
        public Document<TModel> Document { get; set; }
    }
}