using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.IO;

namespace MyComponentTemplate.Infra
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configuration = BuildConfiguration();
            var loggerConfig = ConfigureLogger(configuration);

            Log.Logger = loggerConfig.CreateLogger();

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

        private static IConfiguration BuildConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
        }

        private static LoggerConfiguration ConfigureLogger(IConfiguration configuration)
        {
            var mongoConnectionString = configuration["MongoDB:ConnectionString"];
            var logCollection = "Logs";

            if (string.IsNullOrEmpty(mongoConnectionString))
                throw new ArgumentNullException(nameof(mongoConnectionString), "MongoDB connection string cannot be null or empty.");

            var loggerConfig = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console();

            try
            {
                loggerConfig.WriteTo.MongoDB(mongoConnectionString, collectionName: logCollection);
                Log.Information("MongoDB logging configured successfully.");
            }
            catch (Exception ex)
            {
                loggerConfig.WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day);
                Log.Warning(ex, "Failed to connect to MongoDB. Falling back to file logging.");
            }

            return loggerConfig;
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureAppConfiguration((context, config) =>
                {
                    var env = context.HostingEnvironment;

                    config.SetBasePath(Directory.GetCurrentDirectory())
                          .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                          .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                          .AddEnvironmentVariables();
                })
                .ConfigureServices((context, services) =>
                {
                    // Verifique se a classe DatabaseService existe, ou remova essa linha
                    // services.AddTransient<DatabaseService>();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
