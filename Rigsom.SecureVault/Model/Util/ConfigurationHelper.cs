using Rigsom.SecureVault.Model.Cryptography;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

            SaltGenerator saltGenerator = new SaltGenerator();

            //Save generated salt
            doc.Descendants("salt").First().Value = Convert.ToBase64String(saltGenerator.GenerateSalt());

            //Save configuration file
            doc.Save(this.configurationPath);
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

        /// <summary>
        /// TODO: Comment
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Tuple<string, string, string>> GetAllSavedData()
        {
            List<Tuple<string, string, string>> savedData = new List<Tuple<string, string, string>>();

            XDocument doc = XDocument.Load(this.configurationPath);

            foreach (var data in doc.Descendants("data"))
            {
                savedData.Add(new Tuple<string, string, string>(data.Attribute("name").Value, data.Value, data.Attribute("salt").Value));
            }

            return savedData;
        }

        /// <summary>
        /// TODO: Comment
        /// </summary>
        /// <returns></returns>
        public string GetPasswordHash()
        {
            XDocument doc = XDocument.Load(this.configurationPath);

            return doc.Descendants("masterPassword").First().Value;
        }

        /// <summary>
        /// TODO: Comment
        /// </summary>
        /// <returns></returns>
        public string GetSalt()
        {
            XDocument doc = XDocument.Load(this.configurationPath);

            return doc.Descendants("salt").First().Value;
        }

        /// <summary>
        /// TODO: Comment
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void AddData(string name, string value, string salt)
        {           
            XDocument doc = XDocument.Load(this.configurationPath);
            XElement data = new XElement("data", value);
            data.Add(new XAttribute("name", name));
            data.Add(new XAttribute("salt", salt));

            doc.Descendants("encryptedData").First().Add(data);
            doc.Save(this.configurationPath);
        }
    }
}
