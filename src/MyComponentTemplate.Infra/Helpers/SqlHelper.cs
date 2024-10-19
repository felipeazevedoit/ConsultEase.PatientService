using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

public class SqlHelper
{
    private readonly string _connectionString;
    private readonly ILogger _logger;

    public SqlHelper(IConfiguration configuration, ILogger logger)
    {
        _connectionString = configuration.GetConnectionString("SQLServer");
        _logger = logger;
    }

    public void Connect()
    {
        _logger.LogInformation($"Conectando ao SQL Server com a ConnectionString: {_connectionString}");
        // Lógica para conectar ao SQL Server
    }
}
