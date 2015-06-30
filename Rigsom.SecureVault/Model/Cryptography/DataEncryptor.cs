using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rigsom.SecureVault.Model.Cryptography
{
    /// <summary>
    /// Responsible for the encryption of
    /// data
    /// </summary>
    internal class DataEncryptor : CryptoBase
    {
        /// <summary>
        /// TODO: Comment
        /// </summary>
        public override bool decrypt
        {
            get { return false; }
        }
        
        /// <summary>
        /// TODO: Comment
        /// </summary>
        /// <param name="key"></param>
        /// <param name="salt"></param>
        public DataEncryptor(byte[] key, byte[] salt) : base(key, salt) { }
    }
}
