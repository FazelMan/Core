namespace FazelMan.Core.Cryptography
{
    public interface ICryptographyService
    {
        string Encrypt(string template, int value);
        int Decrypt(string template, string value);
    }
}
