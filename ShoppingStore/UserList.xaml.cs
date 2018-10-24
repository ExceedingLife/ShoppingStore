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
    /// Interaction logic for UserList.xaml
    /// </summary>
    public partial class UserList : Window
    {
        //List to store all of the users from database table.
        public List<User> users = new List<User>();
        int selectedIndex = -1;


        public UserList()
        {
            InitializeComponent();
            PopulateListBox();
        }

        //Make method to populate listbox on startup
        private void PopulateListBox()
        {
            lstUserList.Items.Clear();
            users = UsersDB.GetUsersList();

            foreach (User user in users)
            {
                lstUserList.Items.Add(user);
            }
        }

        //Method to make a boolean value say 'YES' or 'NO'
        //public static string MakeBooleanYesOrNo(this bool value)
        //{
        //    string boolvalue = "";

        //    if (value.ToString() == "True")
        //    {
        //        boolvalue = "Yes";
        //    }
        //    else
        //    {
        //        boolvalue = "No";
        //    }
        //    //return value.ToString();
        //    return boolvalue;
        //}


        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            //Create a brand new user.
            Window screenNewUser = new CustomerAdd();
            screenNewUser.Show();
            this.Close();
        }

        private void btnModifyUser_Click(object sender, RoutedEventArgs e)
        {
            selectedIndex = lstUserList.SelectedIndex;

            if(selectedIndex != -1)
            {
                User user = users[selectedIndex];
                UsersDB.ReadUserById(selectedIndex);

                //Create message to verify edit.
                string message = "Are you sure you want to edit: " + user.Username + "?";
                MessageBoxResult readUser = MessageBox.Show(message, "Accept Modify", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if(readUser == MessageBoxResult.Yes)
                {
                    Window edit = new CustomerAdd(user);
                    edit.Show();
                    this.Close();
                }
            }
        }

    }
}
