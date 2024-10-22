using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;


public class SqlServerConnectionTests
{
    private readonly string _sqlConnectionString;

    public SqlServerConnectionTests()
    {
        // Carrega as configurações a partir do appsettings.json
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        _sqlConnectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
    }

    [Fact]
    public void Can_Connect_To_SqlServer()
    {
        using var connection = new SqlConnection(_sqlConnectionString);
        connection.Open();

        Assert.Equal(System.Data.ConnectionState.Open, connection.State); // O teste passa se a conexão foi aberta com sucesso
    }
}
