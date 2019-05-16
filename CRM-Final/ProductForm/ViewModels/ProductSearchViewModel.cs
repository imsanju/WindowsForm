using CRM_Final.Business.Models;
using System;

namespace CRM_Final.ProductForm.ViewModels
{
    class ProductSearchViewModel
    {
        public ProductSearchViewModel(Product prod)
        {
            Name = prod.Name;
            ProductNumber = prod.ProductNumber;
            Description = prod.Description;
            ListPrice = prod.ListPrice;
            ModifiedDate = prod.ModifiedDate;
            ProductId = prod.ProductId;
            SellStartDate = prod.SellStartDate;
            Size = prod.Size;
            StandardCost = prod.StandardCost;
            Weight = prod.Weight;
            Category = prod.Category;

        }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string ProductNumber { get; set; }
        public string Description { get; set; }
        public decimal ListPrice { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string Size { get; set; }
        public decimal StandardCost { get; set; }
        public decimal Weight { get; set; }
        public DateTime SellStartDate { get; set; }
        public string Category { get; set; }

    }
}
