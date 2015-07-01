using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rigsom.SecureVault.Frontend.Model;
using System.Security;
using Microsoft.Practices.Prism.Commands;
using System.IO;
using System.Security.Cryptography;
using Rigsom.SecureVault.Model.Util;
using System.Windows;

namespace Rigsom.SecureVault.Frontend.ViewModel
{
    /// <summary>
    /// TODO: Comment
    /// </summary>
    class ConfigurationViewModel : ViewModelBase
    {
        /// <summary>
        /// TODO: Comment
        /// </summary>
        public Configuration Model { get; set; }

        /// <summary>
        /// TODO: Comment
        /// </summary>
        /// <param name="model"></param>
        public ConfigurationViewModel(Configuration model)
        {
            this.Model = model;
        }

        /// <summary>
        /// TODO: Comment
        /// </summary>
        public SecureString MasterPassword
        {
            get { return this.Model.MasterPassword; }
            set { this.Model.MasterPassword = value; NotifyPropertyChanged(); }
        }

        /// <summary>
        /// TODO: Comment
        /// </summary>
        public SecureString MasterPasswordConfirmation
        {
            get { return this.Model.MasterPasswordConfirmation; }
            set { this.Model.MasterPasswordConfirmation = value; NotifyPropertyChanged(); }
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
        public DelegateCommand SaveConfiguration
        {
            get { return new DelegateCommand(SaveConfigurationExcecute); }
        }

        /// <summary>
        /// TODO: Comment
        /// </summary>
        public void SaveConfigurationExcecute()
        {
            //TODO: Save path in configuration
            string configurationPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), 
                "SecureVault", 
                "settings",
                "configuration.dat");

            ConfigurationHelper configHelper = new ConfigurationHelper(configurationPath);
            HashHelper hashHelper = new HashHelper();
            SecureStringHelper secureStringHelper = new SecureStringHelper();

            //Check if password and confirmation are equal
            if (secureStringHelper.SecureStringToString(this.MasterPassword).Equals(secureStringHelper.SecureStringToString(this.MasterPasswordConfirmation)))
            {
                //Create configuration file
                configHelper.CreateConfigurationFile();

                //Save master password
                string passwordHash = hashHelper.ComputeHash(secureStringHelper.SecureStringToString(this.MasterPassword));
                configHelper.SaveMasterPassword(passwordHash);

                //Close the configuration window
                Application.Current.Windows[1].Close();
            }
            else
            {
                this.Error = "Not equal";
            }
        }
    }
}
