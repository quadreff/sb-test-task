using System;
using Microsoft.Extensions.Configuration;
using SBTestTask.Common.Infrastructure.RabbitMq;
using SBTestTask.WebApi.Common;

namespace SBTestTask.WebApi.Helpers.RabbitMq
{
    public class RabbitMqConfiguration : IRabbitMqConfiguration
    {
        public const string Separator = Constants.ConfigurationSeparator;
        public const string RabbitMqPath = "RabbitMq";
        public const string HostPath = RabbitMqPath + Separator + "Host";
        public const string PortPath = RabbitMqPath + Separator + "Port";
        public const string UsernamePath = RabbitMqPath + Separator + "Username";
        public const string PasswordPath = RabbitMqPath + Separator + "Password";

        private readonly RabbitConnectionInfo _connectionInfo;

        public RabbitMqConfiguration(IConfiguration configuration)
        {
            // made for simplicity, logic can be moved to get the config params on-the-fly.
            _connectionInfo = new RabbitConnectionInfo(configuration[UsernamePath],
                configuration[PasswordPath], configuration[HostPath], Convert.ToInt32(configuration[PortPath]));
        }

        public RabbitConnectionInfo Get()
        {
            return _connectionInfo;
        }
    }
}