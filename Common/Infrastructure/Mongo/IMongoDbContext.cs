using MongoDB.Driver;

namespace SBTestTask.Common.Infrastructure.Mongo
{
    public interface IMongoDbContext
    {
        IMongoCollection<T>? GetCollection<T>(string name);
    }
}