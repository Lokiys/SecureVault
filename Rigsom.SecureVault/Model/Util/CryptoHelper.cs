using Rigsom.SecureVault.Model.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rigsom.SecureVault.Model.Util
{
    /// <summary>
    /// TODO: Comment
    /// </summary>
    public class CryptoHelper
    {
        /// <summary>
        /// TODO: Comment
        /// </summary>
        private readonly DataEncryptor encryptor;

        /// <summary>
        /// TODO: Comment
        /// </summary>
        private readonly DataDecryptor decryptor;

        /// <summary>
        /// TODO: Comment
        /// </summary>
        /// <param name="key"></param>
        /// <param name="salt"></param>
        public CryptoHelper(byte[] key, byte[] salt)
        {
            this.encryptor = new DataEncryptor(key, salt);
            this.decryptor = new DataDecryptor(key, salt);
        }

        /// <summary>
        /// TODO: Comment
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string EncryptValue(string value)
        {
            return this.encryptor.TransformValue(value);
        }

        /// <summary>
        /// TODO: Comment
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string DecryptValue(string value)
        {
            return this.decryptor.TransformValue(value);
        }
    }
}
