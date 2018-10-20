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
    public static class UsersDB
    {
        //Get the new database connection in the ConnectionString class.
        public static SqlConnection connection = ConnectionString.GetSqlConnection();

        //Method to get the all users in the database users table.
        public static List<User> GetUsersList()
        {
            //create a list to store retrieved users.
            List<User> users = new List<User>();
            //Create SQL statement to select users.
            string SQLselectAll = "SELECT * FROM Users";
            //Create SQLCommand to use SQL string.
            SqlCommand cmdSelectAll = new SqlCommand(SQLselectAll, connection);
            //try catch finally block
            try
            {
                //Open conection
                connection.Open();
                //Use SQLDataReader to use cmd and read data from table.
                //DataReader = fast and lightweight / DataAdapter = holds record heavier but more options
                SqlDataReader reader = cmdSelectAll.ExecuteReader();
                while(reader != null && reader.Read())
                {
                    //retrieve records from database table. ( .Parse && Convert. to change datatypes)
                    User user = new User();
                    user.UserID = Int32.Parse(reader["UserId"].ToString());
                    user.Username = reader["Username"].ToString();
                    user.Password = reader["Password"].ToString();
                    user.IsAdmin = Boolean.Parse(reader["IsAdmin"].ToString());
                    user.UserCreatedDate = Convert.ToDateTime(reader["UserCreatedDate"]);
                    users.Add(user);
                }   //Close the SQLDataReader
                reader.Close();
            }
            catch(Exception e)
            {
                throw e;
            }
            finally
            {
                connection.Close();
            }
            return users;
        }
    }
}
