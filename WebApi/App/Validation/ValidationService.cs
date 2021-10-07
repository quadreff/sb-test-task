using Microsoft.Extensions.Configuration;
using SBTestTask.WebApi.Common;
using SBTestTask.WebApi.Models;

namespace SBTestTask.WebApi.App.Validation
{
    public class ValidationService : IValidationService
    {
        public const string Separator = Constants.ConfigurationSeparator;
        public const string CredentialsPath = "TestCredentials";
        public const string UsernamePath = CredentialsPath + Separator + "Username";
        public const string PasswordPath = CredentialsPath + Separator + "Password";
        private readonly IConfiguration _configuration;

        public ValidationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Validate(AuthInfo authInfo)
        {
            var (username, password) = GetCredentialsFromConfig(_configuration);

            if ((username, password) != (authInfo.Name, authInfo.Password))
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