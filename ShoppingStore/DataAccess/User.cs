using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingStore.DataAccess
{
    public class User
    {
        //create UserID property
        private int userID;
        public int UserID
        {
            get
            {
                return userID;
            }
            set
            {
                userID = value;
            }
        }
        //create username property
        private string username;
        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
            }
        }
        //create password property
        private string password;
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
            }
        }
        //THINK ABOUT OTHER OPTIONS FOR REGISTERING AS AN ADMINISTRATOR!?*
        //create isAdmin property
        private bool isAdmin;
        public bool IsAdmin
        {
            get
            {
                return isAdmin;
            }
            set
            {
                isAdmin = value;
            }
        }
        //Creating a new property thought of this while creating the Database.
        //create UserCreatedDate property
        private DateTime userCreatedDate;
        public DateTime UserCreatedDate
        {
            get
            {
                return userCreatedDate;
            }
            set
            {
                userCreatedDate = value;
            }
        }

        //Default Constructor for User class
        public User() { }

        //Constructor without userID
        public User(string uname, string pass, bool isadmin, DateTime userDate)
        {
            this.Username = uname;
            this.Password = pass;
            this.IsAdmin = isadmin;
            this.UserCreatedDate = userDate;
        }
        //Constructor with USERID not sure which constructor ill be using yet WITH OR WITHOUT ID
        public User(int id, string uname, string pass, bool isadmin, DateTime userDate)
        {
            this.UserID = id;
            this.Username = uname;
            this.Password = pass;
            this.IsAdmin = isadmin;
            this.UserCreatedDate = userDate;
        }
        //Constructor for UPDATE and to keep orginal DateCreated
        public User(string uname, string pass, bool isadmin)
        {
            this.Username = uname;
            this.Password = pass;
            this.IsAdmin = isadmin;
        }

        //Override the .ToString()
        public override string ToString()
        {
            return String.Format("Username: {0} - Password: {1} - IsAdmin: {2}", Username, Password, IsAdmin);
            //return base.ToString();
        }

        //Override the .Equals()
        public override bool Equals(object obj)
        {
            if(obj == null)
            {
                return false;
            }
            User user = (User)obj;

            return Username.Equals(user.Username) && Password.Equals(user.Password) && IsAdmin.Equals(user.IsAdmin);
            //return base.Equals(obj);
        }

        //Override the .GetHashCode()
        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
            //return base.GetHashCode();
        }
    }
}
