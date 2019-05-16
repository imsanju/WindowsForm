using System;
using System.Collections.Generic;
using CRM_Final.Business.Models;
using System.Data.SqlClient;

namespace CRM_Final.Business.Data
{
    public class DbOrderUtility : IOrderUtility
    {
        public Order CreateNewOrder(Order newOrder)
        {
            Order orderToReturn = null;

            SqlCommand cmd = DbManager.GetDbCommandObject();

            cmd.CommandText = @"
                INSERT INTO [SalesLT].[SalesOrderHeader]
                (CustomerID, OrderDate, DueDate, ShipDate, Status, OnlineOrderFlag, ShipMethod)
                VALUES
                (@CustomerID, @OrderDate, @DueDate, @ShipDate, @Status, @OnlineOrderFlag, @ShipMethod)

                SELECT @@Identity from [SalesLT].[SalesOrderHeader];
            ";

            cmd.Parameters.AddWithValue("@CustomerID", newOrder.CustomerId);
            cmd.Parameters.AddWithValue("@OrderDate", newOrder.OrderDate);
            cmd.Parameters.AddWithValue("@DueDate", newOrder.DueDate);
            cmd.Parameters.AddWithValue("@ShipDate", newOrder.ShipDate);
            cmd.Parameters.AddWithValue("@Status", newOrder.Status);
            cmd.Parameters.AddWithValue("@OnlineOrderFlag", newOrder.IsOnlineOrder);
            cmd.Parameters.AddWithValue("@ShipMethod", newOrder.ShipMethod);

            object objNewOrderId;
            try
            {
                cmd.Connection.Open();
                objNewOrderId = cmd.ExecuteScalar();
                cmd.Connection.Close();
            }
            catch (Exception)
            {
                throw;
            }
            orderToReturn = GetById(Convert.ToInt32(objNewOrderId));
            return orderToReturn;
        }

        public List<Order> GetByCustomerId(int customerId)
        {
            List<Order> orders = new List<Order>();

            SqlCommand cmd = DbManager.GetDbCommandObject();
            cmd.CommandText = @"
                SELECT *
                FROM [SalesLT].[SalesOrderHeader]
                WHERE
                    [SalesLT].[SalesOrderHeader].[CustomerID] = @customerId
            ";

            cmd.Parameters.AddWithValue("@customerId", customerId);

            try
            {
                cmd.Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

                while (reader.Read())
                {
                    Order newO = BuildOrder(reader);
                    orders.Add(newO);
                }
                reader.Close();
            }
            catch (Exception)
            {
               throw;
            }
            return orders;
        }

        public Order GetById(int orderId)
        {
            Order orderToReturn = new Order();

            SqlCommand cmd = DbManager.GetDbCommandObject();
            cmd.CommandText = @"
                SELECT *
                FROM [SalesLT].[SalesOrderHeader]
                WHERE
                    [SalesLT].[SalesOrderHeader].[SalesOrderID] = @SalesOrderID
            ";

            cmd.Parameters.AddWithValue("@SalesOrderID", orderId);

            try
            {
                cmd.Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

                if (reader.Read())
                {
                    orderToReturn = BuildOrder(reader);
                }
                reader.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return orderToReturn;
        }

        public List<Order> GetList()
        {
            List<Order> orders = new List<Order>();

            SqlCommand cmd = DbManager.GetDbCommandObject();
            cmd.CommandText = @"
                SELECT *
                FROM [SalesLT].[SalesOrderHeader]
            ";

            try
            {
                cmd.Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

                while (reader.Read())
                {
                    Order newO = BuildOrder(reader);
                    orders.Add(newO);
                }
                reader.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return orders;
        }

        public void UpdateOrder(Order orderToUpdate)
        {
            SqlCommand cmd = DbManager.GetDbCommandObject();

            cmd.CommandText = @"
                UPDATE [SalesLT].[SalesOrderHeader]
                SET OrderDate = @OrderDate,
                    DueDate = @DueDate,
                    ShipDate = @ShipDate,
                    Status = @Status,
                    OnlineOrderFlag = @OnlineOrderFlag,
                    ShipMethod = @ShipMethod,
                    ModifiedDate = @ModifiedDate,
                WHERE SalesOrderID = @SalesOrderID
            ";
            DateTime lastModified = DateTime.Now;
            cmd.Parameters.AddWithValue("@SalesOrderID", orderToUpdate.SalesOrderID);
            cmd.Parameters.AddWithValue("@OrderDate", orderToUpdate.OrderDate);
            cmd.Parameters.AddWithValue("@DueDate", orderToUpdate.DueDate);
            cmd.Parameters.AddWithValue("@ShipDate", orderToUpdate.ShipDate);
            cmd.Parameters.AddWithValue("@Status", orderToUpdate.Status);
            cmd.Parameters.AddWithValue("@OnlineOrderFlag", orderToUpdate.IsOnlineOrder);
            cmd.Parameters.AddWithValue("@ShipMethod", orderToUpdate.ShipMethod);
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
            orderToUpdate.ModifiedDate = lastModified;
        }

        public List<Order> OrderSearch(string query)
        {
            List<Order> orders = new List<Order>();

            SqlCommand cmd = DbManager.GetDbCommandObject();
            cmd.CommandText = @"
                 SELECT *
                FROM [SalesLT].[SalesOrderHeader]
                WHERE
                    [SalesLT].[SalesOrderHeader].[Status] LIKE '%' + @query + '%' OR
                    [SalesLT].[SalesOrderHeader].[SalesOrderNumber] LIKE '%' + @query + '%' OR
                    [SalesLT].[SalesOrderHeader].[Comment] LIKE '%' + @query + '%' 
            ";

            cmd.Parameters.AddWithValue("@query", query);

            try
            {
                cmd.Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

                while (reader.Read())
                {
                    Order newOrder = BuildOrder(reader);
                    orders.Add(newOrder);
                }
                reader.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return orders;
        }

        private static Order BuildOrder(SqlDataReader reader)
        {
            return new Order
            {
                SalesOrderID = (int)reader["SalesOrderID"],
                OrderNumber = (reader["SalesOrderNumber"] == DBNull.Value) ? "" : (string)reader["SalesOrderNumber"],
                OrderDate = (DateTime)reader["OrderDate"],
                DueDate = (DateTime)reader["DueDate"],
                Status = (byte)reader["Status"],
                IsOnlineOrder = (bool)reader["OnlineOrderFlag"],
                Tax = (decimal)reader["TaxAmt"],
                Freight = (decimal)reader["Freight"],
                Total = (decimal)reader["TotalDue"],
                ModifiedDate = (DateTime)reader["ModifiedDate"],
                CustomerId = (int)reader["CustomerID"],
                ShippingAddressId = (reader["ShipToAddressID"] == DBNull.Value) ? 0 : (int)reader["ShipToAddressID"],
                BillingAddressId = (reader["BillToAddressID"] == DBNull.Value) ? 0 : (int)reader["BillToAddressID"],
                ShipMethod = (string)reader["ShipMethod"]
            };
        }

    }
}
