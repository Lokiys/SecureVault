using Microsoft.Practices.Prism.Commands;
using Rigsom.SecureVault.Frontend.Model;
using Rigsom.SecureVault.Model.Cryptography;
using Rigsom.SecureVault.Model.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Rigsom.SecureVault.Frontend.ViewModel
{
    /// <summary>
    /// TODO: Comment
    /// </summary>
    class NewDataViewModel : ViewModelBase
    {
        /// <summary>
        /// The associated model
        /// </summary>
        public NewData Model { get; set; }

        /// <summary>
        /// TODO: Comment
        /// </summary>
        /// <param name="model"></param>
        public NewDataViewModel(NewData model)
        {
            this.Model = model;
        }

        /// <summary>
        /// TODO: Comment
        /// </summary>
        public string Name
        {
            get { return this.Model.Name; }
            set { this.Model.Name = value; NotifyPropertyChanged(); }
        }
        
        /// <summary>
        /// TODO: Comment
        /// </summary>
        public SecureString SecureData
        {
            get { return this.Model.SecureData; }
            set { this.Model.SecureData = value; NotifyPropertyChanged(); }
        }

        /// <summary>
        /// TODO: Comment
        /// </summary>
        public SecureString Password
        {
            get { return this.Model.Password; }
            set { this.Model.Password = value; NotifyPropertyChanged(); }
        }

        /// <summary>
        /// TODO: Comment
        /// </summary>
        public DelegateCommand SaveData
        {
            get { return new DelegateCommand(SaveDataExcecute); }
        }

        /// <summary>
        /// TODO: Comment
        /// </summary>
        public void SaveDataExcecute()
        {
            //TODO: Save path in configuration
            string configurationPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "SecureVault",
                "settings",
                "configuration.dat");

            ConfigurationHelper configHelper = new ConfigurationHelper(configurationPath);
            SecureStringHelper secureStringHelper = new SecureStringHelper();
            KeyHelper keyHelper = new KeyHelper();
            SaltGenerator saltGenerator = new SaltGenerator();

            //Generate random salt
            byte[] salt = saltGenerator.GenerateSalt();

            byte[] key = keyHelper.DeriveKey(secureStringHelper.SecureStringToString(this.Password), configHelper.GetSalt());

            CryptoHelper cryptoHelper = new CryptoHelper(key, salt);

            var plainTextBytes = Encoding.UTF8.GetBytes(secureStringHelper.SecureStringToString(this.SecureData));
            var plainPasswordBase64 = Convert.ToBase64String(plainTextBytes);

            string encryptedValue = cryptoHelper.EncryptValue(plainPasswordBase64);

            configHelper.AddData(this.Name, encryptedValue, Convert.ToBase64String(salt));

            //Close the NewData window
            Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive).Close();
        }
    }
}
