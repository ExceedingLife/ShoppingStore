using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingStore.DataAccess
{
    public class Receipt
    {
        //create ReceiptID property
        private int _receiptID;
        public int ReceiptID
        {
            get => _receiptID;
            set => _receiptID = value;
        }
        //create UserID property
        private int _userId;
        public int UserId
        {
            get => _userId;
            set => _userId = value;
        }
        //create ReceiptDate property
        private DateTime _receiptDate;
        public DateTime ReceiptDate
        {
            get => _receiptDate;
            set => _receiptDate = value;
        }

        // All of my List<T> could be combined into 1 List<Product>.

        //List of all products bought
        private List<string> _productNames;
        public List<string> ProductNames
        {
            get => _productNames;
            set => _productNames = value;
        }
        //create List<$> of product subtotal / total w/o tax
        private List<decimal> _productsSubtotal;
        public List<decimal> ProductsSubtotal
        {
            get => _productsSubtotal;
            set => _productsSubtotal = value;
        }
        //create list of ProductTotals property
        private List<decimal> _productsTotal;
        public List<decimal> ProductsTotal
        {
            get => _productsTotal;
            set => _productsTotal = value;
        }
        private List<int> _productQuantity;
        public List<int> ProductQuantity
        {
            get => _productQuantity;
            set => _productQuantity = value;
        }
        //create SalesTax property
        private List<decimal> _salesTax;
        public List<decimal> SalesTax
        {
            get => _salesTax;
            set => _salesTax = value;
        }
        //create ReceiptTotal property
        private decimal _receiptTotal;
        public decimal ReceiptTotal
        {
            get => _receiptTotal;
            set => _receiptTotal = value;
        }

        //Default receipt constructor
        public Receipt()
        {
            ProductNames = new List<string>();
            ProductQuantity = new List<int>();
            SalesTax = new List<decimal>();
            ProductsSubtotal = new List<decimal>();
            ProductsTotal = new List<decimal>();
        }

        //Calculating total into receipt
        public Receipt(List<string> pNames, List<int> pQuan, List<decimal> tax,
               List<decimal> pSub, List<decimal> pPrices, decimal rtot)
        {
            ProductNames = pNames;
            ProductQuantity = pQuan;
            SalesTax = tax;
            ProductsSubtotal = pSub;
            ProductsTotal = pPrices;
            ReceiptTotal = rtot;
        }
        //inserting receipt in database
        public Receipt(int uid, DateTime rdate, List<string> pNames, List<int> pQuan, List<decimal> tax, 
                       List<decimal> pSub, List<decimal> pPrices, decimal rtot)
        {
            UserId = uid;
            ReceiptDate = rdate;
            ProductNames = pNames;
            ProductQuantity = pQuan;            
            SalesTax = tax;
            ProductsSubtotal = pSub;
            ProductsTotal = pPrices;
            ReceiptTotal = rtot;
        }
        //updating receipt in database
        public Receipt(int rid, int uid, DateTime rdate, List<string> pNames, List<int> pQuan, List<decimal> tax, 
                       List<decimal> pSub, List<decimal> pPrices, decimal rtot)
        {
            ReceiptID = rid;
            UserId = uid;
            ReceiptDate = rdate;
            ProductNames = pNames;
            ProductQuantity = pQuan;
            SalesTax = tax;
            ProductsSubtotal = pSub;
            ProductsTotal = pPrices;
            ReceiptTotal = rtot;
        }

        //Override the .ToString()
        public override string ToString()
        {
            return String.Format(" ");
            //return base.ToString();
        }


        //Override the .GetHashCode()
        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
            //return base.GetHashCode();
        }
    }
}
