using System.Collections.Generic;
using System.Linq;
using FazelMan.Core.Common.Enums;
using HashidsNet;
using Microsoft.Extensions.Configuration;

namespace FazelMan.Core.Common.Cryptography
{
    public class CryptographyService : ICryptographyService
    {
        private readonly IConfiguration _configuration;
        Hashids _hashids;

        public CryptographyService(IConfiguration configuration)
        {
            _configuration = configuration;

        }

        private Dictionary<HashType, string> HashTypeDic = new Dictionary<HashType, string>()
        {
            {HashType.BankTransaction, "Cryptography:HashIds:Transaction"},
            {HashType.General, "Cryptography:HashIds:General"},
            {HashType.Discount, "Cryptography:HashIds:Discount"},
            {HashType.ForgetPassword, "Cryptography:HashIds:ForgetPassword"},
        };

        public string Encrypt(HashType hashType, int value)
        {
            var hashIdKey = _configuration[HashTypeDic[hashType] + ":Key"];
            var hashIdLenght = int.Parse(_configuration[HashTypeDic[hashType] + ":Lenght"]);
            var hashIdAcceptedAlphabet = _configuration[HashTypeDic[hashType] + ":AcceptedAlphabet"];

            _hashids = new Hashids(hashIdKey, hashIdLenght, hashIdAcceptedAlphabet);
            return EncryptHashids(value);
        }

        public int Decrypt(HashType hashType, string value)
        {
            var hashIdKey = _configuration[HashTypeDic[hashType] + ":Key"];
            var hashIdLenght = int.Parse(_configuration[HashTypeDic[hashType] + ":Lenght"]);
            var hashIdAcceptedAlphabet = _configuration[HashTypeDic[hashType] + ":AcceptedAlphabet"];

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
