using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddSerilog(dispose: true);
            });

            services.AddHttpClient("ExternalAPI")
            .AddPolicyHandler((serviceProvider, request) =>
            {
                var logger = serviceProvider.GetRequiredService<ILogger<MyComponent>>();

                return Policy<HttpResponseMessage>
                    .Handle<HttpRequestException>()
                    .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                    (exception, timeSpan, retryCount, context) =>
                    {
                        logger.LogWarning($"Attempt {retryCount} failed. Retrying in {timeSpan.TotalSeconds} seconds...");
                    });
            });




            services.AddTransient<MyComponent>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health");
            });


            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                    var logger = context.RequestServices.GetRequiredService<ILogger<Startup>>();

                    if (exceptionHandlerPathFeature != null)
                    {
                        var exception = exceptionHandlerPathFeature.Error;

                        logger.LogError(exception, "An unhandled exception occurred");

                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("An unexpected error occurred.");
                    }
                });
            });


        }
    }
}

