﻿using System;
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
using System.Data;
using System.Data.SqlClient;
using ShoppingStore.DataAccess;
using ShoppingStore.DataBase;

namespace ShoppingStore
{
    public partial class AdminWindow : Window
    {
        private User adminUser = null;

        public AdminWindow()
        {
            InitializeComponent();
        }
        public AdminWindow(User user)
        {
            adminUser = user;
            InitializeComponent();
            //Display specific username on window.
            TextBlockName.Text = "Hello:" + Environment.NewLine +user.Username;
        }
        
        private void BtnUserList_Click(object sender, RoutedEventArgs e)
        {
            //Button click to show the UsersList screen.
            Window srcUsersList = new UserList();
            srcUsersList.ShowDialog();
            this.Close();
        }

        private void BtnUserEdit_Click(object sender, RoutedEventArgs e)
        {
            //Button click to show the UsersAdd screen.
            Window srcUsersAdd = new CustomerAdd();
            srcUsersAdd.Show();
            this.Close();
            //.Show() lets user click parent window
            //.ShowDialog does not let user click parent window
        }

        private void BtnProductList_Click(object sender, RoutedEventArgs e)
        {
            Window WindowProductList = new ProductsList();
            WindowProductList.Show();
            Close();
        }

        private void BtnProductEdit_Click(object sender, RoutedEventArgs e)
        {
            Window WindowProductAdd = new ProductAdd();
            WindowProductAdd.Show();
            Close();
        }

        private void BtnReceiptList_Click(object sender, RoutedEventArgs e)
        {
            Window WindowReceiptList = new ReceiptList(adminUser);
            WindowReceiptList.Show();
            Close();
        }

        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
