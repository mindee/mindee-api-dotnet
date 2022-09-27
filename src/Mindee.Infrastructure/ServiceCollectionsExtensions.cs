using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Mindee.Infrastructure.Api;
using Mindee.Infrastructure.Prediction;

namespace Mindee.Infrastructure
{
    public static class ServiceCollectionsExtensions
    {
        public static IServiceCollection AddMindeeApi(this IServiceCollection services)
        {
            services.TryAddSingleton<MindeeApi>();
            services.AddOptions<MindeeApiSettings>()
                    .Validate(s => !string.IsNullOrWhiteSpace(s.ApiKey));

            return services;
        }

        public static IServiceCollection AddInvoiceParsing(this IServiceCollection services)
        {
            services.TryAddTransient<InvoiceParsing>();
            services.AddMindeeApi();

            return services;
        }
    }
}
