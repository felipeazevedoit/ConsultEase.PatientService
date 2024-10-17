using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using MyComponentTemplate.Infra.Context;

namespace MyComponentTemplate.Infra
{
    public static class HealthCheckRegistrar
    {
        public static IServiceCollection AddInfraHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");

            // Adicionar health checks relacionados à infraestrutura
            services.AddHealthChecks()
                    .AddCheck<DatabaseHealthCheck>("Database Health Check", tags: new[] { "db", "sql" });

            return services;
        }
    }
}
