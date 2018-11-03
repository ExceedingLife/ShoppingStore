using System;
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

        //create productListedDate property
        public DateTime ProductListedDate { get; set; }

        //create productPurchasedDate property
        public DateTime ProductPurchasedDate { get; set; }

        //create userID property
        public int UserID { get; set; }

        //create receiptID property
        public int ReceiptID { get; set; }

//       ~Constructors listed below ~
        //Default Product Constructor
        public Product() { }

        //Product ALL Properties Constructor w/ID's. (USED FOR CREATING NEW PRODUCT)
        public Product(int prodId, string pName, decimal pPrice, int pQuan, decimal pTax, 
                       decimal pTotal, DateTime pListedDate, DateTime pPurchasedDate, 
                       int userId, int receiptId)
        {
            ProductID = prodId;
            ProductName = pName;
            ProductPrice = pPrice;
            ProductQuantity = pQuan;
            ProductTax = pTax;
            ProductTotal = pTotal;
            ProductListedDate = pListedDate;
            ProductPurchasedDate = pPurchasedDate;
            UserID = userId;
            ReceiptID = receiptId;
        }
        //Product ALL Properties Constructor w/o ProdID.
        public Product(string pName, decimal pPrice, int pQuan, decimal pTax,
                       decimal pTotal, DateTime pListedDate, DateTime pPurchasedDate, 
                       int userId, int receiptId)
        {
            ProductName = pName;
            ProductPrice = pPrice;
            ProductQuantity = pQuan;
            ProductTax = pTax;
            ProductTotal = pTotal;
            ProductListedDate = pListedDate;
            ProductPurchasedDate = pPurchasedDate;
            UserID = userId;
            ReceiptID = receiptId;
        }
        //methods
    }
}
