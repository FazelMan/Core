using FazelMan.Core.Common.Enums;

namespace FazelMan.Core.Common.Cryptography
{
    public interface ICryptographyService
    {
        string Encrypt(HashType hashType, int value);
        int Decrypt(HashType hashType, string value);
    }
}
