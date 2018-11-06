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
//--------------------------------------------------
using ShoppingStore.StoreDBDataSetTableAdapters;

namespace ShoppingStore
{
    /// <summary>
    /// Interaction logic for CustomerAdd.xaml
    /// </summary>
    public partial class CustomerAdd : Window
    {
        private User user = null;
        private DateTime date = new DateTime();
        private int isAdministrator = 0;

        public CustomerAdd()
        {
            InitializeComponent();
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

            if (rbtnYes.IsChecked == true)
            {
                isAdministrator = 1;
            }
            //Store all textbox data into a User object.
            //Check weather this is a NEW user or to UPDATE user.
            if (BtnCreateCustomer.Content.ToString() == "Update Customer")
            {   
                try
                {
                    date = user.UserCreatedDate;
                    user = new User(Convert.ToInt32(txtUserID.Text), txtUsername.Text, txtPassword.Text, 
                                    Convert.ToBoolean(isAdministrator), date);
                    //Update selected user
                    //UsersDB.UpdateSelectedUserVoid(user);
                    UsersDB.UpdateCurrentUser(user);

                    srcUserlist = new UserList();
                    srcUserlist.Show();
                    Close();
                    //this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }                   
            }
            else
            {
                try
                {
                    date = DateTime.Now;
                    user = new User(txtUsername.Text, txtPassword.Text, Convert.ToBoolean(isAdministrator), date);
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

        private void BtnAdminScreen_Click(object sender, RoutedEventArgs e)
        {
            //Go back to Administrator Screen
            Window adminSRC = new AdminWindow();
            adminSRC.Show();
            this.Close();
        }

        private void BtnCustomerList_Click(object sender, RoutedEventArgs e)
        {
            //Go back to Userlist Screen
            Window userlistSRC = new UserList();
            userlistSRC.Show();            
            this.Close();
        }
    }
}
