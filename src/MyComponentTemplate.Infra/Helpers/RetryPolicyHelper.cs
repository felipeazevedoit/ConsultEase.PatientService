using Polly;
using Polly.Retry;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

public static class RetryPolicyHelper
{
    public static AsyncRetryPolicy<HttpResponseMessage> CreateRetryPolicy(IConfiguration configuration, ILogger logger)
    {
        int retryCount = configuration.GetValue<int>("RetryPolicy:RetryCount");
        int backoffFactor = configuration.GetValue<int>("RetryPolicy:ExponentialBackoffFactor");

        return Policy
            .Handle<HttpRequestException>()
            .OrResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
            .WaitAndRetryAsync(retryCount, attempt =>
                TimeSpan.FromSeconds(Math.Pow(backoffFactor, attempt)),
                (result, timeSpan, retryAttempt, context) =>
                {
                    logger.LogWarning($"Falha na tentativa {retryAttempt}. Retentando em {timeSpan.TotalSeconds} segundos.");
                });
    }
}
