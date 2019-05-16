using System.Collections.Generic;
using CRM_Final.Business.Models;

namespace CRM_Final.Business.Data
{
    public interface IProductInventoryUtility
    {
        List<Product> GetInventory();
        Product GetInventory(int productId);
        List<Product> ProductInventorySearch(string query);
        Product AddProductToInventory(Product newProduct);
        void UpdateProduct(Product productToUpdate);
        void DeleteProduct(Product productToDelete);
        void UpdateProductPicture(int productId, byte[] bufferData);
    }
}