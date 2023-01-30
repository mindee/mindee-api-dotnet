using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Mindee.Parsing;
using Mindee.Pdf;

namespace Mindee.Extensions.DependencyInjection
{
    /// <summary>
    /// To configure DI.
    /// </summary>
    public static class ServiceCollectionsExtensions
    {
        /// <summary>
        /// Configure the Mindee client in the DI.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/></param>
        /// <param name="sectionName">The name of the section to bind from the configuration.</param>
        /// <remarks>The <see cref="MindeeClient"/> instance is registered as a transient.</remarks>
        public static IServiceCollection AddMindeeClient(
            this IServiceCollection services,
            string sectionName = "MindeeSettings")
        {
            services.TryAddTransient<MindeeClient>();
            services.TryAddTransient<IPredictable, MindeeApi>();
            services.AddOptions<MindeeSettings>()
                .BindConfiguration(sectionName)
                .Validate(settings =>
                {
                    return !string.IsNullOrEmpty(settings.ApiKey);
                }, "The Mindee api key is missing");

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
