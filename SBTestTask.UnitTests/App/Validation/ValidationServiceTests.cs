using Microsoft.Extensions.Configuration;
using Moq;
using SBTestTask.WebApi.App.Validation;
using SBTestTask.WebApi.Models;
using Xunit;

namespace SBTestTask.UnitTests.App.Validation
{
    public class ValidationServiceTests
    {
        private readonly Mock<IConfiguration> _configurationMock = new Mock<IConfiguration>();
        private readonly ValidationService _sut;

        public ValidationServiceTests()
        {
            _sut = new ValidationService(_configurationMock.Object);
        }

        [Fact]
        public void Validate_DoNotThrow_IfUsernameIsValid()
        {
            // arrange
            var authInfo = new AuthInfo
            {
                Username = "test",
                Password = "test"
            };

            _configurationMock.Setup(x => x[ValidationService.UsernamePath]).Returns("123").Verifiable();
            
            // act
            _sut.Validate(authInfo);

            // assert
            _configurationMock.Verify();
        }
    }
}