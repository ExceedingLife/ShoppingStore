﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingStore.DataAccess
{
    public class Product
    {
        //THIS CLASS WILL BE USED WITH TRYING AUTO-IMPLEMENTED PROPERTIES

        //create productID property
        public int ProductID { get; set; }

        //create productName property
        public string ProductName { get; set; }

        //create productPrice property
        public decimal ProductPrice { get; set; }

        //create productQuantity property
        public int ProductQuantity { get; set; }

        //create productTax property
        public decimal ProductTax { get; set; }

        //create productTotal property
        public decimal ProductTotal { get; set; }

        //create userID property
        public int UserID { get; set; }

        //create receiptID property
        public int ReceiptID { get; set; }

//       ~Constructors listed below ~
        //Default Product Constructor
        public Product() { }

        //product property constructor
        //public Product()
        //{
        //    //all properties listed here
        //}
        //methods
    }
}