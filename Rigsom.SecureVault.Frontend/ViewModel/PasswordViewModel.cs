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
using System.Windows.Controls;

namespace Rigsom.SecureVault.Frontend.ViewModel
{
    /// <summary>
    /// TODO: Comment
    /// </summary>
    class PasswordViewModel : ViewModelBase
    {
        /// <summary>
        /// The associated model
        /// </summary>
        public Vault Model { get; set; }

        /// <summary>
        /// TODO: Comment
        /// </summary>
        public Frame MainFrame { get; set; }

        /// <summary>
        /// TODO: Comment
        /// </summary>
        /// <param name="model"></param>
        public PasswordViewModel(Vault model, Frame mainFrame)
        {
            this.Model = model;
            this.MainFrame = mainFrame;
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

                this.Model.SavedData = dataCollection;

                //Navigate to the VaultPage
                VaultPage vaultPage = new VaultPage();
                vaultPage.DataContext = new VaultViewModel(this.Model);
                this.MainFrame.Navigate(vaultPage);
            }
            else
            {
                this.Error = "Invalid Password";
                this.Model.SavedData = null;
            }
        }
    }
}
