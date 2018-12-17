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
            if (!r.ProductNames.Any())
            {
                AddProductsToReceipt(receipt);
            }
            PopulateCustomerDetailsReceipt(customer);
            PopulateReceiptDetails(receipt);
            PopulateProductsData(receipt);
            CreatePopulateTotals(receipt);
        }
        public ReceiptWindow(User u, Receipt r)
        {
            InitializeComponent();
            user = u;
            receipt = r;
            //Get Customer data from UserId and List of Product data.
            GetCutomerFromUserId(receipt.UserId);
            AddProductsToReceipt(receipt);
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
        private void GetCutomerFromUserId(int uid)
        {
            if(uid != 0)
            {
                try
                {
                    customer = UsersDB.ReadCustomerById(uid);
                }
                catch(Exception ex) { MessageBox.Show(ex.Message.ToString()); }
            }
        }
        private void AddProductsToReceipt(Receipt r)
        {
            List<ReturnReceiptProductList> productList = new List<ReturnReceiptProductList>();
            try
            {
                //retrieve orderlist table to populate products in receipt.
                productList = Extras.GetReceiptProductList(r.ReceiptID);
                if(productList != null)
                {
                    foreach(ReturnReceiptProductList p in productList)
                    {
                        r.ProductNames.Add(p.Product.ProductName);
                        r.ProductsTotal.Add(p.Product.ProductPrice);
                        r.ProductQuantity.Add(p.Product.ProductQuantity);
                        r.SalesTax.Add(p.Product.ProductTax);
                        //calculate subtotal to be used on receipt
                        decimal subtotal = p.Product.ProductPrice * p.Product.ProductQuantity;
                        r.ProductsSubtotal.Add(subtotal);
                    }
                }
                
            }
            catch(Exception ex) { MessageBox.Show(ex.Message.ToString()); }
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
            int numberofproducts = 0;

            date = "Date: " + receipt.ReceiptDate.ToString();
            receiptid = "ReceiptID: " + receipt.ReceiptID.ToString("00000");
            userid = "UserID: " + receipt.UserId.ToString();
            foreach(int p in receipt.ProductQuantity)
            {
                numberofproducts += p;
            }
            TxtReceiptData.Text = date + "\n" + receiptid + "\n" + userid + "\nNumber of products: " + numberofproducts;
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
            if(user.IsAdmin == true)
            {
                Window WindowAdminMenu = new AdminWindow(user);
                WindowAdminMenu.Show();
                Close();
            }
            else
            {
                //user in customerscreen constructor
                Window WindowCustomerMenu = new CustomerScreen(user);
                WindowCustomerMenu.Show();
                Close();
            }
        }
    }
}
