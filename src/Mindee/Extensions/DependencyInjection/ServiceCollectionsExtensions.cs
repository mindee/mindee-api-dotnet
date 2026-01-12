using System;
// ReSharper disable once RedundantUsingDirective
// Note: Necessary for .NET 4.7.2/4.8.2
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Mindee.Http;
using Mindee.Pdf;
using RestSharp;

namespace Mindee.Extensions.DependencyInjection
{
    /// <summary>
    ///     To configure DI.
    /// </summary>
    public static class ServiceCollectionsExtensions
    {
        /// <summary>
        ///     Configure the Mindee API in the DI, mainly used for mocking/testing purposes.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configureOptions"></param>
        /// <param name="throwOnError"></param>
        /// <returns></returns>
        public static void AddMindeeApi(
            this IServiceCollection services,
            Action<MindeeSettings> configureOptions,
            bool throwOnError = false)
        {
            services.Configure(configureOptions);
            RegisterV1RestSharpClient(services, throwOnError);

            services.AddSingleton(serviceProvider =>
            {
                var settings = serviceProvider.GetRequiredService<IOptions<MindeeSettings>>();
                var restClient = serviceProvider.GetRequiredService<RestClient>();
                var logger = serviceProvider.GetService<ILoggerFactory>()?.CreateLogger<MindeeApi>();
                return new MindeeApi(settings, restClient, logger);
            });
        }

        /// <summary>
        ///     Configure the Mindee API in the DI, mainly used for mocking/testing purposes.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configureOptions"></param>
        /// <param name="loggerFactory"></param>
        /// <param name="throwOnError"></param>
        /// <returns></returns>
        public static void AddMindeeApiV2(
            this IServiceCollection services,
            Action<MindeeSettingsV2> configureOptions,
            ILoggerFactory loggerFactory = null,
            bool throwOnError = false)
        {
            services.Configure(configureOptions);
            RegisterV2RestSharpClient(services, throwOnError);

            if (loggerFactory is not null)
            {
                services.AddSingleton(loggerFactory);
            }
            else if (services.All(d => d.ServiceType != typeof(ILoggerFactory)))
            {
                services.AddSingleton<ILoggerFactory, NullLoggerFactory>();
                services.AddSingleton(typeof(ILogger<>), typeof(NullLogger<>));
            }

            services.AddSingleton(serviceProvider =>
            {
                var settings = serviceProvider.GetRequiredService<IOptions<MindeeSettingsV2>>();
                var restClient = serviceProvider.GetRequiredKeyedService<RestClient>("MindeeV2RestClient");
                var logger = serviceProvider.GetService<ILoggerFactory>()?.CreateLogger<MindeeApiV2>();
                return new MindeeApiV2(settings, restClient, logger);
            });
        }

        private static void RegisterV1RestSharpClient(IServiceCollection services, bool throwOnError)
        {
            services.AddSingleton(provider =>
            {
                var settings = provider.GetRequiredService<IOptions<MindeeSettings>>().Value;
                settings.MindeeBaseUrl = Environment.GetEnvironmentVariable("Mindee__BaseUrl");
                if (string.IsNullOrEmpty(settings.MindeeBaseUrl))
                {
                    settings.MindeeBaseUrl = "https://api.mindee.net";
                }

                if (settings.RequestTimeoutSeconds <= 0)
                {
                    settings.RequestTimeoutSeconds = 120;
                }

                if (string.IsNullOrEmpty(settings.ApiKey))
                {
                    settings.ApiKey = Environment.GetEnvironmentVariable("Mindee__ApiKey");
                }

                var clientOptions = new RestClientOptions(settings.MindeeBaseUrl)
                {
                    FollowRedirects = false,
                    Timeout = TimeSpan.FromSeconds(settings.RequestTimeoutSeconds),
                    UserAgent = BuildUserAgent(),
                    Expect100Continue = false,
                    CachePolicy = new CacheControlHeaderValue { NoCache = true, NoStore = true },
                    ThrowOnAnyError = throwOnError
                };
                return new RestClient(clientOptions);
            });
        }

