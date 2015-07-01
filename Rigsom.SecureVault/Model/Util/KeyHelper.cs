using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Rigsom.SecureVault.Model.Util
{
    /// <summary>
    /// Responsible for the derivation of
    /// the key
    /// </summary>
    public class KeyHelper
    {
        /// <summary>
        /// Size of the Key
        /// </summary>
        private const int KEY_SIZE = 256;

        /// <summary>
        /// Number of iterations for the PBKDF2
        /// function
        /// </summary>
        private const int PBKDF2_ITERATIONS = 1000;
        
        /// <summary>
        /// Derives the key with the help of
        /// PBKDF2
        /// </summary>
        /// <param name="password">Password for the
        /// key derivation</param>
        /// <param name="salt">Salt for the key
        /// derivation</param>
        /// <returns>Derived key</returns>
        public byte[] DeriveKey(string password, string salt)
        {
            byte[] convertedPassword = Encoding.UTF8.GetBytes(password);
            byte[] convertedSalt = Convert.FromBase64String(salt);

            using (DeriveBytes deriveBytes = this.CreatePBKDF2(convertedPassword, convertedSalt))
            {
                return deriveBytes.GetBytes(KEY_SIZE);
            }
        }

        /// <summary>
        /// Creates the PBKDF2 function
        /// </summary>
        /// <param name="password">Password for the
        /// key derivation</param>
        /// <param name="salt">Salt for the key
        /// derivation</param>
        /// <returns>PBKDF2 function</returns>
        private DeriveBytes CreatePBKDF2(byte[] password, byte[] salt)
        {
            if (password == null)
                throw new ArgumentNullException("password");
            if (salt == null)
                throw new ArgumentNullException("salt");

            return new Rfc2898DeriveBytes(password, salt, PBKDF2_ITERATIONS);
        }
    }
}
