using System.Collections.Generic;
using CRM_Final.Business.Models;


namespace CRM_Final.Business.Data
{
    public interface IOrderUtility
    {
        List<Order> GetList();
        List<Order> GetByCustomerId(int customerId);
        Order GetById(int orderId);
        Order CreateNewOrder(Order newOrder);
        void UpdateOrder(Order orderToUpdate);
        List<Order> OrderSearch(string query);
    }
}