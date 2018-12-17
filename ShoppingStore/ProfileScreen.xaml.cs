using System;
using System.Collections.Generic;
using System.Collections;
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
    public partial class ProfileScreen : Window
    {
        private bool boolBtnPush = false;
        private bool boolIsCustomer = false;
        private User currentUser = null;
        private Customer currentCustomer = null;        
        // Field below MUST BE PROPERTY for data-binding.
        //public List<US_State> Get_States = StatesArray.Get_States;
        public List<US_State> Get_States => StatesArray.Get_States;

        public ProfileScreen()
        {
            InitializeComponent();
            DataContext = this;
            DisableControls();
        }

        public ProfileScreen(User user)
        {
            currentUser = user;
            DataContext = this;
            InitializeComponent();            
            DisableControls();

            if (user.IsCustomer == true)
            {
                currentCustomer = UsersDB.ReadCustomerById(user.UserID);
                PopulateControls(currentCustomer);
                EnableControls();
                BtnCmenu.Visibility = Visibility.Visible;
            }            
        }


        private void PopulateControls(Customer insertedCustomer)
        {
            TextboxFirstName.Text = insertedCustomer.FirstName.ToString();
            TextboxLastName.Text = insertedCustomer.LastName.ToString();
            TextboxAddress.Text = insertedCustomer.Address.ToString();
            TextboxCity.Text = insertedCustomer.City.ToString();
            ComboboxState.SelectedValue = insertedCustomer.State;
            TextboxZip.Text = insertedCustomer.ZipCode.ToString();
            TextEmailAddress.Text = insertedCustomer.EmailAddress.ToString();
            //Combobox Customer TRUE/FALSE - YES/NO selection
            int selection = 1;
            if(currentUser.IsCustomer == true)
            {
                selection = 2;
            }
            CboCustomer.SelectedIndex = selection;
        }

        private void DisableControls()
        {
            TextboxFirstName.IsEnabled = false;
            TextboxLastName.IsEnabled = false;
            TextboxAddress.IsEnabled = false;
            TextboxCity.IsEnabled = false;
            ComboboxState.IsEnabled = false;
            TextboxZip.IsEnabled = false;
            TextEmailAddress.IsEnabled = false;
        }
        private void EnableControls()
        {            
            TextboxFirstName.IsEnabled = true;
            TextboxLastName.IsEnabled = true;
            TextboxAddress.IsEnabled = true;
            TextboxCity.IsEnabled = true;
            ComboboxState.IsEnabled = true;
            TextboxZip.IsEnabled = true;
            TextEmailAddress.IsEnabled = true;            
        }

        private void BtnProfileEdit_Click(object sender, RoutedEventArgs e)
        {
            //Change button content to 'Save' on first btn click.
            Customer newCreatedCustomer = new Customer();            
            EnableControls();

            if (boolBtnPush == true)
            {
                if (BtnProfileEdit.Content.Equals("Save")) // || if(BtnProfileEdit.Content.ToString() == "Save")
                {
                    if(boolIsCustomer == true)
                    {
                        if (TextboxFirstName.Text.Equals(""))
                        {   //Display messagebox so user MUST enter firstname.
                            MessageBox.Show("You 'MUST' enter a First name,", "WARNING", MessageBoxButton.OK);
                        }
                        else
                        {
                            try
                            {
                                var userId = currentUser.UserID;
                                var firstname = TextboxFirstName.Text;
                                var lastname = TextboxLastName.Text;
                                var address = TextboxAddress.Text;
                                var city = TextboxCity.Text;
                                var cboState = ComboboxState.SelectedValue;
                                var zipcode = TextboxZip.Text;
                                var email = TextEmailAddress.Text;

                                if (cboState != null)
                                {
                                    newCreatedCustomer = new Customer(userId, firstname, lastname, address, city,
                                                                      cboState.ToString(), zipcode, email);
                                    UsersDB.UpdateCustomer(newCreatedCustomer);
                                    MessageBox.Show("Database updated.", currentUser.Username + " Profile Update",
                                                     MessageBoxButton.OK);
                                }
                                else
                                    MessageBox.Show("Select your state.", "SELECT STATE");
                                                                                               
                                Window SrcCustomerScreen = new CustomerScreen(currentUser);
                                SrcCustomerScreen.Show();
                                Close();
                            }
                            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
                        }
                    }
                    else
                    {
                        MessageBox.Show("You must select 'Yes' in combobox Customer\n" +
                            "to add customer data.", "UnAcceptable", MessageBoxButton.OK);
                    }                  
                }
            }
            //ON THE FIRST BUTTON CLICK DO THIS SO ON SECOND ABOVE CODE IS EXECUTED.
            boolBtnPush = true;
            BtnProfileEdit.Content = "Save";
        }

        private void CboCustomer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string cboValue = "";
            if (CboCustomer.SelectedIndex > 0)
                cboValue = ((ComboBoxItem)CboCustomer.SelectedItem).Content.ToString();

            if(cboValue.Equals("Yes")) // || if (CboCustomer.SelectedItem.ToString().Equals("Yes"))
            {
                boolIsCustomer = true;
                User addCustomer = null;
                Customer newCustomer = null;

                //MessageBoxResult result = MessageBox.Show("Updating database to customer status.",
                //    "Customer Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                //if (result == MessageBoxResult.Yes)
                //{
                if (currentUser.IsCustomer == true)
                {
                    try
                    {                          
                        newCustomer = UsersDB.ReadCustomerById(currentUser.UserID);
                        //Customer custTestID = UsersDB.ReadCustomerById(currentUser.UserID);
                        //MessageBox.Show(newCustomer.ToString());
                        if (currentUser.UserID == newCustomer.UserId)//custTestID.UserID)
                        {
                            return;
                        }                        
                        //else
                        //{
                        //    addCustomer = new User(currentUser.UserID, currentUser.Username,
                        //    currentUser.Password, currentUser.IsAdmin, currentUser.UserCreatedDate, boolIsCustomer);
                        //    UsersDB.UpdateCurrentUser(addCustomer);

                        //    newCustomer = new Customer(currentUser.UserID, currentUser.Username, TextboxLastName.Text,
                        //                            TextboxAddress.Text, TextboxCity.Text, null, 
                        //                            TextboxZip.Text, TextEmailAddress.Text);
                        //    UsersDB.CreateCustomer(newCustomer);
                        //}
                    }
                    catch(Exception ex) { MessageBox.Show(ex.Message.ToString()); }
                }
                else
                {
                    try
                    {   //UPDATE User so 'IsCustomer' property is set to True.
                        addCustomer = new User(currentUser.UserID, currentUser.Username, currentUser.Password,
                                                currentUser.IsAdmin, currentUser.UserCreatedDate, boolIsCustomer);
                        UsersDB.UpdateCurrentUser(addCustomer);
                        //CREATE Customer from current User linking together by 'UserId'
                        newCustomer = new Customer(currentUser.UserID, currentUser.Username, TextboxLastName.Text,
                                                    TextboxAddress.Text, TextboxCity.Text, null, TextboxZip.Text,
                                                    TextEmailAddress.Text);
                        UsersDB.CreateCustomer(newCustomer);
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
                }
            }
        }

        private void BtnCmenu_Click(object sender, RoutedEventArgs e)
        {
            //Button to go back to main menu
            Window SrcCustomerMenu = new CustomerScreen(currentUser);
            SrcCustomerMenu.Show();
            Close();
        }
    }
}
