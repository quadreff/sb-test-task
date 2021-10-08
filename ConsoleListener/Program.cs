using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SBTestTask.Common;
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
            rabbitMqQueue.Setup(new RabbitConnectionInfo("myuser", "mypassword", "localhost"), Constants.RabbitQueueName);
            var userRepository = new UserRepository(new MongoDbContext(new LocalMongoConfiguration()));
            rabbitMqQueue.Received += async eventArgs =>
            {
                var username = Encoding.UTF8.GetString(eventArgs.Body.ToArray());
                await userRepository.AddAsync(new User { Name = username });
            };
            var cancellationTokenSource = new CancellationTokenSource();
            Task.Run(async () => await ConsumeRoutineAsync(rabbitMqQueue, cancellationTokenSource.Token));

            Console.ReadLine();
            cancellationTokenSource.Cancel();
        }

        private static async Task ConsumeRoutineAsync(IRabbitQueue rabbitQueue, CancellationToken token)
        {
            const int timeoutMs = 500;
            while (!token.IsCancellationRequested)
            {
                rabbitQueue.Consume();
                await Task.Delay(timeoutMs, token);
            }
        }
    }
}
