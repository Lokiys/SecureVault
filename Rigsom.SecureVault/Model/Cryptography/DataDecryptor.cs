using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rigsom.SecureVault.Model.Cryptography
{
    /// <summary>
    /// Responsible for the decryption of
    /// data
    /// </summary>
    internal class DataDecryptor : CryptoBase
    {
        /// <summary>
        /// TODO: Comment
        /// </summary>
        public override bool decrypt
        {
            get { return true; }
        }
        
        /// <summary>
        /// TODO: Comment
        /// </summary>
        /// <param name="key"></param>
        /// <param name="salt"></param>
        public DataDecryptor(byte[] key, byte[] salt) : base(key, salt) { }
    }
}
