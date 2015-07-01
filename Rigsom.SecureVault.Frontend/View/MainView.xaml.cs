using Rigsom.SecureVault.Frontend.Model;
using Rigsom.SecureVault.Frontend.ViewModel;
using Rigsom.SecureVault.Model.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Rigsom.SecureVault.Frontend.View
{
    /// <summary>
    /// Interaktionslogik für MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();

            //TODO: Save path in configuration
            string configurationPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "SecureVault",
                "settings",
                "configuration.dat");

            ConfigurationHelper configHelper = new ConfigurationHelper(configurationPath);

            if (!configHelper.CheckConfiguration())
            {
                ConfigurationView configView = new ConfigurationView();
                configView.ShowDialog();
            }

            this.DataContext = new VaultViewModel(new Vault());
        }

        private void Password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            {
                ((VaultViewModel)this.DataContext).Password = ((PasswordBox)sender).SecurePassword;
            }
        }
    }
}
