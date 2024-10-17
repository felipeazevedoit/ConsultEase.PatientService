using Moq;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Net;

public static class TestHelpers
{
    // Helper para criar mock de ILogger<T>
    public static Mock<ILogger<T>> CreateLoggerMock<T>()
    {
        return new Mock<ILogger<T>>();
    }

    // Helper para configurar mock de HttpClientFactory
    public static Mock<IHttpClientFactory> CreateHttpClientFactoryMock(HttpResponseMessage responseMessage)
    {
        var httpClientFactoryMock = new Mock<IHttpClientFactory>();
        var httpClient = new HttpClient(new FakeHttpMessageHandler(responseMessage)) { BaseAddress = new Uri("https://fakeapi.com") };
        httpClientFactoryMock.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClient);

        return httpClientFactoryMock;
    }

    // Helper para criar uma resposta mock do HttpClient
    public static HttpResponseMessage CreateHttpResponseMessage(string content, HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        return new HttpResponseMessage
        {
            StatusCode = statusCode,
            Content = new StringContent(content)
        };
    }

    // Helper para criar mock de HealthCheckService
    public static Mock<HealthCheckService> CreateHealthCheckServiceMock()
    {
        var healthCheckServiceMock = new Mock<HealthCheckService>();
        // Aqui, você pode configurar comportamentos do HealthCheckService se necessário
        return healthCheckServiceMock;
    }
}

// FakeHttpMessageHandler é necessário para simular chamadas do HttpClient
public class FakeHttpMessageHandler : HttpMessageHandler
{
    private readonly HttpResponseMessage _responseMessage;

    public FakeHttpMessageHandler(HttpResponseMessage responseMessage)
    {
        _responseMessage = responseMessage;
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_responseMessage);
    }
}
