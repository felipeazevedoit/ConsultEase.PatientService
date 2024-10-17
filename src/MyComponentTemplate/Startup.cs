using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyComponentTemplate.Interfaces;
using Polly;
using Serilog;
namespace MyComponentTemplate
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Registrar o Logger (ex: Serilog)
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddSerilog(dispose: true); // Certifique-se de que o Serilog está configurado
            });

            // Registrar Polly com uma política de Retry
            services.AddHttpClient("ExternalAPI")
                .AddPolicyHandler((IAsyncPolicy<HttpResponseMessage>)Polly.Policy.Handle<HttpRequestException>()
                    .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                    (exception, timeSpan, retryCount, context) =>
                    {
                        // Log de erro ao tentar
                        var logger = services.BuildServiceProvider().GetRequiredService<ILogger<MyComponent>>();
                        logger.LogWarning($"Tentativa {retryCount} falhou. Tentando novamente...");
                    }));

            // Registrar o componente que vai usar Logger e Polly
            services.AddTransient<MyComponent>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Configuração do health check endpoint
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health");
            });
        }
    }
}

