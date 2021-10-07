using Microsoft.Extensions.Configuration;
using Moq;
using SBTestTask.WebApi.App.Validation;

namespace SBTestTask.UnitTests.App.Validation
{
    public class ValidationServiceTests
    {
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly ValidationService _sut;

        public ValidationServiceTests()
        {
            _sut = new ValidationService(_configurationMock.Object);
        }

        public void Validate_DoNotThrow_IfCredentialsAreValid()
        {
            // arrange
            // act
            // assert
        }
    }
}