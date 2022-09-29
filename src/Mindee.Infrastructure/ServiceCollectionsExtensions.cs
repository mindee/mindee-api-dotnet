using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Mindee.Infrastructure.Api;
using Mindee.Infrastructure.Prediction;
using Mindee.Prediction;

namespace Mindee.Extensions.DependencyInjection
{
    public static class ServiceCollectionsExtensions
    {
        public static IServiceCollection AddMindeeApi(
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

        public static IServiceCollection AddInvoiceParsing(
            this IServiceCollection services)
        {
            services.TryAddTransient<DocumentParser>();
            services.TryAddTransient<IInvoiceParsing, InvoiceParsing>();
            services.AddMindeeApi();

            return services;
        }

        public static IServiceCollection AddReceiptParsing(
            this IServiceCollection services)
        {
            services.TryAddTransient<DocumentParser>();
            services.TryAddTransient<IReceiptParsing, ReceiptParsing>();
            services.AddMindeeApi();

            return services;
        }

        public static IServiceCollection AddPassportParsing(
            this IServiceCollection services)
        {
            services.TryAddTransient<DocumentParser>();
            services.TryAddTransient<IPassportParsing, PassportParsing>();
            services.AddMindeeApi();

            return services;
        }
    }
}
