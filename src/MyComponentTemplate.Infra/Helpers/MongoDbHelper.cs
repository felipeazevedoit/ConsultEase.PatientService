using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

public class MongoDbHelper
{
    private readonly string _connectionString;
    private readonly ILogger _logger;

    public MongoDbHelper(IConfiguration configuration, ILogger logger)
    {
        _connectionString = configuration.GetConnectionString("MongoDB");
        _logger = logger;
    }

    public void Connect()
    {
        _logger.LogInformation($"Conectando ao MongoDB com a ConnectionString: {_connectionString}");
        // Lógica para conectar ao MongoDB
    }
}
