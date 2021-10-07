namespace SBTestTask.Common.Infrastructure.RabbitMq
{
    public class RabbitConnectionInfo
    {
        public string UserName { get; }
        public string Password { get; }
        public string HostName { get; }
        public int Port { get; }

        public RabbitConnectionInfo(string userName, string password, string hostName, int port = 5672)
            => (UserName, Password, HostName, Port) = (userName, password, hostName, port);
    }
}