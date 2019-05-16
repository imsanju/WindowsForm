using System.Collections.Generic;
using CRM_Final.Business.Models;


namespace CRM_Final.Business.Data
{
    public interface ICustomerUtility
    {
        List<Customer> GetList();
        Customer GetById(int customerId);
        List<Customer> CustomerSearch(string query);
        Customer CreateNewCustomer(Customer newCustomer);
        void UpdateCustomer(Customer customerToUpdate);
        void DeleteCustomer(Customer customerToDelete);
    }
}