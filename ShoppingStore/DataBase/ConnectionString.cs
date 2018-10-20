using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace ShoppingStore.DataBase
{
    public class ConnectionString
    {
        public static SqlConnection GetSqlConnection()
        {
            string connectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Andrew\Documents\GitHub\ShoppingStore\ShoppingStore\StoreDB.mdf;Integrated Security=True;";
            SqlConnection connection = new SqlConnection(connectionString);
            return connection;
        }
    }
}
