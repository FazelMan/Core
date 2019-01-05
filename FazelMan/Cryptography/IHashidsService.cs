namespace FazelMan.Cryptography
{
    public interface IHashidsService
    {
        string Encrypt(string template, int value);
        int Decrypt(string template, string value);
    }
}
