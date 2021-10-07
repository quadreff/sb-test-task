using SBTestTask.Common.Infrastructure.RabbitMq;

namespace SBTestTask.WebApi.Helpers.RabbitMq
{
    public interface IRabbitMqConfiguration
    {
        RabbitConnectionInfo Get();
    }
}