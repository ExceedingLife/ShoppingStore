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
        int isAdministrator = 0;

        public CustomerAdd()
        {
            InitializeComponent();
        }

        //Populate CustomerAdd window with User data.
        public CustomerAdd(User u)
        {
            this.user = u;
            InitializeComponent();
            PopulateUserData(u);
        }

        private void PopulateUserData(User selectedUser)
        {
            txtUserID.Text = selectedUser.UserID.ToString(); ;
            txtUsername.Text = selectedUser.Username.ToString();
            txtPassword.Text = selectedUser.Password.ToString();
            //if(selectedUser.IsAdmin == true)
            //{
            //    rbtnYes = this.rbtnYes.Checked;
            //}

        }

        private void btnCreateCustomer_Click(object sender, RoutedEventArgs e)
        {
            // user = new User();
            date = DateTime.Now;
            if (rbtnYes.IsChecked == true)
            {
                isAdministrator = 1;
            }
            //else
            //{
            //    isAdministrator = 0;
            //}
            user = new User(txtUsername.Text, txtPassword.Text, Convert.ToBoolean(isAdministrator), date);

            //Insert new user object into database.
            UsersDB.CreateNewUser(user);

            //Open the UserList window after creating new user.
            Window srcUserlist = new UserList();
            srcUserlist.Show();
            this.Close();
        }

        private void btnAdminScreen_Click(object sender, RoutedEventArgs e)
        {
            //Go back to Administrator Screen
            Window adminSRC = new AdminWindow();
            adminSRC.Show();
            this.Close();
        }

        private void btnCustomerList_Click(object sender, RoutedEventArgs e)
        {
            //Go back to Userlist Screen
            Window userlistSRC = new UserList();
            userlistSRC.Show();
            this.Close();
        }
    }
}
