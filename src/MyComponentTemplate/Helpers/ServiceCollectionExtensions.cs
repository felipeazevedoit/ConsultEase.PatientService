using Azure.Storage.Blobs;
using Microsoft.Extensions.DependencyInjection;
using MyComponentTemplate.Entities;
using MyComponentTemplate.Interfaces;

namespace FosforosFramework.Components
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMyComponent(this IServiceCollection services)
        {
            // Registro do componente
            services.AddTransient<IMyComponentOptions, MyComponentOptions>();

            // Se precisar de algum serviço adicional, como um BlobClient, você pode configurar aqui
            services.AddSingleton(x =>
            {
                var config = x.GetRequiredService<IConfiguration>();
                var connectionString = config.GetValue<string>("AzureBlobStorage:ConnectionString");
                return new BlobServiceClient(connectionString);
            });

            return services;
        }
    }
}
