using Microsoft.Extensions.DependencyInjection;
using MyComponentTemplate.Infra.Context;

namespace MyComponentTemplate.Infra
{
    public static class HealthCheckExtensions
    {
        public static IServiceCollection AddDatabaseHealthCheck(this IServiceCollection services, string connectionString)
        {
            services.AddHealthChecks()
                    .AddCheck<DatabaseHealthCheck>("Database", tags: new[] { "db", "sql" });

            return services;
        }
    }
}
