using System.Collections.Generic;
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
    }
}