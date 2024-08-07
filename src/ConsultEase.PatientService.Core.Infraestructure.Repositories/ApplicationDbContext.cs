using AspNetCore.Identity.MongoDbCore.Infrastructure;
using AspNetCore.Identity.MongoDbCore.Models;
using MongoDB.Driver;
using Microsoft.Extensions.Options;

namespace ConsultEase.PatientService.Core.Infraestructure.Repositories
{
    public class ApplicationDbContext
    {
        private readonly IMongoDatabase _database = null;

        public ApplicationDbContext(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                _database = client.GetDatabase(settings.Value.DatabaseName);
        }

        //public IMongoCollection<ApplicationUser> Users => _database.GetCollection<ApplicationUser>("Users");
        //public IMongoCollection<ApplicationRole> Roles => _database.GetCollection<ApplicationRole>("Roles");

    }
}
