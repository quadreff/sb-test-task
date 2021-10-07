using MongoDB.Driver;

namespace SBTestTask.Common.Infrastructure.Mongo
{
    public class MongoDbContext : IMongoDbContext
    {
        private readonly MongoClient? _client;
        private readonly IMongoDatabase? _database;

        public MongoDbContext(IMongoDbConfiguration configuration)
        {
            _client = new MongoClient(configuration.GetConnectionString());
            _database = _client.GetDatabase(Constants.MongoDbName);
        }

        public IMongoCollection<T>? GetCollection<T>(string name)
        {
            return _database?.GetCollection<T>(name);
        }
    }
}