using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;

namespace MyComponentTemplate.Infra
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IConfiguration Get_configuration()
        {
            return _configuration;
        }

        public void ConfigureServices(IServiceCollection services, IConfiguration _configuration)
        {
            // Suponha que você tenha uma connection string no appsettings.json
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            // Registrar o Health Check da sua Class Library
            services.AddDatabaseHealthCheck(connectionString);

            // Adicionando o endpoint para Health Checks
            services.AddHealthChecks();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Configura o endpoint de health checks
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health");
            });
        }
    }
}
