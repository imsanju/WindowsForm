using System;
using System.Collections.Generic;
using CRM_Final.Business.Models;
using System.Data.SqlClient;

namespace CRM_Final.Business.Data
{
    public class DbAddressUtility : IAddressUtility
    {
        public Address CreateNewAddress(Address newAddress)
        {
            Address addressToReturn = null;

            SqlCommand cmd = DbManager.GetDbCommandObject();

            cmd.CommandText = @"
                DECLARE @@AddressID AS INT;

                INSERT INTO [SalesLT].[Address]
                (AddressLine1, AddressLine2, City, StateProvince, CountryRegion, PostalCode)
                VALUES
                (@AddressLine1, @AddressLine2, @City, @State, @CountryRegion, @PostalCode)
                
                SELECT @@AddressID = @@Identity from [SalesLT].[Address];
                
                INSERT INTO [SalesLT].[CustomerAddress]
                (CustomerID, AddressID, AddressType)
                VALUES
                (@CustomerID, @@AddressID, @AddressType);
                
                SELECT @@AddressID;
            ";

            cmd.Parameters.AddWithValue("@AddressLine1", newAddress.Line1);
            cmd.Parameters.AddWithValue("@AddressLine2", newAddress.Line2);
            cmd.Parameters.AddWithValue("@City", newAddress.City);
            cmd.Parameters.AddWithValue("@State", newAddress.State);
            cmd.Parameters.AddWithValue("@CountryRegion", newAddress.CountryRegion);
            cmd.Parameters.AddWithValue("@PostalCode", newAddress.PostalCode);
            cmd.Parameters.AddWithValue("@AddressType", newAddress.Type);
            cmd.Parameters.AddWithValue("@CustomerID", newAddress.CustomerID);

            object objNewAddressId;
            try
            {
                cmd.Connection.Open();
                objNewAddressId = cmd.ExecuteScalar();
                cmd.Connection.Close();
            }
            catch (Exception)
            {
                throw;
            }
            addressToReturn = GetById(Convert.ToInt32(objNewAddressId));
            return addressToReturn;
        }

        public Address GetById(int addressId)
        {
            Address addressToReturn = null;
            SqlCommand cmd = DbManager.GetDbCommandObject();

            cmd.CommandText = @"
                SELECT 
                     [SalesLT].[Address].[AddressID]
                    ,[AddressLine1]
                    ,[AddressLine2]
                    ,[City]
                    ,[StateProvince]
                    ,[CountryRegion]
                    ,[PostalCode]
                    ,[SalesLT].[Address].[ModifiedDate]
                    ,[SalesLT].[CustomerAddress].[AddressType]
                    ,[SalesLT].[CustomerAddress].[CustomerID]
                FROM [SalesLT].[Address]
                JOIN [SalesLT].[CustomerAddress] ON [SalesLT].[Address].[AddressID] = [SalesLT].[CustomerAddress].[AddressID]
                WHERE [SalesLT].[Address].[AddressID] = @AddressID                
            ";
            cmd.Parameters.AddWithValue("@AddressID", addressId);

            try
            {
                cmd.Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

                if (reader.Read())
                {
                    addressToReturn = BuildAddress(reader);
                }
                reader.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return addressToReturn;
        }

        public List<Address> GetForCustomer(int customerId)
        {
            List<Address> addressesForCustomer = new List<Address>();

            SqlCommand cmd = DbManager.GetDbCommandObject();
            cmd.CommandText = @"
                SELECT 
                     [SalesLT].[Address].[AddressID]
                    ,[AddressLine1]
                    ,[AddressLine2]
                    ,[City]
                    ,[StateProvince]
                    ,[CountryRegion]
                    ,[PostalCode]
                    ,[SalesLT].[Address].[ModifiedDate]
                    ,[SalesLT].[CustomerAddress].[AddressType]
                    ,[SalesLT].[CustomerAddress].[CustomerID]
                FROM [SalesLT].[Address]
                JOIN [SalesLT].[CustomerAddress] ON [SalesLT].[Address].[AddressID] = [SalesLT].[CustomerAddress].[AddressID]
                WHERE [SalesLT].[CustomerAddress].[CustomerID] = @customerId
            ";

            cmd.Parameters.AddWithValue("@customerId", customerId);

            try
            {
                cmd.Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

                while (reader.Read())
                {
                    Address newA = BuildAddress(reader);
                    addressesForCustomer.Add(newA);
                }
                reader.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return addressesForCustomer;
        }

        public void UpdateAddress(Address addressToUpdate)
        {
            SqlCommand cmd = DbManager.GetDbCommandObject();

            cmd.CommandText = @"
                UPDATE [SalesLT].[CustomerAddress]
                SET AddressType = @AddressType
                Where AddressID = @AddressID;

                UPDATE [SalesLT].[Address]
                SET AddressLine1 = @AddressLine1,
                    AddressLine2 = @AddressLine2,
                    City = @City,
                    StateProvince = @State,
                    CountryRegion = @CountryRegion,
                    PostalCode = @PostalCode,
                    ModifiedDate = @ModifiedDate
                WHERE AddressID = @AddressID;
            ";
            DateTime lastModified = DateTime.Now;
            cmd.Parameters.AddWithValue("@AddressLine1", addressToUpdate.Line1);
            cmd.Parameters.AddWithValue("@AddressLine2", addressToUpdate.Line2);
            cmd.Parameters.AddWithValue("@City", addressToUpdate.City);
            cmd.Parameters.AddWithValue("@State", addressToUpdate.State);
            cmd.Parameters.AddWithValue("@CountryRegion", addressToUpdate.CountryRegion);
            cmd.Parameters.AddWithValue("@PostalCode", addressToUpdate.PostalCode);
            cmd.Parameters.AddWithValue("@AddressType", addressToUpdate.Type);
            cmd.Parameters.AddWithValue("@ModifiedDate", lastModified);
            cmd.Parameters.AddWithValue("@AddressID", addressToUpdate.AddressID);

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
            addressToUpdate.ModifiedDate = lastModified;
        }

        private Address BuildAddress(SqlDataReader reader)
        {
            return new Address
            {
                AddressID = (int)reader["AddressID"],
                Line1 = (reader["AddressLine1"] == DBNull.Value) ? "" : (string)reader["AddressLine1"],
                Line2 = (reader["AddressLine2"] == DBNull.Value) ? "" : (string)reader["AddressLine2"],
                City = (reader["City"] == DBNull.Value) ? "" : (string)reader["City"],
                State = (reader["StateProvince"] == DBNull.Value) ? "" : (string)reader["StateProvince"],
                CountryRegion = (reader["CountryRegion"] == DBNull.Value) ? "" : (string)reader["CountryRegion"],
                PostalCode = (reader["PostalCode"] == DBNull.Value) ? "" : (string)reader["PostalCode"],
                Type = (reader["AddressType"] == DBNull.Value) ? "" : (string)reader["AddressType"],
                CustomerID = (reader["CustomerID"] == DBNull.Value) ? -1 : (int)reader["CustomerID"],
                ModifiedDate = (DateTime)reader["ModifiedDate"]
            };
        }
    }
}
