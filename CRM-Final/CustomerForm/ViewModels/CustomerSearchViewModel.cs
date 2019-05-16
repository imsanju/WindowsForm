using CRM_Final.Business.Models;

namespace CRM_Final.CustomerForm.ViewModels
{
    class CustomerSearchViewModel
    {
        public CustomerSearchViewModel(Customer customer)
        {
            CustomerID = customer.CustomerID;
            FirstName = customer.FirstName;
            MiddleName = customer.MiddleName;
            LastName = customer.LastName;
            Suffix = customer.Suffix;
            Email = customer.Email;
            Phone = customer.Phone;
            CompanyName = customer.CompanyName;
            SalesPerson = customer.SalesPerson;
        }
        public int CustomerID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string CompanyName { get; set; }
        public string SalesPerson { get; set; }
    }
}
