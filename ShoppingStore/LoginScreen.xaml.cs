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

namespace ShoppingStore
{
    /// <summary>
    /// Interaction logic for LoginScreen.xaml
    /// </summary>
    public partial class LoginScreen : Window
    {
        public LoginScreen()
        {
            InitializeComponent();
        }

        private void btnLoginUser_Click(object sender, RoutedEventArgs e)
        {
            Window showAdminSrc = new AdminWindow();
            showAdminSrc.Show();
            this.Close();
        }

        private void btnCreateUser_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
