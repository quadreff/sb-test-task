using Microsoft.Extensions.Configuration;

namespace SBTestTask.Common.Infrastructure.Mongo
{
    public class MongoDbConfiguration : IMongoDbConfiguration
    {
        public const string MongoDbPath = "MongoDb";
        private readonly string _connectionString;

        public MongoDbConfiguration(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString(MongoDbPath);
        }

        public string GetConnectionString()
        {
            return _connectionString;
        }
    }
}