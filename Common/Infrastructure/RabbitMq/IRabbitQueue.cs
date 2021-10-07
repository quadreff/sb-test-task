using System;
using System.Collections.Generic;
using RabbitMQ.Client.Events;

namespace SBTestTask.Common.Infrastructure.RabbitMq
{
    public interface IRabbitQueue : IDisposable
    {
        public event Action<BasicDeliverEventArgs>? Received;
        void Setup(RabbitConnectionInfo connectionInfo, string queueName);
        void Publish(string routingKey, IEnumerable<byte> body);
        void Consume();
    }
}