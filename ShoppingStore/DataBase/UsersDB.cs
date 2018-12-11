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
            try
            {
                connection.Open();
                //Use SQLDataReader to use cmd and read data from table.
                //DataReader = fast and lightweight / DataAdapter = holds record heavier but more options
                SqlDataReader reader = cmdSelectAll.ExecuteReader();
                while (reader != null && reader.Read())
                {
                    //retrieve records from database table. ( .Parse && Convert. to change datatypes)
                    User user = new User
                    {
                        UserID = Int32.Parse(reader["UserId"].ToString()),
                        Username = reader["Username"].ToString(),
                        Password = reader["Password"].ToString(),
                        IsAdmin = Boolean.Parse(reader["IsAdmin"].ToString()),
                        UserCreatedDate = Convert.ToDateTime(reader["UserCreatedDate"]),
                        IsCustomer = Boolean.Parse(reader["IsCustomer"].ToString())
                    };
                    users.Add(user);
                }   //Close the SQLDataReader
                reader.Close();
            }
            catch (Exception e) { throw e; }
            finally { connection.Close(); }
            return users;
        }
        //CREATE        ~CRUD FOR USER~
        //Method to create a new user and add it to the database.
        public static int CreateNewUser(User newUser)
        {
            string SQLinsertQuery = "INSERT INTO Users (Username, Password, IsAdmin, UserCreatedDate, IsCustomer) " +
                                    "VALUES(@username, @password, @isadmin, @usercreateddate, @iscustomer)";
            //SQL command
            SqlCommand cmdCreate = new SqlCommand(SQLinsertQuery, connection);
            cmdCreate.Parameters.AddWithValue("@username", newUser.Username);
            cmdCreate.Parameters.AddWithValue("@password", newUser.Password);
            cmdCreate.Parameters.AddWithValue("@isadmin", newUser.IsAdmin);
            cmdCreate.Parameters.AddWithValue("@usercreateddate", newUser.UserCreatedDate);
            cmdCreate.Parameters.AddWithValue("@iscustomer", newUser.IsCustomer);
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
            finally { connection.Close(); }
        }
        //READ
        //-GET USER AND ALL DATA BY ID AND EXECUTE BY SINGLEROW
        public static User GetUserById(int userId)
        {
            string SQLreadQuery = "SELECT * " +
                                  "FROM Users " +
                                  "WHERE UserId = @userid";
            SqlCommand cmdRead = new SqlCommand(SQLreadQuery, connection);
            cmdRead.Parameters.AddWithValue("@userid", userId);
            try
            {
                connection.Open();
                SqlDataReader reader = cmdRead.ExecuteReader(CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    User user = new User()
                    {                                           //SIMPLIFIED OBJECT INSTANTIATIONS
                        UserID = Convert.ToInt32(reader["UserId"]),
                        Username = reader["Username"].ToString(),
                        Password = reader["Password"].ToString(),
                        IsAdmin = Convert.ToBoolean(reader["IsAdmin"]),
                        UserCreatedDate = Convert.ToDateTime(reader["UserCreatedDate"]),
                        IsCustomer = Convert.ToBoolean(reader["IsCustomer"])
                    };
                    return user;
                }
                else { return null; }
            }
            catch(Exception ex)
            {
                ex.Message.ToString();
                throw ex;
            }
            finally { connection.Close(); }
        }
        //UPDATE
        //Method to update current selected user and store it in database.
        public static bool UpdateCurrentUser(User user)
        {
            bool result = false;
            string SQLupdateQuery = "UPDATE Users " +
                                    "SET Username = @username, Password = @password, IsAdmin = @isadmin, " +
                                    "UserCreatedDate = @usercreateddate, IsCustomer = @iscustomer " +
                                    "WHERE UserId = @userid";
            SqlCommand cmdUpdate = new SqlCommand(SQLupdateQuery, connection);
            cmdUpdate.Parameters.AddWithValue("@userid", user.UserID);
            cmdUpdate.Parameters.AddWithValue("@username", user.Username);
            cmdUpdate.Parameters.AddWithValue("@password", user.Password);
            cmdUpdate.Parameters.AddWithValue("@isadmin", user.IsAdmin);
            cmdUpdate.Parameters.AddWithValue("@usercreateddate", user.UserCreatedDate);
            cmdUpdate.Parameters.AddWithValue("@iscustomer", user.IsCustomer);
            try
            {
                connection.Open();
                cmdUpdate.ExecuteNonQuery();
                result = true;
            }
            catch(Exception ex)
            {
                ex.Message.ToString();
                throw ex;
            }
            finally { connection.Close(); }
            return result;
        }      
        //DELETE
        //Method to DELETE currently selected user.
        public static bool DeleteSelectedUser(User user)
        {
            bool result = false;
            string SQLdeleteQuery = "DELETE FROM Users " +
                                    "WHERE UserId=@userid AND Username=@username " +
                                    "AND Password=@password AND IsAdmin=@isadmin " +
                                    "AND UserCreatedDate=@usercreateddate";
            SqlCommand cmdDelete = new SqlCommand(SQLdeleteQuery, connection);
            cmdDelete.Parameters.AddWithValue("@userid", user.UserID);
            cmdDelete.Parameters.AddWithValue("@username", user.Username);
            cmdDelete.Parameters.AddWithValue("@password", user.Password);
            cmdDelete.Parameters.AddWithValue("@isadmin", user.IsAdmin);
            cmdDelete.Parameters.AddWithValue("@usercreateddate", user.UserCreatedDate);
            try
            {
                connection.Open();
                int row = cmdDelete.ExecuteNonQuery();
                if (row > 0)
                    result = true;
                else
                    result = false;
            }
            catch(Exception ex)
            {
                ex.Message.ToString();
                result = false;
                throw ex;
            }
            finally { connection.Close(); }
            return result;
        }

        //  ~ CRUD FOR CUSTOMERS ~
        //CREATE
        //myCommand.Parameters.AddWithValue("@AgeIndex",(AgeItem.AgeIndex== null) ? DBNull.Value : AgeItem.AgeIndex);
        //command.Parameters.AddWithValue("@param1", param1 ?? Convert.DBNull);
        public static int CreateCustomer(Customer customer)
        {
            string SQLcreateQuery = "INSERT INTO Customers (UserId, FirstName, LastName, Address, " +
                "City, State, ZipCode, EmailAddress) VALUES(@id, @fn, @ln, @ad, @ci, @st, @zc, @ea)";
            SqlCommand cmdCreate = new SqlCommand(SQLcreateQuery, connection);
            cmdCreate.Parameters.AddWithValue("@id", customer.UserId);
            cmdCreate.Parameters.AddWithValue("@fn", customer.FirstName ?? Convert.DBNull);
            cmdCreate.Parameters.AddWithValue("@ln", customer.LastName ?? Convert.DBNull);
            cmdCreate.Parameters.AddWithValue("@ad", customer.Address ?? Convert.DBNull);
            cmdCreate.Parameters.AddWithValue("@ci", customer.City ?? Convert.DBNull);
            cmdCreate.Parameters.AddWithValue("@st", customer.State ?? Convert.DBNull);
            cmdCreate.Parameters.AddWithValue("@zc", customer.ZipCode ?? Convert.DBNull);
            cmdCreate.Parameters.AddWithValue("@ea", customer.EmailAddress ?? Convert.DBNull);
            try
            {
                connection.Open();
                cmdCreate.ExecuteNonQuery();
                string SQLselect = "SELECT @@IDENTITY FROM Customers";
                SqlCommand cmdSELECT = new SqlCommand(SQLselect, connection);
                int CustomerId = Convert.ToInt32(cmdSELECT.ExecuteScalar());
                return CustomerId;
            }
            catch (Exception ex) { throw ex; }
            finally { connection.Close(); }
        }
        //READ
        public static Customer ReadCustomerById(int id)
        {
            string SQLreadQuery = "SELECT * FROM Customers WHERE UserId=@uid";
            SqlCommand cmdRead = new SqlCommand(SQLreadQuery, connection);
            cmdRead.Parameters.AddWithValue("@uid", id);
            try
            {
                connection.Open();
                SqlDataReader reader = cmdRead.ExecuteReader(CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    Customer customer = new Customer()
                    {
                        UserId = Convert.ToInt32(reader["UserId"]),
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        Address = reader["Address"].ToString(),
                        City = reader["City"].ToString(),
                        State = reader["State"].ToString(),
                        ZipCode = reader["ZipCode"].ToString(),
                        EmailAddress = reader["EmailAddress"].ToString()
                    };
                    return customer;
                }
                else
                    return null;
            }
            catch(Exception ex) { throw ex; }
            finally { connection.Close(); }
        }
        //UPDATE
        public static bool UpdateCustomer(Customer customer)
        {
            bool result = false;
            string SQLupdateQuery = "UPDATE Customers " +
                                    "SET FirstName=@fn, LastName=@ln, Address=@ad, " +
                                    "City=@ci, State=@st, ZipCode=@zc, EmailAddress=@ea " +
                                    "WHERE UserId=@cid";
            SqlCommand cmdUpdate = new SqlCommand(SQLupdateQuery, connection);
            cmdUpdate.Parameters.AddWithValue("@cid", customer.UserId);
            cmdUpdate.Parameters.AddWithValue("@fn", customer.FirstName ?? Convert.DBNull);
            cmdUpdate.Parameters.AddWithValue("@ln", customer.LastName ?? Convert.DBNull);
            cmdUpdate.Parameters.AddWithValue("@ad", customer.Address ?? Convert.DBNull);
            cmdUpdate.Parameters.AddWithValue("@ci", customer.City ?? Convert.DBNull);
            cmdUpdate.Parameters.AddWithValue("@st", customer.State ?? Convert.DBNull);
            cmdUpdate.Parameters.AddWithValue("@zc", customer.ZipCode ?? Convert.DBNull);
            cmdUpdate.Parameters.AddWithValue("@ea", customer.EmailAddress ?? Convert.DBNull);
            try
            {
                connection.Open();
                cmdUpdate.ExecuteNonQuery();
                result = true;
            }
            catch(Exception ex)
            {
                ex.Message.ToString();
                throw ex;
            }            
            finally { connection.Close(); }
            return result;
        }



        //EXTRA METHODS THAT I CAN/MAY USE AND/OR WANT TO TEST.
        //2ND UPDATE METHOD SQL QUERY "VOID" INSTEAD OF "INT"
        public static void UpdateSelectedUserVoid(User user)
        {
            string SQLupdateQuery = "UPDATE Users SET Username=@username, Password=@password, " +
                "IsAdmin=@isadmin, UserCreatedDate=@usercreateddate WHERE UserId=@userid";
            SqlCommand cmdUpdate = new SqlCommand(SQLupdateQuery, connection);
            cmdUpdate.Parameters.AddWithValue("@userid", user.UserID);
            cmdUpdate.Parameters.AddWithValue("@username", user.Username);
            cmdUpdate.Parameters.AddWithValue("@password", user.Password);
            cmdUpdate.Parameters.AddWithValue("@isadmin", user.IsAdmin);
            cmdUpdate.Parameters.AddWithValue("@usercreateddate", user.UserCreatedDate);
            try
            {
                connection.Open();
                cmdUpdate.ExecuteNonQuery();
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
            catch (SqlException sqlex)
            {
                sqlex.Message.ToString();
                throw sqlex;
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

    }
}
