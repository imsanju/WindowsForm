using CRM_Final.Business.Models;
using System;
using System.Collections.Generic;

namespace CRM_Final.Business.Data
{
    public class ProductInventoryUtility : IProductInventoryUtility
    {
        public Product AddProductToInventory(Product newProduct)
        {
            throw new NotImplementedException();
        }

        public void DeleteProduct(Product productToDelete)
        {
            throw new NotImplementedException();
        }

        public void UpdateProductPicture(int productId, byte[] bufferData)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetInventory ()
        {
            List<Product> inventory = new List<Product>();  //Declared list

            //inventory.Add(new Product() { Name = "Baseball", Price = 2.75f, QuantityOnHand = 10, Department = "Sporting Goods" });
            //inventory.Add(new Product() { Name = "Soccer Ball", Price = 10.75f, QuantityOnHand = 5, Department = "Sporting Goods" });
            //inventory.Add(new Product() { Name = "Foot Ball", Price = 23.75f, QuantityOnHand = 30, Department = "Sporting Goods" });
            //inventory.Add(new Product() { Name = "Golf Ball", Price = 2.00f, QuantityOnHand = 100, Department = "Sporting Goods" });
            //inventory.Add(new Product() { Name = "Basketball", Price = 12.00f, QuantityOnHand = 50, Department = "Sporting Goods" });

            //inventory.Add(new Product() { Name = "Apple", Price = 1f, QuantityOnHand = 50, Department = "Produce" });
            //inventory.Add(new Product() { Name = "Oranges", Price = 1.5f, QuantityOnHand = 50, Department = "Produce" });
            //inventory.Add(new Product() { Name = "Grapes", Price = .5f, QuantityOnHand = 50, Department = "Produce" });

            return inventory;
        }

        public Product GetInventory(int productId)
        {
            throw new NotImplementedException();
        }

        public List<Product> ProductInventorySearch(string query)
        {
            throw new NotImplementedException();
        }

        public void UpdateProduct(Product productToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
