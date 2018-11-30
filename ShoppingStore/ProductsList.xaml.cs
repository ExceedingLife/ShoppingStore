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
        public ProductsList()
        {
            InitializeComponent();
        }

        private void BtnProductAdd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnModifyProduct_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnDeleteProduct_Click(object sender, RoutedEventArgs e)
        {

        }

        public static DataTable DataTableProductList(List<Product> prodList)
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("ProductId");
            dataTable.Columns.Add("ProductName");
            dataTable.Columns.Add("ProductPrice");
            dataTable.Columns.Add("ProductQuantity");
            dataTable.Columns.Add("ProductListedDate");
            foreach(var p in prodList)
            {
                var row = dataTable.NewRow();
                row["ProductId"] = p.ProductID;//.ToString();
                row["ProductName"] = p.ProductName;
                row["ProductPrice"] = p.ProductPrice;
                row["ProductQuantity"] = p.ProductQuantity;
                row["ProductListedDate"] = p.ProductListedDate;
                dataTable.Rows.Add(row);
            }
            return dataTable;
        }
    }
}
/*
static DataTable ConvertToDatatable(List<Item> list)
{
    DataTable dt = new DataTable();

    dt.Columns.Add("Name");
    dt.Columns.Add("Price");
    dt.Columns.Add("URL");
    foreach (var item in list)
    {
        var row = dt.NewRow();

        row["Name"] = item.Name;
        row["Price"] = Convert.ToString(item.Price);
        row["URL"] = item.URL;

        dt.Rows.Add(row);
    }

    return dt;
}*/