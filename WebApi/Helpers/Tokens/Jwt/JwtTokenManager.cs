using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace SBTestTask.WebApi.Helpers.Tokens.Jwt
{
    public class JwtTokenManager : IJwtTokenManager
    {
        private readonly IJwtConfiguration _configuration;

        public JwtTokenManager(IJwtConfiguration configuration)
        {
            _configuration = configuration;
        }

        public JwtSecurityToken GenerateToken(string username)
        {
            // get token specs here to make token more flexible later on
            var tokenSpecs = _configuration.Get();
            var key = GetSymmetricKey(tokenSpecs.Secret);
            var timeNow = DateTime.Now;
            var usedAlgorithm = SecurityAlgorithms.HmacSha256Signature;

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {new Claim(ClaimTypes.Name, username)}),
                Expires = timeNow + tokenSpecs.Lifetime,
                SigningCredentials = new SigningCredentials(key, usedAlgorithm),
                Issuer = tokenSpecs.Issuer,
                Audience = tokenSpecs.Audience
            };

            return new JwtSecurityTokenHandler().CreateJwtSecurityToken(tokenDescriptor);
        }

        private static SymmetricSecurityKey GetSymmetricKey(string secret)
        {
            return new SymmetricSecurityKey(Convert.FromBase64String(secret));
        }
    }
}