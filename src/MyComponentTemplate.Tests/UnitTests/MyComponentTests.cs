//using Moq;
//using Microsoft.Extensions.Logging;
//using Microsoft.Extensions.Diagnostics.HealthChecks;

//namespace MyComponentTemplate.Tests
//{
//    public class MyComponentTests
//    {
//        private readonly Mock<IHttpClientFactory> _httpClientFactoryMock;
//        private readonly Mock<ILogger<BaseService>> _loggerMock;
//        private readonly Mock<HealthCheckService> _healthCheckServiceMock;
//        private readonly MyComponent _myComponent;

//        public MyComponentTests()
//        {
//            _httpClientFactoryMock = new Mock<IHttpClientFactory>();
//            _loggerMock = new Mock<ILogger<BaseService>>();
//            _healthCheckServiceMock = new Mock<HealthCheckService>();

//            // Instancia o MyComponent com o mock de HttpClient, Logger e HealthCheckService
//            _myComponent = new MyComponent(_loggerMock.Object, _httpClientFactoryMock.Object, _healthCheckServiceMock.Object);
//        }

//        [Fact]
//        public void DoWork_Should_Log_CorrectMessages()
//        {
//            // Act
//            _myComponent.DoWork();

//            // Assert
//            // Verifica se o log "Iniciando operação no MyComponent." foi chamado
//            _loggerMock.Verify(
//                x => x.Log(
//                    It.Is<LogLevel>(logLevel => logLevel == LogLevel.Information),
//                    It.IsAny<EventId>(),
//                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Iniciando operação no MyComponent.")),
//                    It.IsAny<Exception>(),
//                    It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)),
//                Times.Once);

//            // Verifica se o log "Operação finalizada com sucesso." foi chamado
//            _loggerMock.Verify(
//                x => x.Log(
//                    It.Is<LogLevel>(logLevel => logLevel == LogLevel.Information),
//                    It.IsAny<EventId>(),
//                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Operação finalizada com sucesso.")),
//                    It.IsAny<Exception>(),
//                    It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)),
//                Times.Once);
//        }
//    }

//}
