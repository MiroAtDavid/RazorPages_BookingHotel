using System.Security.Cryptography;
using System.Text;

namespace BookMe.Infrastructure;

public class CryptService : ICryptService {
    
        public string GenerateHash(string key, string data) =>
            GenerateHash(Encoding.UTF8.GetBytes(key), data);
        
        public string GenerateHash(byte[] key, string data) =>
            GenerateHash(key, Encoding.UTF8.GetBytes(data));
        
        // Generating password hash
        // https://en.wikipedia.org/wiki/HMAC
        public string GenerateHash(byte[] key, byte[] data) {
            using var hmac = new HMACSHA512(key);
            var hash = hmac.ComputeHash(data);
            return Convert.ToBase64String(hash);
        }
        
        // Cryptografic secure random number generator
        public string GenerateSecret(int bits = 256) {
            var rnd = RandomNumberGenerator.GetBytes(bits / 8);
            return Convert.ToBase64String(rnd);
        }
}
