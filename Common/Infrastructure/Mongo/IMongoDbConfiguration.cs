namespace SBTestTask.Common.Infrastructure.Mongo
{
    public interface IMongoDbConfiguration
    {
        string GetConnectionString();
    }
}