﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingStore.DataAccess
{
    public class User
    {
        //IMPLEMENTING/UPDATING C# CODE TO VERSION 7.0.
        //create UserID property
        private int _userId;
        public int UserID
        {
            get => _userId;                     //get { return _userId; }
            set => _userId = value;             //set { _userId = value; }
        }
        //create username property
        private string _username;
        public string Username
        {
            get => _username;                   //get { return username; }
            set => _username = value;           //set { _username = value; }
        }
        //create password property
        private string _password;
        public string Password
        {
            get => _password;                   //get { return _password; }
            set => _password = value;           //set { _password = value; }
        }
        //create isAdmin property
        private bool _isAdmin;
        public bool IsAdmin
        {
            get => _isAdmin;                    //get { return _isAdmin; }
            set => _isAdmin = value;            //set { _isAdmin = value; }
        }
        //Creating a new property thought of this while creating the Database.
        //create UserCreatedDate property
        private DateTime _userCreatedDate;
        public DateTime UserCreatedDate
        {
            get => _userCreatedDate;            //get { return _userCreatedDate; }
            set => _userCreatedDate = value;    //set { _userCreatedDate = value; }
        }
        //Created this property while figuring out inheritance - User : Customer
        //create isCustomer property 11/8/18
        private bool _isCustomer;
        public bool IsCustomer
        {
            get => _isCustomer;
            set => _isCustomer = value;
        }

        //Default Constructor for User class
        public User() { }

        //Constructor without UserId (USED FOR CREATING NEW USER)
        public User(string uname, string pass, bool isadmin, DateTime userDate, bool iscust)
        {
            Username = uname;
            Password = pass;
            IsAdmin = isadmin;
            UserCreatedDate = userDate;
            IsCustomer = iscust;
        }
        //Constructor with UserId (USED FOR UPDATING CURRENTLY SELECTED USER)
        public User(int id, string uname, string pass, bool isadmin, DateTime userDate)
        {
            UserID = id;
            Username = uname;
            Password = pass;
            IsAdmin = isadmin;
            UserCreatedDate = userDate;
        }
        //Constructor for CREATE CUSTOMER in ProfileScreen 11/8/18 ~
        public User(int id, string uname, string pass, bool isadmin, DateTime userDate, bool iscustom)
        {
            UserID = id;
            Username = uname;
            Password = pass;
            IsAdmin = isadmin;
            UserCreatedDate = userDate;
            IsCustomer = iscustom;
        }

        //Override the .ToString()
        public override string ToString()
        {
            return String.Format("ID: {4} - Username: {0} - Password: {1} - IsAdmin: {2} - UserCreatedDate: {3} - Cust: {4}",
                                  Username, Password, IsAdmin, UserCreatedDate, UserID, IsCustomer);
            //return base.ToString();
        }

        //Override the .Equals()
        //public override bool Equals(object obj)
        //{
        //    if(obj == null)
        //    {
        //        return false;
        //    }
        //    User user = (User)obj;

        //    return Username.Equals(user.Username) && Password.Equals(user.Password) && IsAdmin.Equals(user.IsAdmin);
        //    //return base.Equals(obj);
        //}

        //Override the .GetHashCode()
        public override int GetHashCode()
        {
            return ToString().GetHashCode();
            //return base.GetHashCode();
        }
    }
}
