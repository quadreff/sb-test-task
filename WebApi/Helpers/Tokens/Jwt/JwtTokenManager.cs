﻿using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;

namespace SBTestTask.WebApi.Helpers.Tokens.Jwt
{
    public class JwtTokenManager : ITokenManager<JwtSecurityToken>
    {
        private readonly IJwtConfiguration _configuration;

        public JwtTokenManager(IJwtConfiguration configuration)
        {
            _configuration = configuration;
        }

        public JwtSecurityToken GenerateToken(string username)
        {
            return new JwtSecurityToken();
        }
    }
}