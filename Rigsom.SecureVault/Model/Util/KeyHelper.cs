using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Rigsom.SecureVault.Model.Util
{
    /// <summary>
    /// TODO: Comment
    /// </summary>
    public class KeyHelper
    {
        /// <summary>
        /// TODO: Comment
        /// </summary>
        private const int KEY_SIZE = 256;
        
        /// <summary>
        /// TODO: Comment
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        public byte[] DeriveKey(string password, string salt)
        {
            byte[] convertedPassword = Encoding.UTF8.GetBytes(password);
            byte[] convertedSalt = Convert.FromBase64String(salt);

            using (DeriveBytes deriveBytes = this.CreatePBKDF2(convertedPassword, convertedSalt))
            {
                return deriveBytes.GetBytes(256);
            }
        }

        /// <summary>
        /// TODO Comment
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        private DeriveBytes CreatePBKDF2(byte[] password, byte[] salt)
        {
            if (password == null)
                throw new ArgumentNullException("password");
            if (salt == null)
                throw new ArgumentNullException("salt");

            return new Rfc2898DeriveBytes(password, salt, 10000);
        }
    }
}
