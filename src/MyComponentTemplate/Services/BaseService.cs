using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Memory;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

public abstract class BaseService
{
    protected readonly ILogger<BaseService> _logger;
    protected readonly IHttpClientFactory _httpClientFactory;
    protected readonly HealthCheckService _healthCheckService;
    protected readonly IConfiguration _configuration;
    protected readonly IMemoryCache _memoryCache;

    private readonly RabbitMqHelper _rabbitMqHelper;
    private readonly RedisHelper _redisHelper;
    private readonly MongoDbHelper _mongoDbHelper;
    private readonly SqlHelper _sqlHelper;

    protected BaseService(ILogger<BaseService> logger, IHttpClientFactory httpClientFactory, HealthCheckService healthCheckService, IConfiguration configuration, IMemoryCache memoryCache)
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;
        _healthCheckService = healthCheckService;
        _configuration = configuration;
        _memoryCache = memoryCache;

        // Inicializando os Helpers
        _rabbitMqHelper = new RabbitMqHelper(configuration, logger);
        _redisHelper = new RedisHelper(configuration, logger);
        _mongoDbHelper = new MongoDbHelper(configuration, logger);
        _sqlHelper = new SqlHelper(configuration, logger);
    }

    public virtual async Task CheckHealthAsync()
    {
        var retryPolicy = RetryPolicyHelper.CreateRetryPolicy(_configuration, _logger);

        await retryPolicy.ExecuteAsync(async () =>
        {
            try
            {
                var report = await _healthCheckService.CheckHealthAsync();
                foreach (var entry in report.Entries)
                {
                    _logger.LogInformation(_configuration["Logging:Messages:StartOperation"].Replace("{component}", nameof(BaseService)));
                }
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK); // Retorna um HttpResponseMessage válido
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _configuration["Logging:Messages:ErrorDuringOperation"]);
                return new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError); // Retorna um HttpResponseMessage válido
            }
        });
    }

    // Exemplo de uso dos Helpers
    protected void ConnectToSqlServer()
    {
        _sqlHelper.Connect();
    }

    protected void ConnectToMongoDb()
    {
        _mongoDbHelper.Connect();
    }

    protected void ConfigureRabbitMq()
    {
        _rabbitMqHelper.Configure();
    }

    protected void ConnectToRedis()
    {
        _redisHelper.Connect();
    }

    // Exemplo de armazenamento no InMemory Cache
    protected void SetInMemoryCache<T>(string key, T value)
    {
        var cacheOptions = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_configuration.GetValue<int>("InMemory:ExpirationMinutes"))
        };
        _memoryCache.Set(key, value, cacheOptions);
    }

    // Exemplo de recuperação de dados do InMemory Cache
    protected T GetFromInMemoryCache<T>(string key)
    {
        _memoryCache.TryGetValue(key, out T value);
        return value;
    }
}
