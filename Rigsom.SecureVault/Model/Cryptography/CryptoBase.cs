using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Rigsom.SecureVault.Model.Cryptography
{
    /// <summary>
    /// Base class for the text tranformation classes
    /// </summary>
    internal abstract class CryptoBase
    {
        /// <summary>
        /// TODO: Comment
        /// </summary>
        public abstract bool decrypt { get; }

        /// <summary>
        /// TODO: Comment
        /// </summary>
        private byte[] key;

        /// <summary>
        /// TODO: Comment
        /// </summary>
        private byte[] salt;

        /// <summary>
        /// TODO: Comment
        /// </summary>
        /// <param name="key"></param>
        /// <param name="salt"></param>
        public CryptoBase(byte[] key, byte[] salt)
        {
            this.key = key;
            this.salt = salt;
        }

        /// <summary>
        /// TODO: Comment
        /// </summary>
        public string TransformValue(string value)
        {
            MemoryStream stream;

            if (this.decrypt)
            {
                stream = new MemoryStream(Convert.FromBase64String(value));

                using (var transform = RijndaelHelper.GenerateRijndael(this.decrypt, this.key, this.salt))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(stream, transform, CryptoStreamMode.Read))
                    {
                        using (StreamReader reader = new StreamReader(cryptoStream))
                        {
                            return Encoding.UTF8.GetString(Convert.FromBase64String(reader.ReadToEnd()));
                        }
                    }
                }
            }
            else
            {
                stream = new MemoryStream();

                using (var transform = RijndaelHelper.GenerateRijndael(this.decrypt, this.key, this.salt))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(stream, transform, CryptoStreamMode.Write))
                    {
                        using (StreamWriter writer = new StreamWriter(cryptoStream))
                        {
                            writer.Write(value);
                        }
                    }
                }

                return Convert.ToBase64String(stream.ToArray());
            }
        }
    }
}
