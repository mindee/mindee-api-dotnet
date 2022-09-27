using Microsoft.Extensions.DependencyInjection.Extensions;
using Mindee.Infrastructure.Api;
using Mindee.Infrastructure.Prediction;
using Mindee.Prediction;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionsExtensions
    {
        public static IServiceCollection AddMindeeApi(this IServiceCollection services)
        {
            services.TryAddSingleton<MindeeApi>();
            services.AddOptions<MindeeApiSettings>(nameof(MindeeApiSettings))
                    .Validate(s => !string.IsNullOrWhiteSpace(s.ApiKey));

            return services;
        }

        public static IServiceCollection AddInvoiceParsing(this IServiceCollection services)
        {
            services.TryAddTransient<IInvoiceParsing, InvoiceParsing>();
            services.AddMindeeApi();

            return services;
        }
    }
}
