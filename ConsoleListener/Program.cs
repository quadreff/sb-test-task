using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SBTestTask.Common.Infrastructure;
using SBTestTask.Common.Infrastructure.Mongo;
using SBTestTask.Common.Infrastructure.RabbitMq;
using SBTestTask.Common.Logging;
using SBTestTask.Common.Models;
using Serilog;
using Serilog.Core;
using Constants = SBTestTask.Common.Constants;
using Log = SBTestTask.Common.Logging.Log;

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
        private static readonly LoggerConfiguration LogConfiguration = new LoggerConfiguration();
        private static Logger _logger;

        private static void SetupLogger()
        {
            _logger = LogConfiguration
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.Debug()
                .CreateLogger();

            // Depend upon log abstraction
            Log.Setup((severity, s) =>
            {
                switch (severity)
                {
                    case LogSeverity.Error:
                        _logger.Error(s);
                        break;
                    case LogSeverity.Info:
                        _logger.Information(s);
                        break;
                    case LogSeverity.Trace:
                        _logger.Debug(s);
                        break;
                    default: throw new ArgumentException("Invalid log level");
                }
            });
        }

        private static void Main()
        {
            SetupLogger();
            var rabbitMqQueue = new RabbitQueue();
            rabbitMqQueue.Setup(new RabbitConnectionInfo("myuser", "mypassword", "localhost"), Constants.RabbitQueueName);
            var userRepository = new UserRepository(new MongoDbContext(new LocalMongoConfiguration()));
            rabbitMqQueue.Received += async eventArgs =>
            {
                var username = Encoding.UTF8.GetString(eventArgs.Body.ToArray());
                Log.Trace($"Received {username}");
                await userRepository.AddAsync(new User { Name = username });
                Log.Trace($"Writing {username} to db");
            };

            var cancellationTokenSource = new CancellationTokenSource();
            Task.Run(async () => await ConsumeRoutineAsync(rabbitMqQueue, cancellationTokenSource.Token));

            Console.WriteLine("Press any button to finish...");
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
