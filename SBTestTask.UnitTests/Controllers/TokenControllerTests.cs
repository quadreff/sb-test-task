using System;
using System.IdentityModel.Tokens.Jwt;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SBTestTask.WebApi.App.Validation;
using SBTestTask.WebApi.Controllers;
using SBTestTask.WebApi.Helpers.Tokens.Jwt;
using SBTestTask.WebApi.Models;
using Xunit;

namespace SBTestTask.UnitTests.Controllers
{
    public class TokenControllerTests
    {
        private readonly Mock<IJwtTokenManager> _tokenManagerMock = new Mock<IJwtTokenManager>();
        private readonly Mock<IValidationService> _validationServiceMock = new Mock<IValidationService>();
        private readonly TokenController _sut;

        public TokenControllerTests()
        {
            _sut = new TokenController(_tokenManagerMock.Object, _validationServiceMock.Object);
        }

        [Fact]
        public void GenerateToken_WhenTheCredentialsAreOk_ShouldReturnOk()
        {
            // arrange
            var username = "testuser";
            var authInfo = new AuthInfo
            {
                Name = username,
                Password = "testpass"
            };
            var token = new JwtSecurityToken();

            _tokenManagerMock.Setup(x => x.GenerateToken(username)).Returns(token);
            _validationServiceMock.Setup(x => x.Validate(authInfo));
            
            // act
            var actualResult = _sut.GenerateToken(authInfo) as OkObjectResult;

            // assert
            actualResult.Should().NotBeNull();
            actualResult!.Value.As<JwtSecurityToken>().AsString().Should().BeEquivalentTo(token.AsString());
        }

        [Fact]
        public void GenerateToken_WhenTheCredentialsAreNotOk_ShouldReturnUnauthorized()
        {
            // arrange
            var username = "testuser";
            var authInfo = new AuthInfo
            {
                Name = username,
                Password = "1234"
            };
            var token = new JwtSecurityToken();

            _tokenManagerMock.Setup(x => x.GenerateToken(username)).Returns(token);
            _validationServiceMock.Setup(x => x.Validate(authInfo)).Throws<UnauthorizedException>();

            // act
            var actualResult = _sut.GenerateToken(authInfo) as UnauthorizedResult;

            // assert
            actualResult.Should().NotBeNull();
        }
    }
}