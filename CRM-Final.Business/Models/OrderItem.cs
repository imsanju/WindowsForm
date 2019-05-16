using System;

namespace CRM_Final.Business.Models
{
    public class OrderItem
    {
        public int SalesOrderID { get; set; }
        public int SalesOrderItemID { get; set; }
        public int ProductID { get; set; }
        public decimal UnitPrice { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public DateTime ModifiedDate { get; set; }
        public decimal Discount { get; set; }

        public decimal LineItemTotal
        {
            get
            {
                return (this.Product.ListPrice * (1.0m - this.Discount)) * this.Quantity;
            }
        }

        public override string ToString()
        {
            return string.Format("{0}\t{1}\t{2:c}", Product.Name, Quantity, LineItemTotal);
        }
    }
}
