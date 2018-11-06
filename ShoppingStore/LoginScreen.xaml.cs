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
    public partial class LoginScreen : Window
    {
        private User loginUser = new User();

        public LoginScreen()
        {
            InitializeComponent();
            txtUsername.Text = "Username";
            txtPassword.Password = "Password";
        }

        private void BtnLoginUser_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Password;

            if (string.IsNullOrEmpty(txtUsername.Text))
            {
                //verify and enter username.
                MessageBox.Show("Enter your username.", "Empty", MessageBoxButton.OK, MessageBoxImage.Information);
                txtUsername.Focus();
                return;
            }
            else if (string.IsNullOrEmpty(txtPassword.Password))
            {
                MessageBox.Show("Enter your password.", "Empty", MessageBoxButton.OK, MessageBoxImage.Information);
                txtPassword.Focus();
                return;
            }
            else
            {
                try
                {
                    loginUser = SQLuserAccess.UserLogin(txtUsername.Text, txtPassword.Password.ToString());
                    //if(!string.IsNullOrEmpty(SQLuserAccess.UserLoginString(txtUsername.Text, txtPassword.Password)))      --Boolean Login.
                         //string Id = SQLuserAccess.UserLoginString(txtUsername.Text, txtPassword.Password);
                         //loginUser = UsersDB.GetUserById(Id);

                    if (txtUsername.Text == loginUser.Username && txtPassword.Password == loginUser.Password)
                    {
                        if (loginUser.IsAdmin == true)
                        {
                            Window showAdminSrc = new AdminWindow();
                            showAdminSrc.Show();
                            Close();
                        }
                        else if (loginUser.IsAdmin == false)
                        {
                            Window nonAdmin = new UserList();
                            nonAdmin.Show();
                            Close();
                        }
                        else
                            lblInvalidText.Content = "Admin is not True or False!";
                    }
                    else
                    {
                        lblInvalidText.Visibility = Visibility.Visible;
                    }
                }
                catch(Exception ex)
                {
                    ex.Message.ToString();
                    throw ex;
                }
            }
        }

        private void BtnCreateUser_Click(object sender, RoutedEventArgs e)
        {
            Window showCustAdd = new CustomerAdd();
            showCustAdd.Show();
            this.Close();
        }
        
        private void txtUsername_GotFocus(object sender, RoutedEventArgs e)
        {
            if(txtUsername.Text.Equals("Username"))
               txtUsername.Text = "";
        }
        private void txtUsername_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
                txtUsername.Text = "Username";
        }

        private void txtPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            txtPassword.Password = "";
        }
        private void txtPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPassword.Password))
                txtPassword.Password = "Password";
        }
    }
}
