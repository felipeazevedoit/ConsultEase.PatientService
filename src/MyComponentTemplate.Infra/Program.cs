using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using MyComponentTemplate.Infra.Context;
using MyComponentTemplate.Infra.MongoDb;
using Serilog;
using Serilog.Extensions.Logging;

namespace MyComponentTemplate.Infra
{
    public class Program
    {
        // Propriedades para armazenar strings de conexão e configurações
        private static IConfiguration Configuration { get; set; }
        private static string SqlConnectionString { get; set; }
        private static string MySqlConnectionString { get; set; }
        private static MongoDbSettings MongoDbSettings { get; set; }

        public static void Main(string[] args)
        {
            // Configurando o logger Serilog
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
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

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {
                    var env = context.HostingEnvironment;

                    config.SetBasePath(Directory.GetCurrentDirectory())
                          .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                          .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                          .AddEnvironmentVariables();

                    Configuration = config.Build();
                })
                .ConfigureServices((context, services) =>
                {
                    // Obtendo strings de conexão
                    SqlConnectionString = Configuration.GetConnectionString("DefaultConnection");
                    MySqlConnectionString = Configuration.GetConnectionString("mySqlConnectionString");
                    MongoDbSettings = Configuration.GetSection("MongoDB").Get<MongoDbSettings>();

                    // Configuração dos serviços
                    ConfigureDatabases(services);
                    ConfigureLogging(services);

                    // Exemplo de registro de outros serviços
                    services.AddTransient<DatabaseService>();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    // Configurar o Startup da aplicação
                    webBuilder.UseStartup<Startup>();
                });
        }

        // Métodos para manter o código organizado, mas na mesma classe
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
            services.AddDbContext<MySql.MyComponentDbContext>(options =>
                options.UseMySql(MySqlConnectionString, Microsoft.EntityFrameworkCore.ServerVersion.AutoDetect(MySqlConnectionString)));
        }

        private static void ConfigureLogging(IServiceCollection services)
        {
            // Adiciona o serviço de logging
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddSerilog();  // Certifique-se de que o pacote Serilog.Extensions.Logging está instalado
            });
        }

    }
}
