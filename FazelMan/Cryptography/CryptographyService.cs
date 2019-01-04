using System.Linq;
using HashidsNet;
using Microsoft.Extensions.Configuration;

namespace FazelMan.Cryptography
{
    public class CryptographyService : ICryptographyService
    {
        private readonly IConfiguration _configuration;
        Hashids _hashids;

        public CryptographyService(IConfiguration configuration)
        {
            _configuration = configuration;

        }

        public string Encrypt(string template, int value)
        {
            var hashIdKey = _configuration[template + ":Key"];
            var hashIdLenght = int.Parse(_configuration[template + ":Lenght"]);
            var hashIdAcceptedAlphabet = _configuration[template + ":AcceptedAlphabet"];

            _hashids = new Hashids(hashIdKey, hashIdLenght, hashIdAcceptedAlphabet);
            return EncryptHashids(value);
        }

        public int Decrypt(string template, string value)
        {
            var hashIdKey = _configuration[template + ":Key"];
            var hashIdLenght = int.Parse(_configuration[template + ":Lenght"]);
            var hashIdAcceptedAlphabet = _configuration[template + ":AcceptedAlphabet"];

            _hashids = new Hashids(hashIdKey, hashIdLenght, hashIdAcceptedAlphabet);
            return DecryptHashids(value);
        }

        private string EncryptHashids(int stringToEncrypt)
        {
            return _hashids.Encode(stringToEncrypt);
        }

        private int DecryptHashids(string stringToDecrypt)
        {
            return _hashids.Decode(stringToDecrypt).First();
        }
    }
}
