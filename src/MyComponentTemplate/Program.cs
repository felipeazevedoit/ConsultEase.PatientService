using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Polly;
using Serilog;

namespace MyComponentTemplate
{
    public class Program
    {
        private static IConfiguration Configuration { get; set; }
        private static string SqlConnectionString { get; set; }

        public static void Main(string[] args)
        {
            // Configurando Serilog e carregando a configuração de appsettings.json
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            SqlConnectionString = Configuration.GetConnectionString("DefaultConnection")
                                  ?? throw new InvalidOperationException("SqlConnectionString is null");

            // Configuração do logger Serilog
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            try
            {
                Log.Information("Starting application");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {
                    Configuration = ConfigureAppSettings(context, config);
                })
                .ConfigureServices((context, services) =>
                {
                    ConfigureDatabases(services);
                    ConfigureLogging(services);
                    ConfigureHealthChecks(services);
                    ConfigurePolly(services);

                    // Registrar outros serviços, como MyComponent
                    // services.AddTransient<IMyComponentService, MyComponentService>();
                })
                .UseSerilog();  // Configura Serilog como logger

        private static IConfiguration ConfigureAppSettings(HostBuilderContext context, IConfigurationBuilder config)
        {
            var env = context.HostingEnvironment;
            config.SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                  .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                  .AddEnvironmentVariables();  // Adiciona variáveis de ambiente

            return config.Build();
        }

        private static void ConfigureDatabases(IServiceCollection services)
        {
            // Configura o banco de dados SQL Server
            services.AddDbContext<DbContext>(options =>
                options.UseSqlServer(SqlConnectionString));
        }

        private static void ConfigureLogging(IServiceCollection services)
        {
            // Adiciona Serilog como provedor de logging
            services.AddLogging(loggingBuilder => { loggingBuilder.AddSerilog(); });
        }

        private static void ConfigureHealthChecks(IServiceCollection services)
        {
            // Configura health checks
            services.AddHealthChecks();
            services.AddSingleton<HealthCheckService>();
        }

        private static void ConfigurePolly(IServiceCollection services)
        {
            // Configura o Polly para gerenciamento de resiliência com política de retry
            services.AddHttpClient("ExternalAPI")
                .AddPolicyHandler((IAsyncPolicy<HttpResponseMessage>)Policy.Handle<HttpRequestException>()
                    .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                    (exception, timeSpan, retryCount, context) =>
                    {
                        var logger = services.BuildServiceProvider().GetRequiredService<ILogger<Program>>();
                        logger.LogWarning($"Tentativa {retryCount} falhou. Tentando novamente em {timeSpan.TotalSeconds} segundos.");
                    }));
        }
    }
}
