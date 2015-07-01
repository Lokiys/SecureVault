using Rigsom.SecureVault.Frontend.Model;
using Rigsom.SecureVault.Frontend.ViewModel;
using System;
using System.Collections.Generic;
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
    /// Interaktionslogik für ConfigurationView.xaml
    /// </summary>
    public partial class ConfigurationView : Window
    {
        /// <summary>
        /// TODO: Comment
        /// </summary>
        public ConfigurationView()
        {
            InitializeComponent();

            this.DataContext = new ConfigurationViewModel(new Configuration());
        }

        /// <summary>
        /// TODO: Comment
        /// </summary>
        private void MasterPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            {
                ((ConfigurationViewModel)this.DataContext).MasterPassword = ((PasswordBox)sender).SecurePassword; 
            }
        }

        /// <summary>
        /// TODO: Comment
        /// </summary>
        private void MasterPasswordConfirmation_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            {
                ((ConfigurationViewModel)this.DataContext).MasterPasswordConfirmation = ((PasswordBox)sender).SecurePassword;
            }
        }
    }
}
