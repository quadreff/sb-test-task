using Microsoft.Extensions.Configuration;
using SBTestTask.Common.Models;
using SBTestTask.WebApi.Common;

namespace SBTestTask.WebApi.App.Validation
{
    public class ValidationService : IValidationService
    {
        private readonly IConfiguration _configuration;

        public const string Separator = Constants.ConfigurationSeparator;
        public const string CredentialsPath = "TestCredentials";
        public const string UsernamePath = CredentialsPath + Separator + "Username";
        public const string PasswordPath = CredentialsPath + Separator + "Password";

        public ValidationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Validate(User user)
        {
            var (username, password) = GetCredentialsFromConfig(_configuration);

            if ((username, password) != (user.Name, user.Password))
            {
                throw new UnauthorizedException();
            }
        }

        private static (string Username, string Password) GetCredentialsFromConfig(IConfiguration configuration)
        {
            return (configuration[UsernamePath], configuration[PasswordPath]);
        }
    }
}