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
        // private User CastErrorUserFix = null;
        // private Customer currentCustomer = null;
        private bool boolBtnPush = false;
        private bool boolIsCustomer = false;

        private User currentUser = null;
        private Customer customerLoaded = null;
        private Customer testcus = null;
        //private List<string> statesList = StateArray.Abbreviations().ToList();
        DataSet dataSetStates = new DataSet();

        public ProfileScreen()
        {
            InitializeComponent();

            CreateDataTableStates();
            BindComboBoxStates();
            DisableControls();
        }
        //public ProfileScreen(Customer customer)

        public ProfileScreen(User user)
        {
            currentUser = user;
            InitializeComponent();
            CreateDataTableStates();
            BindComboBoxStates();

            //CastErrorUserFix = customer;
            //if(user.IsCustomer == true)
            //{
            //    customerLoaded = UsersDB.ReadCustomerById(user.UserID);
            //    PopulateControls(customerLoaded);
            //}
            // PopulateControls(customer);

            DisableControls();
           // typeof(SomeType).IsAssignableFrom(typeof(Derived))
           // typeof(Derived).IsSubclassOf(typeof(SomeType))
        }

        private void BindComboBoxStates()
        {
            DataView dataview = dataSetStates.Tables[0].DefaultView;
            ComboboxState.ItemsSource = dataview;
            ComboboxState.DisplayMemberPath = dataSetStates.Tables[0].Columns["Name"].ToString();
            ComboboxState.SelectedValuePath = dataSetStates.Tables[0].Columns["Abbreviations"].ToString();
        }
        private void CreateDataTableStates()
        {
            DataTable dataTableStates = new DataTable("States");
            dataTableStates.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("Name", typeof(string)),
                new DataColumn("Abbreviations", typeof(string))
            });

            foreach(US_State st in StateArray.States())
            {
                dataTableStates.Rows.Add(st.Name);
            }
            dataSetStates.Tables.Add(dataTableStates);
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
            
            EnableControls();
            //MessageBox.Show(currentCustomer.GetType() + " VS " + NewCustomer.GetType());

            if (boolBtnPush == true)
            {
                if (BtnProfileEdit.Content.Equals("Save"))
                //if(BtnProfileEdit.Content.ToString() == "Save")
                //error label.
                {
                    if(boolIsCustomer == true)
                    //if (currentUser.IsCustomer == true)
                    //if (NewCustomer.FirstName.Equals(""))
                    {
                        if (TextboxFirstName.Text.Equals(""))
                        {   //Display messagebox so user MUST enter firstname.
                            MessageBox.Show("You 'MUST' enter a First name,", "WARNING", MessageBoxButton.OK);
                        }
                        else
                        {
                            try
                            {
                                //Customer newCreatedCustomer = new Customer();
                                //customerLoaded
                                Customer
                                newCreatedCustomer = new Customer(currentUser.UserID, TextboxFirstName.Text,
                                    TextboxLastName.Text, TextboxAddress.Text, TextboxCity.Text,
                                    ComboboxState.SelectedValue.ToString(), TextboxZip.Text, TextEmailAddress.Text);
                                UsersDB.UpdateCustomer(newCreatedCustomer);

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
            MessageBox.Show(currentUser.ToString()+"\n ID " +currentUser.UserID);
        }

        private void CboCustomer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string cboValue = "";
            if (CboCustomer.SelectedIndex > 0)
                cboValue = ((ComboBoxItem)CboCustomer.SelectedItem).Content.ToString();
                //(ComboBoxItem)CboCustomer.SelectedItem)
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
                    try
                    {
                        addCustomer = new User(currentUser.UserID, currentUser.Username,
                            currentUser.Password, currentUser.IsAdmin, currentUser.UserCreatedDate, boolIsCustomer);
                        UsersDB.UpdateCurrentUser(addCustomer);
                        //Create New Customer
                        newCustomer = new Customer(currentUser.UserID, currentUser.Username, null, null,
                                                    null, null, null, null);
                        UsersDB.CreateCustomer(newCustomer);

                        //Load the newly created customer object so it can be read.
                        //customerLoaded = UsersDB.ReadCustomerById(currentUser.UserID);
                        //PopulateControls(customerLoaded);
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
                }
                //else
                   // boolIsCustomer = false;
            }
        }
    }

    //Class to be used in Combo Box. State name and abbrevation.
    public class US_State
    {
        public string Name { get; set; }

        public string Abbreviations { get; set; }

        public US_State()
        {
            Name = null;
            Abbreviations = null;
        }

        public US_State(string ab, string name)
        {
            Name = name;
            Abbreviations = ab;
        }

        public override string ToString()
        {
            return string.Format("{0} - {1}", Abbreviations, Name);
        }
    }
    //STATIC ARRAY CLASS WHICH CONTAINS ALL THE STATE DATA.
    public static class StateArray
    {
        static List<US_State> states;

        static StateArray()
        {
            states = new List<US_State>(50);
            states.Add(new US_State("AL", "Alabama"));
            states.Add(new US_State("AK", "Alaska"));
            states.Add(new US_State("AZ", "Arizona"));
            states.Add(new US_State("AR", "Arkansas"));
            states.Add(new US_State("CA", "California"));
            states.Add(new US_State("CO", "Colorado"));
            states.Add(new US_State("CT", "Connecticut"));
            states.Add(new US_State("DE", "Delaware"));
            states.Add(new US_State("DC", "District Of Columbia"));
            states.Add(new US_State("FL", "Florida"));
            states.Add(new US_State("GA", "Georgia"));
            states.Add(new US_State("HI", "Hawaii"));
            states.Add(new US_State("ID", "Idaho"));
            states.Add(new US_State("IL", "Illinois"));
            states.Add(new US_State("IN", "Indiana"));
            states.Add(new US_State("IA", "Iowa"));
            states.Add(new US_State("KS", "Kansas"));
            states.Add(new US_State("KY", "Kentucky"));
            states.Add(new US_State("LA", "Louisiana"));
            states.Add(new US_State("ME", "Maine"));
            states.Add(new US_State("MD", "Maryland"));
            states.Add(new US_State("MA", "Massachusetts"));
            states.Add(new US_State("MI", "Michigan"));
            states.Add(new US_State("MN", "Minnesota"));
            states.Add(new US_State("MS", "Mississippi"));
            states.Add(new US_State("MO", "Missouri"));
            states.Add(new US_State("MT", "Montana"));
            states.Add(new US_State("NE", "Nebraska"));
            states.Add(new US_State("NV", "Nevada"));
            states.Add(new US_State("NH", "New Hampshire"));
            states.Add(new US_State("NJ", "New Jersey"));
            states.Add(new US_State("NM", "New Mexico"));
            states.Add(new US_State("NY", "New York"));
            states.Add(new US_State("NC", "North Carolina"));
            states.Add(new US_State("ND", "North Dakota"));
            states.Add(new US_State("OH", "Ohio"));
            states.Add(new US_State("OK", "Oklahoma"));
            states.Add(new US_State("OR", "Oregon"));
            states.Add(new US_State("PA", "Pennsylvania"));
            states.Add(new US_State("RI", "Rhode Island"));
            states.Add(new US_State("SC", "South Carolina"));
            states.Add(new US_State("SD", "South Dakota"));
            states.Add(new US_State("TN", "Tennessee"));
            states.Add(new US_State("TX", "Texas"));
            states.Add(new US_State("UT", "Utah"));
            states.Add(new US_State("VT", "Vermont"));
            states.Add(new US_State("VA", "Virginia"));
            states.Add(new US_State("WA", "Washington"));
            states.Add(new US_State("WV", "West Virginia"));
            states.Add(new US_State("WI", "Wisconsin"));
            states.Add(new US_State("WY", "Wyoming"));
        }

        public static string[] Abbreviations()
        {
            List<string> abbrevList = new List<string>(states.Count);
            foreach (var state in states)
            {
                abbrevList.Add(state.Abbreviations);
            }
            return abbrevList.ToArray();
        }

        public static string[] Names()
        {
            List<string> nameList = new List<string>(states.Count);
            foreach (var state in states)
            {
                nameList.Add(state.Name);
            }
            return nameList.ToArray();
        }

        public static US_State[] States()
        {
            return states.ToArray();
        }
    }
}
