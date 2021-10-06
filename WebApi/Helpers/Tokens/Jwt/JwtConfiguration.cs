﻿using Microsoft.Extensions.Configuration;

namespace SBTestTask.WebApi.Helpers.Tokens.Jwt
{
    public class JwtConfiguration : IJwtConfiguration
    {
        // leave it public for reference, keep it in constants in case the naming will change
        public const string Separator = ":";
        public const string TokenPath = "Token";
        public const string JwtPath = TokenPath + Separator + "Jwt";
        public const string SecretPath = JwtPath + Separator + "Secret";
        public const string IssuerPath = JwtPath + Separator + "Issuer";
        public const string AudiencePath = JwtPath + Separator + "AudiencePath";
        public const string LifeTimePath = JwtPath + Separator + "LifetimeInMinutes";

        private TokenSpecs _tokenSpecs;
        public JwtConfiguration(IConfiguration configuration)
        {
        }

        public TokenSpecs Get()
        {
            throw new System.NotImplementedException();
        }
    }
}