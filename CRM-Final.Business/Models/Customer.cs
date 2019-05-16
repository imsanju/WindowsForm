using System;
using System.Collections.Generic;

namespace CRM_Final.Business.Models
{
    public class Customer
    {
        public int CustomerID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string CompanyName { get; set; }
        public string SalesPerson { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public Guid RowGuid { get; set; }
        public List<Order> ExistingOrders { get; set; }
        public List<Address> Addresses { get; set; }

        public void AddAddress(Address _address)
        {
            this.Addresses.Add(_address);
        }

        public void AddOrder(Order _order)
        {
            this.ExistingOrders.Add(_order);
        }

        public Customer ()
        {
            this.ExistingOrders = new List<Order>();
            this.Addresses = new List<Address>();
        }

    }
}
