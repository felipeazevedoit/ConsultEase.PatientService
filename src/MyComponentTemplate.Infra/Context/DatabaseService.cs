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
            _retryPolicy = Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(2),
                    onRetry: (exception, timespan, retryCount, context) =>
                    {
                        _logger.LogWarning($"Retrying... Attempt {retryCount}");
                    });
        }

        public async Task<string> GetDataFromDatabaseAsync()
        {
            try
            {
                _logger.LogInformation("Trying to access the database...");
                return await _retryPolicy.ExecuteAsync(async () =>
                {
                    _logger.LogInformation("Attempting database operation...");
                    throw new Exception("Temporary DB failure!");
                    return "Database data successfully retrieved!";
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while accessing the database.");
                throw;
            }
        }
    }
}

