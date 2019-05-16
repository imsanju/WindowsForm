using System;
using System.Collections.Generic;

namespace CRM_Final.Business.Models
{
    public class Order
    {
        public int SalesOrderID { get; set; }
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime ShipDate { get; set; }
        public int Status { get; set; }
        public string ShipMethod { get; set; }
        public bool IsOnlineOrder { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public decimal Tax { get; set; }
        public decimal Freight { get; set; }
        public decimal Total { get; set; }
        public int ShippingAddressId { get; set; }
        public Address ShippingAddress { get; set; }
        public int BillingAddressId { get; set; }
        public Address BillingAddress { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public DateTime ModifiedDate { get; set; }
        public decimal Subtotal
        {
            get
            {
                decimal _subtotal = 0;

                for (int counter = 0; counter < OrderItems.Count; counter++)
                {
                    _subtotal += OrderItems[counter].LineItemTotal;
                }
                return _subtotal;
            }
        }

        public Order()
        {
            OrderItems = new List<OrderItem>();
        }
    }
}
