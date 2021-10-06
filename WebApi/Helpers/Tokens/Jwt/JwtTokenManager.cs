using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;

namespace SBTestTask.WebApi.Helpers.Tokens.Jwt
{
    public class JwtTokenManager : ITokenManager<JwtSecurityToken>
    {
        private readonly IConfiguration _configuration;

        public JwtTokenManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public JwtSecurityToken GenerateToken(string username)
        {
        }
    }
}