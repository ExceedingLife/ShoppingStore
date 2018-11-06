using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using ShoppingStore.DataAccess;
using System.Windows;

namespace ShoppingStore.DataBase
{
    public static class SQLuserAccess
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



        public static User UserLogin(string username, string password)
        {
            //SQL Login Query.
            string SQLloginQuery = "SELECT * FROM Users WHERE Username=@username AND Password=@password";
            SqlCommand cmdLogin = new SqlCommand(SQLloginQuery, connection);
            cmdLogin.Parameters.AddWithValue("@username", username);
            cmdLogin.Parameters.AddWithValue("@password", password);
            try
            {
                connection.Open();
                //int result = (int)cmdLogin.ExecuteScalar();
                SqlDataReader reader = cmdLogin.ExecuteReader(CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    User user = new User
                    {
                        UserID = Convert.ToInt32(reader["UserId"]),
                        Username = reader["Username"].ToString(),
                        Password = reader["Password"].ToString(),
                        IsAdmin = Convert.ToBoolean(reader["IsAdmin"]),
                        UserCreatedDate = Convert.ToDateTime(reader["UserCreatedDate"])
                    };
                    return user;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }


        //user but trying STRING instead to verify login
        public static string UserLoginString(string username, string password)
        {
            string result;
            //SQL Login Query.
            string SQLloginQuery = "SELECT UserId FROM Users WHERE Username=@username AND Password=@password";
            SqlCommand cmdLogin = new SqlCommand(SQLloginQuery, connection);
            cmdLogin.Parameters.AddWithValue("@username", username);
            cmdLogin.Parameters.AddWithValue("@password", password);
            try
            {
                connection.Open();
                result = Convert.ToString(cmdLogin.ExecuteScalar());               
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                throw ex;
            }
            finally
            {
                connection.Close();
            }
            return result;
        }




        public static int SHOW_CreateNewUser(User newUser)
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
            catch (Exception e) { throw e; }
        }
    }
}
