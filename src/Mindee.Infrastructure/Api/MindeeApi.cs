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
            , IOptions<MindeeApiSettings> mindeeApiSettings)
        {
            _logger = logger;
            _apiKey = mindeeApiSettings.Value.ApiKey;
            _httpClient = BuildClient();
        }

        private RestClient BuildClient()
        {
            var options = new RestClientOptions(BaseUrl);
            var client = new RestClient(options,
                p => p.Add("Authorization", $"Token {_apiKey}")
            );

            return client;
        }
    }
}
