using Microsoft.Extensions.Configuration;


namespace MyComponentTemplate.Tests.TestConfig
{
    public class ConfigTests
    {
        private readonly IConfiguration _configuration;

        public ConfigTests()
        {
            // Carrega o TestSettings.json que está na raiz do projeto de testes
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("TestSettings.json", optional: false, reloadOnChange: true);

            _configuration = builder.Build();
        }

        [Fact]
        public void TestDefaultConnectionString()
        {
            // Testa se a connection string está carregada corretamente a partir do TestSettings.json
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            Assert.Equal("Server=(localdb)\\mssqllocaldb;Database=TestDatabase;Trusted_Connection=True;", connectionString);
        }
    }
}
