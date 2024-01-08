using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace trello_app.Services
{
    public class PassHasher
    {

        public static string GenerateSalt()
        {
            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return Convert.ToBase64String(salt);
        }

        public static string HashPassword(string password, string salt)
        {
            byte[] saltBytes = Convert.FromBase64String(salt);
            byte[] hashed = KeyDerivation.Pbkdf2(
                password: password,
                salt: saltBytes,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8
            );

            return Convert.ToBase64String(hashed);
        }


        public static bool VerifyPassword(string enteredPassword, string storedPasswordHash, string salt)
        {
            string hashedEnteredPassword = HashPassword(enteredPassword, salt);
            return hashedEnteredPassword == storedPasswordHash;
        }
    }
}
