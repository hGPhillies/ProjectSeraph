using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ProjectSeraphBackend.InterfaceAdapters.RepositoryImplementations
{
    public class CitizenRepository
    {
        public string ConnectionString { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;

        private readonly IMongoDatabase _database;

        public MongoDbContext(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.DatabaseName);
        }

        public IMongoCollection<Citizen> Citizens => _database.GetCollection<Citizen>("citizens");



    }
}
