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
using System.Data;
using System.Data.SqlClient;
using ShoppingStore.DataAccess;
using ShoppingStore.DataBase;

namespace ShoppingStore
{
    /// <summary>
    /// Interaction logic for CustomerScreen.xaml
    /// </summary>
    public partial class CustomerScreen : Window
    {
        private Customer customerUser = null;
        private User insertedUser = null;

        public CustomerScreen()
        {
            InitializeComponent();
        }
        public CustomerScreen(User user)
        {
            insertedUser = user;

            string capitalLaterName = "";
            capitalLaterName = user.Username.Substring(1);
            string firstLetter = user.Username.Substring(0,1).ToUpper();
            customerUser = user as Customer;
            InitializeComponent();
            //Display Users 'username' on Src / *future - Data Binding.
            TxtBlockName.Text = "Hello:" + Environment.NewLine +
                firstLetter + capitalLaterName;
                //user.Username;
                //customerUser.Username;
        }

        private void BtnProfile_Click(object sender, RoutedEventArgs e)
        {
            //Window SrcCustomerProfile = new ProfileScreen((Customer)insertedUser);
            Window SrcCustomerProfile = new ProfileScreen(insertedUser);

            //Window SrcCustomerProfile = new ProfileScreen(customerUser);
            SrcCustomerProfile.Show();
            Close();
        }

        private void BtnProducts_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(insertedUser.ToString());
        }
    }
}
