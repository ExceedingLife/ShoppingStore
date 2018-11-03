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
        public User selectedUser = null;
        int selectedIndex = -1;

        public UserList()
        {
            InitializeComponent();
            PopulateListView();
        }

        //Make method to populate listbox on startup
        private void PopulateListView()
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


        //Get the currently selected UserId
        private void GetCurrentUser(int userid)
        {
            try
            {
                selectedUser = UsersDB.GetUserById(userid);
                
                //selectedUser = UsersDB.ReadUserById(userid);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.GetType().ToString());
            }
        }

        private void BtnAddUser_Click(object sender, RoutedEventArgs e)
        {
            Window screenNewUser = new CustomerAdd();
            screenNewUser.Show();
            this.Close();
        }

        private void BtnModifyUser_Click(object sender, RoutedEventArgs e)
        {
            selectedIndex = lstUserList.SelectedIndex;

            if (selectedIndex != -1)
            {
                selectedUser = users[selectedIndex];

                //Create message to verify edit.
                string message = "Are you sure you want to edit: " + selectedUser.Username + "?";
                MessageBoxResult readUser = MessageBox.Show(message, "Accept Modify", 
                    MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (readUser == MessageBoxResult.Yes)
                {
                    Window edit = new CustomerAdd(selectedUser);
                    edit.Show();
                    this.Close();
                }
            }
        }

        //Delete the currently selected User in Listview.
        private void BtnDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            selectedUser = users[lstUserList.SelectedIndex];

            //DISPLAY A MESSAGEBOX TO CONFIRM DELETE.
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete: " +
                selectedUser.Username + "?", "Delete Confirmation", 
                MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if(result == MessageBoxResult.Yes)
            {
                try
                {
                    if(lstUserList.SelectedIndex != -1)
                    {
                        if (!UsersDB.DeleteSelectedUser(users[lstUserList.SelectedIndex]))
                        {
                            GetCurrentUser(selectedUser.UserID);                            
                        }                        
                    }
                    else
                    {
                        MessageBox.Show("No selection made, plz try again.", "Delete Btn Error");
                    }                     
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }  
                PopulateListView();
            }            
        }
    }
}
