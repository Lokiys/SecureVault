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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Rigsom.SecureVault.Frontend.View
{
    /// <summary>
    /// Interaction logic for ConfigurationPage.xaml
    /// </summary>
    public partial class ConfigurationPage : Page
    {
        /// <summary>
        /// TODO: Comment
        /// </summary>
        public ConfigurationPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// TODO: Comment
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MasterPasswordConfirmation_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            {
                ((ConfigurationViewModel)this.DataContext).MasterPasswordConfirmation = ((PasswordBox)sender).SecurePassword;
            }
        }
    }
}
