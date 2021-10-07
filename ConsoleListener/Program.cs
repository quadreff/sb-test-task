using System;
using System.Text;
using SBTestTask.Common.Infrastructure;
using SBTestTask.Common.Infrastructure.Mongo;
using SBTestTask.Common.Infrastructure.RabbitMq;
using SBTestTask.Common.Models;

namespace SBTestTask.ConsoleListener
{
    public class LocalMongoConfiguration : IMongoDbConfiguration
    {
        public string GetConnectionString()
        {
            return "mongodb://myuser:mypassword@localhost";
        }
    }

    public class Program
    {
        private static void Main()
        {
            var rabbitMqQueue = new RabbitQueue();
            var userRepository = new UserRepository(new MongoDbContext(new LocalMongoConfiguration()));
            rabbitMqQueue.Received += async eventArgs =>
            {
                var username = Encoding.UTF8.GetString(eventArgs.Body.ToArray());
                await userRepository.AddAsync(new User { Name = username });
            };

            rabbitMqQueue.Consume();
        }
    }
}
