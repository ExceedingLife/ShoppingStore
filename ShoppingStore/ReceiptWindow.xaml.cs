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
using ShoppingStore.DataAccess;
using ShoppingStore.DataBase;

namespace ShoppingStore
{
    /// <summary>
    /// Interaction logic for ReceiptWindow.xaml
    /// </summary>
    public partial class ReceiptWindow : Window
    {
        private Customer customer = null;
        private Receipt receipt = null;
        private User user = null;

        public ReceiptWindow()
        {
            InitializeComponent();
        }
        public ReceiptWindow(Customer c, Receipt r)
        {
            InitializeComponent();
            customer = c;
            receipt = r;
            GetUser(customer);
            PopulateCustomerDetailsReceipt(customer);
            PopulateReceiptDetails(receipt);
            PopulateProductsData(receipt);
            CreatePopulateTotals(receipt);
        }

        private void GetUser(Customer c)
        {
            if(c != null)
            {
                if (c.UserId != 0)
                {
                    try
                    {
                        user = UsersDB.GetUserById(c.UserId);
                    }
                    catch(Exception ex) { MessageBox.Show(ex.Message.ToString()); }
                }
                else
                    MessageBox.Show("Error retrieving UserId", "Invalid Id");
            }
        }
        private void PopulateCustomerDetailsReceipt(Customer customer)
        {
            string fullname = "";
            string fulladdress = "";
            string fullcitystate = "";
            string emailaddress = "";

            fullname = customer.FirstName + " " + customer.LastName;
            fulladdress = customer.Address;
            fullcitystate = customer.City + " " + customer.State + " " + customer.ZipCode;
            emailaddress = customer.EmailAddress;

            TxtCustomerDetails.Text = fullname + "\n" + fulladdress + "\n" + fullcitystate + "\n" + emailaddress;
        }
        private void PopulateReceiptDetails(Receipt receipt)
        {
            string date = "";
            string receiptid = "";
            string userid = "";
            string numberofproducts = "";

            date = "Date: " + receipt.ReceiptDate.ToString();
            receiptid = "ReceiptID: " + receipt.ReceiptID.ToString("00000");
            userid = "UserID: " + receipt.UserId.ToString();
            numberofproducts = "Number of products: " + receipt.ProductNames.Count.ToString();

            TxtReceiptData.Text = date + "\n" + receiptid + "\n" + userid + "\n" + numberofproducts;
        }
        private void PopulateProductsData(Receipt receipt)
        {
            foreach(string name in receipt.ProductNames)
            {
                TxtProductNameData.Text += name + "\n";
            }
            foreach(decimal price in receipt.ProductsTotal)
            {
                TxtProductPriceData.Text += price.ToString("C2") + "\n";
            }
            foreach(int quantity in receipt.ProductQuantity)
            {
                TxtProductQuantityData.Text += quantity + "\n";
            }
            foreach(decimal subtotal in receipt.ProductsSubtotal)
            {
                TxtProductTotalData.Text += subtotal.ToString("C2") + "\n";
            }
            
        }
        private void CreatePopulateTotals(Receipt receipt)
        {
            decimal subtotal = 0m;
            decimal tax = 0m;

            for (int i = 0; i < receipt.ProductsTotal.Count; i++)
            {
                subtotal += receipt.ProductsTotal[i] * receipt.ProductQuantity[i];
            }

            tax = receipt.ReceiptTotal - subtotal;

            TxtProductNameData.Text += "\n\nSubtotal: " + subtotal.ToString("C2");
            TxtProductNameData.Text += "\nTax Added: " + tax.ToString("C2");
            TxtProductNameData.Text += "\nTotal: " + receipt.ReceiptTotal.ToString("C2");
        }

        private void BtnMenu_Click(object sender, RoutedEventArgs e)
        {
            //user in customerscreen constructor
            Window WindowCustomerMenu = new CustomerScreen(user);
            WindowCustomerMenu.Show();
            Close();
        }
    }
}