        private static void RegisterV2RestSharpClient(IServiceCollection services, bool throwOnError)
        {
            services.AddKeyedSingleton("MindeeV2RestClient", (provider, _) =>
            {
                var settings = provider.GetRequiredService<IOptions<MindeeSettingsV2>>().Value;
                settings.MindeeBaseUrl = Environment.GetEnvironmentVariable("MindeeV2__BaseUrl");
                if (string.IsNullOrEmpty(settings.MindeeBaseUrl))
                {
                    settings.MindeeBaseUrl = "https://api-v2.mindee.net";
                }

                if (settings.RequestTimeoutSeconds <= 0)
                {
                    settings.RequestTimeoutSeconds = 120;
                }

                if (string.IsNullOrEmpty(settings.ApiKey))
                {
                    settings.ApiKey = Environment.GetEnvironmentVariable("MindeeV2__ApiKey");
                }

                var clientOptions = new RestClientOptions(settings.MindeeBaseUrl)
                {
                    FollowRedirects = false,
                    Timeout = TimeSpan.FromSeconds(settings.RequestTimeoutSeconds),
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
            string platform;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                platform = "windows";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                platform = "linux";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                platform = "macos";
            }
            else
            {
                platform = "other";
            }

            return $"mindee-api-dotnet@v{Assembly.GetExecutingAssembly().GetName().Version}"
                   + $" dotnet-v{Environment.Version}"
                   + $" {platform}";
        }

        /// <summary>
        ///     Configure the Mindee client in the DI.
        /// </summary>
        /// <param name="services">
        ///     <c>IServiceCollection</c>
        /// </param>
        /// <param name="sectionName">The name of the section to bind from the configuration.</param>
        /// <remarks>The <see cref="MindeeClient" /> instance is registered as a transient.</remarks>
        public static IServiceCollection AddMindeeClient(
            this IServiceCollection services,
            string sectionName = "Mindee")
        {
            services.AddSingleton<MindeeClient>();
            services.AddSingleton<IHttpApi, MindeeApi>();
            services.AddOptions<MindeeSettings>()
                .BindConfiguration(sectionName)
                .Validate(settings => !string.IsNullOrEmpty(settings.ApiKey), "The Mindee V1 API key is missing");
            RegisterV1RestSharpClient(services, false);

            services.AddPdfOperation();

            return services;
        }

        /// <summary>
        ///     Configure the Mindee client V2 in the DI.
        /// </summary>
        /// <param name="services">
        ///     <c>IServiceCollection</c>
        /// </param>
        /// <param name="sectionName">The name of the section to bind from the configuration.</param>
        /// <remarks>The <see cref="MindeeClient" /> instance is registered as a transient.</remarks>
        public static IServiceCollection AddMindeeClientV2(
            this IServiceCollection services,
            string sectionName = "MindeeV2")
        {
            services.AddSingleton<MindeeClientV2>();
            services.AddSingleton<HttpApiV2, MindeeApiV2>();
            services.AddOptions<MindeeSettingsV2>()
                .BindConfiguration(sectionName)
                .Validate(settings => !string.IsNullOrEmpty(settings.ApiKey), "The Mindee V2 API key is missing");
            RegisterV2RestSharpClient(services, false);

            services.AddPdfOperation();

            return services;
        }

        /// <summary>
        ///     Configure the Mindee client in the DI with your own custom pdf implementation.
        /// </summary>
        /// <typeparam name="TPdfOperationImplementation">Will be registered as a singleton.</typeparam>
        /// <remarks>The <see cref="MindeeClient" /> instance is registered as a transient.</remarks>
        public static IServiceCollection AddMindeeClientWithCustomPdfImplementation<TPdfOperationImplementation>(
            this IServiceCollection services)
            where TPdfOperationImplementation : class, IPdfOperation, new()
        {
            services.AddSingleton<MindeeClient>();
            services.AddSingleton<IPdfOperation, TPdfOperationImplementation>();

            return services;
        }

        /// <summary>
        ///     Configure the Mindee client V2 in the DI with your own custom pdf implementation.
        /// </summary>
        /// <typeparam name="TPdfOperationImplementation">Will be registered as a singleton.</typeparam>
        /// <remarks>The <see cref="MindeeClient" /> instance is registered as a transient.</remarks>
        public static IServiceCollection AddMindeeClientV2WithCustomPdfImplementation<TPdfOperationImplementation>(
            this IServiceCollection services)
            where TPdfOperationImplementation : class, IPdfOperation, new()
        {
            services.AddSingleton<MindeeClientV2>();
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
