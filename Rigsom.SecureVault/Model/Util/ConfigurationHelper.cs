using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Rigsom.SecureVault.Model.Util
{
    /// <summary>
    /// TODO: Comment
    /// </summary>
    public class ConfigurationHelper
    {
        /// <summary>
        /// TODO: Comment
        /// </summary>
        private readonly string configurationPath;

        /// <summary>
        /// TODO: Comment
        /// </summary>
        /// <param name="configurationPath"></param>
        public ConfigurationHelper(string configurationPath)
        {
            this.configurationPath = configurationPath;
        }

        /// <summary>
        /// TODO: Comment
        /// </summary>
        /// <returns></returns>
        public bool CheckConfiguration()
        {
            return File.Exists(this.configurationPath);
        }

        /// <summary>
        /// TODO: Comment
        /// </summary>
        public void CreateConfigurationFile()
        {
            //Create Path
            Directory.CreateDirectory(Path.GetDirectoryName(this.configurationPath));
            
            //Create basic configuration file
            XDocument doc = new XDocument(new XElement("configuration",
                new XElement("masterPassword"), new XElement("salt"), new XElement("encryptedData")));

            //Generate random salt
            RandomNumberGenerator rng = new RNGCryptoServiceProvider();
            byte[] salt = new byte[16];
            rng.GetBytes(salt);

            //Save generated salt
            doc.Descendants("salt").First().Value = Convert.ToBase64String(salt);

            //Save configuration file
            doc.Save(this.configurationPath);

            //Encrypt configuration file
            //TODO: Check
            //File.Encrypt(this.configurationPath);
        }
        
        /// <summary>
        /// TODO: Comment
        /// </summary>
        /// <param name="password"></param>
        public void SaveMasterPassword(string password)
        {
            XDocument doc = XDocument.Load(this.configurationPath);
            doc.Descendants("masterPassword").First().Value = password;

            doc.Save(this.configurationPath);
        }
    }
}
