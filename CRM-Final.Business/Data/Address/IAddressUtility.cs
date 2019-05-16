using System.Collections.Generic;
using CRM_Final.Business.Models;

namespace CRM_Final.Business.Data
{
    public interface IAddressUtility
    {
        List<Address> GetForCustomer(int customerId);
        void UpdateAddress(Address addressToUpdate);
        Address CreateNewAddress(Address newAddress);
        Address GetById(int addressId);
    }
}
