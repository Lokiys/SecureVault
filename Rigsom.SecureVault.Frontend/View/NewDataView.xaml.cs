using Rigsom.SecureVault.Frontend.Model;
using Rigsom.SecureVault.Frontend.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
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
    /// Interaktionslogik für NewDataView.xaml
    /// </summary>
    public partial class NewDataView : Window
    {
        public NewDataView()
        {
            InitializeComponent();
        }

        public NewDataView(SecureString password)
        {
            InitializeComponent();

            this.DataContext = new NewDataViewModel(new NewData(password));
        }

        private void SecureData_PasswordChanged(object sender, RoutedEventArgs e)
        {
            ((NewDataViewModel)this.DataContext).SecureData = ((PasswordBox)sender).SecurePassword;
        }
    }
}
