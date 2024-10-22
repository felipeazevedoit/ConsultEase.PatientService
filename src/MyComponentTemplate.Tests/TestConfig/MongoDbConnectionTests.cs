using MongoDB.Driver;
using Xunit;
using Microsoft.Extensions.Configuration;
using System.IO;

public class MongoDbConnectionTests
{
    private readonly string _mongoConnectionString;
    private readonly string _mongoDatabase;

    public MongoDbConnectionTests()
    {
        // Carrega as configurações a partir do appsettings.json
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        _mongoConnectionString = configuration["MongoDB:ConnectionString"];
        _mongoDatabase = configuration["MongoDB:Database"];
    }

    [Fact]
    public void Can_Connect_To_MongoDB()
    {
        var client = new MongoClient(_mongoConnectionString);
        var database = client.GetDatabase(_mongoDatabase);

        // Verifica se a conexão foi estabelecida ao tentar listar as coleções
        var collections = database.ListCollectionNames().ToList();

        Assert.NotNull(collections);  // O teste passa se a conexão foi bem-sucedida
    }
}
