using System.IdentityModel.Tokens.Jwt;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SBTestTask.WebApi.App.Validation;
using SBTestTask.WebApi.Controllers;
using SBTestTask.WebApi.Helpers.Tokens.Jwt;
using Xunit;

namespace SBTestTask.UnitTests.Controllers
{
    public class TokenControllerTests
    {
        private readonly Mock<IJwtTokenManager> _tokenManagerMock = new Mock<IJwtTokenManager>();
        private readonly Mock<IValidationService> _validationServiceMock = new Mock<IValidationService>();
        private readonly TokenController _sut;

        private const string JwtString
            = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9l" +
              "IiwiYWRtaW4iOnRydWUsImlhdCI6MTYzMzU4Mzg1OSwiZXhwIjoxNjMzNTg3NDU5fQ.fmINRc1ngZto5ekFRQRMEDAiFbqARz3C5lVuhUx4UpA";
        public TokenControllerTests()
        {
            _sut = new TokenController(_tokenManagerMock.Object, _validationServiceMock.Object);
        }

        [Fact]
        public void GenerateToken_WhenTheCredentialsAreOk_ShouldReturnOk()
        {
            // arrange
            var username = "testuser";
            var password = "testpass";
            var token = new JwtSecurityToken(JwtString);

            _tokenManagerMock.Setup(x => x.GenerateToken(username)).Returns(token);
            _validationServiceMock.Setup(x => x.Validate(username, password));
            
            // act
            var actualResult = _sut.GenerateToken(username, password) as OkObjectResult;

            // assert

            actualResult.Should().NotBeNull();
            actualResult!.Value.As<JwtSecurityToken>().AsString().Should().BeEquivalentTo(JwtString);
        }
    }
}