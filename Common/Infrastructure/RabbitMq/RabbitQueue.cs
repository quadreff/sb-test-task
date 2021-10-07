using System;
using System.Collections.Generic;
using System.Linq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace SBTestTask.Common.Infrastructure.RabbitMq
{
    public sealed class RabbitQueue : IRabbitQueue
    {
        private IModel? _channel;
        private IConnection? _connection;
        private EventingBasicConsumer? _consumer;

        private string? _queueName;
        public event Action<BasicDeliverEventArgs>? Received;

        // setup is simplified due to time constraints, no validation, can be a whole factory
        public void Setup(RabbitConnectionInfo connectionInfo, string queueName)
        {
            _channel?.QueuePurge(Constants.RabbitQueueName);
            _channel?.Dispose();
            _connection?.Dispose();

            _connection = new ConnectionFactory
            {
                HostName = connectionInfo.HostName,
                Port = connectionInfo.Port,
                UserName = connectionInfo.UserName,
                Password = connectionInfo.Password
            }.CreateConnection();
            _channel = _connection.CreateModel();
            _consumer = new EventingBasicConsumer(_channel);

            _consumer.Received += (sender, args) => Received?.Invoke(args);
            _queueName = queueName;
            _channel.QueueDeclare(_queueName, exclusive: false, durable: false);
        }

        public void Consume()
        {
            _channel?.BasicConsume(_queueName, true, _consumer);
        }

        public void Dispose()
        {
            _channel?.Dispose();
            _connection?.Dispose();
        }

        public void Publish(string routingKey, IEnumerable<byte> body)
        {
            _channel?.BasicPublish(string.Empty, routingKey, body: body.ToArray());
        }
    }
}