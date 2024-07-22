using System;
using System.Net;
using System.Net.Http.Headers;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Mindee.Http;
using Mindee.Pdf;
using RestSharp;

namespace Mindee.Extensions.DependencyInjection
{
    /// <summary>
    /// To configure DI.
    /// </summary>
    public static class ServiceCollectionsExtensions
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


            services.AddSingleton<MindeeApi>();
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
        }

        private static string BuildUserAgent()
        {
            return $"mindee-api-dotnet@v{Assembly.GetExecutingAssembly().GetName().Version}"
                   + $" dotnet-v{Environment.Version}"
                   + $" {Environment.OSVersion}";
        }

        /// <summary>
        /// Configure the Mindee client in the DI.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/></param>
        /// <param name="sectionName">The name of the section to bind from the configuration.</param>
        /// <remarks>The <see cref="MindeeClient"/> instance is registered as a transient.</remarks>
        public static IServiceCollection AddMindeeClient(
            this IServiceCollection services,
            string sectionName = "Mindee")
        {
            services.TryAddTransient<MindeeClient>();
            services.TryAddTransient<IHttpApi, MindeeApi>();
            services.AddOptions<MindeeSettings>()
                .BindConfiguration(sectionName)
                .Validate(settings => !string.IsNullOrEmpty(settings.ApiKey), "The Mindee api key is missing");
            services.AddSingleton<RestClient>();

            services.AddPdfOperation();

            return services;
        }

        /// <summary>
        /// Configure the Mindee client in the DI with your own custom pdf implementation.
        /// </summary>
        /// <typeparam name="TPdfOperationImplementation">Will be registered as a singleton.</typeparam>
        /// <remarks>The <see cref="MindeeClient"/> instance is registered as a transient.</remarks>
        public static IServiceCollection AddMindeeClientWithCustomPdfImplementation<TPdfOperationImplementation>(
            this IServiceCollection services)
            where TPdfOperationImplementation : class, IPdfOperation, new()
        {
            services.TryAddTransient<MindeeClient>();
            services.AddSingleton<IPdfOperation, TPdfOperationImplementation>();

            return services;
        }

        internal static IServiceCollection AddPdfOperation(
            this IServiceCollection services)
        {
            services.AddSingleton<IPdfOperation, DocNetApi>();

            return services;
        }
    }
}
