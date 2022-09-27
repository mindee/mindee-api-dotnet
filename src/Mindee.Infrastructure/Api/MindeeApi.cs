using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RestSharp;

namespace Mindee.Infrastructure.Api
{
    internal partial class MindeeApi
    {
        private const string BaseUrl = "https://api.mindee.net/v1";
        private readonly string _apiKey;
        private readonly RestClient _httpClient;
        private readonly ILogger _logger;

        public MindeeApi(
            ILogger<MindeeApi> logger
            , IOptions<MindeeApiSettings> mindeeApiSettings
            , IConfiguration configuration
            , HttpMessageHandler httpMessageHandler = null
            )
        {
            _logger = logger;
            _apiKey = configuration.GetSection($"{nameof(MindeeApiSettings)}:{nameof(MindeeApiSettings.ApiKey)}").Value;
            if (httpMessageHandler != null)
            {
                _httpClient = BuildClient(httpMessageHandler);
            }
            else
            {
                _httpClient = BuildClient();
            }
        }

        private RestClient BuildClient()
        {
            var options = new RestClientOptions(BaseUrl);
            var client = new RestClient(options,
                p => p.Add("Authorization", $"Token {_apiKey}")
            );

            return client;
        }

        private RestClient BuildClient(HttpMessageHandler httpMessageHandler)
        {
            var options = new RestClientOptions(BaseUrl)
            {
                ConfigureMessageHandler = _ => httpMessageHandler
            };
            var client = new RestClient(options,
                p => p.Add("Authorization", $"Token {_apiKey}")
            );

            return client;
        }
    }
}
