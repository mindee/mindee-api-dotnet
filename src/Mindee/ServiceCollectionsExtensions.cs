using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Mindee.Extensions.DependencyInjection
{
    public static class ServiceCollectionsExtensions
    {
        public static IServiceCollection AddMindeeClient(
            this IServiceCollection services)
        {
            services.TryAddTransient<MindeeClient>();

            services.AddParsing();

            return services;
        }
    }
}
