using SBTestTask.WebApi.Models;

namespace SBTestTask.WebApi.App.Validation
{
    public interface IValidationService
    {
        void Validate(AuthInfo authInfo);
    }
}