using System;

namespace SBTestTask.WebApi.Helpers.Tokens.Jwt
{
    public class TokenSpecs
    {
        public TokenSpecs(string secret, string issuer, string audience, TimeSpan lifetime) =>
            (Secret, Issuer, Audience, Lifetime) = (secret, issuer, audience, lifetime);

        public string Secret { get; }
        public string Issuer { get; }
        public string Audience { get; }
        public TimeSpan Lifetime { get; }
    }
}