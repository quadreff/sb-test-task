using Microsoft.Extensions.Configuration;
using SBTestTask.WebApi.Common;

namespace SBTestTask.WebApi.App.Validation
{
    public class ValidationService : IValidationService
    {
        private readonly IConfiguration _configuration;

        public const string Separator = Constants.ConfigurationSeparator;
        private const string CredentialsPath = "TestCredentials";
        private const string UsernamePath = CredentialsPath + Separator + "Username";
        private const string PasswordPath = CredentialsPath + Separator + "Password";

        public ValidationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Validate(string username, string password)
        {
        }

        private static (string Username, string Password) GetCredentialsFromConfig(IConfiguration configuration)
        {
            return ("test", "test");
        }
    }
}