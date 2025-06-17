using System;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.Extensions.DependencyInjection;
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
        /// Configure the Mindee API in the DI, mainly used for mocking/testing purposes.
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
            RegisterRestSharpClient(services, throwOnError);
            RegisterRestSharpClientV2(services, throwOnError);
        }

        /// <summary>
        /// Configure the Mindee API in the DI, mainly used for mocking/testing purposes.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configureOptions"></param>
        /// <param name="throwOnError"></param>
        /// <returns></returns>
        public static void AddMindeeApiV2(this IServiceCollection services,
            Action<MindeeSettings> configureOptions, bool throwOnError = false)
        {
            services.Configure(configureOptions);
            services.AddSingleton<MindeeApiV2>();
            RegisterRestSharpClient(services, throwOnError);
            RegisterRestSharpClientV2(services, throwOnError);
        }

        private static void RegisterRestSharpClient(IServiceCollection services, bool throwOnError)
        {
            services.AddSingleton(provider =>
            {
                var mindeeSettings = provider.GetRequiredService<IOptions<MindeeSettings>>().Value;
                mindeeSettings.MindeeBaseUrl = Environment.GetEnvironmentVariable("Mindee__BaseUrl");
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
                    ThrowOnAnyError = throwOnError,
                };
                return new RestClient(clientOptions);
            });
        }

        private static void RegisterRestSharpClientV2(IServiceCollection services, bool throwOnError)
        {
            services.AddSingleton(provider =>
            {
                var mindeeSettingsV2 = provider.GetRequiredService<IOptions<MindeeSettings>>().Value;
                mindeeSettingsV2.MindeeBaseUrl = Environment.GetEnvironmentVariable("Mindee__V2__BaseUrl");
                if (string.IsNullOrEmpty(mindeeSettingsV2.MindeeBaseUrl))
                    mindeeSettingsV2.MindeeBaseUrl = "https://api.mindee.net";
                if (mindeeSettingsV2.RequestTimeoutSeconds <= 0)
                    mindeeSettingsV2.RequestTimeoutSeconds = 120;
                if (string.IsNullOrEmpty(mindeeSettingsV2.ApiKey))
                    mindeeSettingsV2.ApiKey = Environment.GetEnvironmentVariable("Mindee__V2__ApiKey");

                var clientOptions = new RestClientOptions(mindeeSettingsV2.MindeeBaseUrl)
                {
                    FollowRedirects = false,
                    Timeout = TimeSpan.FromSeconds(mindeeSettingsV2.RequestTimeoutSeconds),
                    UserAgent = BuildUserAgent(),
                    Expect100Continue = false,
                    CachePolicy = new CacheControlHeaderValue { NoCache = true, NoStore = true },
                    ThrowOnAnyError = throwOnError,
                };
                return new RestClient(clientOptions);
            });
        }

        private static string BuildUserAgent()
        {
            string platform;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                platform = "windows";
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                platform = "linux";
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                platform = "macos";
            else
                platform = "other";
            return $"mindee-api-dotnet@v{Assembly.GetExecutingAssembly().GetName().Version}"
                   + $" dotnet-v{Environment.Version}"
                   + $" {platform}";
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
            services.AddSingleton<MindeeClient>();
            services.AddSingleton<IHttpApi, MindeeApi>();
            services.AddOptions<MindeeSettings>()
                .BindConfiguration(sectionName)
                .Validate(settings => !string.IsNullOrEmpty(settings.ApiKey), "The Mindee api key is missing");
            RegisterRestSharpClient(services, false);
            RegisterRestSharpClientV2(services, false);

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
            services.AddSingleton<MindeeClient>();
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
