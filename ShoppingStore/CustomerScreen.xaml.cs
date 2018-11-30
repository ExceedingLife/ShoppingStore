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
            InitializeComponent();

            if (insertedUser.IsCustomer == true)
            {
                customerUser = UsersDB.ReadCustomerById(user.UserID);
                if (customerUser != null)
                {
                    //Display Hello: Customers firstname - *future - Data Binding.
                    TxtBlockName.Text = "Hello:" + Environment.NewLine +
                    CapitalizeFirstLetter(customerUser.FirstName);
                }
            }
            else //Otherwise display Hello: Users username - *future - Data Binding.
                TxtBlockName.Text = "Hello:" + Environment.NewLine + 
                    CapitalizeFirstLetter(insertedUser.Username);
        }

        private string CapitalizeFirstLetter(string nameToCapitalize)
        {
            string nameMinusOne = nameToCapitalize.Substring(1);
            string firstLetter = nameToCapitalize.Substring(0, 1).ToUpper();
            return firstLetter + nameMinusOne;
        }

        private void BtnProfile_Click(object sender, RoutedEventArgs e)
        {
            Window SrcCustomerProfile = new ProfileScreen(insertedUser);
            SrcCustomerProfile.Show();
            Close();
        }

        private void BtnProducts_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(insertedUser.ToString());
        }
    }
}
