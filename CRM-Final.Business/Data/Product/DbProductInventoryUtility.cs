using System;
using System.Collections.Generic;
using CRM_Final.Business.Models;
using System.Data.SqlClient;

namespace CRM_Final.Business.Data
{
    public class DbProductInventoryUtility : IProductInventoryUtility
    {
        public List<Product> GetInventory()
        {
            List<Product> products = new List<Product>();

            SqlCommand cmd = DbManager.GetDbCommandObject();
            cmd.CommandText = @"
                SELECT DISTINCT ([SalesLT].[Product].[ProductID])
                      ,[SalesLT].[Product].[Name] AS [ProductName]
                      ,[SalesLT].[Product].[ProductNumber]
	                  ,[SalesLT].[ProductDescription].[Description] AS [ProductDescription]
                      ,[SalesLT].[Product].[StandardCost]
                      ,[SalesLT].[Product].[ListPrice]
                      ,[SalesLT].[Product].[Size]
                      ,[SalesLT].[Product].[Weight]
	                  ,[SalesLT].[ProductCategory].[Name] AS [Category]
                      ,[SalesLT].[Product].[SellStartDate]
                      ,[SalesLT].[Product].[ThumbNailPhoto]
                      ,[SalesLT].[Product].[ThumbnailPhotoFileName]
                      ,[SalesLT].[Product].[ModifiedDate]
                  FROM [SalesLT].[Product]
                  JOIN [SalesLT].[ProductCategory] ON [SalesLT].[ProductCategory].[ProductCategoryID] = [SalesLT].[Product].[ProductCategoryID]
                  JOIN [SalesLT].[ProductModel] ON [SalesLT].[ProductModel].[ProductModelID] = [SalesLT].[Product].[ProductModelID]
                  JOIN [SalesLT].[ProductModelProductDescription] ON [SalesLT].[ProductModelProductDescription].[ProductModelID] = [SalesLT].[ProductModel].[ProductModelID]
                  JOIN [SalesLT].[ProductDescription] ON [SalesLT].[ProductDescription].[ProductDescriptionID] = [SalesLT].[ProductModelProductDescription].[ProductDescriptionID]
                  WHERE [SalesLT].[ProductModelProductDescription].[Culture] = 'en'
            ";

            try
            {
                cmd.Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

                while (reader.Read())
                {
                    Product newP = BuildProduct(reader) ;
                    products.Add(newP);
                }
                reader.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return products;
        }

        public Product GetInventory(int productId)
        {
            Product productToReturn = null;
            SqlCommand cmd = DbManager.GetDbCommandObject();

            cmd.CommandText = @"
                SELECT DISTINCT ([SalesLT].[Product].[ProductID])
                    ,[SalesLT].[Product].[Name] AS [ProductName]
                    ,[SalesLT].[Product].[ProductNumber]
	                ,[SalesLT].[ProductDescription].[Description] AS [ProductDescription]
                    ,[SalesLT].[Product].[StandardCost]
                    ,[SalesLT].[Product].[ListPrice]
                    ,[SalesLT].[Product].[Size]
                    ,[SalesLT].[Product].[Weight]
	                ,[SalesLT].[ProductCategory].[Name] AS [Category]
                    ,[SalesLT].[Product].[SellStartDate]
                    ,[SalesLT].[Product].[ThumbNailPhoto]
                    ,[SalesLT].[Product].[ThumbnailPhotoFileName]
                    ,[SalesLT].[Product].[ModifiedDate]
                FROM [SalesLT].[Product]
                JOIN [SalesLT].[ProductCategory] ON [SalesLT].[ProductCategory].[ProductCategoryID] = [SalesLT].[Product].[ProductCategoryID]
                JOIN [SalesLT].[ProductModel] ON [SalesLT].[ProductModel].[ProductModelID] = [SalesLT].[Product].[ProductModelID]
                JOIN [SalesLT].[ProductModelProductDescription] ON [SalesLT].[ProductModelProductDescription].[ProductModelID] = [SalesLT].[ProductModel].[ProductModelID]
                JOIN [SalesLT].[ProductDescription] ON [SalesLT].[ProductDescription].[ProductDescriptionID] = [SalesLT].[ProductModelProductDescription].[ProductDescriptionID]
                WHERE [SalesLT].[ProductModelProductDescription].[Culture] = 'en'
                AND [SalesLT].[Product].[ProductID] = @productId
                 
            ";
            cmd.Parameters.AddWithValue("@productId", productId);

            try
            {
                cmd.Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

                if (reader.Read())
                {
                    productToReturn = BuildProduct(reader);
                }
                reader.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return productToReturn;
        }

        public List<Product> ProductInventorySearch(string query)
        {
            List<Product> products = new List<Product>();

            SqlCommand cmd = DbManager.GetDbCommandObject();
            cmd.CommandText = @"
                SELECT DISTINCT ([SalesLT].[Product].[ProductID])
                    ,[SalesLT].[Product].[Name] AS [ProductName]
                    ,[SalesLT].[Product].[ProductNumber]
	                ,[SalesLT].[ProductDescription].[Description] AS [ProductDescription]
                    ,[SalesLT].[Product].[StandardCost]
                    ,[SalesLT].[Product].[ListPrice]
                    ,[SalesLT].[Product].[Size]
                    ,[SalesLT].[Product].[Weight]
	                ,[SalesLT].[ProductCategory].[Name] AS [Category]
                    ,[SalesLT].[Product].[SellStartDate]
                    ,[SalesLT].[Product].[ThumbNailPhoto]
                    ,[SalesLT].[Product].[ThumbnailPhotoFileName]
                    ,[SalesLT].[Product].[ModifiedDate]
                FROM [SalesLT].[Product]
                JOIN [SalesLT].[ProductCategory] ON [SalesLT].[ProductCategory].[ProductCategoryID] = [SalesLT].[Product].[ProductCategoryID]
                JOIN [SalesLT].[ProductModel] ON [SalesLT].[ProductModel].[ProductModelID] = [SalesLT].[Product].[ProductModelID]
                JOIN [SalesLT].[ProductModelProductDescription] ON [SalesLT].[ProductModelProductDescription].[ProductModelID] = [SalesLT].[ProductModel].[ProductModelID]
                JOIN [SalesLT].[ProductDescription] ON [SalesLT].[ProductDescription].[ProductDescriptionID] = [SalesLT].[ProductModelProductDescription].[ProductDescriptionID]
                WHERE [SalesLT].[ProductModelProductDescription].[Culture] = 'en'
                AND (
                    [SalesLT].[Product].[Name] LIKE '%' + @query + '%' OR
                    [SalesLT].[Product].[ProductNumber] = @query OR
                    [SalesLT].[ProductDescription].[Description] LIKE '%' + @query + '%'
                )
            ";

            cmd.Parameters.AddWithValue("@query", query);

            try
            {
                cmd.Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

                while (reader.Read())
                {
                    Product newP = BuildProduct(reader);
                    products.Add(newP);
                }
                reader.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return products;

        }

        public Product AddProductToInventory(Product newProduct)
        {
            Product productToReturn = null;

            SqlCommand cmd = DbManager.GetDbCommandObject();

            cmd.CommandText = @"
                INSERT INTO [SalesLT].[Product]
                (Name, ProductNumber, Size, StandardCost, ListPrice, Weight, SellStartDate, ThumbNailPhoto, ThumbnailPhotoFileName)
                VALUES
                (@Name, @ProductNumber, @Size, @StandardCost, @ListPrice, @Weight, @SellStartDate, @Thumbnail, @ThumbnailFileName)

                SELECT @@Identity from [SalesLT].[Product]";

            cmd.Parameters.AddWithValue("@Name", newProduct.Name);
            cmd.Parameters.AddWithValue("@ProductNumber", newProduct.ProductNumber);
            cmd.Parameters.AddWithValue("@Size", newProduct.Size);
            cmd.Parameters.AddWithValue("@StandardCost", newProduct.StandardCost);
            cmd.Parameters.AddWithValue("@ListPrice", newProduct.ListPrice);
            cmd.Parameters.AddWithValue("@Weight", newProduct.Weight);
            cmd.Parameters.AddWithValue("@SellStartDate", newProduct.SellStartDate);
            cmd.Parameters.AddWithValue("@Thumbnail", newProduct.ThumbNailPhoto);
            cmd.Parameters.AddWithValue("@ThumbnailFileName", newProduct.ThumbnailFileName);

            object objNewProductId;
            try
            {
                cmd.Connection.Open();
                objNewProductId = cmd.ExecuteScalar();
                cmd.Connection.Close();
            }
            catch (Exception)
            {
                throw;
            }
            productToReturn = GetInventory(Convert.ToInt32(objNewProductId));
            return productToReturn;
        }

        public void UpdateProduct(Product productToUpdate)
        {
            SqlCommand cmd = DbManager.GetDbCommandObject();

            cmd.CommandText = @"
                UPDATE [SalesLT].[Product]
                SET Name = @Name,
                    ProductNumber = @ProductNumber,
                    Size = @Size,
                    StandardCost = @StandardCost,
                    ListPrice = @ListPrice,
                    Weight = @Weight, 
                    SellStartDate = @SellStartDate
                WHERE ProductID = @ProductId
            ";
            DateTime lastModified = DateTime.Now;
            cmd.Parameters.AddWithValue("@ProductId", productToUpdate.ProductId);
            cmd.Parameters.AddWithValue("@Name", productToUpdate.Name);
            cmd.Parameters.AddWithValue("@ProductNumber", productToUpdate.ProductNumber);
            cmd.Parameters.AddWithValue("@Size", productToUpdate.Size);
            cmd.Parameters.AddWithValue("@StandardCost", productToUpdate.StandardCost);
            cmd.Parameters.AddWithValue("@ListPrice", productToUpdate.ListPrice);
            cmd.Parameters.AddWithValue("@Weight", productToUpdate.Weight);
            cmd.Parameters.AddWithValue("@SellStartDate", productToUpdate.SellStartDate);
            cmd.Parameters.AddWithValue("@ModifiedDate", lastModified);

            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception)
            {
                throw;
            }
            productToUpdate.ModifiedDate = lastModified;
        }

        public void DeleteProduct(Product productToDelete)
        {
            SqlCommand cmd = DbManager.GetDbCommandObject();

            cmd.CommandText = @"
                DELETE [SalesLT].[Product]
                WHERE ProductID = @ProductId
            ";
            DateTime lastModified = DateTime.Now;
            cmd.Parameters.AddWithValue("@ProductId", productToDelete.ProductId);
            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UpdateProductPicture(int productId, byte[] bufferData)
        {
            SqlCommand cmd = DbManager.GetDbCommandObject();

            cmd.CommandText = @"
                UPDATE [SalesLT].[Product]
                SET ThumbNailPhoto = @ThumbnailPhoto
                WHERE ProductID = @ProductId
            ";

            
            cmd.Parameters.AddWithValue("@ThumbNailPhoto", bufferData);
            cmd.Parameters.AddWithValue("@ProductId", productId);

            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static Product BuildProduct(SqlDataReader reader)
        {
            return new Product
            {
                ProductId = (int)reader["ProductID"],
                Name = (reader["ProductName"] == DBNull.Value) ? "" : (string)reader["ProductName"],
                ProductNumber = (reader["ProductNumber"] == DBNull.Value) ? "" : (string)reader["ProductNumber"],
                Size = (reader["Size"] == DBNull.Value) ? "" : (string)reader["Size"],
                StandardCost = (reader["StandardCost"] == DBNull.Value) ? 0 : (decimal)reader["StandardCost"],
                ListPrice = (reader["ListPrice"] == DBNull.Value) ? 0 : (decimal)reader["ListPrice"],
                Weight = (reader["Weight"] == DBNull.Value) ? 0 : (decimal)reader["Weight"],
                SellStartDate = (DateTime)reader["SellStartDate"],
                ThumbNailPhoto = (byte[])reader["ThumbNailPhoto"],
                ThumbnailFileName = (reader["ThumbNailPhotoFileName"] == DBNull.Value) ? "" : (string)reader["ThumbNailPhotoFileName"],
                Description = (reader["ProductDescription"] == DBNull.Value) ? "" : (string)reader["ProductDescription"],
                Category = (reader["Category"] == DBNull.Value) ? "" : (string)reader["Category"],
                ModifiedDate = (DateTime)reader["ModifiedDate"]
            };
        } 
    }
} 
