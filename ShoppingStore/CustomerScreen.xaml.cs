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
    public partial class CustomerScreen : Window
    {
        private Customer customerUser = null;
        private User insertedUser = null;
        private Customer fullCustomer = null;

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
                    //CapitalizeFirstLetter(customerUser.FirstName);
                    Extras.CapitalizeFirstLetter(customerUser.FirstName);
                    try
                    {
                        fullCustomer = new Customer(insertedUser.UserID, insertedUser.Username, insertedUser.Password,
                                                    insertedUser.IsAdmin, insertedUser.UserCreatedDate, insertedUser.IsCustomer,
                                                    customerUser.FirstName, customerUser.LastName, customerUser.Address,
                                                    customerUser.City, customerUser.State, customerUser.ZipCode, customerUser.EmailAddress);
                    }
                    catch (Exception ex) { MessageBox.Show("Error making customer object"); }
                }
            }
            else //Otherwise display Hello: Users username - *future - Data Binding.
                TxtBlockName.Text = "Hello:" + Environment.NewLine +
                    //CapitalizeFirstLetter(insertedUser.Username);
                    Extras.CapitalizeFirstLetter(insertedUser.Username);
        }

        private void BtnProfile_Click(object sender, RoutedEventArgs e)
        {
            Window SrcCustomerProfile = new ProfileScreen(insertedUser);
            SrcCustomerProfile.Show();
            Close();
        }

        private void BtnProducts_Click(object sender, RoutedEventArgs e)
        {
            if (insertedUser != null)
            {
                if (insertedUser.IsCustomer == true)
                {
                    if (fullCustomer != null)
                    {
                        Window WindowProductCart = new ProductCart(fullCustomer);//(insertedUser);
                        WindowProductCart.Show();
                        Close();
                    }
                    else
                        MessageBox.Show("Error loading customer please re-login", "Plz Restart program");
                }
                else
                    MessageBox.Show("You MUST be a customer to buy products, \nMake a profile.", "Warning");
            }
            else
                MessageBox.Show("Customer Login Error", "Must Re-Login", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void BtnReceipts_Click(object sender, RoutedEventArgs e)
        {
            if(insertedUser != null)
            {
                if(insertedUser.IsCustomer == true)
                {
                    if (fullCustomer != null)
                    {
                        Window WindowReceiptList = new ReceiptList(fullCustomer);
                        WindowReceiptList.Show();
                        Close();
                    }
                    else
                        MessageBox.Show("Error loading customer please re-login", "Plz Restart program");
                }
                else
                    MessageBox.Show("You MUST be a customer to view receipts, \nMake a profile.", "Warning");
            }
            else
                MessageBox.Show("Customer Login Error", "Must Re-Login", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void BtnLogOut_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
