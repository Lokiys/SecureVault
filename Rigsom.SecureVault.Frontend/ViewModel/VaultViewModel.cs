using Microsoft.Practices.Prism.Commands;
using Rigsom.SecureVault.Frontend.Model;
using Rigsom.SecureVault.Frontend.View;
using Rigsom.SecureVault.Model.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Rigsom.SecureVault.Frontend.ViewModel
{
    /// <summary>
    /// ViewModel for the MainView
    /// </summary>
    class VaultViewModel : ViewModelBase
    {
        /// <summary>
        /// The associated model
        /// </summary>
        public Vault Model { get; set; }

        /// <summary>
        /// TODO: Comment
        /// </summary>
        /// <param name="model"></param>
        public VaultViewModel(Vault model)
        {
            this.Model = model;
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
        public ObservableCollection<SavedData> SavedData
        {
            get { return this.Model.SavedData; }
            set { this.Model.SavedData = value; NotifyPropertyChanged(); }
        }

        /// <summary>
        /// TODO: Comment
        /// </summary>
        private string error;

        /// <summary>
        /// TODO: Comment
        /// </summary>
        public string Error
        {
            get { return error; }
            set { error = value; NotifyPropertyChanged(); }
        }

        /// <summary>
        /// TODO: Comment
        /// </summary>
        private string decryptedPassword;

        /// <summary>
        /// TODO: Comment
        /// </summary>
        public string DecryptedPassword
        {
            get { return decryptedPassword; }
            set { decryptedPassword = value; NotifyPropertyChanged(); }
        }

        /// <summary>
        /// TODO: Comment
        /// </summary>
        private bool decryptionEnabled;

        /// <summary>
        /// TODO: Comment
        /// </summary>
        public bool DecryptionEnabled
        {
            get { return decryptionEnabled; }
            set { decryptionEnabled = value; NotifyPropertyChanged(); }
        }

        /// <summary>
        /// TODO: Comment
        /// </summary>
        public DelegateCommand Authenticate
        {
            get { return new DelegateCommand(AuthenticateExcecute); }
        }

        /// <summary>
        /// TODO: Comment
        /// </summary>
        public void AuthenticateExcecute()
        {
            //TODO: Save path in configuration
            string configurationPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "SecureVault",
                "settings",
                "configuration.dat");

            ConfigurationHelper configHelper = new ConfigurationHelper(configurationPath);
            HashHelper hashHelper = new HashHelper();
            SecureStringHelper secureStringHelper = new SecureStringHelper();

            //Check if password is correct
            if (hashHelper.ComputeHash(secureStringHelper.SecureStringToString(this.Password)).Equals(configHelper.GetPasswordHash()))
            {
                IEnumerable<Tuple<string, string, string>> savedData = configHelper.GetAllSavedData();
                ObservableCollection<SavedData> dataCollection = new ObservableCollection<SavedData>();

                foreach (var data in savedData)
                {
                    dataCollection.Add(new SavedData() { Name = data.Item1, EncryptedValue = data.Item2, Salt = data.Item3 });
                }

                this.SavedData = dataCollection;
                this.Error = String.Empty;
                this.DecryptionEnabled = true;
            }
            else
            {
                this.Error = "Invalid Password";
                this.SavedData = null;
                this.DecryptionEnabled = false;
            }
        }

        /// <summary>
        /// TODO: Comment
        /// </summary>
        public DelegateCommand<int?> CopyPassword
        {
            get { return new DelegateCommand<int?>(CopyPasswordExcecute); }
        }

        /// <summary>
        /// TODO: Comment
        /// </summary>
        public void CopyPasswordExcecute(int? selectedID)
        {
            //TODO: Save path in configuration
            string configurationPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "SecureVault",
                "settings",
                "configuration.dat");

            ConfigurationHelper configHelper = new ConfigurationHelper(configurationPath);
            KeyHelper keyHelper = new KeyHelper();
            SecureStringHelper secureStringHelper = new SecureStringHelper();

            selectedID = selectedID == -1 ? 0 : selectedID;

            byte[] key = keyHelper.DeriveKey(secureStringHelper.SecureStringToString(this.Password), configHelper.GetSalt());

            CryptoHelper cryptoHelper = new CryptoHelper(key, Convert.FromBase64String(this.SavedData[selectedID.Value].Salt));

            string decryptedValue = cryptoHelper.DecryptValue(this.SavedData[selectedID.Value].EncryptedValue);

            Clipboard.SetText(decryptedValue);
        }

        /// <summary>
        /// TODO: Comment
        /// </summary>
        public DelegateCommand<int?> ShowPassword
        {
            get { return new DelegateCommand<int?>(ShowPasswordExcecute); }
        }

        /// <summary>
        /// TODO: Comment
        /// </summary>
        public void ShowPasswordExcecute(int? selectedID)
        {
            //TODO: Save path in configuration
            string configurationPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "SecureVault",
                "settings",
                "configuration.dat");

            ConfigurationHelper configHelper = new ConfigurationHelper(configurationPath);
            KeyHelper keyHelper = new KeyHelper();
            SecureStringHelper secureStringHelper = new SecureStringHelper();



            byte[] key = keyHelper.DeriveKey(secureStringHelper.SecureStringToString(this.Password), configHelper.GetSalt());

            CryptoHelper cryptoHelper = new CryptoHelper(key, Convert.FromBase64String(this.SavedData[selectedID.Value].Salt));

            string decryptedValue = cryptoHelper.DecryptValue(this.SavedData[selectedID.Value].EncryptedValue);

            this.DecryptedPassword = decryptedValue;
        }

        /// <summary>
        /// TODO: Comment
        /// </summary>
        public DelegateCommand AddData
        {
            get { return new DelegateCommand(AddDataExcecute); }
        }

        /// <summary>
        /// TODO: Comment
        /// </summary>
        public void AddDataExcecute()
        {
            NewDataView newDataView = new NewDataView(this.Password);
            newDataView.ShowDialog();
        }
    }
}
