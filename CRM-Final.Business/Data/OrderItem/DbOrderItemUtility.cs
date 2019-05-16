using System;
using System.Collections.Generic;
using CRM_Final.Business.Models;
using System.Data.SqlClient;

namespace CRM_Final.Business.Data
{
    public class DbOrderItemUtility : IOrderItemUtility
    {
        public OrderItem CreateNewOrderItem(OrderItem newOrderItem)
        {
            OrderItem orderItemToReturn = null;

            SqlCommand cmd = DbManager.GetDbCommandObject();

            cmd.CommandText = @"
                INSERT INTO [SalesLT].[SalesOrderDetail]
                (SalesOrderID, OrderQty, ProductID, UnitPrice, UnitPriceDiscount)
                VALUES
                (@SalesOrderID, @OrderQty, @ProductID, @UnitPrice, @UnitPriceDiscount)

                SELECT @@Identity from [SalesLT].[SalesOrderDetail];
            ";

            cmd.Parameters.AddWithValue("@SalesOrderID", newOrderItem.SalesOrderID);
            cmd.Parameters.AddWithValue("@OrderQty", newOrderItem.Quantity);
            cmd.Parameters.AddWithValue("@ProductID", newOrderItem.Product.ProductId);
            cmd.Parameters.AddWithValue("@UnitPrice", newOrderItem.Product.ListPrice);
            cmd.Parameters.AddWithValue("@UnitPriceDiscount", newOrderItem.Discount);

            object objNewOrderItemId;
            try
            {
                cmd.Connection.Open();
                objNewOrderItemId = cmd.ExecuteScalar();
                cmd.Connection.Close();
            }
            catch (Exception)
            {
                throw;
            }
            orderItemToReturn = GetById(Convert.ToInt32(objNewOrderItemId));
            return orderItemToReturn;
        }

        public void DeleteOrderItem(OrderItem orderItemToDelete)
        {
            SqlCommand cmd = DbManager.GetDbCommandObject();
            cmd.CommandText = @"
                DELETE
                FROM [SalesLT].[SalesOrderDetail]
                WHERE
                    [SalesLT].[SalesOrderDetail].[SalesOrderDetailID] = @salesOrderItemId
            ";

            cmd.Parameters.AddWithValue("@salesOrderItemId", orderItemToDelete.SalesOrderItemID);

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

        public void DeleteOrderItemsForOrder(int orderId)
        {
            SqlCommand cmd = DbManager.GetDbCommandObject();
            cmd.CommandText = @"
                DELETE
                FROM [SalesLT].[SalesOrderDetail]
                WHERE
                    [SalesLT].[SalesOrderDetail].[SalesOrderID] = @salesOrderId
            ";

            cmd.Parameters.AddWithValue("@salesOrderId", orderId);

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

        public OrderItem GetById(int orderItemId)
        {
            OrderItem orderItemToReturn = null;

            SqlCommand cmd = DbManager.GetDbCommandObject();
            cmd.CommandText = @"
                SELECT *
                FROM [SalesLT].[SalesOrderDetail]
                WHERE
                    [SalesLT].[SalesOrderDetail].[SalesOrderDetailID] = @orderItemID
            ";

            cmd.Parameters.AddWithValue("@orderItemID", orderItemId);

            try
            {
                cmd.Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

                if (reader.Read())
                {
                    orderItemToReturn = BuildOrderItem(reader);
                }
                reader.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return orderItemToReturn;
        }

        public List<OrderItem> GetByOrderId(int orderId)
        {
            List<OrderItem> orderItems = new List<OrderItem>();

            SqlCommand cmd = DbManager.GetDbCommandObject();
            cmd.CommandText = @"
                SELECT *
                FROM [SalesLT].[SalesOrderDetail]
                WHERE
                    [SalesLT].[SalesOrderDetail].[SalesOrderID] = @salesOrderId
            ";

            cmd.Parameters.AddWithValue("@salesOrderId", orderId);

            try
            {
                cmd.Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

                while (reader.Read())
                {
                    OrderItem newOI = BuildOrderItem(reader);
                    orderItems.Add(newOI);
                }
                reader.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return orderItems;
        }

        public List<OrderItem> GetList()
        {
            List<OrderItem> orderItems = new List<OrderItem>();

            SqlCommand cmd = DbManager.GetDbCommandObject();
            cmd.CommandText = @"
                SELECT *
                FROM [SalesLT].[SalesOrderDetail]
            ";

            try
            {
                cmd.Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

                while (reader.Read())
                {
                    OrderItem newOI = BuildOrderItem(reader);
                    orderItems.Add(newOI);
                }
                reader.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return orderItems;
        }

        public void UpdateOrderItem(OrderItem orderItemToUpdate)
        {
            SqlCommand cmd = DbManager.GetDbCommandObject();

            cmd.CommandText = @"
                UPDATE [SalesLT].[SalesOrderDetail]
                SET OrderQty = @Quantity,
                    ProductId = @ProductId,
                    UnitPriceDiscount = @Discount,
                    ModifiedDate = @ModifiedDate,
                WHERE SaledOrderID = @SaledOrderID
                AND SalesOrderItemID = @SalesOrderItemID
            ";
            DateTime lastModified = DateTime.Now;
            cmd.Parameters.AddWithValue("@Quantity", orderItemToUpdate.Quantity);
            cmd.Parameters.AddWithValue("@ProductId", orderItemToUpdate.ProductID);
            cmd.Parameters.AddWithValue("@Discount", orderItemToUpdate.Discount);
            cmd.Parameters.AddWithValue("@ModifiedDate", lastModified);
            cmd.Parameters.AddWithValue("@SaledOrderID", orderItemToUpdate.SalesOrderID);
            cmd.Parameters.AddWithValue("@SalesOrderItemID", orderItemToUpdate.SalesOrderItemID);

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
            orderItemToUpdate.ModifiedDate = lastModified;
        }

        private static OrderItem BuildOrderItem(SqlDataReader reader)
        {
            return new OrderItem
            {
                SalesOrderID = (int)reader["SalesOrderID"],
                SalesOrderItemID = (int)reader["SalesOrderDetailID"],
                ProductID = (int)reader["ProductID"],
                UnitPrice = (decimal)reader["UnitPrice"],
                Discount = (decimal)reader["UnitPriceDiscount"],
                Quantity = Convert.ToInt16(reader["OrderQty"]),
                ModifiedDate = (DateTime)reader["ModifiedDate"]
            };
        }
    }
}
