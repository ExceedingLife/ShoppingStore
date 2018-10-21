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


        public UserList()
        {
            InitializeComponent();
            //use the PopulateListBox() to populate listbox with database table.
            PopulateListBox();
        }

        //Make method to populate listbox on startup
        private void PopulateListBox()
        {
            lstUserList.Items.Clear();
            users = UsersDB.GetUsersList();
            //use just add or "+ \n"
            foreach(User user in users)
            {
                lstUserList.Items.Add(user);
            }
        }

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
