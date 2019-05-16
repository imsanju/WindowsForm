using CRM_Final.Business.Models;
using System.Collections.Generic;

namespace CRM_Final.Business.Data
{
    public interface IOrderItemUtility
    {
        List<OrderItem> GetList();
        List<OrderItem> GetByOrderId(int orderId);
        OrderItem GetById(int orderItemId);
        OrderItem CreateNewOrderItem(OrderItem newOrderItem);
        void UpdateOrderItem(OrderItem orderItemToUpdate);
        void DeleteOrderItem(OrderItem orderItemToDelete);
        void DeleteOrderItemsForOrder(int orderId);
    }
}
