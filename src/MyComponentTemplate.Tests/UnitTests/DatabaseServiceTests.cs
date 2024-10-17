using System;
using System.Threading.Tasks;
using Moq;
using Xunit;
using Microsoft.Extensions.Logging;
using MyComponentTemplate.Helpers;
using Polly;

namespace MyComponentTemplate.Tests.UnitTests
{
    public class DatabaseServiceTests
    {
        private readonly Mock<ILogger<DatabaseService>> _loggerMock;
        private readonly DatabaseService _databaseService;

        public DatabaseServiceTests()
        {
            // Criar um mock do logger
            _loggerMock = new Mock<ILogger<DatabaseService>>();

            // Criar uma instância do DatabaseService com o logger mockado
            _databaseService = new DatabaseService(_loggerMock.Object);
        }

        [Fact]
        public async Task GetDataFromDatabaseAsync_Should_Log_Retry()
        {
            // Simula o erro para acionar o Polly
            await Assert.ThrowsAsync<Exception>(async () =>
            {
                await _databaseService.GetDataFromDatabaseAsync();
            });

            // Verifica se o logger registrou as tentativas de retry
            _loggerMock.Verify(
                x => x.Log(
                    It.Is<LogLevel>(logLevel => logLevel == LogLevel.Information),
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Retrying")),
                    It.IsAny<Exception>(),
                    It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)),
                Times.Exactly(3)); // Verifica se foi logado 3 vezes, de acordo com a política de retry
        }
    }
}
