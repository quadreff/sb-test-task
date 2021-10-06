namespace SBTestTask.WebApi.Helpers.Tokens
{
    public interface ITokenManager<T>
    {
        T GenerateToken(string username);
    }
}