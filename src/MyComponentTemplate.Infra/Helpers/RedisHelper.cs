using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

public class RedisHelper
{
    private readonly string _host;
    private readonly int _port;
    private readonly ILogger _logger;

    public RedisHelper(IConfiguration configuration, ILogger logger)
    {
        _host = configuration["Redis:Host"];
        _port = configuration.GetValue<int>("Redis:Port");
        _logger = logger;
    }

    public void Connect()
    {
        _logger.LogInformation($"Conectando ao Redis Host: {_host} na porta {_port}");
        // Lógica para conectar ao Redis
    }
}
