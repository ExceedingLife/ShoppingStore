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
    /// Interaction logic for ProductCart.xaml
    /// </summary>
    public partial class ProductCart : Window
    {
        //Property for Data-Binding products to listview.
        public List<Product> ProductList => ProductDB.GetProductList(); //new List<Product>();
        private Product selectedProduct = null;
        //private User user = null;
        private Customer customer = null;
        int selectedIndex = -1;
        //private List<string> listProductNames = new List<string>();
        //private List<decimal> listProductPrices = new List<decimal>();
        //private List<decimal> listProductSalesTax = new List<decimal>();
        //private List<int> listProductQuantity = new List<int>();
        private List<Product> listProductCart = new List<Product>();

        public ProductCart()
        {
            InitializeComponent();
            PopulateListview();
        }
        public ProductCart(Customer insertedUser)
        {
            InitializeComponent();
            PopulateListview();
            customer = insertedUser;
            if(insertedUser != null)
            {
                LblHelloName.Content = "Hello " + Extras.CapitalizeFirstLetter(insertedUser.Username) + ".";

                if(insertedUser.IsCustomer == true)
                {
                    try
                    {
                        //customer = UsersDB.ReadCustomerById(insertedUser.UserID);
                        LblHelloName.Content = "Hello " + Extras.CapitalizeFirstLetter(customer.FirstName) + ".";
                    }
                    catch (Exception ex) { MessageBox.Show("Did not read customer status", "Error"); }                    
                }
            }
        }

        private void PopulateListview()
        {
            ListViewProducts.Items.Clear();
            try
            {
                ListViewProducts.ItemsSource = ProductList;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
        }

        private void BtnAddToCart_Click(object sender, RoutedEventArgs e)
        {
            int prodQuantity;
            bool moveOn = false;

            if(selectedIndex == -1)
            {
                MessageBox.Show("No Product Selected", "No Selection");
            }
            else
            {
                if(selectedProduct != null)
                {
                    string msg = "Are you sure you want to add " + selectedProduct.ProductName + " to your cart?";
                    MessageBoxResult boxResult = MessageBox.Show(msg, "Confirm Cart", MessageBoxButton.YesNo, 
                                                                 MessageBoxImage.Question);
                    if(boxResult == MessageBoxResult.Yes)
                    {
                        if (!string.IsNullOrEmpty(TxtQuantity.Text))
                        {
                            try
                            {
                                prodQuantity = Convert.ToInt32(TxtQuantity.Text);
                                moveOn = true;

                                selectedProduct.ProductQuantity = Convert.ToInt32(TxtQuantity.Text);
                            }
                            catch (Exception ex) { MessageBox.Show("Quantity must be an Integer."); }
                        }
                        else
                            MessageBox.Show("Need to enter a 'quantity'");

                        if (moveOn)
                        {
                            //Add selected ProductName, ProductPrice, & ProductSalesTax to Lists.
                            //listProductNames.Add(selectedProduct.ProductName);
                            //listProductPrices.Add(selectedProduct.ProductPrice);
                            //listProductSalesTax.Add(selectedProduct.ProductTax);
                            //listProductQuantity.Add(selectedProduct.ProductQuantity);
                            //------------------------------
                            listProductCart.Add(selectedProduct);
                            //Clear textboxes of currently selected product.
                            TxtProductName.Text = "";
                            TxtQuantity.Text = "";
                            selectedIndex = -1;
                            selectedProduct = null;
                        }
                    }
                    //to-do : adjust item quantity --ProductQuantity. or quantity-=quantity
                }
            }
        }

        private void ListViewProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedIndex = ListViewProducts.SelectedIndex;

            if(selectedIndex != -1)
            {
                selectedProduct = ProductList[selectedIndex];
            //Set selected product in textbox.
                TxtProductName.Text = selectedProduct.ProductName;
            }
        }

        private void BtnViewCart_Click(object sender, RoutedEventArgs e)
        {
            if(listProductCart.Any())          //||.Count==0   (listProductNames.Any())
            {
                Window WindowShoppingCart = new ShoppingCart(customer, listProductCart);
                WindowShoppingCart.Show();
                Close();
            }
            else
                MessageBox.Show("Nothing in your shopping cart", "Empty cart");
        }
    }
}



 
 //listProductNames, listProductPrices, listProductSalesTax, listProductQuantity,

  

