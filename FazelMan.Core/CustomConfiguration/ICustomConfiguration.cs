namespace FazelMan.Core.CustomConfiguration
{
    public interface ICustomConfiguration
    {
        bool IsLocal();
        bool IsDebug();
    }
}