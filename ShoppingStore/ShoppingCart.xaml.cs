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
    /// Interaction logic for ShoppingCart.xaml
    /// </summary>
    public partial class ShoppingCart : Window
    {
        private Customer customer = null;
        private Product selectedProduct = null;
        //private List<string> listProductNames = new List<string>();               listProductNames = lpn;
        //private List<decimal> listProductPrices = new List<decimal>();                listProductPrices = lpp;
        //private List<decimal> listProductSalesTax = new List<decimal>();              listProductSalesTax = lpt;
        //private List<int> listProductQuantity = new List<int>();              listProductQuantity = lpq;
        //List<string> lpn, List<decimal> lpp, List<decimal> lpt, List<int> lpq, 

        private List<Product> listOfP = new List<Product>();
        //public List<Product> Cart => new List<Product>();
        public List<Product> Cart { get; } = new List<Product>();

        DateTime date = new DateTime();
        Receipt receiptCart = new Receipt();
        decimal productTotal = 0m;
        int selectedIndex = -1;


        public ShoppingCart()
        {
            InitializeComponent();
        }
        public ShoppingCart(Customer c, List<Product> listprod)
        {
            InitializeComponent();
            customer = c;
            listOfP = listprod;
            //CreateCart(listOfP);
            PopulateListViewCart();
            receiptCart = CalculateTotal(listOfP);
            LblProductTotalPrice.Content = string.Format("{0:C}", receiptCart.ReceiptTotal);
            LblHelloName.Content = Extras.CapitalizeFirstLetter(customer.FirstName);
        }

        private void CreateCart(List<Product> lcart)
        {
            //selectedProduct = new Product(); foreach(string name in lpn){selectedProduct.ProductName = name;}
            //foreach(decimal price in lpp)//{ //    selectedProduct.ProductPrice = price; //}
            //foreach(decimal tax in lpt) //{/    selectedProduct.ProductTax = tax;  //}
            //foreach(int quantity in lpq){selectedProduct.ProductQuantity = quantity;} Cart.Add(selectedProduct);
            foreach(Product product in listOfP)
            {
                Cart.Add(product);
            }
        }

        private void PopulateListViewCart()
        {
            try
            {
                ListViewCart.ItemsSource = listOfP;
            }
            catch(Exception ex) { MessageBox.Show(ex.Message.ToString()); }
        }
        private Receipt CalculateTotal(List<Product> products)
        {
            Receipt receipt = new Receipt();
            string name = "";
            int quantity = 0;
            decimal tax = 0m, ttax = 0m,  price = 0m, subtotal = 0m, total = 0m, sub = 0m;
            if (products != null)
            {
                foreach (Product p in products)
                {
                    name = p.ProductName;
                    price = p.ProductPrice;
                    quantity = p.ProductQuantity;
                    tax = p.ProductTax;
                    ttax = price * tax;
                    subtotal = price * quantity;
                    total = subtotal + ttax;
                    sub += subtotal;
                    productTotal += total;

                    receipt.ProductNames.Add(name);
                    receipt.ProductQuantity.Add(quantity);
                    receipt.SalesTax.Add(tax);
                    receipt.ProductsSubtotal.Add(sub);
                    receipt.ProductsTotal.Add(price);
                    receipt.ReceiptTotal = productTotal;
                }
            }
            else
                MessageBox.Show("error calculating product total", "Error Calculating");

            return receipt;
        }

        private void ListViewProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedIndex = ListViewCart.SelectedIndex;

            if(selectedIndex != -1)
            {
                selectedProduct = listOfP[selectedIndex];
            }
        }

        private void BtnDeleteFromCart_Click(object sender, RoutedEventArgs e)
        {
            if (selectedIndex != -1)
            {
                string msg = "Are you sure you want to remove-" + selectedProduct.ProductName +
                             "\nFrom your Shopping Cart?";
                MessageBoxResult boxResult = MessageBox.Show(msg, "Delete Confirmation", MessageBoxButton.YesNo);
                if(boxResult == MessageBoxResult.Yes)
                {
                    listOfP.Remove(selectedProduct);
                    Window WindowCart = new ShoppingCart(customer, listOfP);
                    WindowCart.Show();
                    Close();
                }
            }
            else
                MessageBox.Show("No product currently selected", "No Selection");
        }

        private void BtnConfirmCart_Click(object sender, RoutedEventArgs e)
        {
            if (listOfP.Count != 0) //!= null?
            {
                string msg = "Are you sure you CONFIRM your cart?";
                MessageBoxResult boxResult = MessageBox.Show(msg, "Confirmation", MessageBoxButton.YesNo);
                if(boxResult == MessageBoxResult.Yes)
                {
                    //Confirm cart /Add to SQL(Create Receipt, Create Product List) /Go to receipt.
                    try
                    {
                        date = DateTime.Now;
                        decimal total = receiptCart.ReceiptTotal;
                        int userid = customer.UserId;
                        int receiptid = Extras.CreateReceipt(userid, total, date);
                        foreach(Product p in listOfP)
                        {
                            Extras.CreateProductInOrder(receiptid, userid, p.ProductName, p.ProductPrice, p.ProductQuantity, p.ProductTax);
                        }

                        Window WindowReceipt = new ReceiptWindow();
                        WindowReceipt.Show();
                        Close();
                    }
                    catch(Exception ex) { MessageBox.Show(ex.Message.ToString()); }
                }
            }
            else
            {
                BtnGoBack.Visibility = Visibility.Hidden;
                MessageBox.Show("No items in your cart.", "Add items.");
            }
        }

        private void BtnGoBack_Click(object sender, RoutedEventArgs e)
        {
            Window WindowProductCart = new ProductCart(customer);
            WindowProductCart.Show();
            Close();
        }
    }
}
