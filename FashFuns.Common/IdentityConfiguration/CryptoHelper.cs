using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Security.Cryptography;
using System.Text;

namespace FashFuns.Common.IdentityConfiguration
{
    /// <summary>
    /// Provides helper methods for encrypting and decrypting strings
    /// </summary>
    public static class CryptoHelper
    {
        /// <summary>
        /// Create a random salt.
        /// </summary>
        /// <remarks>See https://en.wikipedia.org/wiki/Salt_(cryptography) for more information.</remarks>
        /// <returns>A salt value.</returns>
        public static string CreateSalt()
        {
            byte[] randomBytes = new byte[128 / 8];
            using (var generator = RandomNumberGenerator.Create())
            {
                generator.GetBytes(randomBytes);
                return Convert.ToBase64String(randomBytes);
            }
        }

        /// <summary>
        /// Hashes a string value with the provided salt.
        /// </summary>
        /// <param name="value">The string value to hash.</param>
        /// <param name="salt">A unique salt value.</param>
        /// <returns>A hashed value.</returns>
        /// <remarks>The salt value can be provided using the <see cref="CreateSalt"/> method.</remarks>
        public static string Hash(
            string value,
            string salt)
        {
            var valueBytes = KeyDerivation.Pbkdf2(
                                password: value,
                                salt: Encoding.UTF8.GetBytes(salt),
                                prf: KeyDerivationPrf.HMACSHA512,
                                iterationCount: 10000,
                                numBytesRequested: 256 / 8);

            return Convert.ToBase64String(valueBytes);
        }

        public static bool Validate(
            string value,
            string salt,
            string hash) => Hash(value, salt) == hash;
    }
}