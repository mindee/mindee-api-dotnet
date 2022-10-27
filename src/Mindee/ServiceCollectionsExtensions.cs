using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Mindee.Pdf;

namespace Mindee.Extensions.DependencyInjection
{
    public static class ServiceCollectionsExtensions
    {
        /// <summary>
        /// Configure the Mindee client in the DI.
        /// </summary>
        /// <remarks>The <see cref="MindeeClient"/> instance is registered as a transient.</remarks>
        public static IServiceCollection AddMindeeClient(
            this IServiceCollection services)
        {
            services.TryAddTransient<MindeeClient>();

            services.AddPdfOperation();

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
