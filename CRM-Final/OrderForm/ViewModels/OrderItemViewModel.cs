using CRM_Final.Business.Models;

namespace CRM_Final.OrderForm.ViewModels
{
    public class OrderItemViewModel
    {
        public int ProductID { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public int Quantity { get; set; }

        public decimal LineItemTotal
        {
            get
            {
                return (this.UnitPrice * (1.0m - this.Discount)) * this.Quantity;
            }
        }

        public OrderItemViewModel (OrderItem orderItem)
        {
            this.ProductID = orderItem.ProductID;
            this.UnitPrice = orderItem.UnitPrice;
            this.Discount = orderItem.Discount;
            this.Quantity = orderItem.Quantity;
        }
    }
}
