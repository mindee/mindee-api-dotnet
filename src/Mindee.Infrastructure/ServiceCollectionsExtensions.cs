using Microsoft.Extensions.Configuration;
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
            this IServiceCollection services, 
            IConfiguration configuration)
        {
            services.AddSingleton<MindeeApi>()
                    .AddOptions<MindeeApiSettings>()
                        .Validate(settings =>
                        {
                            return !string.IsNullOrWhiteSpace(settings.ApiKey);
                        }, "The Mindee API key is not defined.");
            return services;
        }

        public static IServiceCollection AddInvoiceParsing(
            this IServiceCollection services, 
            IConfiguration configuration)
        {
            services.TryAddTransient<DocumentParser>();
            services.TryAddTransient<IInvoiceParsing, InvoiceParsing>();
            services.AddMindeeApi(configuration);

            return services;
        }
    }
}
