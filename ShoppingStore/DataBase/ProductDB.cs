using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using ShoppingStore.DataAccess;

namespace ShoppingStore.DataBase
{
    public static class ProductDB
    {
        //public static SqlConnection conn = ConnectionString.GetSqlConnection();

        public static List<Product> GetProductList()
        {
             List<Product> listProducts = new List<Product>();

            using (SqlConnection connection = ConnectionString.GetSqlConnection())
            {
                connection.Open();
                using (SqlCommand cmdList = new SqlCommand( "SELECT * FROM Products", connection))
                {                    
                    using (SqlDataReader reader = cmdList.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Product product = new Product
                            {
                                ProductID = Int32.Parse(reader["ProductId"].ToString()),
                            };
                            listProducts.Add(product);
                        }
                    }
                }
                    
            }
            return listProducts;
        }
        //Create Product 
        public static void CreateProduct(Product product)
        {
            using (SqlConnection connection = ConnectionString.GetSqlConnection())
            {
                connection.Open();
                string sqlCreate = "INSERT INTO Products (ProductName, ProductPrice, ProductQuantity, " +
                    "ProductTax, ProductTotal, ProductListedDate, ProductPurchasedDate, UserID, ReceiptID " +
                    "VALUES(@pn, @pp, @pq, @pta, @pto, @pld, @ppd, @uid, @rid)";
                using (SqlCommand cmdCreate = new SqlCommand(sqlCreate, connection))
                {
                    cmdCreate.Parameters.AddWithValue("@pn", product.ProductName);
                    cmdCreate.Parameters.AddWithValue("@pp", product.ProductPrice);
                    cmdCreate.Parameters.AddWithValue("@pq", product.ProductQuantity);
                    cmdCreate.Parameters.AddWithValue("@pta", product.ProductTax);
                    cmdCreate.Parameters.AddWithValue("@pto", product.ProductTotal);
                    cmdCreate.Parameters.AddWithValue("@pld", product.ProductListedDate);
                    cmdCreate.Parameters.AddWithValue("@ppd", product.ProductPurchasedDate);
                    cmdCreate.Parameters.AddWithValue("@uid", product.UserID);       // ?? Convert.DBNull);
                    cmdCreate.Parameters.AddWithValue("@rid", product.ReceiptID);

                    cmdCreate.ExecuteNonQuery();
                }
            }
        }


        /*
         
            using(SqlConnection con = new SqlConnection("connection string"))
            {
                con.Open();
                using(SqlCommand cmd = new SqlCommand("SELECT * FROM SomeTable", connection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader != null)
                        {
                            while (reader.Read())
                            {
                                //do something
                            }
                        }
                    } // reader closed and disposed up here

                } // command disposed here

            } /


         * */
    }
}
