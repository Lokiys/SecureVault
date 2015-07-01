using Microsoft.Practices.Prism.Commands;
using Rigsom.SecureVault.Frontend.Model;
using Rigsom.SecureVault.Model.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Rigsom.SecureVault.Frontend.ViewModel
{
    /// <summary>
    /// TODO: Comment
    /// </summary>
    class VaultViewModel : ViewModelBase
    {
        /// <summary>
        /// TODO: Comment
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
                IEnumerable<Tuple<string, string>> savedData = configHelper.GetAllSavedData();
                ObservableCollection<SavedData> dataCollection = new ObservableCollection<SavedData>();

                foreach (var data in savedData)
                {
                    dataCollection.Add(new SavedData() { Name = data.Item1, EncryptedValue = data.Item2 });
                }

                this.SavedData = dataCollection;
            }
            else
            {
                
            }
        }
    }
}
