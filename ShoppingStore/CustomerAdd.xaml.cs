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
    public partial class CustomerAdd : Window
    {
        private User user = null;
        private DateTime date = new DateTime();
        private int isAdministrator = 0;

        public CustomerAdd()
        {
            InitializeComponent();
            lblContent0.Visibility = Visibility.Hidden;
            txtUserID.Visibility = Visibility.Hidden;
        }

        //Populate CustomerAdd window with User data.
        public CustomerAdd(User u)
        {
            user = u;
            InitializeComponent();
            PopulateUserData(u);
            //Change Create Button text to Update
            BtnCreateCustomer.Content = "Update Customer";
        }

        private void PopulateUserData(User selectedUser)
        {
            txtUserID.Text = selectedUser.UserID.ToString(); ;
            txtUsername.Text = selectedUser.Username.ToString();
            txtPassword.Text = selectedUser.Password.ToString();
            if (selectedUser.IsAdmin == true)
            {
                rbtnYes.IsChecked = true;
            }
            else
                rbtnNo.IsChecked = true;
        }
        
        private void BtnCreateCustomer_Click(object sender, RoutedEventArgs e)
        {
            //Open the UserList window after Creating/Updating a user.
            Window srcUserlist = null;
            bool result = false;

            if (rbtnYes.IsChecked == true)
            {
                isAdministrator = 1;
            }
            //Store all textbox data into a User object.
            //Check weather this is a NEW user or to UPDATE user.
            if (BtnCreateCustomer.Content.ToString() == "Update Customer")
            {   
                if (!string.IsNullOrEmpty(txtUsername.Text))
                {
                    result = true;
                }
                else
                    MessageBox.Show("Username cannot be empty", "Invalid username");
                if (!string.IsNullOrEmpty(txtPassword.Text))
                {
                    result = true;
                }
                else
                    MessageBox.Show("Password cannot be empty", "Invalid Password");

                if (result)
                {
                    try
                    {
                        date = user.UserCreatedDate;
                        user = new User(Convert.ToInt32(txtUserID.Text), txtUsername.Text, txtPassword.Text, 
                                        Convert.ToBoolean(isAdministrator), date, false);
                        //Update selected user
                        UsersDB.UpdateCurrentUser(user);

                        srcUserlist = new UserList();
                        srcUserlist.Show();
                        Close();
                    }
                catch(Exception ex) { MessageBox.Show(ex.Message.ToString()); }
                }                  
            }
            else
            {
                if (!string.IsNullOrEmpty(txtUsername.Text))
                {
                    result = true;
                }
                else
                    MessageBox.Show("Username cannot be empty", "Invalid username");
                if (!string.IsNullOrEmpty(txtPassword.Text))
                {
                    result = true;
                }
                else
                    MessageBox.Show("Password cannot be empty", "Invalid Password");

                if (result)
                {
                    try
                    {
                        date = DateTime.Now;
                        user = new User(txtUsername.Text, txtPassword.Text, Convert.ToBoolean(isAdministrator), date, false);
                        //Create new user
                        UsersDB.CreateNewUser(user);

                        srcUserlist = new UserList();
                        srcUserlist.Show();
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
            }
        }

        private void BtnCancelCreate_Click(object sender, RoutedEventArgs e)
        {
            Window SrcUserList = new UserList();
            SrcUserList.Show();
            Close();
        }
    }
}
