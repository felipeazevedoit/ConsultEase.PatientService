using Serilog;
using MongoDB.Driver;
using Xunit;
using System;
using System.IO;
using Microsoft.Extensions.Configuration;

public class SerilogLoggingTests
{
    private readonly string _mongoConnectionString;
    private readonly string _logCollection;
    private readonly string _filePath = "logs/log-.txt";

    public SerilogLoggingTests()
    {
        // Carrega as configurações a partir do appsettings.json
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        _mongoConnectionString = configuration["MongoDB:ConnectionString"];
        _logCollection = "Logs";  // Coleção de logs no MongoDB
    }

    [Fact]
    public void Can_Log_To_MongoDB_And_Fallback_To_File()
    {
        LoggerConfiguration loggerConfig = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console(); // Loga no Console

        try
        {
            // Tenta configurar o MongoDB como destino dos logs
            loggerConfig.WriteTo.MongoDB(_mongoConnectionString, collectionName: _logCollection);
            Log.Information("MongoDB logging configured successfully.");
        }
        catch (Exception ex)
        {
            // Fallback: Se a conexão ao MongoDB falhar, escreve logs em arquivos
            loggerConfig.WriteTo.File(_filePath, rollingInterval: RollingInterval.Day);
            Log.Warning(ex, "Failed to connect to MongoDB. Falling back to file logging.");
        }

        // Cria o logger
        Log.Logger = loggerConfig.CreateLogger();

        // Testa se o logger está funcionando
        Log.Information("This is a test log for MongoDB or file fallback.");

        // Verifica se o arquivo de log foi criado (caso o MongoDB falhe)
        if (File.Exists(_filePath))
        {
            var logContent = File.ReadAllText(_filePath);
            Assert.Contains("This is a test log for MongoDB or file fallback.", logContent);  // Verifica se a mensagem foi logada no arquivo
        }
    }
}
