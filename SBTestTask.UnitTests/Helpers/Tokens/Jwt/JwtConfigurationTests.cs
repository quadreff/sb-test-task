using System;
using FluentAssertions;
using Moq;
using Microsoft.Extensions.Configuration;
using Xunit;
using SBTestTask.WebApi.Helpers.Tokens.Jwt;

namespace SBTestTask.UnitTests.Helpers.Tokens.Jwt
{
    public class JwtConfigurationTests
    {
        private readonly Mock<IConfiguration> _configurationMock = new Mock<IConfiguration>();

        [Fact]
        public void JwtConfiguration_RetrievesProperSecret_ReturnsProperSecret()
        {
            // arrange
            var expectedValue = "test";
            _configurationMock
                .SetupGet(x => x[JwtConfiguration.SecretPath])
                .Returns(expectedValue)
                .Verifiable();

            // act
            var sut = new JwtConfiguration(_configurationMock.Object);
            var actualResult = sut.Get();

            // assert
            _configurationMock.Verify();
            actualResult.Secret.Should().BeEquivalentTo(expectedValue);
        }

        [Fact]
        public void JwtConfiguration_RetrievesProperIssuer_ReturnsProperIssuer()
        {
            // arrange
            var expectedValue = "test";
            _configurationMock
                .SetupGet(x => x[JwtConfiguration.IssuerPath])
                .Returns(expectedValue)
                .Verifiable();

            // act
            var sut = new JwtConfiguration(_configurationMock.Object);
            var actualResult = sut.Get();

            // assert
            _configurationMock.Verify();
            actualResult.Issuer.Should().BeEquivalentTo(expectedValue);
        }

        [Fact]
        public void JwtConfiguration_RetrievesProperAudience_ReturnsProperAudience()
        {
            // arrange
            var expectedValue = "test";
            _configurationMock
                .SetupGet(x => x[JwtConfiguration.AudiencePath])
                .Returns(expectedValue)
                .Verifiable();

            // act
            var sut = new JwtConfiguration(_configurationMock.Object);
            var actualResult = sut.Get();

            // assert
            _configurationMock.Verify();
            actualResult.Audience.Should().BeEquivalentTo(expectedValue);
        }

        [Fact]
        public void JwtConfiguration_RetrievesProperLifetime_ReturnsProperLifetime()
        {
            // arrange
            var lifetime = 5;
            var expectedValue = TimeSpan.FromMinutes(lifetime);
            _configurationMock
                .SetupGet(x => x[JwtConfiguration.LifeTimePath])
                .Returns(lifetime.ToString)
                .Verifiable();

            // act
            var sut = new JwtConfiguration(_configurationMock.Object);
            var actualResult = sut.Get();

            // assert
            _configurationMock.Verify();
            actualResult.Lifetime.Should().Be(expectedValue);
        }
    }
}