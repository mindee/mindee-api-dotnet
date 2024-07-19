using System;
using System.Net;
using System.Net.Http.Headers;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Mindee.Http;
using RestSharp;

namespace Mindee.DependencyInjection
{
    /// <summary>
    ///
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configureOptions"></param>
        /// <param name="throwOnError"></param>
        /// <returns></returns>
        public static void AddMindeeApi(this IServiceCollection services,
            Action<MindeeSettings> configureOptions, bool throwOnError = false)
        {
            services.Configure(configureOptions);

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12; // Safety for .NET 4.7.2
            services.AddSingleton(provider =>
            {
                var mindeeSettings = provider.GetRequiredService<IOptions<MindeeSettings>>().Value;
                if (string.IsNullOrEmpty(mindeeSettings.MindeeBaseUrl))
                    mindeeSettings.MindeeBaseUrl = "https://api.mindee.net";
                if (mindeeSettings.RequestTimeoutSeconds <= 0)
                    mindeeSettings.RequestTimeoutSeconds = 120;
                if (string.IsNullOrEmpty(mindeeSettings.ApiKey))
                    mindeeSettings.ApiKey = Environment.GetEnvironmentVariable("Mindee__ApiKey");

                var clientOptions = new RestClientOptions(mindeeSettings.MindeeBaseUrl)
                {
                    FollowRedirects = false,
                    Timeout = TimeSpan.FromSeconds(mindeeSettings.RequestTimeoutSeconds),
                    UserAgent = BuildUserAgent(),
                    Expect100Continue = false,
                    CachePolicy = new CacheControlHeaderValue { NoCache = true, NoStore = true },
                    ThrowOnAnyError = throwOnError
                };
                return new RestClient(clientOptions);
            });

            services.AddSingleton<MindeeApi>();
        }

        private static string BuildUserAgent()
        {
            return $"mindee-api-dotnet@v{Assembly.GetExecutingAssembly().GetName().Version}"
                   + $" dotnet-v{Environment.Version}"
                   + $" {Environment.OSVersion}";
        }
    }
}
