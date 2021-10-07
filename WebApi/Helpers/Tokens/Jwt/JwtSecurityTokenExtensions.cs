using System.IdentityModel.Tokens.Jwt;

namespace SBTestTask.WebApi.Helpers.Tokens.Jwt
{
    public static class JwtSecurityTokenExtensions
    {
        public static string AsString(this JwtSecurityToken token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
    }
}
