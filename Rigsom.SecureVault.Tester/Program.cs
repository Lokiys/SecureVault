using Rigsom.SecureVault.Model.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rigsom.SecureVault.Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            KeyHelper keyHelper = new KeyHelper();
            byte[] key = keyHelper.DeriveKey("testPW", "I+e2/x4P+32RsAp+iZhQaw==");

            CryptoHelper cryptoHelper = new CryptoHelper(key, Convert.FromBase64String("I+e2/x4P+32RsAp+iZhQaw=="));

            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes("test");
            var test = System.Convert.ToBase64String(plainTextBytes);

            string encryptedValue = cryptoHelper.EncryptValue(test);
            string decryptedValue = cryptoHelper.DecryptValue(encryptedValue);

            Console.WriteLine(encryptedValue);
            Console.WriteLine(decryptedValue);
            Console.ReadKey();
        }
    }
}
