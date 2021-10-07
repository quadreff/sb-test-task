using System;
using Microsoft.Extensions.Configuration;
using SBTestTask.WebApi.Common;

namespace SBTestTask.WebApi.Helpers.Tokens.Jwt
{
    public class JwtConfiguration : IJwtConfiguration
    {
        // leave it public for reference, keep it in constants in case the naming will change
        public const string Separator = Constants.ConfigurationSeparator;
        public const string TokenPath = "Token";
        public const string JwtPath = TokenPath + Separator + "Jwt";
        public const string SecretPath = JwtPath + Separator + "Secret";
        public const string IssuerPath = JwtPath + Separator + "Issuer";
        public const string AudiencePath = JwtPath + Separator + "Audience";
        public const string LifeTimePath = JwtPath + Separator + "LifetimeInMinutes";

        private readonly TokenSpecs _tokenSpecs;

        public JwtConfiguration(IConfiguration configuration)
        {
            var secret = configuration[SecretPath];
            var issuer = configuration[IssuerPath];
            var audience = configuration[AudiencePath];
            var lifeTimeInMinutes = Convert.ToInt32(configuration[LifeTimePath]);

            // made for simplicity, logic can be moved to get the config params on-the-fly.
            _tokenSpecs = new TokenSpecs(secret, issuer, audience, TimeSpan.FromMinutes(lifeTimeInMinutes));
        }

        public TokenSpecs Get()
        {
            return _tokenSpecs;
        }
    }
}