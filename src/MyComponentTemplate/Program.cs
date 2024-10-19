using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MyComponentTemplate.Infra.Context;
using MyComponentTemplate.Infra.MongoDb;
using Polly;
using Serilog;

namespace MyComponentTemplate
{
    public class Program
    {
        private static Microsoft.Extensions.Configuration.IConfiguration Configuration { get; set; }  // Especifica o namespace correto
        private static string SqlConnectionString { get; set; }
        private static string MySqlConnectionString { get; set; }
        private static MongoDbSettings MongoDbSettings { get; set; }

        public static void Main(string[] args)
        {
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
                    // Carregar variáveis como propriedades
                    SqlConnectionString = Configuration.GetConnectionString("DefaultConnection");
                    MySqlConnectionString = Configuration.GetConnectionString("mySqlConnectionString");
                    MongoDbSettings = Configuration.GetSection("MongoDB").Get<MongoDbSettings>();

                    ConfigureDatabases(services);
                    ConfigureLogging(services);
                    ConfigureHealthChecks(services);
                    ConfigurePolly(services);

                    //services.AddTransient<IMyComponentService, MyComponentService>();
                    //services.AddTransient<MyComponent>();  // Registrar o MyComponent
                })
                .UseSerilog();  // Configura Serilog como logger

        private static Microsoft.Extensions.Configuration.IConfiguration ConfigureAppSettings(HostBuilderContext context, IConfigurationBuilder config)
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
            // SQL Server
            services.AddDbContext<MyComponentDbContext>(options =>
                options.UseSqlServer(SqlConnectionString));

            // MongoDB
            services.AddSingleton<IMongoClient>(sp => new MongoClient(MongoDbSettings.ConnectionString));
            services.AddScoped<IMongoDatabase>(sp =>
            {
                var client = sp.GetRequiredService<IMongoClient>();
                return client.GetDatabase(MongoDbSettings.DatabaseName);
            });

            // MySQL
            services.AddDbContext<Infra.MySql.MyComponentDbContext>(options =>
                options.UseMySql(MySqlConnectionString, Microsoft.EntityFrameworkCore.ServerVersion.AutoDetect(MySqlConnectionString)));
        }

        private static void ConfigureLogging(IServiceCollection services)
        {
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddSerilog();
            });
        }

        private static void ConfigureHealthChecks(IServiceCollection services)
        {
            services.AddHealthChecks();
            services.AddSingleton<HealthCheckService>();
        }

        private static void ConfigurePolly(IServiceCollection services)
        {
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
