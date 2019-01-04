namespace FazelMan.CustomConfiguration
{
    public interface ICustomConfiguration
    {
        bool IsLocal();
        bool IsDebug();
    }
}