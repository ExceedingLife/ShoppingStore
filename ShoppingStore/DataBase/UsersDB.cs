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

        //SQL_cmd_Query.ExecuteScalar(); - only returns value from first row of the first column.
        //SQL_cmd_Query.ExecuteReader(); returns object that can iterate over entire result keeping one record in memory.
        //SQL_cmd_Query.ExecuteNonQuery(); - Does not return data, only # of rows affected by insert, update, or delete.

        //SqlDataReader is used for querying data from a single SQL table.
        //DataTable is a subitem of a dataset and represents a database stored in memory.
        //SqlCommand is responsible with sending the SQL query to the server and returning the results.
        //SqlDataAdapter is responsible with filling a dataset with the data returned from database.

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
                while (reader != null && reader.Read())
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
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                connection.Close();
            }
            return users;
        }

        //Method to create a new user and add it to the database.
        public static int CreateNewUser(User newUser)
        {
            //SQL Create Query
            string SQLinsertQuery = "INSERT INTO Users (Username, Password, IsAdmin, UserCreatedDate) " +
                "VALUES(@username, @password, @isadmin, @usercreateddate)";
            //SQL command
            SqlCommand cmdCreate = new SqlCommand(SQLinsertQuery, connection);
            cmdCreate.Parameters.AddWithValue("@username", newUser.Username);
            cmdCreate.Parameters.AddWithValue("@password", newUser.Password);
            cmdCreate.Parameters.AddWithValue("@isadmin", newUser.IsAdmin);
            cmdCreate.Parameters.AddWithValue("@usercreateddate", newUser.UserCreatedDate);
            try
            {
                connection.Open();
                cmdCreate.ExecuteNonQuery();
                string SQLselect = "SELECT @@IDENTITY FROM Users";
                SqlCommand cmdSelect = new SqlCommand(SQLselect, connection);
                int userId = Convert.ToInt32(cmdSelect.ExecuteScalar());
                return userId;
            }
            catch (SqlException sql)
            {
                sql.Message.ToString();
                throw sql;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return -1;
            }
            finally
            {
                connection.Close();
            }
        }

        //Read specific user by id and return it
        public static User ReadUserById(int userId)
        {
            User user = new User();
            //SQL Query
            string SQLreadQuery = "SELECT * FROM Users WHERE UserId =" + userId;
            //SQL Command
            SqlCommand cmdRead = new SqlCommand(SQLreadQuery, connection);            
            try
            {
                connection.Open();

                //SQLDataReader 
                SqlDataReader reader = cmdRead.ExecuteReader();

                if (reader.Read())
                {
                    user.UserID = Convert.ToInt32(reader["UserId"]);
                    user.Username = reader["Username"].ToString();
                    user.Password = reader["Password"].ToString();
                    user.IsAdmin = Convert.ToBoolean(reader["IsAdmin"]);
                    user.UserCreatedDate = Convert.ToDateTime(reader["UserCreatedDate"]);
                }
                return user;
            }
            catch(SqlException sqlex)
            {
                sqlex.Message.ToString();
                throw sqlex;
            }
            catch(Exception ex)
            {
                ex.Message.ToString();
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

    }
}
