using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

public class RabbitMqHelper
{
    private readonly string _host;
    private readonly string _username;
    private readonly string _password;
    private readonly ILogger _logger;

    public RabbitMqHelper(IConfiguration configuration, ILogger logger)
    {
        _host = configuration["RabbitMQ:Host"];
        _username = configuration["RabbitMQ:Username"];
        _password = configuration["RabbitMQ:Password"];
        _logger = logger;
    }

    public void Configure()
    {
        _logger.LogInformation($"Conectando ao RabbitMQ Host: {_host} com Username: {_username}");
        // Lógica para configurar a conexão RabbitMQ
    }
}
