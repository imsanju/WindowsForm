using CRM_Final.Business.Models;
using System;
using System.Collections.Generic;

namespace CRM_Final.OrderForm.ViewModels
{
    public class OrderSearchViewModel
    {
        public OrderSearchViewModel (Order order)
        {
            SalesOrderID = order.SalesOrderID;
            OrderNumber = order.OrderNumber;
            OrderDate = order.OrderDate;
            DueDate = order.DueDate;
            ShipDate = order.ShipDate;
            Status = order.Status;
            IsOnlineOrder = order.IsOnlineOrder;
            CustomerId = order.CustomerId;
            Subtotal = order.CustomerId;
            Tax = order.Tax;
            Freight = order.Freight;
            Total = order.Total;
            ShippingAddressId = order.ShippingAddressId;
            BillingAddressId = order.BillingAddressId;
            OrderItems = order.OrderItems;
            ShipMethod = order.ShipMethod;
            ModifiedDate = order.ModifiedDate;
        }
        public int SalesOrderID { get; set; }
        public string OrderNumber { get; set; }
        public string ShipMethod { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime ShipDate { get; set; }
        public int Status { get; set; }
        public bool IsOnlineOrder { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Tax { get; set; }
        public decimal Freight { get; set; }
        public decimal Total { get; set; }
        public int ShippingAddressId { get; set; }
        public int BillingAddressId { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
