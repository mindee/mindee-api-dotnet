using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Mindee.Infrastructure.Api;
using Mindee.Infrastructure.Prediction;
using Mindee.Domain.Parsing;
using MindeeApi = Mindee.Infrastructure.Api.MindeeApi;

namespace Mindee.Extensions.DependencyInjection
{
    public static class ServiceCollectionsExtensions
    {
        /// <summary>
        /// Add parsing services.
        /// </summary>
        /// <remarks>In transient scope, except for the HTTP API calls.</remarks>
        public static IServiceCollection AddParsing(
            this IServiceCollection services)
        {
            services.AddInvoiceParsing();
            services.AddReceiptParsing();
            services.AddPassportParsing();

            return services;
        }

        private static IServiceCollection AddInvoiceParsing(
            this IServiceCollection services)
        {
            services.TryAddTransient<IInvoiceParsing, InvoiceParsing>();
            services.AddMindeeApi();

            return services;
        }

        private static IServiceCollection AddReceiptParsing(
            this IServiceCollection services)
        {
            services.TryAddTransient<IReceiptParsing, ReceiptParsing>();
            services.AddMindeeApi();

            return services;
        }

        private static IServiceCollection AddPassportParsing(
            this IServiceCollection services)
        {
            services.TryAddTransient<IPassportParsing, PassportParsing>();
            services.AddMindeeApi();

            return services;
        }

        private static IServiceCollection AddMindeeApi(
            this IServiceCollection services)
        {
            services.TryAddSingleton<MindeeApi>();
            services.AddOptions<MindeeApiSettings>()
                        .Validate(settings =>
                        {
                            return !string.IsNullOrWhiteSpace(settings.ApiKey);
                        }, "The Mindee API key is not defined.");

            return services;
        }
    }
}
