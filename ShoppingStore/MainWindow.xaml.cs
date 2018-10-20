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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ShoppingStore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnUserList_Click(object sender, RoutedEventArgs e)
        {
            //Button click to show the UsersList screen.
            Window srcUsersList = new UserList();
            srcUsersList.ShowDialog();
            this.Close();
        }

        private void btnUserEdit_Click(object sender, RoutedEventArgs e)
        {
            //Button click to show the UsersAdd screen.
            Window srcUsersAdd = new CustomerAdd();
            srcUsersAdd.ShowDialog();
            this.Close();
        }
    }
}
