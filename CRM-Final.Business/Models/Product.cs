using System;

namespace CRM_Final.Business.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string ProductNumber { get; set; }
        public string Size { get; set; }
        public decimal StandardCost { get; set; }
        public decimal ListPrice { get; set; }
        public decimal Weight { get; set; }
        public DateTime SellStartDate { get; set; }
        public byte[] ThumbNailPhoto { get; set; }
        public string ThumbnailFileName { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public DateTime ModifiedDate { get; set; }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
