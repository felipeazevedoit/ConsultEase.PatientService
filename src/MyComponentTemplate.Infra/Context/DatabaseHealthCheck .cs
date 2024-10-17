using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace MyComponentTemplate.Infra.Context
{
    public class DatabaseHealthCheck : IHealthCheck
    {
        private readonly string _connectionString;

        public DatabaseHealthCheck(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            // Aqui você pode verificar a conexão com o banco de dados, por exemplo
            bool dbConnectionIsHealthy = CheckDatabaseConnection();

            if (dbConnectionIsHealthy)
            {
                return Task.FromResult(HealthCheckResult.Healthy("The database is healthy."));
            }

            return Task.FromResult(HealthCheckResult.Unhealthy("The database connection failed."));
        }

        private bool CheckDatabaseConnection()
        {
            // Simulação de verificação de conexão com banco de dados
            // Retorne true se a conexão estiver saudável ou false se falhou
            return true; // Coloque aqui sua lógica real
        }
    }
}
