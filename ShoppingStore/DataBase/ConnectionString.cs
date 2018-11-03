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
        /*
         *  Had to create a .MDF database in Server Explorer and/or SQL Server Object Explorer
         *  then create the tables needed. 
         *  In the SQL SERVER OBJECT EXPLORER created the database under (localdb)\MSSQLLocalDB 
         *  the db threw an error saying wrong version (Error 852 incorrect version - needed to be version
         *  701 or lower)
         *  Then i created database under (localdb)\v11.0 database created successfully without errors.
         *  Next opened the Data Sources window creating a DataSet to be able to access the sql database.
         */

        public static SqlConnection GetSqlConnection()
        {
            string connectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\extre_000\Documents\GitHub\ShoppingStore\ShoppingStore\StoreDB.mdf; Integrated Security = True; Connect Timeout = 80";

            //@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Andrew\Documents\GitHub\ShoppingStore\ShoppingStore\StoreDB.mdf;Integrated Security=True;";

            SqlConnection connection = new SqlConnection(connectionString);
            return connection;
        }
    }
}
