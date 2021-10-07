namespace SBTestTask.WebApi.App.Validation
{
    public interface IValidationService
    {
        void Validate(string username, string password);
    }
}