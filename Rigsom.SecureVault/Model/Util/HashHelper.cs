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
    public class HashHelper
    {
        /// <summary>
        /// TODO: Comment
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string ComputeHash(string data)
        {
            HashAlgorithm sha256 = SHA256.Create();

            string hash = String.Empty;
            byte[] crypto = sha256.ComputeHash(Encoding.UTF8.GetBytes(data), 0, Encoding.UTF8.GetByteCount(data));
            
            foreach (byte bit in crypto)
            {
                hash += bit.ToString("x2");
            }

            return hash;
        }
    }
}
