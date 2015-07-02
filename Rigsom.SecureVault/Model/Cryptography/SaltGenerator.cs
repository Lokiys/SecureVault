using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Rigsom.SecureVault.Model.Cryptography
{
    /// <summary>
    /// TODO: Comment
    /// </summary>
    public class SaltGenerator
    {
        /// <summary>
        /// TODO: Comment
        /// </summary>
        private const int SALT_SIZE = 16;

        /// <summary>
        /// TODO: Comment
        /// </summary>
        /// <returns></returns>
        public byte[] GenerateSalt()
        {
            //Generate random salt
            RandomNumberGenerator rng = new RNGCryptoServiceProvider();
            byte[] salt = new byte[SALT_SIZE];
            rng.GetBytes(salt);

            return salt;
        }
    }
}
