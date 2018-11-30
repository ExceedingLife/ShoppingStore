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
    /// <summary>
    /// Interaction logic for ProfileScreen.xaml
    /// </summary>
    public partial class ProfileScreen : Window
    {
        private bool boolBtnPush = false;
        private bool boolIsCustomer = false;

        private User currentUser = null;
        private Customer testcus = null;


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
            InitializeComponent();

            DataContext = this;
            DisableControls();

            if (user.IsCustomer == true)
            {
                Customer customerLoaded = UsersDB.ReadCustomerById(user.UserID);
                PopulateControls(customerLoaded);
                EnableControls();
                CboCustomer.SelectedIndex = 1;
                string loadedState = customerLoaded.State;
                ComboboxState.SelectedValue = loadedState;
            }            
           // typeof(SomeType).IsAssignableFrom(typeof(Derived))
           // typeof(Derived).IsSubclassOf(typeof(SomeType))
        }


        private void PopulateControls(Customer insertedCustomer)
        {
            TextboxFirstName.Text = insertedCustomer.FirstName.ToString();
            TextboxLastName.Text = insertedCustomer.LastName.ToString();
            TextboxAddress.Text = insertedCustomer.Address.ToString();
            TextboxCity.Text = insertedCustomer.City.ToString();
            //STATE combobox select
            TextboxZip.Text = insertedCustomer.ZipCode.ToString();
            TextEmailAddress.Text = insertedCustomer.EmailAddress.ToString();
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
            //User NewCustomer = null;
            //NewCustomer = UsersDB.GetUserById(currentCustomer.UserID);
            //Customer NewCustomer = null;
            //NewCustomer =(Customer) UsersDB.GetUserById(currentCustomer.UserID);
            //NewCustomer = (Customer)currentCustomer;
            //(User) NewCustomer = currentCustomer;
            //currentCustomer = (User)NewCustomer;
            //currentCustomer = NewCustomer;
            //Change button content to 'Save'.
            Customer newCreatedCustomer = new Customer();


            EnableControls();
            //MessageBox.Show(currentCustomer.GetType() + " VS " + NewCustomer.GetType());

            if (boolBtnPush == true)
            {
                if (BtnProfileEdit.Content.Equals("Save"))
                //if(BtnProfileEdit.Content.ToString() == "Save")
                //error label.
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
                                                                                               
                                //Window SrcCustomerScreen = new CustomerScreen(currentUser);
                                ////or use newcustomer to insert values.
                                //SrcCustomerScreen.Show();
                                //Close();
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

        private void Btntest_Click(object sender, RoutedEventArgs e)
        {
            var stateI = ComboboxState.SelectedItem;
            var stateV = ComboboxState.SelectedValue;
            MessageBox.Show("I: " + stateI);
            MessageBox.Show("V: " + stateV);
            MessageBox.Show(currentUser.ToString()+"\n ID " +currentUser.UserID);
        }

        private void CboCustomer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string cboValue = "";
            if (CboCustomer.SelectedIndex > 0)
                cboValue = ((ComboBoxItem)CboCustomer.SelectedItem).Content.ToString();

            if(cboValue.Equals("Yes"))
            //if (CboCustomer.SelectedItem..ToString().Equals("Yes"))
            {
                boolIsCustomer = true;
                User addCustomer = null;
                Customer newCustomer = null;

                MessageBoxResult result = MessageBox.Show("Updating database to customer status.",
                    "Customer Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                if (result == MessageBoxResult.Yes)
                {
                    if (currentUser.IsCustomer == true)
                    {
                        try
                        {
                            //bool boole = false;

                            Customer custTestID = UsersDB.ReadCustomerById(currentUser.UserID); //?? custTestID == null;
                                //? boole : custTestID = null;

                            if(currentUser.UserID == custTestID.UserID)
                            {
                                return;
                            }
                            else
                            {
                                addCustomer = new User(currentUser.UserID, currentUser.Username,
                                currentUser.Password, currentUser.IsAdmin, currentUser.UserCreatedDate, boolIsCustomer);
                                UsersDB.UpdateCurrentUser(addCustomer);

                                newCustomer = new Customer(currentUser.UserID, currentUser.Username, TextboxLastName.Text,
                                                        TextboxAddress.Text, TextboxCity.Text, null, 
                                                        TextboxZip.Text, TextEmailAddress.Text);
                                UsersDB.CreateCustomer(newCustomer);
                            }
                        }
                        catch(Exception ex) { MessageBox.Show(ex.Message.ToString()); }
                    }
                    // return;
                    //PopulateControls(customerLoaded);
                }
                //else // boolIsCustomer = false;
            }
        }
    }

}
