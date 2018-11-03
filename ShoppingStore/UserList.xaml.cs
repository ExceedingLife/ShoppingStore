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


        //Get the current selected User by Id
        private void GetCurrentUser(int userid)
        {
            try
            {
                selectedUser = UsersDB.GetUserById(userid);
                
                ////selectedProduct = UsersDB.ReadUserById(userid);
                //if(lstUserList.SelectedIndex != -1) //{ }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.GetType().ToString());
            }
        }

        private void BtnAddUser_Click(object sender, RoutedEventArgs e)
        {
            //Create a brand new user.
            Window screenNewUser = new CustomerAdd();
            screenNewUser.Show();
            this.Close();
        }

        private void BtnModifyUser_Click(object sender, RoutedEventArgs e)
        {
            selectedIndex = lstUserList.SelectedIndex;

            if (selectedIndex != -1)
            {
                User user = users[selectedIndex];
                //do i need to read by id below?
                UsersDB.ReadUserById(selectedIndex);

                //Create message to verify edit.
                string message = "Are you sure you want to edit: " + user.Username + "?";
                MessageBoxResult readUser = MessageBox.Show(message, "Accept Modify", 
                    MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (readUser == MessageBoxResult.Yes)
                {
                    Window edit = new CustomerAdd(user);
                    edit.Show();
                    this.Close();
                }
            }
        }

        //Delete the currently selected User in Listview.       ~POPULATE AFTER BTNCLICK :: TO-DO~
        private void BtnDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            //Get a message to confirm the delete.
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete?",
                "Confirmation of Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if(result == MessageBoxResult.Yes)
            {
                try
                {
                    if(lstUserList.SelectedIndex != -1)
                    {   
                        // UsersDB.DeleteCurrentUser(users[lstUserList.SelectedIndex]);
                        if (!UsersDB.DeleteSelectedUser(users[lstUserList.SelectedIndex]))
                        {
                            this.GetCurrentUser(selectedUser.UserID);
                        }
                       // PopulateListView();
                    }
                    else
                    {
                        MessageBox.Show("No selection made, plz try again.", "Delete Btn Error");
                    }                    
                }   //Possible Format Error Exception available.
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }                
            }            
        }
    }
}
