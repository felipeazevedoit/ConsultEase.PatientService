using Microsoft.Extensions.Logging;
using Polly;
namespace MyComponentTemplate.Infra.Context
{
    public class DatabaseService
    {
        private readonly IAsyncPolicy _retryPolicy;
        private readonly ILogger<DatabaseService> _logger;

        public DatabaseService(ILogger<DatabaseService> logger)
        {
            _logger = logger;

            // Configura uma política de retry com 3 tentativas
            _retryPolicy = Policy
                .Handle<Exception>()  // Trata todas as exceções
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(2),
                    onRetry: (exception, timespan, retryCount, context) =>
                    {
                        Console.WriteLine($"Retrying... Attempt {retryCount}");
                    });
        }

        public async Task<string> GetDataFromDatabaseAsync()
        {
            try
            {
                _logger.LogInformation("Trying to access the database...");


                return await _retryPolicy.ExecuteAsync(async () =>
                {
                    // Simulando uma operação de banco de dados que pode falhar
                    Console.WriteLine("Trying to access the database...");

                    // Simulando uma falha temporária no banco de dados
                    throw new Exception("Temporary DB failure!");

                    // Retornar o dado do banco (aqui você deve retornar uma string real)
                    // Exemplo:
                    // return await _databaseContext.GetDataAsync();
                    return "Database data successfully retrieved!";
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while accessing the database.");
                throw;  // Re-lança a exceção após o log
            }

        }

    }
}

