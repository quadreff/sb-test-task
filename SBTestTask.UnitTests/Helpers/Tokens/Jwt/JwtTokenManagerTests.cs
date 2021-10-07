using System;
using FluentAssertions;
using Microsoft.IdentityModel.Tokens;
using Moq;
using SBTestTask.WebApi.Helpers.Tokens.Jwt;
using Xunit;

namespace SBTestTask.UnitTests.Helpers.Tokens.Jwt
{
    public class JwtTokenManagerTests
    {
        private readonly Mock<IJwtConfiguration> _jwtConfigurationMock = new Mock<IJwtConfiguration>();
        private readonly JwtTokenManager _sut;

        public JwtTokenManagerTests()
        {
            _sut = new JwtTokenManager(_jwtConfigurationMock.Object);
        }

        [Fact]
        public void GenerateToken_RetrievesProperTokenSpecs_ReturnsProperToken()
        {
            // arrange
            var testString = "123";
            var username = "testuser";
            var secret = "MDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMA==";
            var minutes = 5;
            var dateTimeNow = DateTime.Now;
            var tokenSpecs = new TokenSpecs(secret, testString, testString, TimeSpan.FromMinutes(minutes));
            _jwtConfigurationMock
                .Setup(x => x.Get())
                .Returns(tokenSpecs);

            // act
            var actualResult = _sut.GenerateToken(username);

            // assert

            // group the results here to simplify the testing for now
            actualResult.Issuer.Should().BeEquivalentTo(tokenSpecs.Issuer);
            actualResult.Audiences.Should().BeEquivalentTo(tokenSpecs.Audience);
            actualResult.SigningCredentials.Key.Should().BeEquivalentTo(new SymmetricSecurityKey(Convert.FromBase64String(tokenSpecs.Secret)));
            actualResult.ValidTo.Should().NotBe(default);
        }
    }
}