using System.IdentityModel.Tokens.Jwt;

namespace SBTestTask.WebApi.Helpers.Tokens.Jwt
{
    public interface IJwtTokenManager
    {
        JwtSecurityToken GenerateToken(string username);
    }
}