using SBTestTask.Common.Models;

namespace SBTestTask.WebApi.App.Validation
{
    public interface IValidationService
    {
        void Validate(User authInfo);
    }
}