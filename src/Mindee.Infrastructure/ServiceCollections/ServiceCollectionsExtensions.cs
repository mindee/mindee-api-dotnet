using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mindee.Infrastructure.Api;

namespace Mindee.Infrastructure.ServiceCollections
{
    public static class ServiceCollectionsExtensions
    {
        public static IServiceCollection AddMindeeApi(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<MindeeApi>()
                .AddOptions<MindeeApiSettings>()
                    .Validate(s => !string.IsNullOrWhiteSpace(s.ApiKey));

            return services;
        }
    }
}
