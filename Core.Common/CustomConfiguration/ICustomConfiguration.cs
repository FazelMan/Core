namespace CoreApp.Api.Helper
{
    public interface ICustomConfiguration
    {
        bool IsLocal();
        bool IsDebug();
    }
}