using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingStore.DataAccess
{
    public class Customer : User
    {
        //Properties for the newly created Customer class. V 7.0
        //CustomerId Property Foreign key of UserID.
        //private int _customerId;
        //public int CustomerId
        //{
        //    get => _customerId;
        //    set => _customerId = value;
        //}
        ////First name property.
        //private string _firstname;
        //public string FirstName
        //{
        //    get => _firstname;
        //    set => _firstname = value;
        //}

    // ~ SWITCHING OVER TO USE AUTO-IMPLEMENTED PROPERTIES ~
        //CustomerId Property needed to create table 
        public int CustomerId { get; set; }

        //UserId inherits/foreign key of UserId. v2.0
        public int UserId { get; set; }

        //FirstName Property.
        public string FirstName { get; set; }

        //LastName Property.
        public string LastName { get; set; }

        //Address Property.
        public string Address { get; set; }

        //City Property.
        public string City { get; set; }

        //State Property.
        public string State { get; set; }

        //Zip Code Property.
        public string ZipCode { get; set; }

        //Email Address Property.
        public string EmailAddress { get; set; }

    // ~ CONSTRUCTORS FOR THE CUSTOMER OBJECT CLASS.        
        //Default Customer Constructor.
        public Customer() { }

        //Contructor for Profile Screen / New Customer.
        public Customer(int uid, string fname, string lname, string address, string city, 
                        string state, string zipcode, string email)
        {
            UserId = uid;
            FirstName = fname;
            LastName = lname;
            Address = address;
            City = city;
            State = state;
            ZipCode = zipcode;
            EmailAddress = email;
        }
        //Contructor for Profile Screen / Update Customer.
        public Customer(string fname, string lname, string address, string city,
                        string state, string zipcode, string email)
        {
            FirstName = fname;
            LastName = lname;
            Address = address;
            City = city;
            State = state;
            ZipCode = zipcode;
            EmailAddress = email;
        }
        //Constructor that inherits all properties.
        public Customer(int id, string uname, string pass, bool isadmin, DateTime udate, bool iscus, string fname, 
                        string lname, string add, string city, string state, string zip, string email)
                :base(id, uname, pass, isadmin, udate, iscus)
        {
            UserId = id;
            FirstName = fname;
            LastName = lname;
            Address = add;
            City = city;
            State = state;
            ZipCode = zip;
            EmailAddress = email;
        }

        //.ToString() overridden to display all User + Customer properties.
        public override string ToString()
        {
            return string.Format("Id: {1} - Username: {2} - Password: {3} - IsAdmin: {4} - CreatedDate: {5} - " +
                "FirstName: {6} - LastName: {7} - Address: {8} - City: {9} - State: {10} - ZipCode: {11} - " +
                "EmailAddress: {12}", UserId, Username, Password, IsAdmin, UserCreatedDate, FirstName, 
                 LastName, Address, City, State, ZipCode, EmailAddress);
            //return base.ToString();
        }
    }
}
