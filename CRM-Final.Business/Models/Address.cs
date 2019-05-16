using System;

namespace CRM_Final.Business.Models
{
    public class Address
    {
        public int AddressID { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string CountryRegion { get; set; }
        public string PostalCode { get; set; }
        public string Type { get; set; }
        public int CustomerID { get; set; }
        public DateTime ModifiedDate { get; set; }

        public override string ToString()
        {
            return this.Type;
        }
    }
}
