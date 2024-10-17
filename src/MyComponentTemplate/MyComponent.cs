using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
namespace MyComponentTemplate
{
    public class MyComponent
    {
        private readonly ILogger<MyComponent> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HealthCheckService _healthCheckService;

        public MyComponent(ILogger<MyComponent> logger, IHttpClientFactory httpClientFactory, HealthCheckService healthCheckService)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _healthCheckService = healthCheckService;  // Injeta o HealthCheckService
        }

        public async Task CheckHealthAsync()
        {
            var report = await _healthCheckService.CheckHealthAsync();
            foreach (var entry in report.Entries)
            {
                Console.WriteLine($"Service: {entry.Key}, Status: {entry.Value.Status}");
            }
        }

        public void DoWork()
        {
            _logger.LogInformation("Iniciando operação no MyComponent.");

            try
            {
                _logger.LogInformation("Operação em andamento...");
                _logger.LogInformation("Operação finalizada com sucesso.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro durante a operação.");
            }
        }

        public async Task DoWorkAsync()
        {
            _logger.LogInformation("Iniciando operação no MyComponent.");

            try
            {
                var httpClient = _httpClientFactory.CreateClient("ExternalAPI");
                var response = await httpClient.GetAsync("https://api.exemplo.com/dados");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    _logger.LogInformation($"Resposta recebida com sucesso: {content}");
                }
                else
                {
                    _logger.LogWarning($"Falha ao fazer a requisição: {response.StatusCode}");
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Erro durante a operação de rede.");
            }

            _logger.LogInformation("Operação finalizada no MyComponent.");
        }
    }


}
