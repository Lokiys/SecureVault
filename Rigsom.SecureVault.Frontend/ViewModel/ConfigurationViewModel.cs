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
using System.Windows.Controls;
using Rigsom.SecureVault.Frontend.View;

namespace Rigsom.SecureVault.Frontend.ViewModel
{
    /// <summary>
    /// ViewModel for the ConfigurationView
    /// </summary>
    class ConfigurationViewModel : ViewModelBase
    {
        /// <summary>
        /// The associated model
        /// </summary>
        public Configuration Model { get; set; }

        /// <summary>
        /// TODO: Comment
        /// </summary>
        public Frame MainFrame { get; set; }

        /// <summary>
        /// TODO: Comment
        /// </summary>
        /// <param name="model"></param>
        public ConfigurationViewModel(Configuration model, Frame mainFrame)
        {
            this.Model = model;
            this.MainFrame = mainFrame;
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
            string configurationPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), 
                "SecureVault", 
                "settings",
                "configuration.dat");

            ConfigurationHelper configHelper = new ConfigurationHelper(configurationPath);
            HashHelper hashHelper = new HashHelper();
            SecureStringHelper secureStringHelper = new SecureStringHelper();

            if (String.IsNullOrWhiteSpace(secureStringHelper.SecureStringToString(this.MasterPassword)) ||
                String.IsNullOrWhiteSpace(secureStringHelper.SecureStringToString(this.MasterPasswordConfirmation)))
            {
                this.Error = "Please set a Password";
                return;
            }

            //Check if password and confirmation are equal
            if (secureStringHelper.SecureStringToString(this.MasterPassword).Equals(secureStringHelper.SecureStringToString(this.MasterPasswordConfirmation)))
            {
                //Create configuration file
                configHelper.CreateConfigurationFile();

                //Save master password
                string passwordHash = hashHelper.ComputeHash(secureStringHelper.SecureStringToString(this.MasterPassword));
                configHelper.SaveMasterPassword(passwordHash);

                //Navigate to PasswordPage
                PasswordPage passwordPage = new PasswordPage();
                passwordPage.DataContext = new PasswordViewModel(new Vault(), this.MainFrame);

                this.MainFrame.Navigate(passwordPage);
            }
            else
            {
                this.Error = "Not equal";
            }
        }
    }
}
