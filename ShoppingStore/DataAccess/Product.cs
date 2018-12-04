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
        public int ProductId { get; set; }

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
        /*public int UserId { get; set; }

        //create receiptID property
        public int ReceiptId { get; set; }*/



//       ~Constructors listed below ~
        //Default Product Constructor
        public Product() { }

        //creating product constructor for SQL database
        public Product(string pname, decimal pprice, decimal ptax, int pquantity, DateTime plisteddate)
        {
            ProductName = pname;
            ProductPrice = pprice;
            ProductTax = ptax;
            ProductQuantity = pquantity;
            ProductListedDate = plisteddate;
        }
        //updating product constructor for SQL database
        public Product(int pid, string pname, decimal pprice, decimal ptax, int pquantity, DateTime plisteddate)
        {
            ProductId = pid;
            ProductName = pname;
            ProductPrice = pprice;
            ProductTax = ptax;
            ProductQuantity = pquantity;
            ProductListedDate = plisteddate;
        }
        //constructor all fields w/Id
        public Product(int prodId, string pname, decimal pprice, decimal ptax, DateTime plisteddate, 
                       int pquan, decimal ptot, DateTime ppdate)
        {
            ProductId = prodId;
            ProductName = pname;
            ProductPrice = pprice;
            ProductTax = ptax;
            ProductListedDate = plisteddate;
            ProductQuantity = pquan;
            ProductTotal = ptot;
            ProductPurchasedDate = ppdate;
        }
        //constructor all fields without Id
        public Product(string pname, decimal pprice, decimal ptax, DateTime plisteddate,
                       int pquan, decimal ptot, DateTime ppdate)
        {
            ProductName = pname;
            ProductPrice = pprice;
            ProductTax = ptax;
            ProductListedDate = plisteddate;
            ProductQuantity = pquan;
            ProductTotal = ptot;
            ProductPurchasedDate = ppdate;
        }

        //methods
        public override string ToString()
        {
            return string.Format("id:{0} - name:{1} - $:{2} - date:{3}", 
                                 ProductId, ProductName, ProductPrice, ProductListedDate);
            //return base.ToString();
        }
    }
}
