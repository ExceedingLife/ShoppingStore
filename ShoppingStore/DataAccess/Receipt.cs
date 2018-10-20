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
        private int receiptID;
        public int ReceiptID
        {
            get
            {
                return receiptID;
            }
            set
            {
                receiptID = value;
            }
        }
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
        //create ReceiptDate property
        private DateTime receiptDate;
        public DateTime ReceiptDate
        {
            get
            {
                return receiptDate;
            }
            set
            {
                receiptDate = value;
            }
        }
        //create ProductTotal property
        private decimal productTotal;
        public decimal ProductTotal
        {
            get
            {
                return productTotal;
            }
            set
            {
                productTotal = value;
            }
        }
        //create SalesTax property
        private decimal salesTax;
        public decimal SalesTax
        {
            get
            {
                return salesTax;
            }
            set
            {
                salesTax = value;
            }
        }
        //create ReceiptTotal property
        private decimal receiptTotal;
        public decimal ReceiptTotal
        {
            get
            {
                return receiptTotal;
            }
            set
            {
                receiptTotal = value;
            }
        }

        //Default receipt constructor
        //public Receipt() { }

        //receipt property constructor
        public Receipt()
        {
            //I AM NOT SURE WHAT TO ADD FOR THE CONSTRUCTOR PROPERTIES YET
            //ReceiptID
            //UserID
            //ReceiptDate
            //ProductTotal
            //SalesTax
            //ReceiptTotal
        }

        //Override the .ToString()
        public override string ToString()
        {
            return String.Format(" ");
            //return base.ToString();
        }

        //Override the .Equals()
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            Receipt user = (Receipt)obj;

            return false;
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
