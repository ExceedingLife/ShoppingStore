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
        //GetAllProductsInList
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
                        if(reader != null)
                        {
                            while (reader.Read())
                            {
                                Product product = new Product
                                {
                                    ProductId = Int32.Parse(reader["ProductId"].ToString()),
                                    ProductName = reader["ProductName"].ToString(),
                                    ProductPrice = Convert.ToDecimal(reader["ProductPrice"]),
                                    ProductTax = Convert.ToDecimal(reader["ProductTax"]),
                                    ProductListedDate = Convert.ToDateTime(reader["ProductListedDate"]),

                                    ProductQuantity = Int32.Parse(reader["ProductQuantity"].ToString()),
                                    //ProductTotal = Convert.ToDecimal(reader["ProductTotal"]),
                                    //ProductPurchasedDate = Convert.ToDateTime(reader["ProductPurchasedDate"])
                                    //Extra fields MAY or MAY NOT be needed.
                                    //UserId = Convert.ToInt32(reader["UserId"]),
                                    //ReceiptId = Convert.ToInt32(reader["ReceiptId"])
                                };
                            listProducts.Add(product);
                            }
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
                string sqlCreate = "INSERT INTO Products (ProductName, ProductPrice, ProductQuantity, " +
                    "ProductTax, ProductTotal, ProductListedDate, ProductPurchasedDate, UserId, ReceiptId) " +
                    "VALUES(@pn, @pp, @pq, @pta, @pto, @pld, @ppd, @uid, @rid)";
                using (SqlCommand cmdCreate = new SqlCommand(sqlCreate, connection))
                {
                    cmdCreate.Parameters.AddWithValue("@pn", product.ProductName);
                    cmdCreate.Parameters.AddWithValue("@pp", product.ProductPrice);
                    cmdCreate.Parameters.AddWithValue("@pta", product.ProductTax);
                    cmdCreate.Parameters.AddWithValue("@pld", product.ProductListedDate);
                    cmdCreate.Parameters.AddWithValue("@pq", product.ProductQuantity);                    
                    cmdCreate.Parameters.AddWithValue("@pto", product.ProductTotal);                    
                    cmdCreate.Parameters.AddWithValue("@ppd", product.ProductPurchasedDate);
                    //extra fields MAY or MAY NOT be needed
                    //cmdCreate.Parameters.AddWithValue("@uid", product.UserId);       // ?? Convert.DBNull);
                    //cmdCreate.Parameters.AddWithValue("@rid", product.ReceiptId);

                    connection.Open();
                    int result = cmdCreate.ExecuteNonQuery();
                    //check erreor
                    if (result < 0)
                        Console.WriteLine("error inserting db");
                }
            }
        }
        //Create Product from ProductAdd
        public static void CreateProductAdd(Product product)
        {
            using(SqlConnection connection = ConnectionString.GetSqlConnection())
            {
                string sqlCreate = "INSERT INTO Products (ProductName, ProductPrice, ProductTax, ProductQuantity, " +
                                   "ProductListedDate) VALUES(@pn, @pp, @pt, @pq, @pld)";
                using (SqlCommand cmdCreate = new SqlCommand(sqlCreate, connection))
                {
                    cmdCreate.Parameters.AddWithValue("@pn", product.ProductName);
                    cmdCreate.Parameters.AddWithValue("@pp", product.ProductPrice);
                    cmdCreate.Parameters.AddWithValue("@pt", product.ProductTax);
                    cmdCreate.Parameters.AddWithValue("@pq", product.ProductQuantity);
                    cmdCreate.Parameters.AddWithValue("@pld", product.ProductListedDate);

                    connection.Open();
                    int result = cmdCreate.ExecuteNonQuery();
                    if (result < 0)
                        Console.WriteLine("Error inserting database");
                }
            }
        }
        //Read product
        public static Product ReadProductById(int prodId)
        {
            Product product = new Product();

            using(SqlConnection connection = ConnectionString.GetSqlConnection())
            {
                string query = "SELET * " +
                               "FROM Products " +
                               "WHERE ProductId = @pid";
                using(SqlCommand cmdRead = new SqlCommand(query, connection))
                {
                    cmdRead.Parameters.AddWithValue("@pid", prodId);
                    using(SqlDataReader reader = cmdRead.ExecuteReader())
                    {
                        if (reader != null)
                        {
                            while (reader.Read())
                            {
                                Product product2 = new Product()
                                {
                                    ProductId = Convert.ToInt32(reader["ProductId"]),
                                    ProductName = reader["ProductName"].ToString(),
                                    ProductPrice = Convert.ToDecimal(reader["ProductPrice"]),
                                    ProductTax = Convert.ToDecimal(reader["ProductTax"]),
                                    ProductListedDate = Convert.ToDateTime(reader["ProductListedDate"]),                                                                     
                                    ProductQuantity = Convert.ToInt32(reader["ProductQuantity"]),
                                    ProductTotal = Convert.ToDecimal(reader["ProductTotal"]),
                                    ProductPurchasedDate = Convert.ToDateTime(reader["ProductPurchasedDate"])
                                    //UserId = Convert.ToInt32(reader["UserId"])
                                    //ReceiptId = Convert.ToInt32(reader["ReceiptId"])
                                };
                                product = product2;
                            }
                        }
                        else { product = null; }
                    }
                }                    
            }
            return product;
        }
        //update product
        public static bool UpdateProduct(Product product)
        {
            bool result = false;
            using(SqlConnection connection = ConnectionString.GetSqlConnection())
            {
                string query = "UPDATE Products " +
                               "SET ProductName = @pn, ProductPrice = @pp, " +
                               "ProductTax = @pt, ProductListedDate = @pld " +
                               "WHERE ProductId = @pid";
                using(SqlCommand cmdUpdate = new SqlCommand(query, connection))
                {
                    cmdUpdate.Parameters.AddWithValue("@pid", product.ProductId);
                    cmdUpdate.Parameters.AddWithValue("@pn", product.ProductName);
                    cmdUpdate.Parameters.AddWithValue("@pp", product.ProductPrice);
                    cmdUpdate.Parameters.AddWithValue("@pt", product.ProductTax);
                    cmdUpdate.Parameters.AddWithValue("@pld", product.ProductListedDate);                    

                    connection.Open();
                    cmdUpdate.ExecuteNonQuery();
                    result = true;
                }
            }
            return result;
        }
        //Delete product
        public static bool DeleteProduct(int prodId)
        {
            bool result = false;
            using(SqlConnection connection = ConnectionString.GetSqlConnection())
            {
                string query = "DELETE FROM Products " +
                               "WHERE ProductId = @pid";
                using(SqlCommand cmdDelete = new SqlCommand(query, connection))
                {
                    cmdDelete.Parameters.AddWithValue("@pid", prodId);

                    connection.Open();
                    cmdDelete.ExecuteNonQuery();
                    result = true;
                }
            }
            return result;
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
