//using Microsoft.Extensions.Diagnostics.HealthChecks;
//using Microsoft.Extensions.Logging;
//using Moq;

//namespace MyComponentTemplate.Tests.IntegrationTests
//{
//    public class MyComponentIntegrationTests
//    {
//        private readonly Mock<IHttpClientFactory> _httpClientFactoryMock;
//        private readonly Mock<ILogger<BaseService>> _loggerMock;
//        private readonly Mock<HealthCheckService> _healthCheckServiceMock;  // Adiciona mock para HealthCheckService
//        private readonly MyComponent _myComponent;

//        public MyComponentIntegrationTests()
//        {
//            _httpClientFactoryMock = new Mock<IHttpClientFactory>();
//            _loggerMock = new Mock<ILogger<MyComponent>>();
//            _healthCheckServiceMock = new Mock<HealthCheckService>();  // Instancia o mock para HealthCheckService

//            // Mock HttpClientFactory para simular requisições
//            var httpClient = new HttpClient { BaseAddress = new Uri("https://fakeapi.com") };
//            _httpClientFactoryMock.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);

//            // Instancia o MyComponent com o mock de HttpClient, Logger e HealthCheckService
//            _myComponent = new MyComponent(_loggerMock.Object, _httpClientFactoryMock.Object, _healthCheckServiceMock.Object);
//        }

//        [Fact]
//        public async Task DoWorkAsync_Should_Log_And_Call_ExternalAPI()
//        {
//            // Simula uma chamada ao método que deve usar HttpClient e logger
//            await _myComponent.DoWorkAsync();

//            // Verifica se o logger foi chamado com sucesso
//            _loggerMock.Verify(x => x.LogInformation(It.IsAny<string>()), Times.AtLeastOnce());

//            // Verifica se o HttpClient foi chamado ao menos uma vez
//            _httpClientFactoryMock.Verify(x => x.CreateClient(It.IsAny<string>()), Times.Once);
//        }
//    }
//}
