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

namespace ShoppingStore
{
    /// <summary>
    /// Interaction logic for ProductsList.xaml
    /// </summary>
    public partial class ProductsList : Window
    {
        private List<Product> productList = new List<Product>();
        private Product selectedProduct = null;
        private int selectedIndex = -1;
        DataTable dt = new DataTable();

        public ProductsList()
        {
            InitializeComponent();
            CreateProductList();
            RefreshProductList();
            //dt = DataTableProductList(productList);
            //DataGridProducts.ItemsSource = dt.AsDataView();
            //this.DataGridProducts.Columns[0].Width = 50;
            //this.DataGridProducts.Columns[1].Width = 150;
            //this.DataGridProducts.Columns[2].Width = 100;
            //this.DataGridProducts.Columns[3].Width = 50;
            //this.DataGridProducts.Columns[4].Width = 200;
            //DataGridProducts.ItemsSource = DataTableProductList(productList);
           // DataGridProducts.ro
        }
        
        private void CreateProductList()
        {            
            try
            {
                productList = ProductDB.GetProductList();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
        }
        private void RefreshProductList()
        {
            dt = DataTableProductList(productList);
            DataGridProducts.ItemsSource = dt.AsDataView();
        }
        private void GetCurrentProduct(int prodId)
        {
            try
            {
                selectedProduct = ProductDB.ReadProductById(prodId);
            }
            catch(Exception ex) { MessageBox.Show(ex.Message.ToString()); }
        }

        private void BtnProductAdd_Click(object sender, RoutedEventArgs e)
        {
            Window WindowProductAdd = new ProductAdd();
            WindowProductAdd.Show();
            Close();
        }

        private void BtnModifyProduct_Click(object sender, RoutedEventArgs e)
        {
            selectedIndex = DataGridProducts.SelectedIndex;

            if(selectedIndex != -1)
            {
                selectedProduct = productList[selectedIndex];

                //message to verify edit
                string msg = "Are you sure you want to edit: " + selectedProduct.ProductName + "?";
                MessageBoxResult boxResult = MessageBox.Show(msg, "Accept Modify", MessageBoxButton.YesNo, 
                                                             MessageBoxImage.Question);
                if(boxResult == MessageBoxResult.Yes)
                {
                    Window WindowProdAdd = new ProductAdd(selectedProduct);
                    WindowProdAdd.Show();
                    Close();
                }
            }
        }

        private void BtnDeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            selectedIndex = DataGridProducts.SelectedIndex;

            if (selectedIndex != -1)
            {
                try
                {
                    selectedProduct = productList[selectedIndex];
                }
                catch(ArgumentOutOfRangeException aex)
                {
                    MessageBox.Show("No Product Selected", "Delete Error");
                    return;
                }
                catch(Exception ex) { MessageBox.Show(ex.Message.ToString()); }

                string msg = "Are you sure you want to delete: " + selectedProduct.ProductName + "?";
                MessageBoxResult boxResult = MessageBox.Show(msg, "Delete Confirmation", MessageBoxButton.YesNo,
                                                             MessageBoxImage.Warning);
                if (boxResult == MessageBoxResult.Yes)
                {
                    try
                    {
                        //MessageBox.Show(selectedProduct.ToString());
                        if (!ProductDB.DeleteProduct(selectedProduct.ProductId))
                        {
                            GetCurrentProduct(selectedProduct.ProductId);
                        }
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
                }
            }
            else
                MessageBox.Show("No Product Selected", "Delete Error");

            //RefreshProductList();
            Window WindowPList = new ProductsList();
            WindowPList.Show();
            Close();
        }

        public static DataTable DataTableProductList(List<Product> prodList)
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("ID");
            dataTable.Columns.Add("Product Name");
            dataTable.Columns.Add("Product Price");
            dataTable.Columns.Add("Quantity");
            dataTable.Columns.Add("Product Listed Date");

            foreach(var p in prodList)
            {
                var row = dataTable.NewRow();
                row["ID"] = p.ProductId;
                row["Product Name"] = p.ProductName;
                row["Product Price"] = string.Format("{0:C}", p.ProductPrice);
                row["Quantity"] = p.ProductQuantity;
                row["Product Listed Date"] = p.ProductListedDate;
                dataTable.Rows.Add(row);
            }
            return dataTable;
        }
    }
}